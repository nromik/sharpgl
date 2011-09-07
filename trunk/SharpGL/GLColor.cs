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



//	This class is useful, and very commonly used, but a better implementation of it
//	would have a custom editor, so that instead of having to use someGLColor.ColorNET
//	you could use the color itself.

using System;
using System.Drawing;

namespace SharpGL.SceneGraph
{

	/// <summary>
	/// This class provides a means of working with standard and opengl colours.
	/// Use the ColorNet and ColorGL properties to set or access the colour in either
	/// mode.
	/// </summary>
	[Serializable()]
	public class GLColor
	{
		public GLColor(){}
		public GLColor(float r, float g, float b, float a)
		{
			this.r = r; this.g = g; this.b = b; this.a = a;
		}

		public static implicit operator System.Drawing.Color(GLColor color)
		{
			return System.Drawing.Color.FromArgb((int)(color.a * 255), (int)(color.r * 255.0f), 
				(int)(color.g * 255.0f), (int)(color.b * 255.0f));
		}
		public static implicit operator float[](GLColor color)
		{
			return new float[] {color.r, color.g, color.b, color.a};
		}
		public static implicit operator GLColor(System.Drawing.Color color)
		{
			GLColor col = new GLColor();
			col.ColorNET = color;
			return col;
		}

		public virtual void Set(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
		}

		public static GLColor operator * (GLColor lhs, GLColor rhs)
		{
			return new GLColor(lhs.r * rhs.r, lhs.g* rhs.g, lhs.b * rhs.b, lhs.a * rhs.a);
		}

		public static GLColor operator * (GLColor lhs, float rhs)
		{
			return new GLColor(lhs.r * rhs, lhs.g* rhs, lhs.b * rhs, lhs.a * rhs);
		}

		public static GLColor operator + (GLColor lhs, GLColor rhs)
		{
			return new GLColor(lhs.r + rhs.r, lhs.g+ rhs.g, lhs.b + rhs.b, lhs.a + rhs.a);
		}

		public virtual void Clamp()
		{
			if(r>1)r=1;
			if(g>1)g=1;
			if(b>1)b=1;
			if(a>1)a=1;
			if(r<1)r=0;
			if(g<1)g=0;
			if(b<1)b=0;
			if(a<1)a=0;
		}

		protected float r = 0.0f;
		protected float g = 0.0f;
		protected float b = 0.0f;
		protected float a = 0.0f;

		/// <summary>
		/// This property allows you to access the color as if it was a .NET
		/// color.
		/// </summary>
		public System.Drawing.Color ColorNET
		{
			get 
			{
				System.Drawing.Color col = System.Drawing.Color.FromArgb((int)(r * 255.0f), 
					(int)(g * 255.0f), (int)(b * 255.0f));
				return col;
			}
			set 
			{
				System.Drawing.Color col = value;
				r = (float)col.R / 255.0f;
				g = (float)col.G / 255.0f;
				b = (float)col.B / 255.0f;
				a = (float)col.A / 255.0f;}
		}

		/// <summary>
		/// This property accesses the color as an opengl value.
		/// </summary>
		public float[] ColorGL
		{
			get {return new float[] {r, g, b, a};}
			set 
			{
				r = value[0];
				g = value[1];
				b = value[2];
				a = value[3];
			}
		}

		public float R
		{
			get {return r;}
			set {r = value;}
		}
		public float G
		{
			get {return g;}
			set {g = value;}
		}
		public float B
		{
			get {return b;}
			set {b = value;}
		}
		public float A
		{
			get {return a;}
			set {a = value;}
		}
	}
}