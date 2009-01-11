using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace CommonObjects 
{
    /// <summary>
    /// Holds a list of tileLayers
    /// Doesnt do much now except set the laver depths
    /// </summary>
    public class TileLayerList : IGameDrawable
    {
        #region Fields
        //protected int mID;
        //protected string mName;
        protected List<TileLayer> mTileLayers;
        #endregion
        #region Constructors
        /// <summary>
        /// Sets up the tilelayerlist
        /// </summary>
        public TileLayerList()
        {
            mTileLayers = new List<TileLayer>();
        }
        #endregion
        #region Indexers
        /// <summary>
        /// returns the tile layer at a specified index
        /// allows the objects to be accessed like an array
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TileLayer this[int index]
        { get { return mTileLayers[index]; } }
        #endregion
        #region Properties
        /// <summary>
        /// returns the total number of layers int he list
        /// </summary>
        public int count
        { get { return mTileLayers.Count; } }
        #endregion
        #region Public Methods
        /// <summary>
        /// replace the tile layer with a new one
        /// </summary>
        /// <param name="theTileLayerList"></param>
        public void OverWrite(List<TileLayer> theTileLayerList)
        {
            mTileLayers = theTileLayerList;
        }

        /// <summary>
        /// adds a tile layer to the collection ... the tile layer must 
        /// have the same ID as the next layers index
        /// </summary>
        /// <param name="theTileLayer"></param>
        public void Add(TileLayer theTileLayer)
        {
            if (mTileLayers.Count == theTileLayer.ID)
            {
                mTileLayers.Add(theTileLayer);
            }
            else
            {
                throw new Exception("Tried to add tile layer with incorect ID");
            }

            float increment = 0.1f / mTileLayers.Count;

            for (int x = 0; x < mTileLayers.Count; x++)
            {
                mTileLayers[x].LayerDepth = x * increment;
            }
        }

        /// <summary>
        /// allows the tile layers to be updated
        /// </summary>
        /// <param name="theTime"></param>
        public virtual void Update(GameTime theTime)
        {
            foreach (TileLayer tl in mTileLayers)
            {
                tl.Update(theTime);
            }
        }

        /// <summary>
        /// Draws the tile layers
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        /// <param name="theGameTime"></param>
        public void Draw(DrawingArgs theDrawingArgs)
        {
            foreach (TileLayer tl in mTileLayers)
            {
                tl.Draw(theDrawingArgs);
            }
        }
 /*       /// <summary>
        /// This right now only sets the scale for the general tile layer.
        /// </summary>
        public void Load()
        {
            foreach (TileLayer tl in mTileLayers)
            {
                tl.Load();
            }
        }
        #endregion
*/
        public int GetWidthInPixels()
        {
            int maxWidth = 0;
            foreach (TileLayer tl in mTileLayers)
            {
                if (tl.WidthInPixels > maxWidth)
                {
                    maxWidth = tl.WidthInPixels;
                }
            }
            return maxWidth;
        }

        public int GetHeightInPixels()
        {
            int maxHeight = 0;
            foreach (TileLayer tl in mTileLayers)
            {
                if (tl.HeightInPixels   > maxHeight)
                {
                    maxHeight = tl.HeightInPixels;
                }
            }
            return maxHeight;
        }

        public int GetNoTilesAccross()
        {
            int maxWidth = 0;
            foreach (TileLayer tl in mTileLayers)
            {
                if (tl.NoTilesAccross  > maxWidth)
                {
                    maxWidth = tl.NoTilesAccross;
                }
            }
            return maxWidth;
        }

        public int GetNoTilesVertical()
        {
            int maxHeight = 0;
            foreach (TileLayer tl in mTileLayers)
            {
                if (tl.NoTilesVertical  > maxHeight)
                {
                    maxHeight = tl.NoTilesVertical;
                }
            }
            return maxHeight;
        }

        public int GetMinTileDims()
        {
            int minWidth = int.MaxValue;
            foreach (TileLayer tl in mTileLayers)
            {
                if (tl.TileDims < minWidth)
                {
                    minWidth = tl.TileDims;
                }
            }
            return minWidth;
        }

        #endregion



    }


}
