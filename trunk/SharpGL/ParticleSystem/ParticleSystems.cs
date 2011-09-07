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


//	This is fairly new code and is immature. The new revision will have custom particles
//	such as water drops and sparks, and more developed systems, and the system will
//	be able to generate particles of different types.

//	A few words are needed to correctly describe this class. In a particle system, 
//	a set of particles will be made. Each particle will be initialised with a call
//	to Initialise(). Use this function to make each particle random. When the particle
//	needs to move on a step, Tick() is called. Make sure the code is random, otherwise
//	all particles will have the same behaviour.


using System;
using System.Collections;

using SharpGL.SceneGraph.Collections;

namespace SharpGL.SceneGraph.ParticleSystems
{
	/// <summary>
	/// A particle system is, you guessed it, just a collection of particles.
	/// </summary>
	[Serializable()]
	public class ParticleSystem : SceneObject
	{
		/// <summary>
		/// This function should create and initialise 'count' particles of the correct
		/// type. This is done automatically by default, only override if you want
		/// to change the standard behaviour.
		/// </summary>
		/// <param name="count"></param>
		public virtual void Initialise(int count)
		{
			//	Get rid of any old particles.
			particles.Clear();

			//	Add the particles.
			for(int i=0; i<count; i++)
			{
				//	Create a particle.
				Particle particle = new BasicParticle();

				//	Initialise it.
				particle.Intialise(rand);

				//	Add it.
				particles.Add(particle);
			}
		}

		/// <summary>
		/// This function draws the particle system. Override from it if you want
		/// to add custom drawing for particles.
		/// </summary>
		/// <param name="gl"></param>
		public override void Draw(OpenGL gl)
		{
			if(DoPreDraw(gl))
			{

				//	Disable lighting.
				SharpGL.SceneGraph.Attributes.Lighting light = new SharpGL.SceneGraph.Attributes.Lighting();
				light.Enable = false;
				light.Set(gl);

				foreach(Particle p in particles)
					p.Draw(gl);

				light.Restore(gl);

				DoPostDraw(gl);
			}
		}

        /// <summary>
        /// This function ticks the particle system.
        /// </summary>
		public virtual void Tick()
		{
			foreach(Particle p in particles)
			{
				//	Tick the particle.
				p.Tick(rand);

			}
		}

		protected Random rand = new Random();
		public ParticleCollection particles = new ParticleCollection();
		protected Type particleType =  typeof(ParticleSystems.BasicParticle);
	}
}
