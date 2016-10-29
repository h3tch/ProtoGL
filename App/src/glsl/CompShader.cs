﻿using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    class CompShader : Shader
    {
#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected uvec3 gl_NumWorkGroups;
        protected uvec3 gl_WorkGroupSize;
        protected uvec3 gl_WorkGroupID;
        protected uvec3 gl_LocalInvocationID;
        protected uvec3 gl_GlobalInvocationID;
        protected uint gl_LocalInvocationIndex;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public CompShader() : this(0) { }

        public CompShader(int startLine) : base(startLine) { }

        #endregion

        internal void Debug()
        {
            if (LineInFile >= 0)
                BeginTracing();
            EndTracing();
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.ComputeShader);
    }
}
