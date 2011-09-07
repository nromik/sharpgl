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
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Collections;

namespace Example4
{
    public partial class FormExample4 : Form
    {
        public FormExample4()
        {
            InitializeComponent();

            //  Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            //  Create the Stock Drawing object, this lets us draw common things 
            //  like axis, grids and cameras.
            gl.StockDrawing.Create(gl);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.databasen.se/hem/johan/raytracing/rayobj.htm");
        }

        private void openGLControl1_OpenGLDraw(object sender, PaintEventArgs e)
        {
            //  Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;

            //  Clear and load the identity.
            gl.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            //  View from a bit away the y axis and a few units above the ground.
            gl.LookAt(0, -5, 3, 0, 0, 0, 0, 1, 0);

            //  Rotate the objects every cycle.
            gl.Rotate(rotate, 0.0f, 0.0f, 1.0f);

            //  Move the objects down a bit so that they fit in the screen better.
            gl.Translate(0, 0, -1);

            //  Draw some axies.
            gl.StockDrawing.Axies.Call(gl);

            //  Draw every polygon in the collection.
            foreach (Polygon polygon in polygons)
                polygon.Draw(gl);

            //  Rotate a bit more each cycle.
            rotate += 1.0f;
        }


        float rotate = 0;

        //  A set of polygons to draw.
        PolygonCollection polygons = new PolygonCollection();

        //  The camera.
        SharpGL.SceneGraph.Cameras.CameraPerspective camera = new SharpGL.SceneGraph.Cameras.CameraPerspective();

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dopecode.co.uk/buymeabeer");
        }

        private void buttonLoad_Click_1(object sender, EventArgs e)
        {
            //  Create a Persistence Engine.
            SharpGL.Persistence.PersistenceEngine engine = new SharpGL.Persistence.PersistenceEngine();

            //  Create a a set of polygons.
            PolygonCollection loaded = (PolygonCollection)engine.UserLoad(typeof(PolygonCollection));

            //  If we successfully loaded, set the collection.
            if (loaded != null)
            {
                polygons = loaded;
                foreach(Polygon polygon in loaded)
                    polygon.Attributes.PolygonDrawMode = SharpGL.SceneGraph.Attributes.Polygon.PolygonMode.Lines;
            }
        }
    }
}