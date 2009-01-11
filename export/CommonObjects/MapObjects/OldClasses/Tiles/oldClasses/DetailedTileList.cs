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
    /// Holds a list of detailed tiles that all TileLayers refer to
    /// </summary>
    public class DetailedTileList
    {
        #region Fields
        // protected int mID;
       // protected string mName;        

        protected List<DetailedTile> mDetailedTiles;

        #endregion
        #region Constructor

        public DetailedTileList()
        {
        //    mID = ID;
        //    mName = Name;
            mDetailedTiles = new List<DetailedTile>();
        }

        #endregion
        #region indexers

        /// <summary>
        /// return a detailed tile at a given index
        /// will throw an error if out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns>a detailedTile</returns>
        public DetailedTile this[int index]
        { get { return mDetailedTiles[index]; } }

        #endregion

        #region Public Methods
        /// <summary>
        /// return a detailed tile at a given index
        /// will throw an error if out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns>a detailedTile</returns>
        public DetailedTile Get(int index)
        { return mDetailedTiles[index]; }

        /// <summary>
        /// Adds a DetailedTile to the end of the list
        /// It must have an ID = index it will occupy
        /// </summary>
        /// <param name="theDetailedTile"></param>
        public void Add(DetailedTile theDetailedTile)
        {
            if (mDetailedTiles.Count == theDetailedTile.ID)
            {
                mDetailedTiles.Add(theDetailedTile);
                
            }
            else
            {
                throw new Exception("Tried to add detail tile with bad index");
            }
        }

        /// <summary>
        /// returns the total number of general tiles
        /// </summary>
        /// <returns>total general tiles</returns>
        public int Count()
        {
            return mDetailedTiles.Count;
        }

        

        /// <summary>
        /// Updates each of the tiles int he list
        /// </summary>
        /// <param name="theTime"></param>
        public void Update(GameTime theTime)
        {
            foreach (DetailedTile dt in mDetailedTiles)
            {
                dt.Update(theTime );
            }
        }

        public void SetScale(int width)
        {
            foreach (DetailedTile dt in mDetailedTiles)
            {
                dt.Get().SetScale(width);
            }
        }

        #endregion        
        
    }
}

/*   public int ID
{ get { return mID; } }

public string Name
{ get { return mName; } }*/