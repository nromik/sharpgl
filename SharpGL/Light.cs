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



//	The OpenGL lights are still a little bit kludgy, especially with the drawing /
//	picking code, the picking needs a larger object.

using System;
using System.Collections;
using System.ComponentModel;

namespace SharpGL.SceneGraph.Lights
{
	/// <summary>
	/// A Light is defined purely mathematically, but works well with OpenGL.
	/// </summary>
	[Serializable()]
	public class Light : SceneObject, IInteractable
	{
		public Light()
		{
			name = "Light";
			translate = new Vertex(0, 3, 0);
		}

		/// <summary>
		/// This function uses OpenGL to draw the light.
		/// </summary>
		/// <param name="gl">OpenGL object.</param>
		public override void Draw(OpenGL gl)
		{
			//	Only draw lights that are on.
			if(on)
			{
				//	Set the matrix, material etc.
				if(DoPreDraw(gl))
				{
					//	...Drawing a 3D circle (from the stock) in the correct colour...
					gl.Color(ambient.ColorGL);

					gl.Scale(0.4f, 0.4f, 0.4f);
					gl.StockDrawing.DrawCircle3D(gl);

					DoPostDraw(gl);
					
				}
			}
		}

		void IInteractable.DrawPick(OpenGL gl)
		{
			if(on)
			{
				if(DoPreDraw(gl))
				{
					
					Polygon poly = new Polygon();
					poly.CreateCube();
					poly.Scale = new Vertex(0.2f, 0.2f, 0.2f);
					poly.Draw(gl);

					gl.PushName(0);
					gl.Vertex(direction);
					gl.PopName();

					DoPostDraw(gl);
				}
			}
		}

		IInteractable IInteractable.GetObjectFromSelectNames(int[] names)
		{
			//	If it's a single name, then it's just the light.
			if(names.Length == 1)
				return this;

			//	If it has another name, then it's a control point.
			return (IInteractable)direction;
		}

		/// <summary>
		/// This function sets all of the lights parameters into OpenGL.
		/// </summary>
		public virtual void Set(OpenGL gl)
		{
			if(on)
			{
				//	Enable this light.
				gl.Enable(glCode);

				//	The light is on, so set it's properties.
				gl.Light(glCode, OpenGL.AMBIENT, ambient);
				gl.Light(glCode, OpenGL.DIFFUSE, diffuse);
				gl.Light(glCode, OpenGL.SPECULAR, specular);
				gl.Light(glCode, OpenGL.POSITION, new float[] {translate.X, translate.Y, translate.Z, 1.0f});
				gl.Light(glCode, OpenGL.SPOT_CUTOFF, spotCutoff);

				Vertex vector = Translate - direction;
				gl.Light(glCode, OpenGL.SPOT_DIRECTION, vector);
			}
			else
				gl.Disable(glCode);
		}

		#region Member Data

		/// <summary>
		/// This is the OpenGL code for the light.
		/// </summary>
		protected uint glCode = 0;

		/// <summary>
		/// The ambient colour of the light.
		/// </summary>
		protected GLColor ambient = new GLColor();
			
		/// <summary>
		/// The diffuse color of the light.
		/// </summary>
		protected GLColor diffuse = new GLColor();
			
		/// <summary>
		/// The specular colour of the light.
		/// </summary>
		protected GLColor specular = new GLColor();
			
		/// <summary>
		/// The colour of the shadow created by this light.
		/// </summary>
		protected GLColor shadowColor = new GLColor(0, 0, 0, 0.4f);

		/// <summary>
		/// True when the light is on.
		/// </summary>
		protected bool on = false;

		/// <summary>
		/// The spotlight cutoff value (between 0-90 for spotlights, or 180 for a 
		/// simple light).
		/// </summary>
		protected float spotCutoff = 180.0f;
			
		/// <summary>
		/// A Vector describing the direction of the spotlight.
		/// </summary>
		protected Vertex direction = new Vertex(0, 1, 0);

		/// <summary>
		/// Should the light cast a shadow?
		/// </summary>
		protected bool castShadow = true;

		/// <summary>
		/// Should the lights shadow be a soft shadow? (Only the raytracer handles this).
		/// </summary>
		protected bool softShadow = true;

		/// <summary>
		/// This variable describes the radius of the spherical light source. As
		/// you get further away from the centre of the sphere, the shadow becomes
		/// softer. I.e, smaller radius gives a sharper shadow.
		/// </summary>
		protected float softShadowRadius = 0.5f;

		#endregion
		
		[Description("This is the internal opengl code of the light (advanced users only!)"), Category("Advanced")]
		public uint GLCode
		{
			get {return glCode;}
			set {glCode = value; modified = true;}
		}
		public System.Drawing.Color Ambient
		{
			get {return ambient.ColorNET;}
			set {ambient.ColorNET = value; modified = true;}
		}
		public System.Drawing.Color Diffuse
		{
			get {return diffuse.ColorNET;}
			set {diffuse.ColorNET = value; modified = true;}
		}
		public System.Drawing.Color Specular
		{
			get {return specular.ColorNET;}
			set {specular.ColorNET = value; modified = true;}
		}
		public GLColor ShadowColor
		{
			get {return shadowColor;}
			set {shadowColor = value; modified = true;}
		}
		[Description("Is the light turned on?"), Category("Light")]
		public bool On
		{
			get {return on;}
			set {on = value;}
		}
		public Vertex Direction
		{
			get {return direction;}
			set {direction = value; modified = true;}
		}
		public float SpotCutoff
		{
			get {return spotCutoff;}
			set {spotCutoff = value; modified = true;}
		}
		public bool CastShadow 
		{
			get {return castShadow;}
			set {castShadow = value; modified = true;}
		}
	}
	
}
