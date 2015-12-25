﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace App
{
    class GLDebugger
    {
        #region FIELDS
        // debug resources definitions
        private static string dbgBufKey;
        private static string dbgTexKey;
        private static string dbgBufDef;
        private static string dbgTexDef;
        // allocate GPU resources
        private static GLBuffer buf;
        private static GLTexture tex;
        // allocate arrays for texture and image units
        private static int[] texUnits;
        private static int[] imgUnits;
        private static Dictionary<int, Uniforms> passes;
        public static DebugSettings settings;
        // watch count for indexing
        private static int watchCount;
        #endregion

        public static void Instantiate()
        {
            // debug resources definitions
            dbgBufKey = "__protogl__dbgbuf";
            dbgTexKey = "__protogl__dbgtex";
            dbgBufDef = "usage DynamicRead\n" +
                        $"size {1024*6*16}\n";
            dbgTexDef = "format RGBA32f\n";
            // allocate arrays for texture and image units
            texUnits = new int[GL.GetInteger((GetPName)All.MaxTextureImageUnits)];
            imgUnits = new int[GL.GetInteger((GetPName)All.MaxImageUnits)];
            passes = new Dictionary<int, Uniforms>();
            settings = new DebugSettings();
            // reset watch count for indexing in debug mode
            watchCount = 0;
        }

        public static void Initilize(Dict<GLObject> scene)
        {
            // allocate GPU resources
            buf = new GLBuffer(new GLParams(dbgBufKey, "dbg", dbgBufDef));
            tex = new GLTexture(new GLParams(dbgTexKey, "dbg", dbgTexDef), null, buf, null);
            // reset scene
            scene.Add(dbgBufKey, buf);
            scene.Add(dbgTexKey, tex);
            passes.Clear();
        }

        public static void Bind(GLPass pass)
        {
            // get debug uniforms for the pass
            Uniforms unif;
            if (!passes.TryGetValue(pass.glname, out unif))
                passes.Add(pass.glname, unif = new Uniforms(pass));
            // set shader debug uniforms
            unif.Bind(settings);
        }

        public static void Unbind(GLPass pass)
        {
            Uniforms unif;
            if (passes.TryGetValue(pass.glname, out unif))
                unif.Unbind();
        }

        #region TEXTURE AND IMAGE BINDING
        public static void BindImg(int unit, int level, bool layered, int layer, TextureAccess access,
            GpuFormat format, int name)
        {
            // bind image to image load/store unit
            imgUnits[unit] = name;
            GL.BindImageTexture(unit, name, level, layered, Math.Max(layer, 0), access,
                (SizedInternalFormat)format);
        }
        
        public static void BindTex(int unit, TextureTarget target, int name)
        {
            // bind texture to texture unit
            texUnits[unit] = name;
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(target, name);
        }

        public static void UnbindImg(int unit) => imgUnits[unit] = 0;

        public static void UnbindTex(int unit, TextureTarget target) => BindTex(unit, target, 0);

        public static int[] GetBoundTextures() => texUnits;

        public static int[] GetBoundImages() => imgUnits;
        #endregion

        #region DEBUG CODE GENERATION FOR GLSL SHADERS
        public static string AddDebugCode(string glsl, ShaderType type, bool debug)
        {
            // find main function and body
            var main = Regex.Match(glsl, @"void\s+main\s*\(\s*\)");
            var head = glsl.Substring(0, main.Index);
            var body = glsl.Substring(main.Index).MatchBrace('{', '}');

            // replace WATCH functions
            var watch = Regex.Matches(body.Value, @"<<<[\w\d_\.\[\]]*>>>");
            if (watch.Count == 0)
                return glsl;

            // replace WATCH functions
            var runBody = body.Value;
            var dbgBody = string.Copy(body.Value);
            foreach (Match match in watch)
            {
                var v = match.Value.Substring(3, match.Value.Length - 6);
                // remove watch function from runtime code
                runBody = runBody.Replace(match.Value, "");
                // replace watch function with actual store function in debug code
                if (debug)
                    dbgBody = dbgBody.Replace(match.Value,
                        $"_dbgIdx = _dbgStoreVar(_dbgIdx, {v}, {watchCount++});");
            }

            if (debug == false)
                return head + main.Value + runBody;

            // gather debug information
            int stage_index;
            string[] debug_uniform = new[]
            {
                "ivec2 _dbgVert", "ivec2 _dbgTess", "int _dbgEval",
                "ivec2 _dbgGeom", "ivec4 _dbgFrag", "uvec3 _dbgComp"
            };
            string[] debug_condition = new[]
            {
                "all(equal(_dbgVert, ivec2(gl_InstanceID, gl_VertexID)))",
                "all(equal(_dbgTess, ivec2(gl_InvocationID, gl_PrimitiveID)))",
                "_dbgEval == gl_PrimitiveID",
                "all(equal(_dbgGeom, ivec2(gl_PrimitiveIDIn, gl_InvocationID)))",
                "all(equal(_dbgFrag, ivec4(int(gl_FragCoord.x), int(gl_FragCoord.y), gl_Layer, gl_ViewportIndex)))",
                "all(equal(_dbgComp, gl_GlobalInvocationID))"
            };
            switch (type)
            {
                case ShaderType.VertexShader: stage_index = 0; break;
                case ShaderType.TessControlShader: stage_index = 1; break;
                case ShaderType.TessEvaluationShader: stage_index = 2; break;
                case ShaderType.GeometryShader: stage_index = 3; break;
                case ShaderType.FragmentShader: stage_index = 4; break;
                case ShaderType.ComputeShader: stage_index = 5; break;
                default: throw new InvalidEnumArgumentException();
            }

            // insert debug information
            var rsHead = Properties.Resources.dbg
                .Replace("<<<stage index>>>", stage_index.ToString());
            var rsBody = Properties.Resources.dbgBody
                .Replace("<<<debug uniform>>>", debug_uniform[stage_index])
                .Replace("<<<debug condition>>>", debug_condition[stage_index])
                .Replace("<<<stage index>>>", stage_index.ToString())
                .Replace("<<<debug code>>>", dbgBody)
                .Replace("<<<runtime code>>>", runBody);
            
            return head + '\n' + rsHead + '\n' + rsBody;
        }
        #endregion

        #region DEBUG SETTINGS
        private class Uniforms
        {
            public int dbgOut;
            public int dbgVert;
            public int dbgTess;
            public int dbgEval;
            public int dbgGeom;
            public int dbgFrag;
            public int dbgComp;
            private int unit;

            public Uniforms(GLPass pass)
            {
                dbgOut = GL.GetUniformLocation(pass.glname, "_dbgOut");
                dbgVert = GL.GetUniformLocation(pass.glname, "_dbgVert");
                dbgTess = GL.GetUniformLocation(pass.glname, "_dbgTess");
                dbgEval = GL.GetUniformLocation(pass.glname, "_dbgEval");
                dbgGeom = GL.GetUniformLocation(pass.glname, "_dbgGeom");
                dbgFrag = GL.GetUniformLocation(pass.glname, "_dbgFrag");
                dbgComp = GL.GetUniformLocation(pass.glname, "_dbgComp");
                unit = -1;
            }

            public void Bind(DebugSettings settings)
            {
                if (dbgOut <= 0)
                    return;

                unit = GetBoundImages().LastIndexOf(x => x == 0);
                if (unit < 0)
                    return;

                tex.BindImg(unit, 0, 0, TextureAccess.WriteOnly, GpuFormat.Rgba32f);
                GL.Uniform1(dbgOut, unit);

                if (dbgVert >= 0)
                    GL.Uniform2(dbgVert, settings.vs_InstanceID, settings.vs_VertexID);
                if (dbgTess >= 0)
                    GL.Uniform2(dbgTess, settings.ts_InvocationID, settings.ts_PrimitiveID);
                if (dbgEval >= 0)
                    GL.Uniform1(dbgEval, settings.ts_PrimitiveID);
                if (dbgGeom >= 0)
                    GL.Uniform2(dbgGeom, settings.gs_InvocationID, settings.gs_PrimitiveIDIn);
                if (dbgFrag >= 0)
                    GL.Uniform4(dbgFrag, settings.fs_FragCoord[0], settings.fs_FragCoord[1],
                        settings.fs_Layer, settings.fs_ViewportIndex);
                if (dbgComp >= 0)
                    GL.Uniform3(dbgComp,
                        (uint)settings.cs_GlobalInvocationID[0],
                        (uint)settings.cs_GlobalInvocationID[1],
                        (uint)settings.cs_GlobalInvocationID[2]);
            }

            public void Unbind()
            {
                if (unit >= 0)
                    tex.UnbindImg(unit);
            }
        }

        public class DebugSettings
        {
            [Category("Vertex Shader"), DisplayName("gl_InstanceID"),
             Description("the index of the current instance when doing some form of instanced " +
                "rendering. The instance count always starts at 0, even when using base instance " +
                "calls. When not using instanced rendering, this value will be 0.")]
            public int vs_InstanceID { get; set; } = 0;

            [Category("Vertex Shader"), DisplayName("gl_VertexID"),
             Description("the index of the vertex currently being processed. When using non-indexed " +
                "rendering, it is the effective index of the current vertex (the number of vertices " +
                "processed + the first​ value). For indexed rendering, it is the index used to fetch " +
                "this vertex from the buffer.")]
            public int vs_VertexID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("gl_InvocationID"),
             Description("the index of the shader invocation within this patch. An invocation " +
                "writes to per-vertex output variables by using this to index them.")]
            public int ts_InvocationID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("gl_PrimitiveID"),
             Description("the index of the current patch within this rendering command.")]
            public int ts_PrimitiveID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("gl_InvocationID"),
             Description("the current instance, as defined when instancing geometry shaders.")]
            public int gs_InvocationID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("gl_PrimitiveIDIn"),
             Description("the current input primitive's ID, based on the number of primitives " +
                "processed by the GS since the current drawing command started.")]
            public int gs_PrimitiveIDIn { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_FragCoord"),
             Description("The location of the fragment in window space. The X and Y components " +
                "are the window-space position of the fragment.")]
            public int[] fs_FragCoord { get; set; } = new int[2] { 0, 0 };

            //[Category("Fragment Shader"), DisplayName("gl_PrimitiveID"),
            // Description("This value is the index of the current primitive being rendered by this " +
            //    "drawing command. This includes any tessellation applied to the mesh, so each " +
            //    "individual primitive will have a unique index. However, if a Geometry Shader is " +
            //    "active, then the gl_PrimitiveID​ is exactly and only what the GS provided as output. " +
            //    "Normally, gl_PrimitiveID​ is guaranteed to be unique, so if two FS invocations have " +
            //    "the same primitive ID, they come from the same primitive. But if a GS is active and " +
            //    "outputs non - unique values, then different fragment shader invocations for different " +
            //    "primitives will get the same value.If the GS did not output a value for gl_PrimitiveID​, " +
            //    "then the fragment shader gets an undefined value.")]
            //public int fs_PrimitiveID { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_Layer"),
             Description("is either 0 or the layer number for this primitive output by the Geometry Shader.")]
            public int fs_Layer { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("gl_ViewportIndex"),
             Description("is either 0 or the viewport index for this primitive output by the Geometry Shader.")]
            public int fs_ViewportIndex { get; set; } = 0;

            [Category("Compute Shader"), DisplayName("gl_GlobalInvocationID"),
             Description("uniquely identifies this particular invocation of the compute shader " +
                "among all invocations of this compute dispatch call. It's a short-hand for the " +
                "math computation: gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID;")]
            public int[] cs_GlobalInvocationID { get; set; } = new int[3] { 0, 0, 0 };
        }
        #endregion
    }
}