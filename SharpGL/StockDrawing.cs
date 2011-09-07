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

namespace SharpGL.SceneGraph
{
	/// <summary>
	/// The StockObjects class contains a set of useful stock objects for you
	/// to draw. It is implemented via OpenGL display lists for speed.
	/// </summary>
	public class StockDrawing
	{
		public StockDrawing()
		{
		}

		/// <summary>
		/// This function creates the internal display lists.
		/// </summary>
		/// <param name="gl">OpenGL object.</param>
		public virtual void Create(OpenGL gl)
		{
			//	Create the arrow.
			arrow.Generate(gl);
			arrow.New(gl, DisplayList.DisplayListMode.Compile);

			    //	Draw the 'line' of the arrow.
			    gl.Begin(OpenGL.LINES);
			    gl.Vertex(0, 0, 0);
			    gl.Vertex(0, 0, 1);
			    gl.End();

			    //	Draw the arrowhead.
			    gl.Begin(OpenGL.TRIANGLE_FAN);
			    gl.Vertex(0, 0, 1);
			    gl.Vertex(0.2f, 0.8f, 0.2f);
			    gl.Vertex(0.2f, 0.8f, -0.2f);
			    gl.Vertex(-0.2f, 0.8f, -0.2f);
			    gl.Vertex(-0.2f, 0.8f, 0.2f);
			    gl.End();

			//	End the arrow list.
			arrow.End(gl);

			//	Create the grid.
			grid.Generate(gl);
            grid.New(gl, DisplayList.DisplayListMode.Compile);

                gl.PushAttrib(OpenGL.LIGHTING_BIT);
                gl.Disable(OpenGL.LIGHTING);

                gl.Color(1, 1, 1);
    			
			    gl.Begin(OpenGL.LINES);
			    for(int i = -10; i <= 10; i++)
			    {
				    gl.Vertex(i, 0, -10);
				    gl.Vertex(i, 0, 10);
				    gl.Vertex(-10, 0, i);
				    gl.Vertex(10, 0, i);
			    }

                gl.End();

                gl.PopAttrib();

			grid.End(gl);

			//	Create the axies.
			axies.Generate(gl);
			axies.New(gl, DisplayList.DisplayListMode.Compile);

			    gl.PushAttrib(OpenGL.ALL_ATTRIB_BITS);
			    gl.Disable(OpenGL.LIGHTING);
			    gl.Disable(OpenGL.DEPTH_TEST);
			    gl.LineWidth(2.0f);

			    gl.Begin(OpenGL.LINES);
			        gl.Color(1, 0, 0, 1);
			        gl.Vertex(0, 0, 0);
			        gl.Vertex(3, 0, 0);
			        gl.Color(0, 1, 0, 1);
			        gl.Vertex(0, 0, 0);
			        gl.Vertex(0, 3, 0);
			        gl.Color(0, 0, 1, 1);
			        gl.Vertex(0, 0, 0);
			        gl.Vertex(0, 0, 3);
			    gl.End();

			    gl.PopAttrib();

			axies.End(gl);

			camera.Generate(gl);
			camera.New(gl, DisplayList.DisplayListMode.Compile);

			    Polygon poly = new Polygon();
			    poly.CreateCube();
			    poly.Scale.Set(.2f ,0.2f, 0.2f);
			    poly.Draw(gl);
		    //	poly.Dispose();

			camera.End(gl);
		}

        public virtual void DrawGrid(OpenGL gl)
        {
            gl.PushAttrib(OpenGL.LINE_BIT | OpenGL.ENABLE_BIT | OpenGL.COLOR_BUFFER_BIT);

            //  Turn off lighting, set up some nice anti-aliasing for lines.
            gl.Disable(OpenGL.LIGHTING);
            gl.Hint(OpenGL.LINE_SMOOTH_HINT, OpenGL.NICEST);
            gl.Enable(OpenGL.LINE_SMOOTH);
            gl.Enable(OpenGL.BLEND);
            gl.BlendFunc(OpenGL.SRC_ALPHA, OpenGL.ONE_MINUS_SRC_ALPHA);

            //	Create the grid.
            gl.LineWidth(1.5f);

            gl.Begin(OpenGL.LINES);

                for (int i = -10; i <= 10; i++)
                {
                    gl.Color(0.2f, 0.2f, 0.2f, 1f);
                    gl.Vertex(i, 0, -10);
                    gl.Vertex(i, 0, 10);
                    gl.Vertex(-10, 0, i);
                    gl.Vertex(10, 0, i);
                }

            gl.End();

            //  Turn off the depth test for the axies.
            gl.Disable(OpenGL.DEPTH_TEST);
            gl.LineWidth(2.0f);

            //	Create the axies.
            gl.Begin(OpenGL.LINES);

                gl.Color(1f, 0f, 0f, 1f);
                gl.Vertex(0f, 0f, 0f);
                gl.Vertex(3f, 0f, 0f);
                gl.Color(0f, 1f, 0f, 1f);
                gl.Vertex(0f, 0f, 0f);
                gl.Vertex(0f, 3f, 0f);
                gl.Color(0f, 0f, 1f, 1f);
                gl.Vertex(0f, 0f, 0f);
                gl.Vertex(0f, 0f, 3f);

            gl.End();

            gl.PopAttrib();

       //     gl.Flush();

        }

        public virtual void DrawCircle(OpenGL gl)
        {
            //	Start drawing a line.
            gl.Begin(OpenGL.LINE_LOOP);

            //	Make a circle of points.
            for (int i = 0; i < 12; i++)
            {
                double angle = 2 * Math.PI * i / 12;

                //	Draw the point.
                gl.Vertex(Math.Cos(angle), 0, Math.Sin(angle));
            }

            //	End the line drawing.
            gl.End();
        }

        public virtual void DrawCircle3D(OpenGL gl)
        {
            gl.PushAttrib(OpenGL.ENABLE_BIT);
            gl.Disable(OpenGL.LIGHTING);

            //	Draw three circles.
            gl.PushMatrix();
            DrawCircle(gl);
            gl.Rotate(90, 1, 0, 0);
            DrawCircle(gl);
            gl.Rotate(90, 0, 0, 1);
            DrawCircle(gl);
            gl.PopMatrix();

            gl.PopAttrib();
        }
		
		/// <summary>
		/// This is a circle, radius 1, that goes around the Z axis.
		/// </summary>
		protected DisplayList circle = new DisplayList();

		/// <summary>
		/// This is three circles, one ringing each axis.
		/// </summary>
		protected DisplayList circle3D = new DisplayList();

		/// <summary>
		/// This is an arrow, one unit long, from (0,0,0) to (0,1,0).
		/// </summary>
		protected DisplayList arrow = new DisplayList();

		/// <summary>
		/// This is a grid, 20 by 20 units.
		/// </summary>
		protected DisplayList grid = new DisplayList();

		/// <summary>
		/// This is a set of axis.
		/// </summary>
		protected DisplayList axies = new DisplayList();

		/// <summary>
		/// This is a camera object.
		/// </summary>
		protected DisplayList camera = new DisplayList();

		public DisplayList Circle
		{
			get {return circle;}
		}
		public DisplayList Circle3D
		{
			get {return circle3D;}
		}
		public DisplayList Arrow
		{
			get {return arrow;}
		}
		public DisplayList Grid
		{
			get {return grid;}
		}
		public DisplayList Axies
		{
			get {return axies;}
		}
		public DisplayList Camera
		{
			get {return camera;}
		}
	}
}
