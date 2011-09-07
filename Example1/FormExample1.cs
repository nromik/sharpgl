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
using System.Text;
using System.Windows.Forms;

using SharpGL;

namespace Example1
{
    public partial class FormExample1 : Form
    {
        public FormExample1()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://nehe.gamedev.net");
        }

        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //  Get the OpenGL object, just to clean up the code.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);	// Clear The Screen And The Depth Buffer
            gl.LoadIdentity();					// Reset The View
            gl.Translate(-1.5f, 0.0f, -6.0f);				// Move Left And Into The Screen

            gl.Rotate(rtri, 0.0f, 1.0f, 0.0f);				// Rotate The Pyramid On It's Y Axis

            gl.Begin(OpenGL.TRIANGLES);					// Start Drawing The Pyramid

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Front)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Left Of Triangle (Front)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Right Of Triangle (Front)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Right)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Left Of Triangle (Right)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Right Of Triangle (Right)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Back)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Left Of Triangle (Back)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Right Of Triangle (Back)

            gl.Color(1.0f, 0.0f, 0.0f);			// Red
            gl.Vertex(0.0f, 1.0f, 0.0f);			// Top Of Triangle (Left)
            gl.Color(0.0f, 0.0f, 1.0f);			// Blue
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Left Of Triangle (Left)
            gl.Color(0.0f, 1.0f, 0.0f);			// Green
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Right Of Triangle (Left)
            gl.End();						// Done Drawing The Pyramid

            gl.LoadIdentity();
            gl.Translate(1.5f, 0.0f, -7.0f);				// Move Right And Into The Screen

            gl.Rotate(rquad, 1.0f, 1.0f, 1.0f);			// Rotate The Cube On X, Y & Z

            gl.Begin(OpenGL.QUADS);					// Start Drawing The Cube

            gl.Color(0.0f, 1.0f, 0.0f);			// Set The Color To Green
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Top)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Top)
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Bottom Left Of The Quad (Top)
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Bottom Right Of The Quad (Top)


            gl.Color(1.0f, 0.5f, 0.0f);			// Set The Color To Orange
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Top Right Of The Quad (Bottom)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Top Left Of The Quad (Bottom)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Bottom)
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Bottom)

            gl.Color(1.0f, 0.0f, 0.0f);			// Set The Color To Red
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Front)
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Front)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Bottom Left Of The Quad (Front)
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Bottom Right Of The Quad (Front)

            gl.Color(1.0f, 1.0f, 0.0f);			// Set The Color To Yellow
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Back)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Back)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Back)
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Back)

            gl.Color(0.0f, 0.0f, 1.0f);			// Set The Color To Blue
            gl.Vertex(-1.0f, 1.0f, 1.0f);			// Top Right Of The Quad (Left)
            gl.Vertex(-1.0f, 1.0f, -1.0f);			// Top Left Of The Quad (Left)
            gl.Vertex(-1.0f, -1.0f, -1.0f);			// Bottom Left Of The Quad (Left)
            gl.Vertex(-1.0f, -1.0f, 1.0f);			// Bottom Right Of The Quad (Left)

            gl.Color(1.0f, 0.0f, 1.0f);			// Set The Color To Violet
            gl.Vertex(1.0f, 1.0f, -1.0f);			// Top Right Of The Quad (Right)
            gl.Vertex(1.0f, 1.0f, 1.0f);			// Top Left Of The Quad (Right)
            gl.Vertex(1.0f, -1.0f, 1.0f);			// Bottom Left Of The Quad (Right)
            gl.Vertex(1.0f, -1.0f, -1.0f);			// Bottom Right Of The Quad (Right)
            gl.End();						// Done Drawing The Q

            gl.Flush();

            rtri += 3.0f;// 0.2f;						// Increase The Rotation Variable For The Triangle 
            rquad -= 3.0f;// 0.15f;						// Decrease The Rotation Variable For The Quad 
        }


        float rtri = 0;
        float rquad = 0;

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dopecode.co.uk/buymeabeer");
        }
    }
}