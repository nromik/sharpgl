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



//  29 June 2007: Updated the code for the new examples and for version 1.8.


using System;
using System.ComponentModel;

namespace SharpGL.SceneGraph.Cameras
{

	/// <summary>
	/// The camera class is a base for a set of derived classes for manipulating the
	/// projection matrix.
	/// </summary>
	[Serializable()]
	public abstract class Camera : SceneObject, IInteractable
	{
		public Camera()
		{
			name = "Camera";
		}

		/// <summary>
		/// Draw the current camera.
		/// </summary>
		/// <param name="gl">OpenGL object.</param>
		public override void Draw(OpenGL gl)
		{
			if(DoPreDraw(gl))
			{
            //  Causes stack overflow.
			//	gl.StockDrawing.Camera.Call(gl);


                //  TODO: Let the camera be drawn!

                //  Draw the outside of the cube as a quad strip.

           /*     gl.Begin(OpenGL.QUAD_STRIP);

                gl.Vertex(-0.5, -0.5, -0.5);
                gl.Vertex(-0.5, 0.5, -0.5);

                gl.Vertex(-0.5, -0.5, 0.5);
                gl.Vertex(-0.5, 0.5, 0.5);

                gl.Vertex(0.5, -0.5, 0.5);
                gl.Vertex(0.5, 0.5, 0.5);


                gl.Vertex(0.5, -0.5, -0.5);
                gl.Vertex(0.5, 0.5, -0.5);

                gl.Vertex(-0.5, -0.5, -0.5);
                gl.Vertex(-0.5, 0.5, -0.5);

                gl.End();*/

                //  Cap the cube.
           /*     gl.Begin(OpenGL.QUADS);

                gl.Vertex(-0.5, -0.5, -0.5);
                gl.Vertex(-0.5, -0.5, 0.5);
                gl.Vertex(0.5, -0.5, 0.5);
                gl.Vertex(0.5, -0.5, -0.5);

                gl.Vertex(-0.5, 0.5, -0.5);
                gl.Vertex(-0.5, 0.5, 0.5);
                gl.Vertex(0.5, 0.5, 0.5);
                gl.Vertex(0.5, 0.5, -0.5);

                gl.End();*/

				DoPostDraw(gl);
			}
		}

		/// <summary>
		///	This function projects through the camera, to OpenGL, ie. it 
		///	creates a projection matrix.
		/// </summary>
		public virtual void Project(OpenGL gl)
		{
			//	Load the projection identity matrix.
			gl.MatrixMode(OpenGL.PROJECTION);
			gl.LoadIdentity();

			//	Perform the projection.
		    TransformProjectionMatrix(gl);

			//	If we have the lookAtTarget set to true, look at it now.
			if(lookAtTarget)
				LookAt(gl);

			//	Get the matrix.
			float[] matrix = new float[16];
			gl.GetFloat(OpenGL.PROJECTION_MATRIX, matrix);
			for(int i=0; i<4; i++)
				for(int j=0; j<4; j++)
					projectionMatrix.data[j][i] = matrix[(i*4) + j];
				
			//	Back to the modelview matrix.
			gl.MatrixMode(OpenGL.MODELVIEW);
        //    if(lookAtTarget)
		//		LookAt(gl);
		}

		IInteractable IInteractable.GetObjectFromSelectNames(int[] names)
		{
			return (names.Length > 1) ? (IInteractable)target : (IInteractable)this;
		}

		/// <summary>
		/// This function is for when you simply want to call only the functions that
		/// would transform the projection matrix. Warning, it won't load the identity
		/// first, and it won't set the current matrix to projection, it's really for
		/// people who need to use it for their own projection functions (e.g Picking
		/// uses it to create a composite 'Pick' projection).
		/// </summary>
		public abstract void TransformProjectionMatrix(OpenGL gl);

		/// <summary>
		/// This function transforms the matricies to 'look' at the target point. To
		/// set the viewport however, it's better to just call Project, this function is
		/// for advanced users only!
		/// </summary>
		public virtual void LookAt(OpenGL gl)
		{
			gl.LookAt((double)translate.X, (double)translate.Y, (double)translate.Z,
				(double)target.X, (double)target.Y, (double)target.Z,
				(double)upVector.X, (double)upVector.Y, (double)upVector.Z);
		}

		#region Member Data

		/// <summary>
		/// This is the point in the scene that the camera is pointed at.
		/// </summary>
		protected Vertex target = new Vertex(0, 0, 0);

		/// <summary>
		/// If this variable is true, the camera will always 'look at' the target point.
		/// </summary>
		protected bool lookAtTarget = true;

		/// <summary>
		/// This is a vector that describes the 'up' direction (normally 0, 1, 0).
		/// Use this to tilt the camera.
		/// </summary>
		protected Vertex upVector = new Vertex(0, 1, 0);

		/// <summary>
		/// Should the target be drawn?
		/// </summary>
		protected bool drawTarget = false;

		/// <summary>
		/// Every time a camera is used to project, the projection matrix calculated 
		/// and stored here.
		/// </summary>
		protected Matrix projectionMatrix = new Matrix();

        /// <summary>
        /// The screen aspect ratio.
        /// </summary>
        public double aspect = 1.0f;

		#endregion

		#region Properties

