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
	/// </summary>
	public class VectorDraw
	{
		protected Texture2D mPixel;
		protected List<Vector2> mVectorList;
		protected GraphicsDevice mGraphicsDevice;
		protected Vector2 mScale = Vector2.One;
		protected Vector2 theVector = Vector2.Zero ;

		public VectorDraw(GraphicsDevice theGraphicsDevice)
		{
			mGraphicsDevice = theGraphicsDevice;
			mPixel = Texture2D.FromFile(theGraphicsDevice, "images\\pixel.png");
  		  

		}

		public Texture2D Texture
		{
			get { return mPixel; }
			set { mPixel = value; }
		}


		#region Methods
		public void DrawRectangleEdge(Vector2 thePosition, Vector2 theDimensions, Color theColor, uint theThickness, spriteBatchArgs theArgs)
		{
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

		public void DrawRectangleFilled(Vector2 thePosition, Vector2 theDimensions, Color theColor, spriteBatchArgs theArgs)
		{
			theArgs.SpriteBatch.Draw(mPixel, thePosition, null, theColor, 0, Vector2.Zero, theDimensions, SpriteEffects.None, theArgs.LayerDepth);

		}

		public void DrawLine(Vector2 thePosition, Vector2 theVector, Color theColor, spriteBatchArgs theArgs)
		{
			SpriteBatch sb = theArgs.SpriteBatch;

			float length = theVector.Length();
			mScale.X=length;
			mScale.Y=1;
			float rotation = (float)Math.Atan2(theVector.Y,theVector.X);
			sb.Draw(mPixel, thePosition, null, theColor, rotation, Vector2.Zero, mScale, SpriteEffects.None, theArgs.LayerDepth);


		}


		#endregion
	}
}
