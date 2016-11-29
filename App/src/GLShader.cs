﻿using App.Glsl;
using OpenTK.Graphics.OpenGL4;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace App
{
    class GLShader : GLObject
    {
        #region Fields

        internal Shader DebugShader { get; private set; }
        internal ShaderType ShaderType;

        #endregion

        /// <summary>
        /// Create OpenGL object. Standard object constructor for ProtoFX.
        /// </summary>
        /// <param name="block"></param>
        /// <param name="scene"></param>
        /// <param name="debugging"></param>
        public GLShader(Compiler.Block block, Dict scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"shader '{name}'");
            
            // COMPILE AND LINK SHADER INTO A SHADER PROGRAM

            switch (anno)
            {
                case "vert": ShaderType = ShaderType.VertexShader; break;
                case "tess": ShaderType = ShaderType.TessControlShader; break;
                case "eval": ShaderType = ShaderType.TessEvaluationShader; break;
                case "geom": ShaderType = ShaderType.GeometryShader; break;
                case "frag": ShaderType = ShaderType.FragmentShader; break;
                case "comp": ShaderType = ShaderType.ComputeShader; break;
                default: throw err.Add($"Shader type '{anno}' is not supported.", block);
            }

            glname = GL.CreateShaderProgram(ShaderType, 1, new[] { block.Body });

            // check for errors

            int status;
            GL.GetProgram(glname, GetProgramParameterName.LinkStatus, out status);
            if (status != 1)
                err.Add($"\n{GL.GetProgramInfoLog(glname)}", block);
            if (HasErrorOrGlError(err, block))
                throw err;
            
            // CREATE CSHARP DEBUG CODE

            if (debugging)
            {
                var code = Converter.Shader2Class(ShaderType, name, block.Body);
                var rs = GLCsharp.CompileFilesOrSource(new[] { code }, null, block, err, new[] { name });
                if (rs.Errors.Count == 0)
                    DebugShader = (Shader)rs.CompiledAssembly.CreateInstance(
                        $"App.Glsl.{name}", false, BindingFlags.Default, null,
                        new object[] { block.LineInFile }, CultureInfo.CurrentCulture, null);
            }

            // check for errors
            if (err.HasErrors())
                throw err;
        }

        /// <summary>
        /// Standard object destructor for ProtoFX.
        /// </summary>
        public override void Delete()
        {
            base.Delete();
            if (glname > 0)
            {
                GL.DeleteProgram(glname);
                glname = 0;
            }
        }
    }
}
