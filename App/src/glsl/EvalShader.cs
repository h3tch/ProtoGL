﻿using OpenTK.Graphics.OpenGL4;

namespace App.Glsl
{
    class EvalShader : Shader
    {
        #region Field

        public static readonly EvalShader Default = new EvalShader(0);

        #endregion

#pragma warning disable 0649
#pragma warning disable 0169

        #region Input

        protected int gl_PatchVerticesIn;
        protected int gl_PrimitiveID;
        protected vec3 gl_TessCoord = new vec3();
        protected float[] gl_TessLevelOuter;
        protected float[] gl_TessLevelInner;
        protected __InOut[] gl_in;

        #endregion

        #region Output

        [__out] protected vec4 gl_Position;
        [__out] protected float gl_PointSize;
        [__out] protected float[] gl_ClipDistance;

        #endregion

#pragma warning restore 0649
#pragma warning restore 0169

        #region Constructors

        public EvalShader() : this(0) { }

        public EvalShader(int startLine) : base(startLine) { }

        #endregion

        internal void Debug()
        {
            GetTesselationOutput(Settings.ts_PrimitiveID, Settings.ts_InvocationID, Settings.ts_TessCoord);
            if (this != Default)
                BeginTracing();
            main();
            EndTracing();
        }

        internal void Execute(int primitiveID, int invocationID, float[] tessCoord)
        {
            GetTesselationOutput(primitiveID, invocationID, tessCoord);
            main();
        }

        public override void main()
        {
            gl_ClipDistance = new float[gl_in[0].gl_ClipDistance.Length];
            if (gl_PatchVerticesIn == 4)
            {
                gl_Position = (
                    mix(gl_in[0].gl_Position, gl_in[1].gl_Position, gl_TessCoord.x) +
                    mix(gl_in[1].gl_Position, gl_in[2].gl_Position, gl_TessCoord.y) +
                    mix(gl_in[2].gl_Position, gl_in[3].gl_Position, gl_TessCoord.x) +
                    mix(gl_in[3].gl_Position, gl_in[0].gl_Position, gl_TessCoord.y)) *0.25f;
                gl_PointSize = (
                    mix(gl_in[0].gl_PointSize, gl_in[1].gl_PointSize, gl_TessCoord.x) +
                    mix(gl_in[1].gl_PointSize, gl_in[2].gl_PointSize, gl_TessCoord.y) +
                    mix(gl_in[2].gl_PointSize, gl_in[3].gl_PointSize, gl_TessCoord.x) +
                    mix(gl_in[3].gl_PointSize, gl_in[0].gl_PointSize, gl_TessCoord.y)) * 0.25f;
                for (int i = 0; i < gl_ClipDistance.Length; i++)
                {
                    gl_ClipDistance[i] = (
                        mix(gl_in[0].gl_ClipDistance[i], gl_in[1].gl_ClipDistance[i], gl_TessCoord.x) +
                        mix(gl_in[1].gl_ClipDistance[i], gl_in[2].gl_ClipDistance[i], gl_TessCoord.y) +
                        mix(gl_in[2].gl_ClipDistance[i], gl_in[3].gl_ClipDistance[i], gl_TessCoord.x) +
                        mix(gl_in[3].gl_ClipDistance[i], gl_in[0].gl_ClipDistance[i], gl_TessCoord.y)) * 0.25f;
                }
            }
            else
            {
                gl_Position =
                    gl_in[0].gl_Position * gl_TessCoord.x +
                    gl_in[1].gl_Position * gl_TessCoord.y +
                    gl_in[2].gl_Position * gl_TessCoord.z;
                gl_PointSize =
                    gl_in[0].gl_PointSize * gl_TessCoord.x +
                    gl_in[1].gl_PointSize * gl_TessCoord.y +
                    gl_in[2].gl_PointSize * gl_TessCoord.z;
                for (int i = 0; i < gl_ClipDistance.Length; i++)
                {
                    gl_ClipDistance[i] =
                        gl_in[0].gl_ClipDistance[i] * gl_TessCoord.x +
                        gl_in[1].gl_ClipDistance[i] * gl_TessCoord.y +
                        gl_in[2].gl_ClipDistance[i] * gl_TessCoord.x;
                }
            }
        }

        private float mix(float a, float b, float t) => a * (1 - t) + b * t;

        private vec4 mix(vec4 a, vec4 b, float t) => a * (1 - t) + b * t;

        private void GetTesselationOutput(int primitiveID, int invocationID, float[] tessCoord)
        {
            gl_PatchVerticesIn = GL.GetInteger(GetPName.PatchVertices);
            gl_PrimitiveID = primitiveID;
            gl_TessCoord.x = tessCoord[0];
            gl_TessCoord.y = tessCoord[1];
            gl_TessCoord.z = tessCoord[2];
            var tess = (TessShader)Prev;
            tess.Execute(gl_PrimitiveID, invocationID);
            gl_TessLevelOuter = tess.GetOutputVarying<float[]>("gl_TessLevelOuter");
            gl_TessLevelInner = tess.GetOutputVarying<float[]>("gl_TessLevelInner");
            gl_in = tess.GetOutputVarying<__InOut[]>("gl_out");
        }

        public static object GetUniform<T>(string uniformName)
            => GetUniform<T>(uniformName, ProgramPipelineParameter.TessEvaluationShader);
    }
}
