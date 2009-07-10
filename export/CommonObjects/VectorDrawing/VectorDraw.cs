using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CommonObjects.Graphics; 

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


		public static void Load(string pixelImagePath)
		{
			Pixel = Texture2D.FromFile(GraphicDeviceSingleton.GetInstance().graphicsDevice , pixelImagePath); 
		}

		#region Methods
		public static void DrawRectangleEdge(Vector2 thePosition, Vector2 theDimensions, Color theColor, uint theThickness, SpriteBatch  theSpriteBatch, float LayerDepth)
		{
			Vector2 theVector = new Vector2();
			float length = theDimensions.X;					 
			float height = theDimensions.Y;
			Vector2 pos = thePosition;
			theVector.X=0;
			theVector.Y=height;
			
			//need to draw all 4 sides
			// start with 2 vertical
			DrawLine(pos, theVector, theColor, theSpriteBatch,LayerDepth );			
			pos.X = pos.X + length;
			DrawLine(pos, theVector, theColor, theSpriteBatch,LayerDepth );
			// then the horixontal
			pos = thePosition;
			theVector.X = length;
			theVector.Y = 0;
			DrawLine(pos, theVector, theColor, theSpriteBatch,LayerDepth );
			pos.Y = pos.Y + height;
			DrawLine(pos, theVector, theColor, theSpriteBatch,LayerDepth );

		}

		public static void DrawRectangleFilled(Vector2 thePosition, Vector2 theDimensions, Color theColor, SpriteBatch  theSpriteBatch, float LayerDepth)
		{
			theSpriteBatch.Draw(Pixel, thePosition, null, theColor, 0, Vector2.Zero, theDimensions, SpriteEffects.None, LayerDepth);

		}

		public static void DrawLine(Vector2 thePosition, Vector2 theVector, Color theColor, SpriteBatch theSpriteBatch, float LayerDepth)
		{
			Vector2 scale = new Vector2();
			

			float length = theVector.Length();
			scale.X=length;
			scale.Y=1;
			float rotation = (float)Math.Atan2(theVector.Y,theVector.X);
			theSpriteBatch.Draw(Pixel, thePosition, null, theColor, rotation, Vector2.Zero, scale, SpriteEffects.None, LayerDepth);


		}


		#endregion
	}
}
