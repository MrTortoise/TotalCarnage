using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;


namespace TheGameEngine
{
    public class Tile : ITileDrawable
    {
        

        #region Fields
        protected int mTileDims;
        protected TextureList mTextures;
        protected Texture mTextureEnum;                 
        protected float mLayerDepth;
        protected Vector2 mScale;
        protected Vector2 mOrigin = Vector2.Zero;
        

        //protected TileEnvironmentType mTileType;
        protected TerrainType mTerrainType;
        #endregion

        #region Properties

        public TerrainType TileTerrainType
        { get { return mTerrainType; } }
        #endregion

        #region Constructors
        public Tile(int theTileDims, TextureList theTextures, int theTextureIndex, ContentManager theContentManager, 
            int theDrawOrder, TerrainType  theTerrainType)
        {
            mTileDims = theTileDims;
            mTextures = theTextures;
            mTextureEnum = (eTexture)theTextureIndex;
            mTerrainType = theTerrainType;
                     
            mLayerDepth = 1f / (float)theDrawOrder;
            mScale.X = (float)mTileDims / (float)mTextures[(int)mTextureEnum].Width;
            mScale.Y = (float)mTileDims / (float)mTextures[(int)mTextureEnum].Height;            
        }
        #endregion

        #region ITileDrawable Members

        public void Draw(SpriteBatch theSpriteBatch, GameTime theGameTime, Vector2 thePosition)
        {
            thePosition.X = thePosition.X * mTileDims;
            thePosition.Y = thePosition.Y * mTileDims;
            theSpriteBatch.Draw(mTextures[(int)mTextureEnum], thePosition, null, Color.White, 0, mOrigin, mScale, SpriteEffects.None, mLayerDepth );
        }

        public int drawOrder
        {
            get{return (int)(1 / mLayerDepth);}
            set{mLayerDepth = 1f / (float)value;}
        }

        public event EventHandler DrawOrderChanged;
        protected virtual void onDrawOrderChanged(DrawOrderEventArgs e)
        {
            if (DrawOrderChanged != null)
            {
                DrawOrderChanged(this, e);
            }
        }

        #endregion

    }
}
