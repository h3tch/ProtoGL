﻿using OpenTK.Graphics.OpenGL4;

namespace App
{
    class GLSampler : GLObject
    {
        #region FIELDS
        [Field] private TextureMinFilter minfilter = TextureMinFilter.Nearest;
        [Field] private TextureMagFilter magfilter = TextureMagFilter.Nearest;
        [Field] private TextureWrapMode wrap = TextureWrapMode.ClampToEdge;
        #endregion
        
        public GLSampler(Compiler.Block block, Dict<GLObject> scene, bool debugging)
            : base(block.Name, block.Anno)
        {
            var err = new CompileException($"sampler '{name}'");

            // PARSE ARGUMENTS
            Cmds2Fields(this, block, err);

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

            HasErrorOrGlError(err, block);
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
