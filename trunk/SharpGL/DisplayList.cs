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

namespace SharpGL.SceneGraph
{
	/// <summary>
	/// The display list class basicly wraps an OpenGL display list, making them easier
	/// to manage. Remember this class is completely OpenGL dependant. In time this class
	/// will derive from the IOpenGLDependant interface.
	/// </summary>
	public class DisplayList
	{
		public enum DisplayListMode
		{
			Compile = (int)OpenGL.COMPILE,
			CompileAndExecute = (int)OpenGL.COMPILE_AND_EXECUTE,
		}

		public DisplayList()
		{
		}

		/// <summary>
		/// This function generates the display list. You must call it before you call
		/// anything else!
		/// </summary>
		/// <param name="gl">OpenGL</param>
		public virtual void Generate(OpenGL gl)
		{
			//	Generate one list.
			list = gl.GenLists(1);
		}

		/// <summary>
		/// This function makes the display list.
		/// </summary>
		/// <param name="gl">OpenGL</param>
		/// <param name="mode">The mode, compile or compile and execute.</param>
		public virtual void New(OpenGL gl, DisplayListMode mode)
		{
			//	Start the list.
			gl.NewList(list, (uint)mode);
		}

		/// <summary>
		/// This function ends the compilation of a list.
		/// </summary>
		/// <param name="gl"></param>
		public virtual void End(OpenGL gl)
		{
			//	This function ends the display list
			gl.EndList();
		}

		public virtual bool IsList(OpenGL gl)
		{
			//	Is the list a proper display list?
			if(gl.IsList(list) == OpenGL.TRUE)
				return true;
			else
				return false;
		}

		public static bool IsList(OpenGL gl, DisplayList displayList)
		{
			//	Is the specified list a proper display list?
			if(gl.IsList(displayList.list) == OpenGL.TRUE)
				return true;
			else
				return false;
		}

		public virtual void Call(OpenGL gl)
		{
			gl.CallList(list);
		}

		public virtual void Delete(OpenGL gl)
		{
			gl.DeleteLists(list, 1);
			list = 0;
		}

		protected uint list = 0;

		public uint List
		{
			get {return list;}
		}
	}
}
