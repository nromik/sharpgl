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


//	This is the core control, check it out...
//	By the way, ANYONE who can help with my persistent problem of having flickering
//	GDI code, even though I render to an offscreen DC then blit it please email me
//	(focus_business@hotmail.com) cause I really want this problem sorted, the GDI
//	and OpenGL drawing working together is a really powerful feature, that is weakened
//	by the annoying flickering!


using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Interaction;
using SharpGL.SceneGraph.Collections;

namespace SharpGL
{
   	/// <summary>
	/// The SceneControl is an OpenGLControl that contains and draws a Scene object.
    /// </summary>
    [System.Drawing.ToolboxBitmap(typeof(SceneControl), "SharpGL.png")]
	public class SceneControl : SharpGL.OpenGLControl
	{
		private System.ComponentModel.IContainer components;

        public SceneControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.UserPaint, true);
			
			//	Create the scene.
            scene.Initialise(Width, Height, SceneType.HighQuality);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			// 
			// OpenGLCtrl
			// 
			this.Name = "OpenGLCtrl";

		}
		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			//	If we're doing GDI drawing, clear it now.
			if(gdiEnabled)
				OpenGL.GDIGraphics.Clear(Color.Transparent);

			//	Get the time (for speed checking reasons).
			System.DateTime timeBefore = System.DateTime.Now;

			//	Make sure it's our instance of openSharpGL that's active.
			OpenGL.MakeCurrent();

			//	Do the scene drawing.
			OpenGL.Clear(OpenGL.COLOR_BUFFER_BIT | OpenGL.DEPTH_BUFFER_BIT | OpenGL.STENCIL_BUFFER_BIT);
			scene.Draw();
			if(currentBuilder != null)
				currentBuilder.Draw(OpenGL);

			//	If there is a draw handler, then call it.
            DoOpenGLDraw(e);		
			
			//	Swap the buffers, i.e draw.
			OpenGL.SwapBuffers();

			System.DateTime timeAfter = System.DateTime.Now;

			//	If's there's a GDI draw handler, then call it.
            DoGDIDraw(e);

			//	Draw the render time.
			if(drawRenderTime)
			{
				System.TimeSpan span = new System.TimeSpan(timeAfter.Ticks - timeBefore.Ticks);
				OpenGL.GDIGraphics.DrawString("Draw Time : " + span.Milliseconds + " milliseconds",
					new System.Drawing.Font(FontFamily.GenericSerif, 10), Brushes.White, new PointF(1, 1));
			}

			//	Blit our offscreen bitmap.
			e.Graphics.DrawImageUnscaled(OpenGL.OpenGLBitmap, 0, 0);
			
			//	If the client wants GDI drawing, blit it on now.
			if(gdiEnabled)
				e.Graphics.DrawImageUnscaled(OpenGL.GDIBitmap, 0, 0);
		}


		protected override void OnPaintBackground(PaintEventArgs e)
		{
			//	We override this, and don't call the base, i.e we don't paint
			//	the background.
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			//  Don't call the base- we handle sizing ourselves.

			//	Resize the DIB Surface.
			OpenGL.Create(Width, Height);

			//	OpenGL needs to resize the viewport.
			scene.Resize(Width, Height);

			Invalidate();
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			//	Call the base.
			base.OnMouseDown(e);

			//	If we don't want any mouse operations then return.
			if(mouseOperation == MouseOperation.None)
				return;

			//	If we are either in select mode, or on always select, do a hit test.
			if(autoSelect || mouseOperation == MouseOperation.Select)
			{
				//	If we have control held down, then add to the selection.
				hits.Clear();
				hits = scene.DoHitTest(e.X, e.Y);
				
				//	If we hit nothing, then move the camera instead.
				if(hits.Count == 0)
					hits.Add(scene.CurrentCamera);
			}

			//	If we've hit stuff, we hide the pointer.
			if(hits.Count > 0)
            	Cursor.Current = null;

			//	If there is a Builder, then click it.
			if(currentBuilder != null)
			{
				//	Tell the builder there's been a click. It may return
				//	us an object, if so, we jam it into the scene.
				object val = currentBuilder.OnMouseDown(scene.OpenGL, e);
				if(val != null)
				{
					//	Add the object.
					scene.Jam(val);

					//	We can now get rid of the builder and stop
					//	capture.
					Capture = false;
					currentBuilder = null;
				}
			}
			
			curX = e.X;
			curY = e.Y;
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if(currentBuilder != null)
			{
				//	If we are currently using a builder, update it.
				if(currentBuilder.OnMouseMove(Scene.OpenGL, e))
					Invalidate();
			}
			//	If we have selected items, move them.
			else if(hits.Count > 0)
			{
				//	The 'effect' vertex describes the mouse movement in 3D space.
				Vertex effect = new Vertex();

				//	Set the effect values.
				if(e.Button == System.Windows.Forms.MouseButtons.Left)
				{
					effect.X += ((float)e.X - (float)curX) / 20.0f;
					effect.Z += ((float)e.Y - (float)curY) / 20.0f;
				}
				else if(e.Button == System.Windows.Forms.MouseButtons.Right)
					effect.Y -= ((float)e.Y - (float)curY) / 20.0f;
				
				//	Interact with each hit object.
				foreach(IInteractable interact in hits)
					interact.DoMouseInteract(mouseOperation, effect.X, effect.Y, effect.Z);
				
				//	Redraw.
				Invalidate();

				curX = e.X;
				curY = e.Y;
			}
			else
			{

				//	If we don't have it's, but we're over an object, change the cursor.
				if(mouseHover)
				{
					InteractableCollection overhits = scene.DoHitTest(e.X, e.Y);
					if(overhits.Count > 0)
						Cursor.Current = Cursors.Hand;
					else
						Cursor.Current = Cursors.Default;
				}
			}
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			if(e.Clicks == 1)
			{
				if(SelectedItemChanged != null)
					SelectedItemChanged(this, new EventArgs());
			}

			//	If there is a Builder, then update it.
			if(currentBuilder != null)
			{
				//	Tell the builder there's been a click. It may return
				//	us an object, if so, we jam it into the scene.
				object val = currentBuilder.OnMouseUp(scene.OpenGL, e);
				if(val != null)
				{
					//	Add the object.
					scene.Jam(val);

					//	We can now get rid of the builder and stop
					//	capture.
					Capture = false;
					currentBuilder = null;
				}
			}

			if(autoSelect)
				hits.Clear();
		}

		/// <summary>
		/// This function initialises the building of a building 
		/// object.
		/// </summary>
		/// <param name="builder">The builder.</param>
		public virtual void StartBuilding(Builder builder)
		{
			//	Set the current builder.
			currentBuilder = builder;
			
			//	Start mouse capture.
			Capture = true;
		}
		
		#region Events

		[Description("Called whenever the user clicks on a sceneobject."), Category("Interaction")]
		public event EventHandler SelectedItemChanged;

		#endregion

		#region Member Data

		/// <summary>
		/// This is the last set of objects hit by the mouse.
		/// </summary>
		protected InteractableCollection hits = new InteractableCollection();

		/// <summary>
		/// This is the current X position.
		/// </summary>
		protected int curX = 0;

		/// <summary>
		/// This is the current Y position.
		/// </summary>
		protected int curY = 0;

		/// <summary>
		/// This is the current function of the mouse.
		/// </summary>
		protected MouseOperation mouseOperation =  MouseOperation.Translate;
		
		/// <summary>
		/// This is the scene itself.
		/// </summary>
		protected Scene scene = new Scene();

		/// <summary>
		/// This means that whatever mode the mouse is in, you can select as well.
		/// </summary>
		protected bool autoSelect = true;

		/// <summary>
		/// When true, the mouse will turn into a hand when hovering over objects.
		/// </summary>
		protected bool mouseHover = true;

		/// <summary>
		/// The current builder, if any.
		/// </summary>
		protected Builder currentBuilder = null;

		#endregion

		#region Properties

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Scene Scene 
		{
			get {return scene;}
			set {scene = value;}
		}
        [Description("With design mode on lights, cameras and the grid will be drawn."), Category("SharpGL")]
        public bool SceneDesignMode
        {
            get { return scene.DesignMode; }
            set { scene.DesignMode = value; }
        }
		[Description("What operation is the mouse performing"), Category("SharpGL")]
		public MouseOperation Mouse
		{
			get {return mouseOperation;}
			set {mouseOperation = value;}
		}
        [Description("Selection hits"), Category("SharpGL")]
		public InteractableCollection Hits
		{
			get {return hits;}
			set {hits = value;}
		}
        [Description("Allows you to select in any mode."), Category("SharpGL")]
		public bool AutoSelect
		{
			get {return autoSelect;}
			set {autoSelect = value;}
		}
        [Description("The mouse will change to a hand when over an object if this is on."), Category("SharpGL")]
		public bool ShowHandOnHover
		{
			get {return mouseHover;}
			set {mouseHover = value;}
		}

		#endregion
	}
}
