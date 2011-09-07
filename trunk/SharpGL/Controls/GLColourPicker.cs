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
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SharpGL.Controls
{
	/// <summary>
	/// Summary description for GLColourPicker.
	/// </summary>
	public class GLColourPicker : System.Windows.Forms.Control
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GLColourPicker()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		protected override void OnPaint(PaintEventArgs pe)
		{
			float width = pe.ClipRectangle.Width;
			float height = pe.ClipRectangle.Height;

			Graphics graphics = pe.Graphics;
			Bitmap bmp = new Bitmap((int)width, (int)height,
				System.Drawing.Imaging.PixelFormat.Format32bppRgb);

			float red = 0, green = 0, blue = 0;

			float redadd = 255.0f / width;
			float greenadd = 255.0f / (height / 2);
			float blueadd = 255.0f / (height / 2);

			int y=0;
			for(y=0; y<(pe.ClipRectangle.Height/2); y++)
			{
				for(int x=0; x<pe.ClipRectangle.Width; x++)
				{
					red += redadd;
					bmp.SetPixel(x, y, Color.FromArgb((int)red, (int)green, (int)blue));
				}
				green += greenadd;
				red = 0;
			}
			for(; y<pe.ClipRectangle.Height; y++)
			{
				for(int x=0; x<pe.ClipRectangle.Width; x++)
				{
					red += redadd;
					bmp.SetPixel(x, y, Color.FromArgb((int)red, (int)green, (int)blue));
				}
				green -= greenadd;
				blue += blueadd;
				red = 0;
			}

			graphics.DrawImage(bmp, 0, 0);

			bmp.Dispose();

			// Calling the base class OnPaint
			base.OnPaint(pe);
		}
	
		protected override void OnSizeChanged(EventArgs e)
		{
			//	We need to know the size of the control so we can
			//	have the correctly variables for a function that selects the
			//	colour.

			theWidth = Width;
			theHeight = Height;

			base.OnSizeChanged (e);
		}

		float theWidth = 0;
		float theHeight = 0;
	}
}
