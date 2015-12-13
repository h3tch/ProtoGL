﻿using OpenTK.Graphics.OpenGL4;
using System;

namespace App
{
    class GLTexture : GLObject
    {
        #region FIELDS
        [Field] private string samp = null;
        [Field] private string buff = null;
        [Field] private string img = null;
        [Field] private SizedInternalFormat format = 0;
        private GLSampler glsamp = null;
        private GLBuffer glbuff = null;
        private GLImage glimg = null;
        #endregion

        /// <summary>
        /// Create OpenGL object.
        /// </summary>
        /// <param name="params">Input parameters for GLObject creation.</param>
        public GLTexture(GLParams @params)
            : base(@params)
        {
            var err = new CompileException($"texture '{@params.name}'");

            // PARSE TEXT
            var body = new Commands(@params.text, err);

            // PARSE ARGUMENTS
            body.Cmds2Fields(this, err);

            // GET REFERENCES
            if (samp != null)
                @params.scene.TryGetValue(samp, out glsamp, err);
            if (buff != null)
                @params.scene.TryGetValue(buff, out glbuff, err);
            if (img != null)
                @params.scene.TryGetValue(img, out glimg, err);
            if (glbuff != null && glimg != null)
                err.Add("Only an image or a buffer can be bound to a texture object.");
            if (glbuff == null && glimg == null)
                err.Add("Ether an image or a buffer has to be bound to a texture object.");

            // IF THERE ARE ERRORS THROW AND EXCEPTION
            if (err.HasErrors())
                throw err;

            // INCASE THIS IS A TEXTURE OBJECT
            if (glimg != null)
            {
                glname = glimg.glname;
            }
            else if (glbuff != null)
            {
                if (format == 0)
                    throw err.Add($"No texture buffer format defined " +
                        "for buffer '{buff}' (e.g. format RGBA8).");
                // CREATE OPENGL OBJECT
                glname = GL.GenTexture();
                GL.BindTexture(TextureTarget.TextureBuffer, glname);
                GL.TexBuffer(TextureBufferTarget.TextureBuffer, format, glbuff.glname);
                GL.BindTexture(TextureTarget.TextureBuffer, 0);
                HasErrorOrGlError(err);
            }

            if (err.HasErrors())
                throw err;
        }

        /// <summary>
        /// Bind texture to texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void Bind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, glsamp.glname);
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(glimg != null ? glimg.target : TextureTarget.TextureBuffer, glname);
        }

        /// <summary>
        /// Bind texture to compute image unit.
        /// </summary>
        /// <param name="unit">Image unit.</param>
        /// <param name="level">Texture mipmap level.</param>
        /// <param name="layer">Texture array index or texture depth.</param>
        /// <param name="access">How the texture will be accessed by the shader.</param>
        /// <param name="format">Pixel format of texture pixels.</param>
        public void Bind(int unit, int level, int layer, TextureAccess access, SizedInternalFormat format)
        {
            GL.BindImageTexture(unit, glname, level, layer >= 0, Math.Max(layer, 0), access, format);
        }

        /// <summary>
        /// Unbind texture from texture unit.
        /// </summary>
        /// <param name="unit">Texture unit.</param>
        public void Unbind(int unit)
        {
            if (samp != null)
                GL.BindSampler(unit, 0);
            GL.ActiveTexture(TextureUnit.Texture0 + unit);
            GL.BindTexture(glimg != null ? glimg.target : TextureTarget.TextureBuffer, 0);
        }

        public override void Delete()
        {
            if (glname > 0)
            {
                GL.DeleteTexture(glname);
                glname = 0;
            }
        }
        
    }

}
