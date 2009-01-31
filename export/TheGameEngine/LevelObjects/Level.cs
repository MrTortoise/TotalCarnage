using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using CommonObjects;

namespace TheGameEngine
{
    public class Level 
    {
        #region Fields
        public   TextureList mTextures;
       // public  TileSet mTiles;        
        protected int[,] mLevel;
        public int width;
        protected int mTotalWidth;
        protected int mTotalHeight;

        #endregion
        #region Constructor
        public Level(ContentManager theContentManager, int width, int height)
        {
            mTiles = new TileSet();
            mLevel = new int[width, height];
            LoadContent(theContentManager);
            mTotalWidth = mTiles.mTileDims * mLevel.GetLength(0);
            mTotalHeight= mTiles.mTileDims * mLevel.GetLength(1);    
        }

        public Level(ContentManager theContentManager, int[,] theMap)
        {
            mTiles = new TileSet();
            mLevel = theMap;
            LoadContent(theContentManager);                   
            mTotalWidth = mTiles.mTileDims * mLevel.GetLength(0);
            mTotalHeight= mTiles.mTileDims * mLevel.GetLength(1); 
        }

        public int[,] map
        { get { return mLevel; } }

        public int totalWidth
        { get { return mTotalWidth; } }

        public int totalHeight
        { get { return mTotalHeight; } }


        #endregion

        #region IGameDrawable Members

        public void LoadContent(ContentManager theContentManager)
        {
            eTexture[] theTexture = { eTexture.dirt, eTexture.grass, eTexture.ground, eTexture.mud, eTexture.road, 
                              eTexture.rock, eTexture.wood };
            mTextures = new TextureList(theContentManager);
            mTextures.BulkImport(theTexture);

            Tile[] theTile;
            theTile = new Tile[7];            
/*
           CommonObjects.TerrainType mNormalTerrain = new TerrainType("normal", true, 1f);
           CommonObjects.TerrainType mImpassableTerrain = new TerrainType("Impassable", false, 0f);

            theTile[(int)eTexture.dirt] = new Tile(mTiles.mTileDims ,mTextures, (int)Texture.dirt, theContentManager, 1, mNormalTerrain);
            theTile[(int)eTexture.grass] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.grass, theContentManager, 1, mNormalTerrain);
            theTile[(int)eTexture.ground] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.ground, theContentManager, 1, mNormalTerrain);
            theTile[(int)eTexture.mud] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.mud, theContentManager, 1, mNormalTerrain);
            theTile[(int)eTexture.road] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.road, theContentManager, 1, mNormalTerrain);
            theTile[(int)eTexture.rock] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.rock, theContentManager, 1, mImpassableTerrain);
            theTile[(int)eTexture.wood] = new Tile(mTiles.mTileDims, mTextures, (int)Texture.wood, theContentManager, 1, mImpassableTerrain);
*/
            mTiles.BulkInsert(theTile);
         

        }

        public void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime)
        {
            for (int x = 0;x<mLevel.GetLength(0);x++)
            {
                for (int y = 0;y<mLevel.GetLength(1);y++)
                {
                    mTiles.Draw(mLevel[x,y],theSpriteBatch,theGameTime,new Vector2((float)x,(float)y));
                }
            }
        }



        #endregion


    }
}
