using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects.VectorDrawing
{
	/// <summary>
	/// This class provides basic 2D Vector Drawing Methods
	/// <para>Intended to form the basis of a UI rendering system</para>
	/// <para>The Graphics Device Must be set before any drawing is done. It is up to the user of this class to set it up and moreover to reset it when the graphicsDeice resets</para>
	/// <para>Load must also be called to load in the pixel texture. It can be set to any initialised texture2D via the property</para>
	/// </summary>
	public static  class VectorDraw
	{
		public static Texture2D Pixel;
		public static GraphicsDevice GraphicsDevice;

		/*public VectorDraw(GraphicsDevice theGraphicsDevice)
		{
			GraphicsDevice = theGraphicsDevice;
			Pixel = Texture2D.FromFile(theGraphicsDevice, "images\\pixel.png"); 
			 		  

		}  */

		public static void SetGraphicsDevice(GraphicsDevice thegraphicsDevice)
		{
			GraphicsDevice = thegraphicsDevice;
		}

		public static void Load()
		{
			Pixel = Texture2D.FromFile(GraphicsDevice, "images\\pixel.png"); 
		}

		/*public Texture2D Texture
		{
			get { return Pixel; }
			set { Pixel = value; }
		}  */


		#region Methods
		public static void DrawRectangleEdge(Vector2 thePosition, Vector2 theDimensions, Color theColor, uint theThickness, spriteBatchArgs theArgs)
		{
			Vector2 theVector = new Vector2();
			float length = theDimensions.X;					 
			float height = theDimensions.Y;
			Vector2 pos = thePosition;
			theVector.X=0;
			theVector.Y=height;
			
			//need to draw all 4 sides
			// start with 2 vertical
			DrawLine(pos, theVector, theColor, theArgs);			
			pos.X = pos.X + length;
			DrawLine(pos, theVector, theColor, theArgs);
			// then the horixontal
			pos = thePosition;
			theVector.X = length;
			theVector.Y = 0;
			DrawLine(pos, theVector, theColor, theArgs);
			pos.Y = pos.Y + height;
			DrawLine(pos, theVector, theColor, theArgs);

		}

		public static void DrawRectangleFilled(Vector2 thePosition, Vector2 theDimensions, Color theColor, spriteBatchArgs theArgs)
		{
			theArgs.SpriteBatch.Draw(Pixel, thePosition, null, theColor, 0, Vector2.Zero, theDimensions, SpriteEffects.None, theArgs.LayerDepth);

		}

		public static void DrawLine(Vector2 thePosition, Vector2 theVector, Color theColor, spriteBatchArgs theArgs)
		{
			Vector2 scale = new Vector2();
			SpriteBatch sb = theArgs.SpriteBatch;

			float length = theVector.Length();
			scale.X=length;
			scale.Y=1;
			float rotation = (float)Math.Atan2(theVector.Y,theVector.X);
			sb.Draw(Pixel, thePosition, null, theColor, rotation, Vector2.Zero, scale, SpriteEffects.None, theArgs.LayerDepth);


		}


		#endregion
	}
}
