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

namespace Example5
{
    public partial class FormExample5 : Form
    {
        public FormExample5()
        {
            InitializeComponent();

            //  Create a sphere.
            SharpGL.SceneGraph.Quadrics.Sphere sphere = new SharpGL.SceneGraph.Quadrics.Sphere();
            sceneControl1.Scene.Jam(sphere);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.databasen.se/hem/johan/raytracing/rayobj.htm");
        }

        private void checkBoxSceneDesignMode_CheckedChanged(object sender, EventArgs e)
        {
            //  Set the design mode of the Scene.
            sceneControl1.SceneDesignMode = checkBoxSceneDesignMode.Checked;
        }
    }
}