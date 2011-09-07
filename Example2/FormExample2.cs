//  Copyright (c) 2007 - Dave Kerr
//  http://www.dopecode.co.uk
//
//  
//  This file is part of SharpGL.  
//
//  SharpGL is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 3 of the License, or
//  (at your option) any later version.
//
//  SharpGL is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see http://www.gnu.org/licenses/.
//
//  If you are using this code for commerical purposes then you MUST
//  purchase a license. See http://www.dopecode.co.uk for details.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;

using SharpGL;

namespace Example2
{
    public partial class FormExample2 : Form
    {
        public FormExample2()
        {
            InitializeComponent();

            //  Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            //  We need to load the texture from file.
            textureImage = new Bitmap("Crate.bmp");

            //  A bit of extra initialisation here, we have to enable textures.
            gl.Enable(OpenGL.TEXTURE_2D);
	
            //  Get one texture id, and stick it into the textures array.
            gl.GenTextures(1, textures);

		    //  Bind the texture.
		    gl.BindTexture(OpenGL.TEXTURE_2D, textures[0]);

		    //  Tell OpenGL where the texture data is.
            gl.TexImage2D(OpenGL.TEXTURE_2D, 0, 3, textureImage.Width, textureImage.Height, 0, OpenGL.RGB, OpenGL.UNSIGNED_BYTE, 
                textureImage.LockBits(new Rectangle(0, 0, textureImage.Width, textureImage.Height), 
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb).Scan0);

			gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MIN_FILTER, OpenGL.LINEAR);	// Linear Filtering
		    gl.TexParameter(OpenGL.TEXTURE_2D, OpenGL.TEXTURE_MAG_FILTER, OpenGL.LINEAR);	// Linear Filtering
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://nehe.gamedev.net");
        }

        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //  Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.LoadIdentity();
            gl.Translate(0.0f, 0.0f, -6.0f);

            gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);

            gl.BindTexture(OpenGL.TEXTURE_2D, textures[0]);

            gl.Begin(OpenGL.QUADS);

                // Front Face
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);	// Bottom Left Of The Texture and Quad
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);	// Bottom Right Of The Texture and Quad
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);	// Top Right Of The Texture and Quad
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);	// Top Left Of The Texture and Quad

                // Back Face
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);	// Bottom Right Of The Texture and Quad
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);	// Top Right Of The Texture and Quad
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);	// Top Left Of The Texture and Quad
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f);	// Bottom Left Of The Texture and Quad

                // Top Face
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);	// Top Left Of The Texture and Quad
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);	// Bottom Left Of The Texture and Quad
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, 1.0f, 1.0f);	// Bottom Right Of The Texture and Quad
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);	// Top Right Of The Texture and Quad

                // Bottom Face
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);	// Top Right Of The Texture and Quad
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, -1.0f, -1.0f);	// Top Left Of The Texture and Quad
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);	// Bottom Left Of The Texture and Quad
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);	// Bottom Right Of The Texture and Quad

                // Right face
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(1.0f, -1.0f, -1.0f);	// Bottom Right Of The Texture and Quad
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(1.0f, 1.0f, -1.0f);	// Top Right Of The Texture and Quad
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(1.0f, 1.0f, 1.0f);	// Top Left Of The Texture and Quad
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(1.0f, -1.0f, 1.0f);	// Bottom Left Of The Texture and Quad

                // Left Face
                gl.TexCoord(0.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, -1.0f);	// Bottom Left Of The Texture and Quad
                gl.TexCoord(1.0f, 0.0f); gl.Vertex(-1.0f, -1.0f, 1.0f);	// Bottom Right Of The Texture and Quad
                gl.TexCoord(1.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, 1.0f);	// Top Right Of The Texture and Quad
                gl.TexCoord(0.0f, 1.0f); gl.Vertex(-1.0f, 1.0f, -1.0f);	// Top Left Of The Texture and Quad
            gl.End();

            gl.Flush();

            rtri += 1.0f;// 0.2f;						// Increase The Rotation Variable For The Triangle 
        }


        float rtri = 0;

        //  The texture identifier.
        uint[] textures = new uint[1];
        
        //  Storage the texture itself.
        Bitmap textureImage;

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dopecode.co.uk/buymeabeer");
        }
    }
}