using System;
using System.Collections.Generic;
using System.Text;
using Custom.Interfaces;
using CommonObjects.Graphics;

namespace CommonObjects
{
	
    /// <summary>
    /// This is a tile specificially used in maps
    /// Adds a terrain property to a Tile and is then used as part of a MapTileLayer
    /// </summary>
    public class MapTile : Tile, IEquatable<MapTile> , IAgroGarbageCollection 
    {
		//Todo Figure out how switching logic would work for map tiles with several animations
		//Todo Implement IAgroGarbageCollection
        #region Fields
        protected TerrainType  mTerrainType;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a map tile object from a single texture animation and terrain type
        /// </summary>
        /// <param name="theName"></param>
        /// <param name="theID"></param>
        /// <param name="theTextureAnimation"></param>
        /// <param name="theUpdatePeriod"></param>
        /// <param name="theTerrainType"></param>
        public MapTile(string theName, int theID, TextureAnimation theTextureAnimation, int noFrames, int defaultFrame,
                int noLoops, TimeSpan updatePeriod, float theRotation,AnimationType theAnimationType, 
                TerrainType theTerrainType)
            : base(theName, theID, theTextureAnimation, noFrames,defaultFrame,noLoops,
                updatePeriod, theRotation,theAnimationType )
        {
            mTerrainType = theTerrainType;
		}



        #endregion

        #region Properties
        /// <summary>
        /// Gets the TerrainType of the MapTile
        /// </summary>
        public TerrainType TerrainType
        { get { return mTerrainType; } }

        #endregion

        #region Public Methods


        #region OverRidden Methods

        public override string ToString()
        {
            String retVal;
            retVal = "Map Tile Terrain Type: " + mTerrainType.ToString() + "/n";
            retVal = retVal + base.ToString() + "/n";
            retVal = retVal + "End of MapTile";
            return retVal;
        }

        public override int GetHashCode()
        {
            Int64 temp;
            temp = base.GetHashCode() + mTerrainType.GetHashCode();
            return temp.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            MapTile m = obj as MapTile;
            return this.Equals(m);
        }
        #endregion

        #region IEquatable<MapTile> Members

        public bool Equals(MapTile other)
        {
            if ((object)other == null)
            { return false; }

            if (                
                ((Tile)this).Equals((Tile)other)
                && (mTerrainType.Equals(other.mTerrainType))
                )
            {
                return true;
            }
            else
            { return false; }

        }

        #endregion
        #endregion

 
		#region	IAgroGarbageCollection Members

		bool IAgroGarbageCollection.IsDisposed
		{
			get	{ throw	new	NotImplementedException(); }
		}

		int	IAgroGarbageCollection.NoReferences
		{
			get	{ throw	new	NotImplementedException(); }
		}

		void IAgroGarbageCollection.AddReference()
		{
			throw new NotImplementedException();
		}

		void IAgroGarbageCollection.RemoveReference()
		{
			throw new NotImplementedException();
		}

		#endregion 
		#region IDisposable Members

		void IDisposable.Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion


	}
}
