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
using System.Drawing;

using SharpGL;
using SharpGL.SceneGraph;

namespace SceneBuilder
{
	public class BuildablePolygon : Polygon, IInteractable
	{
		public BuildablePolygon()
		{
		}
		public BuildablePolygon(Polygon poly)
		{
			 Attributes = poly.Attributes;
			 castsShadow = poly.CastsShadow;
			 currentContext = poly.CurrentContext;
			 drawNormals = poly.DrawNormals;
			 faces = poly.Faces;
			 material = poly.Material;
			 name = poly.Name;
			 normals = poly.Normals;
			 rotate = poly.Rotate;
			 scale = poly.Scale;
			 shadowSize = poly.ShadowSize;
			 transformationOrder = poly.TransformOrder;
			 translate = poly.Translate;
			 uvs = poly.UVs;
			 vertices = poly.Vertices;
		}

		public override void Draw(OpenGL gl)
		{
			//	Do the ordinary polygon drawing.
			base.Draw(gl);

			//	Now we do our own.
			DoPreDraw(gl);

			//	Get the viewport.
			int[] viewport = new int[4];
			gl.GetInteger(OpenGL.VIEWPORT, viewport);

			int index = 0;
			
			//	Here we're going to go through every vertex..
			foreach(Vertex vertex in Vertices)
			{
				//	Convert the vertex into a coord.
				Vertex screen = gl.Project(vertex);

				//	Get the OpenGL coord as a GDI coord.
				float x = screen.X;
				float y = viewport[3] - screen.Y;
				
				if(gl.GDIGraphics != null)
				{
					//	Label the vertex.
					gl.GDIGraphics.DrawString(index.ToString(),	new System.Drawing.Font(FontFamily.GenericSansSerif, 10, FontStyle.Bold),
						Brushes.Red, new PointF(x, y));
				}

				index++;
			}

			DoPostDraw(gl);
		}

		void IInteractable.DrawPick(OpenGL gl)
		{
			base.Draw(gl);
		}

		public virtual Polygon ToPolygon()
		{
			Polygon poly = new Polygon();
			poly.Attributes = Attributes;
			poly.CastsShadow = castsShadow;
			poly.CurrentContext = currentContext;
			poly.DrawNormals = drawNormals;
			poly.Faces = faces;
			poly.Material = material;
			poly.Name = name;
			poly.Normals = normals;
			poly.Rotate = rotate;
			poly.Scale = scale;
			poly.ShadowSize = shadowSize;
			poly.TransformOrder = transformationOrder;
			poly.Translate = translate;
			poly.UVs = uvs;
			poly.Vertices = vertices;

			return poly;
		}
	}
}