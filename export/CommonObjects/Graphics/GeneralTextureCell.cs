using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace CommonObjects.Graphics
{
	/// <summary>
	/// represents a view into a texture that has various drawing options.
	/// Currently Normal and Tiled. Contains a reference to a general Texture.
    /// Allows a region to be specified in a texture as input. It outputs that region at a specifed scale and rotation.
	/// </summary>
	public class GeneralTextureCell 
	{
		//ToDo: Implement IDisposable to manage the references to Generaltexture
		//ToDo: this object does not require a GeneralTexture, use Texture2D and IGameLoadable, IAgroGarbageCollection

		protected int mID;
		protected GeneralTexture mTexture;
		protected Vector2 mPosition;
		protected Vector2 mTextureSize;
		protected Vector2 mTextureCellSize;
		protected float mRotation;
		//protected float mScale;

        /// <summary>
        /// Constructs a General TextureCell
        /// </summary>
        /// <param name="ID">The unique Id of the GeneralTextureCell</param>
        /// <param name="theTexture">The generalTexture the TextureCell will Reference</param>
        /// <param name="thePosition">The x,y coords of the origin in the texture</param>
        /// <param name="theTextureCellSize">The output size of the TextureCell (ie its width and height)</param>
        /// <param name="theTextureSize">The suze of the region to use in the unput texture</param>
        /// <param name="Rotation">The rotation to apply to the OUTPUT.</param>
		public  GeneralTextureCell(int ID, GeneralTexture theTexture, Vector2 thePosition,Vector2 theTextureCellSize, Vector2 theTextureSize, float Rotation)//, float scale)
		{
            //ToDo remove Scale
			mID = ID;
			mTexture = theTexture;
			mPosition = thePosition;
			mTextureSize = theTextureSize;
			mRotation = Rotation;
		//	mScale = scale;
			mTextureCellSize=theTextureCellSize;
		}

		public int ID
		{
			get { return mID; }		
		}

        /// <summary>
        /// The position of the origion to use in the input GeneralTexture
        /// </summary>
		public Vector2 Position
		{ get { return mPosition; } }

        /// <summary>
        /// The size of the area to use in the General Texture
        /// </summary>
		public Vector2 TextureSize
		{ get { return mTextureSize; } }

		/// <summary>
		/// Gets a REctangle cossresponding to the parts of the general texture to be drawn
		/// </summary>
		public Rectangle TextureRectangle
		{
			get
			{
				return new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)mTextureSize.X+(int)mPosition.X , (int)mPosition.Y+(int)mTextureSize.Y );
			}
		}

        /// <summary>
        /// Draws a single TextureCell.
        /// </summary>
        /// <param name="theSpriteBatch">The spritebatch to draw the TextueCell With.</param>
        /// <param name="thePosition">The position in drawing space to draw the TextureCell.</param>
        /// <param name="theLayerDepth">The Depth at which to position the texturecell in the z buffer</param>
		public void DrawAsIs(SpriteBatch theSpriteBatch, Vector2 thePosition, float theLayerDepth)
		{
			Rectangle r = new Rectangle((int)thePosition.X, (int)thePosition.Y, (int)mTextureCellSize.X, (int)mTextureCellSize.Y);
			theSpriteBatch.Draw(mTexture.Texture, r, TextureRectangle, Color.White, 0, Vector2.Zero,  SpriteEffects.None, theLayerDepth);
		}

        public void DrawStretched(SpriteBatch theSpriteBatch, Rectangle target, float theLayerDepth)
        {   
            
            theSpriteBatch.Draw(mTexture.Texture, target, TextureRectangle, Color.White, 0, Vector2.Zero, SpriteEffects.None, theLayerDepth);
        }

        /// <summary>
        /// This method takes the TextureCell as specified but then tiles it to the output area.
        /// </summary>
        /// <param name="theSpriteBatch">The spritebatch to draw the TextueCell With.</param>
        /// <param name="target">The target rectangle that the texture cell will be tiled into</param>
        /// <param name="theLayerDepth">The Depth at which to position the texturecell in the z buffer</param>
		public void DrawTiled(SpriteBatch theSpriteBatch, Rectangle target,float theLayerDepth)
		{
			int NoAccross = (int)(target.Width/mTextureSize.X);
			int NoDown = (int)(target.Height/mTextureSize.Y);
			int absMaxWidth = (int)target.X+((int)NoAccross*(int)mTextureSize.X);
			int absMaxHeight = (int)target.Y+((int)NoDown*(int)mTextureSize.Y);
			Vector2 position = new Vector2();
			
			int remX = (int)((NoAccross - (int)NoAccross)*mTextureSize.X);
			int remY = (int)((NoDown - (int)NoDown)*mTextureSize.Y );    			

			if (NoAccross>0)
			{
				if (NoDown>0)
				{
					//loop the whole tiles
					for (int i = 0;i<NoAccross ;i++)
					{
						for (int j=0;j<NoDown;j++)
						{
							position.X=target.X+(i*mTextureSize.X);
							position.Y=target.Y+(j*mTextureSize.Y);

							theSpriteBatch.Draw(mTexture.Texture,position,TextureRectangle,Color.White,0,Vector2.Zero,1,SpriteEffects.None,theLayerDepth);
							//dont increment no drawn as part of same layer
							//ToDo finish GENERALTEXTURECELL
						}
					}

					//loop through the right side tiles
					position.X=absMaxWidth;
					position.Y=target.Y;
					DrawRightSide(position, NoDown, remX, theSpriteBatch);			

					//loop through the bottom tiles
					position.X = target.X;
					position.Y = absMaxHeight;
					DrawBottom(position, NoAccross, remY, theSpriteBatch);
					//ToDo: does remainder have to be multiplied out for true value?


					//draw bottom right
					
					position.X = absMaxWidth;
					position.Y = absMaxHeight;

					DrawBottomRight(position, remX, remY, theSpriteBatch);		


				}
				else
				{
					// none down many accross
					// find rectangle that fits and loop horizontally
					position.X = target.X;
					position.Y = target.Y;
					DrawBottom(position, NoAccross, remY, theSpriteBatch);
					if (NoAccross > 1)
					{
						// Draw Bottom Right
						position.X = absMaxWidth;
						position.Y = target.Y;
						DrawBottomRight(position, remX, remY, theSpriteBatch);
					} 
				}										
			}
			else
			{  				
				if (NoDown>0)
				{
					// draw a vertical column < 1 cel wide
					position.X = target.X;
					position.Y = target.Y;
					DrawRightSide(position, NoDown, remX, theSpriteBatch);
					if (NoDown > 1)
					{
						//draw bottom right
						position.X = target.X;
						position.Y = absMaxHeight;
						DrawBottomRight(position, remX, remY, theSpriteBatch);
					}															  				

				}
				else
				{
					// a bottom right situation
					position.X = target.X;
					position.Y = target.Y;
					DrawBottomRight(position, remX, remY, theSpriteBatch);
				}
			}

		}

		protected void DrawBottomRight(Vector2 target, int width, int height, SpriteBatch theSpriteBatch)
		{
			theSpriteBatch.Draw(mTexture.Texture, target, new Rectangle((int)mPosition.X, (int)mPosition.Y, width, height), Color.White);
		}
		protected void DrawBottom(Vector2 target, int NoAccross, int Height, SpriteBatch theSpriteBatch)
		{
								//loop through the bottom tiles
			Vector2 position = new Vector2();
			Rectangle sourceRectangle = new Rectangle();

			for (int i = 0; i < NoAccross; i++)
			{
				position.X = target.X + (i * mTextureSize.X);
				position.Y = target.Y;

				sourceRectangle.X = (int)mPosition.X;
				sourceRectangle.Y = (int)mPosition.Y;
				sourceRectangle.Width = (int)mTextureSize.X;
				sourceRectangle.Height = Height;

				theSpriteBatch.Draw(mTexture.Texture, position, sourceRectangle, Color.White);
			}	   
		}
		protected void DrawRightSide(Vector2 target, int NoDown, int width, SpriteBatch theSpriteBatch)
		{
			Vector2 position = new Vector2();
			Rectangle sourceRectangle = new Rectangle();
			for (int j = 0; j < NoDown; j++)				
			{
				position.X = target.X;
				position.Y = target.Y + (j * mTextureSize.Y);

				sourceRectangle.X = (int)mPosition.X;
				sourceRectangle.Y = (int)mPosition.Y;
				sourceRectangle.Width = width;
				sourceRectangle.Height = (int)mTextureSize.Y;

				theSpriteBatch.Draw(mTexture.Texture, position, sourceRectangle, Color.White);
			}

		}


	}
}
