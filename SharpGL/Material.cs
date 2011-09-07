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


//	The key to having a fast graphics engine is polygons and materials. The material
//	class for this Scene Graph is infantile, and needs to be updated and optimised, and
//	will hopefully be one of the biggest revisions of the libraries updates.

using System;
using System.ComponentModel;
using System.Drawing;

namespace SharpGL.SceneGraph
{
		/// <summary>
		/// A material object is defined in mathematical terms, i.e it's not exclusivly 
		/// for OpenGL. This means later on, DirectX or custom library functions could
		/// be added.
		/// </summary>
	[TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
	[Serializable()]
	public class Material
	{
		public Material() {}

		public override string ToString()
		{
			return name;
		}

		public GLColor CalculateLighting(SharpGL.SceneGraph.Lights.Light light, float angle)
		{
			double angleRadians = angle * 3.14159 / 360.0;
			GLColor reflected = ambient * light.Ambient;
			reflected += diffuse * light.Diffuse * (float)Math.Cos(angleRadians);

			return reflected;
		}
		
		public virtual void Set(OpenGL gl)
		{
			//	Set the material properties.
			gl.Material(OpenGL.FRONT_AND_BACK, OpenGL.AMBIENT, ambient);
			gl.Material(OpenGL.FRONT_AND_BACK, OpenGL.DIFFUSE, diffuse);
			gl.Material(OpenGL.FRONT_AND_BACK, OpenGL.SPECULAR, specular);
			gl.Material(OpenGL.FRONT_AND_BACK, OpenGL.EMISSION, emission);
			gl.Material(OpenGL.FRONT_AND_BACK, OpenGL.SHININESS, shininess);
		
			//	Set the texture properties.
			texture.Bind(gl);
		}

		protected GLColor ambient = new GLColor(0.2f, 0.2f, 0.2f, 1);
		protected GLColor diffuse = new GLColor(0.8f, 0.8f, 0.8f, 1);
		protected GLColor specular = new GLColor(0, 0, 0, 1);
		protected GLColor emission = new GLColor(0.1f, 0.1f, 0.1f, 1);
		protected float shininess = 0;
		protected Texture texture = new Texture();
		protected string name = "Material";
	
		#region Properties

		[Description("The descriptive name of the material"), Category("Name")]
		public string Name
		{
			get {return name;}
			set {name = value;}
		}
		public System.Drawing.Color Ambient
		{
			get {return ambient.ColorNET;}
			set {ambient.ColorNET = value;}
		}
		public System.Drawing.Color Diffuse
		{
			get {return diffuse.ColorNET;}
			set {diffuse.ColorNET = value;}
		}
		public System.Drawing.Color Specular
		{
			get {return specular.ColorNET;}
			set {specular.ColorNET = value;}
		}
		public System.Drawing.Color Emission
		{
			get {return emission.ColorNET;}
			set {emission.ColorNET = value;}
		}
		public float Shininess
		{
			get {return shininess;}
			set {shininess = value;}
		}
		public Texture Texture
		{
			get {return texture;}
			set {texture = value;}
		}

		#endregion
	}
	
}

