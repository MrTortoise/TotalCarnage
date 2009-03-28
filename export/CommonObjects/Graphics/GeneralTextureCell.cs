using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects
{
	class GeneralTextureCell 
	{

		protected int mID;
		protected GeneralTexture mTexture;
		protected Vector2 mPosition;
		protected Vector2 mTextureSize;

		public void GeneraltextureCell(int ID, GeneralTexture theTexture, Vector2 thePosition, Vector2 theTextureSize)
		{
			mID = ID;
			mTexture = theTexture;
			mPosition = thePosition;
			mTextureSize = theTextureSize;
		}

		public int ID
		{
			get { return mID; }		
		}

		public Vector2 Position
		{ get { return mPosition; } }

		public Vector2 TextureSize
		{ get { return mTextureSize; } }

		/// <summary>
		/// Gets a REctangle cossresponding to the parts of the general texture to be drawn
		/// </summary>
		public Rectangle TextureRectangle
		{
			get
			{
				return new Rectangle((int)mPosition.X, (int)mPosition.Y, (int)mTextureSize.X, (int)mTextureSize.Y);
			}
		}

		public void DrawAsIs(SpriteBatch theSpriteBatch, Vector2 thePosition, float theLayerDepth)
		{
			theSpriteBatch.Draw(mTexture.Texture, thePosition, TextureRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, theLayerDepth);
		}
		public void DrawTiled(SpriteBatch theSpriteBatch, Rectangle target,float theLayerDepth)
		{
			int NoAccross = (int)(target.Width/mTextureSize.X);
			int NoDown = (int)(target.Height/mTextureSize.Y);
			int absMaxWidth = (int)target.X+((int)NoAccross*(int)mTextureSize.X);
			int absMaxHeight = (int)target.Y+((int)NoDown*(int)mTextureSize.Y);
			Vector2 position = new Vector2();
			
			int remX = (int)((NoAccross - (int)NoAccross)*mTextureSize.X);
			int remY = (int)((NoDown - (int)NoDown)*mTextureSize.Y );
			Rectangle sourceRectangle = new Rectangle();

			if (NoAccross>0)
			{
				if (NoDown>0)
				{
					//loop the whole tiles
					for (int i = 0;i<NoAccross ;i++)
					{
						for (int j=0;j<NoDown;i++)
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
