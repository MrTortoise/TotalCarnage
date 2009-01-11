using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// takes take of the updates and drawing of a set of tiles
    /// Each layer draws in its own spritebatch ... this is to allow transformations on entire layers
    /// </summary>
    public class TileLayer //: IGameUpdateable , IGameDrawable, IEquatable<TileLayer>
    {

        #region Fields
        protected int mID;
        protected int[,] mLayout;
      //  protected DetailedTileList mDetailedTiles;
        protected List<Tile> mTiles;
                
        protected int mTileDims;
        // TODO implement only drawing visible areas (using a max view radius - just compare x and y ranges)
      //  protected int mDiagDims;
        protected float mLayerDepth;
        #endregion

        #region Constructors

        //public TileLayer(int ID,int tileDims, int[,] Layout,float LayerDepth, DetailedTileList DetailTiles)
        public TileLayer(int theID, int theTileDims, int[,] theLayout,  List<Tile> theTiles)
        {
            if ((object)theTiles == null)
            { throw new NullReferenceException("Tried to create a tile layer from a null tile list"); }

        //    double temp = theTileDims * theTileDims;
            
         //   mDiagDims= (int)Math.Ceiling((Math.Sqrt(temp+temp)));
            mID = ID;
            mLayout = theLayout;
            mTiles = theTiles; ;
            mTileDims = theTileDims;
        }
        #endregion
        #region Properties
        /// <summary>
        /// returns the Id of the TileLayer
        /// </summary>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// returns the array representing the ID's of the detailedTiles
        /// </summary>
        public int[,] Layout
        { get { return mLayout; } }

        
        /// <summary>
        /// Set by the tileLayers as new tiles are added
        /// </summary>
          public float LayerDepth
             {
                 get { return mLayerDepth; }
                 set { mLayerDepth = value; }
             }

        /// <summary>
        /// The tile dimensions in pixels (assumption is square)
        /// </summary>
          public int TileDims
          { get { return mTileDims; } }

        /// <summary>
        /// the number of tiles horizontally in the layer
        /// </summary>
          public int NoTilesAccross
          {get{return mLayout.GetLength(0);}}

        /// <summary>
        /// no of tiles vertically in a layer
        /// </summary>
          public int NoTilesVertical
          { get { return mLayout.GetLength(1); } }

        /// <summary>
        /// total width in pixels of the tile layer
        /// </summary>
          public int WidthInPixels
          {
              get { return NoTilesAccross * mTileDims; }
          }

        /// <summary>
        /// total height in pixels of the tile layer
        /// </summary>
          public int HeightInPixels
          { get { return NoTilesVertical * mTileDims; } }
         
        #endregion
        #region Public Methods
        /// <summary>
        /// Updates each of the detailed tiles int he list
        /// </summary>
        /// <param name="theTime"></param>
        public void Update(GameTime theTime)
        {
            // TODO Add some cunning mechanism to allow the tiles to 'move'
            // really it would just displace the array by a vector, 
            // remake the array with tiles in different places when displaced further than tieSize


        }

        /// <summary>
        /// Starts its own spritebatch and draws each of the detailed tiles in position
        /// </summary>
        /// <param name="theSpriteBatch"></param>
        /// <param name="theGameTime"></param>
        public void Draw(DrawingArgs theDrawingArgs)
        {
            // TODO Implement soureRectangle in detailed texture to allow for sprite sheet
            Vector2 position;

            SpriteBatch theBatch = new SpriteBatch(theDrawingArgs.GraphicsDevice  );
            theBatch.Begin(SpriteBlendMode.AlphaBlend,SpriteSortMode.Texture,SaveStateMode.None,theDrawingArgs.Camera.Transform );
           // theBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Texture, SaveStateMode.None);
          //  theBatch.Begin();
            for (int x = 0; x < mLayout.GetLength(0); x++)
            {
                for (int y = 0; y < mLayout.GetLength(1); y++)
                {
                    position = new Vector2(x * mTileDims, y * mTileDims);
                    spriteBatchArgs sb = new spriteBatchArgs(theBatch);
                    sb.Position = position;
                    sb.Rotation = 0;
                    sb.LayerDepth = 1;
                    mTiles[mLayout[x, y]].Draw(sb);
                }

            }
            theBatch.End();
            //theSpriteBatch.Draw(mTexture, mPosition, null, Color.White, mRotation, mOrigin, mScale, SpriteEffects.None, 0.5F);
        }
   /*     #region IGameUpdateable Members

        public void Update(UpdateArgs theUpdateArgs)
        {
            foreach (Tile t in mTiles)
            {
                t.Update(theUpdateArgs);
            }
        }

        #endregion*/


        #endregion

        #region IEquatable<TileLayer> Members

        public bool Equals(TileLayer other)
        {
            if ((object)other == null)
            { return false; }

            if (
                (mID == other.mID)
                && (mLayout.Equals(other.mLayout))
                && (mTiles.Equals(other.mTiles))
                && (mTileDims == other.mTileDims)
                && (mLayerDepth == other.mLayerDepth)
                )
            {
                return true;
            }
            else { return false; }
        }        

        #endregion
    }
}
