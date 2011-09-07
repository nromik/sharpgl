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


//	This metagon class is unfinished, I've left it in for interest only. It's a polygon
//	who's vertices are effected by the 'gravity' of other metagons.


using System;
using SharpGL.SceneGraph.Collections;

namespace SharpGL.SceneGraph
{
	/// <summary>
	/// Summary description for Metagon.
	/// </summary>
	public class Metagon : Polygon
	{
		public Metagon()
		{
		}

		public override void Draw(OpenGL gl)
		{
			//	Drawing metagons is fairly easy, we save the vertex data, make a copy of
			//	it, apply gravity, then draw the polygon. Afterwards, we restore it.
			VertexCollection oldVertices = vertices;
			VertexCollection newVertices = new VertexCollection();

			foreach(Vertex v in vertices)
			{
				//	Apply gravity, this means we need to know what other metagons there
				//	are.

				//	Find the closest vertex from the other metagon.
				double finddistance = -1;
				Vertex gravPoint = null;
				foreach(Vertex mV in other.Vertices)
				{
					Vertex temp = (mV/* + other.Translate*/) - (v + Translate);
					double tempdist = temp.Magnitude();
					if(finddistance == -1 || tempdist < finddistance)	
					{	
						gravPoint = mV;
						finddistance = tempdist;
					}
				}

				Vertex toGrav = gravPoint - v;
				double distance = toGrav.Magnitude();

				//	The 'force' of gravity is the square root of this value, so it 
				//	only works well up cloes.
				double force = System.Math.Sqrt(distance);
				double growForce = System.Math.Sqrt(force);
				toGrav.UnitLength();

				//	Now we push this vector along by the required amount.
				Vertex newV = v + (toGrav * (float)force);

			

				newVertices.Add(newV);
			}

			vertices = newVertices;

			//	Draw it.
			base.Draw(gl);
		}

		public Metagon other;
	}
}
