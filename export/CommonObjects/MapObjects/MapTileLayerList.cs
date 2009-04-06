using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// Contains and manages a list of MapTileLayers
    /// </summary>
   public class MapTileLayerList : IGameDrawable 
    {
	   // ToDo: Implement IAgroGarabeCollection
        protected List<MapTileLayer> mMapTileLayers;

        public MapTileLayerList()
        {
            mMapTileLayers = new List<MapTileLayer>();
        }

        /// <summary>
        /// allows adding a MapTileLayer if its ID is next in sequence
        /// also sets the LayerDepth of each tile layer
        /// </summary>
        /// <param name="theMapTileLayer"></param>
        public void Add(MapTileLayer theMapTileLayer)
        {
            if ((object)theMapTileLayer == null)
            { throw new NullReferenceException("Tried to Add null MapTile to MapTileList"); }

            foreach (MapTileLayer m in mMapTileLayers)
            {
                if (m.ID == theMapTileLayer.ID)
                { throw new Exception("Tried to add MapTileLayer with an already existing ID"); }
            }

            if (this.Contains(theMapTileLayer))
            { throw new Exception("MapTileLayerList already contains member to be added"); }

            mMapTileLayers.Add(theMapTileLayer);

            float increment = 0.1f / mMapTileLayers.Count;

            for (int x = 0; x < mMapTileLayers.Count; x++)
            {
                mMapTileLayers[x].LayerDepth = x * increment;
            }
        }

        public bool Contains(MapTileLayer theMapTileLayer)
        {
            foreach (MapTileLayer m in mMapTileLayers)
            {
                if (m.Equals(theMapTileLayer))
                { return true; }
            }
            return false;
        }

        /// <summary>
        /// allows array like read and write access
        /// index must be in range
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public MapTileLayer this[int index]
        {
            get
            {
                if ((index > 0) && (index < mMapTileLayers.Count()))
                { return mMapTileLayers[index]; }
                else
                { throw new OverflowException("Tried to get MapTileLayer with out of range index"); }
            }
            set
            {
                if ((index > 0) && (index < mMapTileLayers.Count()))
                {
                    mMapTileLayers[index] = value;
                }
                else
                {
                    throw new OverflowException("Tried to add MapTileLayer to MapTileLayer list with out of range index");
                }
            }
        }

        /// <summary>
        /// Returns the total number of MapTileLayers
        /// </summary>
        /// <returns></returns>
        public int Count()
        { return mMapTileLayers.Count(); }

        #region IGameDrawable Members

        public void Draw(DrawingArgs theDrawingArgs)
        {
            foreach (MapTileLayer m in mMapTileLayers)
            {
                m.Draw(theDrawingArgs);
            }
        }

        #endregion

		#region IGameDrawable Members

		public bool IsVisible
		{
			get { throw new NotImplementedException(); }
		}

		public void SetVisibility(bool theVisibility)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
