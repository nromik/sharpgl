//    Line.cs
//    Created by Dave Kerr, 12/08/2003
//    Copyright (c) Dave Kerr 2003
//    http://www.codechamber.com

//	This class is a 'tutorial' of how you would go about creating new types of
//	scene object. I've tried to make the comments as in-depth as possible.


using System;
using System.Collections;
using System.ComponentModel;
using SharpGL.SceneGraph.Lights;


namespace SharpGL.SceneGraph
{
	/// <summary>
	/// Line is a simple class that draws a line between two points. It is serializable,
	/// so will be saved and loaded when the scene is. It is derived from SceneObject, 
	/// so is a proper SharpGL scene object, ready to be used, and it is interactable,
	/// meaning the client can use the mouse to interact with it.
	/// </summary>
	[Serializable]
	public class Line : SceneObject, IInteractable
	{
		public Line()
		{
			//	As soon as the line is created, we give it a descriptive name. This means
			//	when you have many scene objects, they are more easily organised.
			name = "Line";
		}

		/// <summary>
		/// This is the function that you must override to draw the line.
		/// The code is commented step by step.
		/// </summary>
		public override void Draw(OpenGL gl)
		{
			//	This is the first function that must be called, DoPreDraw. It basicly
			//	sets up certain settings and organises the stack so that what you draw
			//	now doesn't interfere with what you will draw next.
			DoPreDraw(gl);

			//	This next code saves the current line attributes, then sets the line
			//	attributes for this line.
			lineAttributes.Set(gl);

			//	Begin drawing lines.
			gl.Begin(OpenGL.LINES);
			
			//	Add the two vertices.
			gl.Vertex(point1);
			gl.Vertex(point2);

			//	End the drawing.
			gl.End();

			//	Restore the previous line settings.
			lineAttributes.Restore(gl);

			//	Finalise the drawing process.
			DoPostDraw(gl);
		}

		/// <summary>
		/// These are all the opengl settings involved with lines.
		/// </summary>
		protected Attributes.Line lineAttributes = new Attributes.Line();

		/// <summary>
		/// This is the beginning of the line, the first point.
		/// </summary>
		protected Vertex point1 = new Vertex();

		/// <summary>
		/// This is the end of the line, the second point.
		/// </summary>
		protected Vertex point2 = new Vertex();

		[Description("Line Attributes"), Category("Attributes")]
		public Attributes.Line Attributes
		{
			get {return lineAttributes;}
			set {lineAttributes = value;}
		}
		[Description("Point 1"), Category("Line")]
		public Vertex Point1
		{
			get {return point1;}
			set {point1 = value;}
		}
		[Description("Point 2"), Category("Line")]
		public Vertex Point2
		{
			get {return point2;}
			set {point2 = value;}
		}
	}
}
