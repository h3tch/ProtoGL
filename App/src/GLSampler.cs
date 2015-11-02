﻿using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLSampler : GLObject
    {
        #region FIELDS
        [GLField]
        private TextureMinFilter minfilter = TextureMinFilter.Nearest;
        [GLField]
        private TextureMagFilter magfilter = TextureMagFilter.Nearest;
        [GLField]
        private TextureWrapMode wrap = TextureWrapMode.ClampToEdge;
        #endregion

        public GLSampler(string dir, string name, string annotation, string text, Dict<GLObject> classes)
            : base(name, annotation)
        {
            var err = new GLException($"sampler '{name}'");

            // PARSE TEXT
            var body = new Commands(text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // CREATE OPENGL OBJECT
            glname = GL.GenSampler();
            int mini = (int)minfilter;
            int magi = (int)magfilter;
            int wrapi = (int)wrap;
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMinFilter, ref mini);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureMagFilter, ref magi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapR, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapS, ref wrapi);
            GL.SamplerParameterI(glname, SamplerParameterName.TextureWrapT, ref wrapi);

            HasErrorOrGlError(err);
            if (err.HasErrors())
                throw err;
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteSampler(glname);
                glname = 0;
            }
        }
    }
}
