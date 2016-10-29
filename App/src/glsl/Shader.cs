﻿using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace App.Glsl
{
    #region GLSL Qualifiers

    [AttributeUsage(AttributeTargets.All)]
    public class __in : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __out : Attribute { }

    [AttributeUsage(AttributeTargets.All)]
    public class __layout : Attribute
    {
#pragma warning disable 0649
#pragma warning disable 0169
        
        public int location;
        public int binding;
        public int max_vertices;

#pragma warning restore 0649
#pragma warning restore 0169

        public __layout() { }
        public __layout(params object[] param) { }
    }

    #endregion

    #region Necessary Extension

    static class ArrayExtention
    {
        public static int length(this Array data) => data.Length;
    }

    #endregion

    public class Shader : MathFunctions
    {
        #region Fields

        protected int LineInFile;
        internal Shader Prev;
        internal static readonly Type[] BaseIntTypes = new[] { typeof(bool), typeof(int), typeof(uint) };
        internal static readonly Type[] BaseFloatTypes = new[] { typeof(float), typeof(double) };
        internal static readonly Type[] BaseTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();
        internal static readonly Type[] VecIntTypes = new[] {
            typeof(bvec2), typeof(bvec3), typeof(bvec4),
            typeof(ivec2), typeof(ivec3), typeof(ivec4),
            typeof(uvec2), typeof(uvec3), typeof(uvec4),
        };
        internal static readonly Type[] VecFloatTypes = new[] {
            typeof(vec2), typeof(vec3), typeof(vec4),
            typeof(dvec2), typeof(dvec3), typeof(dvec4)
        };
        internal static readonly Type[] VecTypes = BaseIntTypes.Concat(BaseFloatTypes).ToArray();
        internal static readonly Type[] MatFloatTypes = new[] {
            typeof(mat2), typeof(mat3), typeof(mat4),
        };
        internal static readonly Type[] MatTypes = MatFloatTypes;
        internal static readonly Type[] IntTypes = BaseIntTypes.Concat(VecIntTypes).ToArray();
        internal static readonly Type[] FloatTypes = BaseFloatTypes.Concat(VecIntTypes).Concat(MatFloatTypes).ToArray();

        #endregion

        #region Constructors

        public Shader() : this(0) { }

        public Shader(int startLine)
        {
            LineInFile = startLine;
        }

        #endregion

        #region Texture Access

        public static vec4 texture(sampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1D sampler, float P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2D sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler3D sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCube sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler1DArray sampler, vec2 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArray sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(samplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArray sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler1DArrayShadow sampler, vec3 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(isampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(usampler2DArrayShadow sampler, vec4 P, float bias = 0) { return new vec4(0); }
        public static vec4 texture(sampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(isampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(usampler2DRect sampler, vec2 P) { return new vec4(0); }
        public static vec4 texture(sampler2DRectShadow sampler, vec3 P) { return new vec4(0); }
        public static vec4 texture(samplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(isamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }
        public static vec4 texture(usamplerCubeArrayShadow sampler, vec4 P, float compare) { return new vec4(0); }

        #endregion

        #region Debug Trace

        internal static int ShaderLineOffset = 0;
        internal static bool CollectDebugData = false;
        internal static List<TraceInfo> TraceLog = new List<TraceInfo>();
        public static IEnumerable<TraceInfo> DebugTrace => TraceLog;

        public static void ClearDebugTrace() => TraceLog.Clear();
        
        protected void BeginTracing()
        {
            CollectDebugData = true;
            ShaderLineOffset = LineInFile;
        }

        protected void EndTracing()
        {
            CollectDebugData = false;
            ShaderLineOffset = 0;
        }

        public static T TraceVar<T>(T value, string valueName)
        {
            if (!CollectDebugData)
                return value;
            
            var trace = new StackTrace(true);
            var idx = trace.GetFrames().IndexOf(x => x.GetMethod()?.Name == "TraceVar");

            TraceLog.Add(new TraceInfo
            {
                Line = trace.GetFrame(idx + 2).GetFileLineNumber() + ShaderLineOffset,
                Column = trace.GetFrame(idx + 2).GetFileColumnNumber(),
                Function = valueName,
                Output = value?.ToString(),
                Input = null,
            });

            return value;
        }

        public static T TraceFunc<T>(T output, params object[] input)
        {
            if (!CollectDebugData)
                return output;
            
            var trace = new StackTrace(true);
            var idx = trace.GetFrames().IndexOf(x => x.GetMethod()?.Name == "TraceFunc");

            TraceLog.Add(new TraceInfo
            {
                Line = trace.GetFrame(idx + 2).GetFileLineNumber() + ShaderLineOffset,
                Column = trace.GetFrame(idx + 2).GetFileColumnNumber(),
                Function = trace.GetFrame(idx + 1).GetMethod().Name,
                Output = output?.ToString(),
                Input = input?.Select(x => x.ToString()).ToArray(),
            });

            return output;
        }

        public struct TraceInfo
        {
            public int Line;
            public int Column;
            public string Function;
            public string Output;
            public string[] Input;
            public override string ToString()
            {
                if (Input == null)
                {
                    return "[L" + Line + ", C" + Column + "] " + Function + ": " + Output;
                }
                else
                {
                    string func;
                    switch (Function)
                    {
                        case "op_Addition":
                            func = " = " + Input[0] + " + " + Input[1];
                            break;
                        case "op_Substraction":
                            func = " = " + Input[0] + " + " + Input[1];
                            break;
                        case "op_Multiply":
                            func = " = " + Input[0] + " / " + Input[1];
                            break;
                        case "op_Division":
                            func = " = " + Input[0] + " / " + Input[1];
                            break;
                        default:
                            func = Function != null ? " = " + Function + "(" + Input.Cat(", ") + ")" : "";
                            break;
                    }
                    return "[L" + Line + ", C" + Column + "] " + Output + func;
                }
            }
        }

        #endregion

        #region Debug Settings

        internal static GLPass.MultiDrawCall drawcall;

        internal static DebugSettings Settings = new DebugSettings();

        public class DebugSettings
        {
            [Category("Vertex Shader"), DisplayName("InstanceID"),
             Description("the index of the current instance when doing some form of instanced " +
                "rendering. The instance count always starts at 0, even when using base instance " +
                "calls. When not using instanced rendering, this value will be 0.")]
            public int vs_InstanceID { get; set; } = 0;

            [Category("Vertex Shader"), DisplayName("VertexID"),
             Description("the index of the vertex currently being processed. When using non-indexed " +
                "rendering, it is the effective index of the current vertex (the number of vertices " +
                "processed + the first​ value). For indexed rendering, it is the index used to fetch " +
                "this vertex from the buffer.")]
            public int vs_VertexID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("InvocationID"),
             Description("the index of the shader invocation within this patch. An invocation " +
                "writes to per-vertex output variables by using this to index them.")]
            public int ts_InvocationID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("PrimitiveID"),
             Description("the index of the current patch within this rendering command.")]
            public int ts_PrimitiveID { get; set; } = 0;

            [Category("Tesselation"), DisplayName("TessCoord"),
             Description("the index of the current patch within this rendering command.")]
            public float[] ts_TessCoord { get; set; } = new float[3] { 0, 0, 0 };

            [Category("Geometry Shader"), DisplayName("InvocationID"),
             Description("the current instance, as defined when instancing geometry shaders.")]
            public int gs_InvocationID { get; set; } = 0;

            [Category("Geometry Shader"), DisplayName("PrimitiveIDIn"),
             Description("the current input primitive's ID, based on the number of primitives " +
                "processed by the GS since the current drawing command started.")]
            public int gs_PrimitiveIDIn { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("FragCoord"),
             Description("The location of the fragment in window space. The X and Y components " +
                "are the window-space position of the fragment.")]
            public int[] fs_FragCoord { get; set; } = new int[2] { 0, 0 };

            [Category("Fragment Shader"), DisplayName("Layer"),
             Description("is either 0 or the layer number for this primitive output by the Geometry Shader.")]
            public int fs_Layer { get; set; } = 0;

            [Category("Fragment Shader"), DisplayName("ViewportIndex"),
             Description("is either 0 or the viewport index for this primitive output by the Geometry Shader.")]
            public int fs_ViewportIndex { get; set; } = 0;

            [Category("Compute Shader"), DisplayName("GlobalInvocationID"),
             Description("uniquely identifies this particular invocation of the compute shader " +
                "among all invocations of this compute dispatch call. It's a short-hand for the " +
                "math computation: gl_WorkGroupID * gl_WorkGroupSize + gl_LocalInvocationID;")]
            public int[] cs_GlobalInvocationID { get; set; } = new int[3] { 0, 0, 0 };
        }

        #endregion

        #region Shader Data Access

        public virtual T GetInputVarying<T>(string varyingName)
        {
            return Prev != null ? Prev.GetOutputVarying<T>(varyingName) : default(T);
        }

        internal virtual T GetOutputVarying<T>(string varyingName)
        {
            var type = GetType();
            var props = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var prop in props)
            {
                if (prop.GetCustomAttribute(typeof(__out)) != null && prop.Name == varyingName)
                    return (T)prop.GetValue(this);
            }
            var fields = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute(typeof(__out)) != null && field.Name == varyingName)
                    return (T)field.GetValue(this);
            }
            return default(T);
        }

        protected static object GetUniform<T>(string uniformName, ProgramPipelineParameter shader)
        {
            int unit, glbuf, type, size, length, offset, stride;
            int[] locations = new int[1];

            // get current shader program pipeline
            var pipeline = GL.GetInteger(GetPName.ProgramPipelineBinding);
            if (pipeline <= 0)
                return default(T);

            // get vertex shader
            int program;
            GL.GetProgramPipeline(pipeline, shader, out program);
            if (program <= 0)
                return default(T);

            // get uniform buffer object block index
            int block = GL.GetUniformBlockIndex(program, uniformName.Substring(0, uniformName.IndexOf('.')));
            if (block < 0)
                return default(T);

            // get bound buffer object
            GL.GetActiveUniformBlock(program, block, ActiveUniformBlockParameter.UniformBlockBinding, out unit);
            GL.GetInteger(GetIndexedPName.UniformBufferBinding, unit, out glbuf);
            if (glbuf <= 0)
                return default(T);

            // get uniform indices in uniform block
            GL.GetUniformIndices(program, 1, new[] { uniformName }, locations);
            var location = locations[0];
            if (location < 0)
                return default(T);

            // get uniform information
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformType, out type);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformSize, out length);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformOffset, out offset);
            GL.GetActiveUniforms(program, 1, ref location, ActiveUniformParameter.UniformArrayStride, out stride);

            // get size of the uniform type
            switch ((All)type)
            {
                case All.IntVec2:
                case All.UnsignedIntVec2:
                case All.FloatVec2:
                case All.Double:
                    size = 8;
                    break;
                case All.IntVec3:
                case All.UnsignedIntVec3:
                case All.FloatVec3:
                    size = 12;
                    break;
                case All.IntVec4:
                case All.UnsignedIntVec4:
                case All.FloatVec4:
                case All.DoubleVec2:
                case All.FloatMat2:
                    size = 16;
                    break;
                case All.DoubleVec3:
                    size = 24;
                    break;
                case All.DoubleVec4:
                    size = 32;
                    break;
                case All.FloatMat3:
                    size = 36;
                    break;
                case All.FloatMat4:
                    size = 64;
                    break;
                default:
                    size = 4;
                    break;
            }

            // read uniform buffer data
            byte[] array = new byte[Math.Max(stride, size) * length];
            var src = GL.MapNamedBufferRange(glbuf, (IntPtr)offset, array.Length, BufferAccessMask.MapReadBit);
            Marshal.Copy(src, array, 0, array.Length);
            GL.UnmapNamedBuffer(glbuf);
            
            // if the return type is an array
            if (typeof(T).IsArray && BaseTypes.Any(x => x == typeof(T).GetElementType()))
                return array.To(typeof(T).GetElementType());

            // if the return type is a base type
            if (BaseTypes.Any(x => x == typeof(T)))
                return array.To(typeof(T)).GetValue(0);

            // create new object from byte array
            return typeof(T).GetConstructor(new[] { typeof(byte[]) })?.Invoke(new[] { array });
        }

        internal struct __InOut
        {
            public vec4 gl_Position;
            public float gl_PointSize;
            public float[] gl_ClipDistance;
        }

        #endregion
        
        public virtual void main() { }
    }
}
