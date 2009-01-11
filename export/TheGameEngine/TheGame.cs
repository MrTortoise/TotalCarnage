using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using CommonObjects;

namespace TheGameEngine
{
    public class TheGame 
    {
        #region Fields

        protected GraphicsDevice mGraphicsDevice;
        protected ContentManager mContentManager;
        //protected SpriteFont gameFont;
        protected SpriteBatch mSpriteBatch;

        protected Level mLevel;
        protected BasicPlayer mPlayer;
        protected Camera mCamera;

        #endregion

        #region Constructor
        public TheGame(GraphicsDevice theGraphicsDevice)
        {
            mGraphicsDevice = theGraphicsDevice;                      
            }

        #endregion

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            int[,] temp = new int[,]
		{
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, },
			{ 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 6, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 6, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 6, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 4, 0, 0, 0, 0, 0, 0, 0, 0, 6, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, },
			{ 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 6, 4, 0, 0, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 0, 0, 0, 4, 0, 0, 0, 0, },
			{ 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 6, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 6, 6, 6, 6, 6, 4, 5, 5, 5, 0, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 6, 0, 0, 4, 5, 0, 5, 0, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 6, 1, 1, 4, 5, 1, 5, 1, },
			{ 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 6, 1, 1, 4, 5, 5, 5, 1, },
			{ 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 6, 0, 0, 4, 5, 0, 0, 0, },
			{ 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 6, 6, 6, 1, 1, 4, 5, 1, 1, 1, },
			{ 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, },
		};

            mLevel = new Level(mContentManager, temp);
            mPlayer = new BasicPlayer(mContentManager);
            
            SubscribeToMoveRequests(mPlayer);
            
