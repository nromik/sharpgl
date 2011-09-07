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
using System.IO;

using SharpGL.SceneGraph;

namespace SharpGL.Persistence.Formats.Standard
{
	/// <summary>
	/// 
	/// </summary>
	public class StandardImagesFormat : SharpGL.Persistence.Format
	{
		public StandardImagesFormat()
		{
		}

		protected override object LoadData(Stream stream)
		{
			//	We use a binary reader to load data.
			BinaryReader reader = new BinaryReader(stream, System.Text.Encoding.ASCII);

			//	Create a new material.
			Material material = new Material();
        
            //  TODO: fix this data.
		//    //	Create a bitmap object.
		//	System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(reader.BaseStream);
		//	material.Texture.LoadPixelData(bitmap);

			return material;
		}

		protected override bool SaveData(object data, Stream stream)
		{
			return false;
		}

		public override string[] FileTypes
		{
			get {return new string[] {"bmp", "jpg", "gif", "png", "tif"};}
		}

		public override string Filter
		{
			get {return "Image Files (*.bmp, *.jpg, *.gif, *.png, *.tif)|*.bmp;*.jpg;*.tga;*.png;*.tif";}
		}

		public override Type[] DataTypes
		{
			get {return new Type[] {typeof(Material)};}
		}
	}
}
