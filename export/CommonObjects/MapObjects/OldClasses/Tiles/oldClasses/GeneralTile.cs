using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects
{
    /// <summary>
    /// This holds texture and terrain info for a tile on a map
    /// </summary>
    public class GeneralTile
    {

        #region Fields
        protected int mID;
        protected string mName;
        protected DetailedTexture mTexture;
        protected TerrainType mTerrainType;
        protected float mScale;

        #endregion

        #region Constructor
        public GeneralTile(int ID, string Name, DetailedTexture theDetailedTexture, TerrainType theTerrainType)
        {
            mID = ID;
            mName = Name;
            mTexture = theDetailedTexture;
            mTerrainType = theTerrainType;
        }

        #endregion

        #region properties
        /// <summary>
        /// returns the Id of the general Tile
        /// </summary>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// Returns the Name of the General Tile
        /// </summary>
        public string Name
        { get { return mName; } }

        /// <summary>
        /// Returns the DetailedTexture for the General Tile
        /// </summary>
        public DetailedTexture DetailTexture
        { get { return mTexture; } }

        /// <summary>
        /// returns the Terrain Type for the Detailed Tile
        /// </summary>
        public TerrainType TerrainType
        { get { return mTerrainType; } }
        #endregion

        #region public Methods
        /// <summary>
        /// Returns the scaleing factor of the texture int he tile as a float
        /// </summary>
        /// <param name="width"></param>
        /// <returns>the scaling factor of the tiles texture as a float</returns>
        public float Scale()
        {
            // ToDo: this shuold be part of the detailed tile.
            return mScale;
        }

        public void SetScale(int width)
        {
            mScale = (float)width / (float)mTexture.Texture.Texture.Width;
        }

        /// <summary>
        /// returns the name of the tile
        /// </summary>
        /// <returns>the General Tiles name</returns>
        public override string ToString()
        {
            return Name;
        }

        public void Load(GraphicsDevice theGraphicsDevice)
        {
            mTexture.Load(theGraphicsDevice);           
        }

        #endregion
    }
}
