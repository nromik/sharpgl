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
using System.Windows.Forms;

using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Collections;

namespace SharpGL.Persistence
{
	/// <summary>
	/// A Format class has the functionality to load data from a certain type of file.
	/// </summary>
	public abstract class Format
	{
		/// <summary>
		/// This function should load the file specified by filePath and insert any
		/// scene data in it into the scene.
		/// </summary>
		/// <param name="filePath">The path of the file to load.</param>
		/// <returns>The object loaded from the file.</returns>
		public virtual object Load(string filePath)
		{
			//	Make sure the file type is valid.
			if(IsValidFilename(filePath) == false)
				return null;

			//	Open a stream.
			Stream stream = new FileStream(filePath, FileMode.Open);

			//	Load the file.
			object data = LoadData(stream);

			//	Close the stream.
			stream.Close();

			//	Success!
			return data;
		}

		/// <summary>
		/// This function should save the passed object to the specified file path.
		/// </summary>
		/// <param name="data">The object to save.</param>
		/// <param name="filePath">The path of the file to save to.</param>
		/// <returns>True if saving was sucessful, false otherwise.</returns>
		public virtual bool Save(object data, string filePath)
		{
			//	Validate the file path, and the data type.
			if(IsValidFilename(filePath) == false || IsValidData(data.GetType()) == false)
				return false;

			//	Create a stream.
			Stream stream = new FileStream(filePath, FileMode.Create);

			//	Save the file
			bool success = SaveData(data, stream);

			//	Close the stream.
			stream.Close();

			//	Success!
			return success;
		}

		/// <summary>
		/// This is the main loading function, override it to provide the ability to
		/// load your data.
		/// </summary>
		/// <param name="stream">The stream the data is contained in.</param>
		/// <returns>The object contained in the stream.</returns>
		protected abstract object LoadData(Stream stream);

		/// <summary>
		/// This is the main saving function, override it to provide the ability to
		/// save your data.
		/// </summary>
		/// <param name="data">The data to save.</param>
		/// <param name="stream">The stream the data should be written to.</param>
		/// <returns>True if saving was successful.</returns>
		protected abstract bool SaveData(object data, Stream stream);

		/// <summary>
		/// This function quickly checks to see if a specified file path is supported
		/// by this format.
		/// </summary>
		/// <param name="filePath">The path to check, e.g "C:\my documents\file.cob".</param>
		/// <returns>True if the file is of a valid type.</returns>
		public virtual bool IsValidFilename(string filePath)
		{
			//	Get the extension.
			int ext = filePath.LastIndexOf('.') + 1;
			string extension = filePath.Substring(ext, filePath.Length - ext);
			extension = extension.ToLower();

			//	Check it against the types supported.
			string[] types = FileTypes;

			if(types == null)
				return false;

			foreach(string type in types)
			{
				if(extension == type)
					return true;
			}

			return false;
		}

		/// <summary>
		/// This function asserts that the type of object is valid for this format.
		/// </summary>
		/// <param name="dataType">The type of the data.</param>
		/// <returns>True if the data is valid.</returns>
		protected virtual bool IsValidData(Type dataType)
		{
			//	Go through each data type, if we find a valid one, return true.
			foreach(Type type in DataTypes)
			{
				if(dataType == type)
					return true;
			}

			return false;
		}

		/// <summary>
		/// This property returns an array of file types that can be used with this
		/// format, e.g the CaligariFormat would return "cob", "scn".
		/// </summary>
		public abstract string[] FileTypes
		{
			get;
		}

		/// <summary>
		/// This gets a filter suitable for a file open/save dialog, e.g 
		/// "Caligari trueSpace Files (*.cob, *.scn)|*.cob;*.scn".
		/// </summary>
		public abstract string Filter
		{
			get;
		}

		/// <summary>
		/// This is the types of data the format can use, eg polygons, scenes.
		/// </summary>
		public abstract Type[] DataTypes
		{
			get;
		}
	}
}