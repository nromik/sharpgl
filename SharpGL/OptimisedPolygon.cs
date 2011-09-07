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


//	SharpGL 1.7 : This file has been depreciated. All Polygons (and all sceneobjects)
//	are now automatiacally optimised into display lists. See SceneObject.cs for 
//	more info.

/*
using System;
using System.Collections;
using System.ComponentModel;

using SharpGL.SceneGraph.Collections;
using SharpGL.SceneGraph.Raytracing;

namespace SharpGL.SceneGraph
{
	/// <summary>
	/// An optimised polygon is one that is much, much faster. However, it must be created
	/// from an ordinary polygon.
	/// </summary>
	[Serializable]
	public class OptimisedPolygon : SceneObject, IInteractable
	{
		/// <summary>
		/// This is the main constructor. Pass it OpenGL and the polygon you want to
		/// make an optimised version of, and the fully optimised polygon will be 
		/// created.
		/// </summary>
		/// <param name="gl">OpenGL.</param>
		/// <param name="polygon">The polygon to optimise.</param>
		public OptimisedPolygon(OpenGL gl, Polygon polygon) 
		{
			//	Draw the polygon in list compile mode.
			displayList = gl.GenLists(1);
			gl.NewList(displayList, OpenGL.COMPILE);

			polygon.Draw(gl);

			//	End the list.
			gl.EndList();

			//	The polygon is safe to dispose now.
		}

		public override void Draw(OpenGL gl)
		{
			gl.CallList(displayList);
		}

		protected uint displayList = 0;
	}
}
*/