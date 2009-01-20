using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// Holds the list of all mapTiles that are used in the map
    /// Controls the updating of all tiles in a map.
    /// </summary>
    public class MapTileList : IEquatable<MapTileList>,IEnumerable,IEnumerator
    {

        protected List<MapTile> mMapTiles;
        protected int mMapTileEnumerator = -1;

        #region Constructor
        public MapTileList()
        {
            mMapTiles = new List<MapTile>();
        }
        #endregion

        #region Public Methods

        public void Add(MapTile theMapTile)
        {
            if ((object)theMapTile == null)
            { throw new NullReferenceException("Tried to Add null MapTile to MapTileList"); }

            foreach (MapTile m in mMapTiles)
            {
                if (m.ID == theMapTile.ID)
                { throw new Exception("tried to add a map Tile with an existing ID"); }
            }

            if (mMapTiles.Contains(theMapTile))
            { throw new Exception("Tried to add already existing MapTile to the Tile List"); }

            mMapTiles.Add(theMapTile);
        }

        public MapTile this[int index]
        {
            get
            {
                if ((index > 0) && (index < mMapTiles.Count))
                { return mMapTiles[index]; }
                else
                { throw new OverflowException("Tried to get MapTile with out of range index"); }
            }
            set
            {
                if ((index > 0) && (index < mMapTiles.Count))
                {
                    mMapTiles[index] = value;
                }
                else
                {
                    throw new OverflowException("Tried to add MapTile to MapTile list with out of range index");
                }
            }
        }

        public int Count()
        { return mMapTiles.Count; 
         }

        public bool Contains(MapTile item)
        {
            bool result = false;
            foreach (MapTile t in mMapTiles)
            {
                if (t.Equals(item))
                { result = true; }
            }
            return result;
        }

        #region OverRidden Members
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("MapTileList contains " + mMapTiles.Count.ToString() + "members");

            foreach (MapTile m in mMapTiles)
            {
                sb.AppendLine();
                sb.Append("  MapTile ID: " + m.ID.ToString() + ", Name: " + m.Name);
            }
            sb.Append("/n End of MapTile");
            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return mMapTiles.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            MapTileList m = obj as MapTileList;
            return this.Equals(m);
        }

        #endregion

        #region IEquatable<MapTileList> Members

        public bool Equals(MapTileList other)
        {
            if ((object)other == null)
            { return false; }

            return mMapTiles.Equals(other.mMapTiles);
        }

        #endregion
        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion
        #region IEnumerator Members

        public object Current
        {
            get { return mMapTileEnumerator; }
        }

        public bool MoveNext()
        {
            mMapTileEnumerator++;
            if (mMapTileEnumerator >= mMapTiles.Count)
            { return false; }
            else { return true; }
        }

        public void Reset()
        {
            mMapTileEnumerator = -1;
        }

        #endregion
        #endregion





 
    }
}
