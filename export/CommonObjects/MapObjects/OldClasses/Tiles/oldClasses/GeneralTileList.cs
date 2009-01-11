using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects
{
    /// <summary>
    /// A master list of all the general tiles in a given layer
    /// </summary>
    public class GeneralTileList
    {
      //  protected int mID;
      //  protected string mName;
        protected List<GeneralTile> mGeneralTiles;


       public GeneralTileList()
        {
            mGeneralTiles = new List<GeneralTile>();
        }
       /* 
        
        public int ID
        { get { return mID; } }

        public string Name
        { get { return mName; } }*/

        /// <summary>
        /// returns the general tile at a given index
        /// will throw an error f index out of range
        /// </summary>
        /// <param name="index"></param>
        /// <returns>general tile by int index</returns>
        public GeneralTile Get(int index)
        {
            return mGeneralTiles[index];
        }

        /// <summary>
        /// will return a tile of a given name
        /// returns null if not found
        /// </summary>
        /// <param name="Name"></param>
        /// <returns>general tile by name</returns>
        public GeneralTile Get(string Name)
        {
            GeneralTile retVal = null;
            foreach (GeneralTile gt in mGeneralTiles)
            {
                if (gt.Name == Name)
                {
                    retVal = gt;
                }
            }
            return retVal;
        }

        /// <summary>
        /// returns the total number of general tiles
        /// </summary>
        /// <returns>itn total number of general tiles</returns>
        public int Count()
        {
            return mGeneralTiles.Count;
        }

        /// <summary>
        /// Adds a general tile to the list
        /// will error if its ID <> to its index
        /// </summary>
        /// <param name="theTile"></param>
        public void Add(GeneralTile theTile)
        {
            if (theTile.ID == mGeneralTiles.Count)
            {
                mGeneralTiles.Add(theTile);
            }
            else
            {
                throw new Exception("Tried to add General Tile to list that had incorrect Id");
            }
        }

        public void Load(GraphicsDevice theGraphicsDevice)
        {
            foreach (GeneralTile gt in mGeneralTiles)
            {
                gt.Load(theGraphicsDevice);
              
            }
        }


    }
}
