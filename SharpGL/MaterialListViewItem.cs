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
using System.Windows.Forms;

using SharpGL.SceneGraph;

namespace SharpGL.Controls
{
	/// <summary>
	/// This allows a list view item to be created from a material, which is really
	/// useful, especially for 3D apps. For more advanced functionality, just derive
	/// from this class, as there's a lot of good code here. Writing this sort of 
	/// class from scratch is pointless.
	/// 
	/// At the moment this class is not working properly, because of changes to the
	/// material class, use with caution!
	/// </summary>
	public class MaterialListViewItem : System.Windows.Forms.ListViewItem
	{
		public MaterialListViewItem()
		{
		}

		/// <summary>
		/// This function creates the list view item from a material, with an image
		/// of the specified size.
		/// </summary>
		/// <param name="material">The material to create it from.</param>
		/// <param name="sizeX">Size of the thumbnail.</param>
		/// <param name="sizeY">Size of the thumbnail.</param>
		public MaterialListViewItem(Material material, ImageList images)
		{
			//	Create a preview of the material.
		//	Bitmap preview = material.CreatePreview(images.ImageSize.Width, images.ImageSize.Height);

			//	Draw the texture in the top left cornder.
		//	Graphics graphics = Graphics.FromImage(preview);
		//	graphics.DrawImage(material.Texture.ToBitmap(), new Rectangle(5, 5, 25, 25));
		//	graphics.Dispose();

			//	Add this preview image to the imagelist.
		//	ImageIndex = images.Images.Add(preview, Color.Aquamarine);
	
			//	set the text, and the tag.
			Text = material.Name;
			Tag = material;
		}
	}
}