            object_RequestMove(mPlayer,new MovementEventArgs(new Vector2(0f,0f),mLevel.mTiles[mLevel.map[0,0]].TileTerrainType,eDirection.right,10));
            mCamera = new Camera(mPlayer.Position);
            // need to sign up to the players events

        }

        public void UnloadContent()
        {
            mContentManager.Unload();
        }

        public void Update(GameTime gameTime)
        {
 
        }

        public void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (input.PauseGame)
            {
                // If they pressed pause, bring up the pause menu screen.
               
            }
            else
            {
                mPlayer.ProcessKeys(input);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch theSpriteBatch)
        {
            mCamera.SetPosition(new Vector2((float)((mGraphicsDevice.Viewport.Width/2)+mPlayer.Position.X) ,(float)((mGraphicsDevice.Viewport.Height/2)+mPlayer.Position.Y) ));

            mSpriteBatch = theSpriteBatch;

            mSpriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Deferred, SaveStateMode.SaveState, mCamera.Transform);

            mLevel.Draw(mSpriteBatch, gameTime);
            
            mPlayer.Draw(mSpriteBatch, gameTime);

            mSpriteBatch.End();

            // If the game is transitioning on or off, fade it out to black.
            
        }

        public void SubscribeToMoveRequests(Player  thePlayer)
        {
            thePlayer.RequestMove += new EventHandler<MovementEventArgs>(object_RequestMove);

        }

        void object_RequestMove(object theActor, MovementEventArgs e)
        {
            
            // calculate new position and then look at its properties
            bool outOfBounds = false;
            bool mPassable = true;                                      //can the tile be moved through
            float mTileModifier = e.Terrain.MoveModifier;               //th tile may alter the movement (eg slow / speed it up)
            int mDistance = (int)(mTileModifier * e.Distance);          // apply the modifier to find actual dist moved            
            Vector2 newPos = new Vector2(e.position.X,e.position.Y);    // final position after the move 
                                                                        // vals are returned ... 
                                                                        // object then has chance to modify according to its properties
            int x,y;
            int gridX, gridY;
            
            x = y = 0;
            gridX = gridY = 0;

            if (e.Direction == eDirection.up)
            {
                newPos.Y = newPos.Y - mDistance;
                x = (int)newPos.X;
                y = (int)newPos.Y;               

                // need to check if in bounds of the array

                if (y <= 0)
                {
                    mDistance = mDistance + y;
                    y = (int)mPlayer.height / 2;
                    outOfBounds = true;
                }

                gridX = (int)(x / mLevel.mTiles.mTileDims);
                gridY = (int)(y / mLevel.mTiles.mTileDims);
                // if new position is not passable then need to figure out how far away it is
                if (outOfBounds == false)
                {
                    if (mLevel.mTiles[mLevel.map[gridX, gridY]].TileTerrainType.Passable == false)
                    {
                        mDistance = -(int)(e.position.Y / (float)mLevel.mTiles.mTileDims);
                        newPos.Y = e.position.Y + mDistance;
                        mPassable = false;
                        //maxDist=(Int16)mDistance;
                    }
                }
            }
            else if (e.Direction == eDirection.down)
            {
                newPos.Y = newPos.Y + mDistance;
                x = (int)newPos.X;
                y = (int)newPos.Y;                

                // need to check if in bounds of the array

                if (y >= mLevel.totalHeight  )
                {
                    y = (mLevel.totalHeight ) - (mPlayer.height / 2);
                    mDistance = y - (int)e.position.Y;
                    outOfBounds = true;
                }

                gridX = (int)(x / mLevel.mTiles.mTileDims);
                gridY = (int)(y / mLevel.mTiles.mTileDims);
                // if new position is not passable then need to figure out how far away it is
                if (outOfBounds == false)
                {
                    if (mLevel.mTiles[mLevel.map[gridX, gridY]].TileTerrainType.Passable == false)
                    {
                        mDistance = mLevel.mTiles.mTileDims - (int)(e.position.Y / (float)mLevel.mTiles.mTileDims);
                        newPos.Y = e.position.Y + mDistance;
                        mPassable = false;
                        // maxDist = (Int16)mDistance;
                    }
                }
            }
            else if (e.Direction == eDirection.Left)
            {
                newPos.X = newPos.X - mDistance;
                x = (int)newPos.X;
                y = (int)newPos.Y;                

                // need to check if in bounds of the array
                if (x <= 0)
                {
                    mDistance = mDistance + x;
                    x = (int)mPlayer.width / 2;
                    outOfBounds = true;
                }


                gridX = (int)(x / mLevel.mTiles.mTileDims);
                gridY = (int)(y / mLevel.mTiles.mTileDims);

                // if new position is not passable then need to figure out how far away it is
                if (outOfBounds == false)
                {
                    if (mLevel.mTiles[mLevel.map[gridX, gridY]].TileTerrainType.Passable == false)
                    {
                        mDistance = -(int)(e.position.X / (float)mLevel.mTiles.mTileDims);
                        newPos.X = e.position.X + mDistance;
                        mPassable = false;
                        // maxDist = (Int16)mDistance;
                    }
                }
            }
            else if (e.Direction==eDirection.right)
            {
                newPos.X=newPos.X +(float)mDistance;
                x = (int)newPos.X;
                y = (int)newPos.Y;                

                // need to check if in bounds of the array

                if (x >= mLevel.totalWidth )
                {
                    x = (mLevel.totalWidth)-(mPlayer.width/2);
                    mDistance = x - (int)e.position.X;
                    outOfBounds = true;
                }

                gridX = (int)(x / mLevel.mTiles.mTileDims);
                gridY = (int)(y / mLevel.mTiles.mTileDims);
                
                // if new position is not passable then need to figure out how far away it is
                if (outOfBounds == false)
                {
                    if (mLevel.mTiles[mLevel.map[gridX, gridY]].TileTerrainType.Passable == false)
                    {
                        mDistance = mLevel.mTiles.mTileDims - (int)(e.position.X / (float)mLevel.mTiles.mTileDims);
                        newPos.X = e.position.X + mDistance;
                        mPassable = false;
                        // maxDist = (Int16)mDistance;
                    }
                }
            }
            newPos.X = (float)x;
            newPos.Y = (float)y;

            Player temp = (Player)theActor;
            temp.move(new MoveArgs(newPos, mPassable, 1, e.Direction, mLevel.mTiles[mLevel.map[gridX, gridY]].TileTerrainType, (Int16)mDistance,outOfBounds ));

           
            
            
        }





        
    }
}
