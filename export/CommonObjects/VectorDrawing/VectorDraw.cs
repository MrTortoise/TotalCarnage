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
	class VectorDraw
	{
		protected Texture2D mPixel;
		protected List<Vector2> mVectorList;

		public Texture2D Texture
		{
			get { return mPixel; }
			set { mPixel = value; }
		}


		#region Methods
		public void DrawRectangleEdge(Vector2 thePosition, Vector2 theDimensions, Color theColor, uint theThickness, DrawingArgs theArgs)
		{
			SpriteBatch sb = theArgs.SpriteBatch;					 
		}

		public void DrawLine(Vector2 thePosition, Vector2 theVector, Color theColor, spriteBatchArgs theArgs)
		{
			SpriteBatch sb = theArgs.SpriteBatch;

			float length = theVector.Length();
			Vector2 scale = new Vector2(length, 1); // makes the line as long as it needs to be
			float rotation = (float)Math.Atan2(theVector.Y,theVector.X);
			sb.Draw(mPixel, thePosition, null, theColor, rotation, new Vector2(0, 0),scale, SpriteEffects.None, theArgs.LayerDepth);


		}

		public void Load(GraphicsDevice theGD)
		{
			mPixel = Texture2D.FromFile(theGD, "images\\pixel.png");
		}

		#endregion
	}
}