		[Description("The target of the camera (the point it's looking at"), Category("Camera")]
		public Vertex Target
		{
			get {return target;}
			set {target = value;}
		}
		[Description("The up direction, relative to camera. (Controls tilt)."), Category("Camera")]
		public Vertex UpVector
		{
			get {return upVector;}
			set {upVector = value;}
		}
		[Description("If true, the camera will always 'look at' the target point"), Category("Camera")]
		public bool LookAtTarget
		{
			get {return lookAtTarget;}
			set {lookAtTarget = value;}
		}
		[Description("If true, the target will be drawn."), Category("Camera")]
		public bool DrawTarget
		{
			get {return drawTarget;}
			set {drawTarget = value;}
		}
        [Description("Screen Aspect Ratio"), Category("Camera")]
        public double Aspect
        {
            get { return aspect; }
            set { aspect = value; }
        }

		#endregion
			
	}

	/// <summary>
	/// This camera contains the data needed to perform a Frustum transformation
	/// to the projection matrix.
	/// </summary>
	[Serializable()]
	public class CameraFrustum : Camera
	{
		public CameraFrustum()
		{
			name = "Camera (Frustum)";
		}

		/// <summary>
		/// This is the main function of the camera, perform a Frustrum (in this case)
		/// transformation.
		/// </summary>
		public override void TransformProjectionMatrix(OpenGL gl)
		{
			gl.Frustum(left, right, bottom, top, near, far);
		}

		#region Member Data

		public double left = 0.0f;
		public double right = 0.0f;
		public double top = 0.0f;
		public double bottom = 0.0f;
		public double near = 0.0f;
		public double far = 0.0f;

			#endregion

		#region Properties

		public double Left
		{
			get {return left;}
			set {left = value;}
		}
		public double Right
		{
			get {return right;}
			set {right = value;}
		}
		public double Top
		{
			get {return top;}
			set {top = value;}
		}
		public double Bottom
		{
			get {return bottom;}
			set {bottom = value;}
		}
		public double Near
		{
			get {return near;}
			set {near = value;}
		}
		public double Far
		{
			get {return far;}
			set {far = value;}
		}
			
			#endregion
	}

	/// <summary>
	/// This camera contains the data needed to perform a Perspective transformation
	/// to the projection matrix.
	/// </summary>
	[Serializable()]
	public class CameraPerspective : Camera
	{
		public CameraPerspective()
		{
			name = "Camera (Perspective)";
		}

		/// <summary>
		/// This automatically creates a camera that see's what a light see's.
		/// </summary>
		/// <param name="light">The source light.</param>
		public CameraPerspective(Lights.Light light)
		{
			Translate = light.Translate;
			Target = Translate + light.Direction;
		}

		/// <summary>
		/// This is the class' main function, to override this function and perform a 
		/// perspective transformation.
		/// </summary>
		public override void TransformProjectionMatrix(OpenGL gl)
		{
			gl.Perspective(fieldOfView, aspect, near, far);
		}

		/// <summary>
		/// This function returns the parameters needed to make a Frustum camera.
		/// </summary>
		/// <returns>An array of six floats, left, right, top, bottom, near, far.</returns>
		public float[] GetFrustrum()
		{
			float top = (float)(System.Math.Tan(fieldOfView*3.14159/360.0) * near);
			float bottom = -top;
			float left = (float)aspect * bottom;
			float right = (float)aspect * top;

			return new float[] {left, right, top, bottom, (float)near, (float)far};

		}

		#region Member Data

		public double fieldOfView = 60.0f;
		public double near = 0.5f;
		public double far = 40.0f;

		#endregion

		#region Properties

		[Description("The angle of the lense of the camera (60 degrees = human eye)."), Category("Camera (Perspective")]
		public double FieldOfView
		{
			get {return fieldOfView;}
			set {fieldOfView = value;}
		}
		
		[Description("The near clipping distance."), Category("Camera (Perspective")]
		public double Near
		{
			get {return near;}
			set {near = value;}
		}
		[Description("The far clipping distance."), Category("Camera (Perspective")]
		public double Far
		{
			get {return far;}
			set {far = value;}
		}
			
			#endregion
	}

	/// <summary>
	/// This camera contains the data needed to perform an orthographic transformation
	/// to the projection matrix.
	/// </summary>
	[Serializable()]
	public class CameraOrthographic : Camera
	{
		public CameraOrthographic()
		{
			name = "Camera (Orthographic)";
		}

		/// <summary>
		/// This is the main function of the class, to perform a specialised projection
		/// in this case, an orthographic one.
		/// </summary>
		public override void TransformProjectionMatrix(OpenGL gl)
		{
			gl.Ortho(left, right, bottom, top, near, far);
		}

		#region Member Data

		public double left = 0.0f;
		public double right = 0.0f;
		public double top = 0.0f;
		public double bottom = 0.0f;
		public double near = 0.0f;
		public double far = 0.0f;

			#endregion

		#region Properties

		public double Left
		{
			get {return left;}
			set {left = value;}
		}
		public double Right
		{
			get {return right;}
			set {right = value;}
		}
		public double Top
		{
			get {return top;}
			set {top = value;}
		}
		public double Bottom
		{
			get {return bottom;}
			set {bottom = value;}
		}
		public double Near
		{
			get {return near;}
			set {near = value;}
		}
		public double Far
		{
			get {return far;}
			set {far = value;}
		}
			
			#endregion
	}
}
