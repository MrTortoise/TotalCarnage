using System;
using System.Collections.Generic;

using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// Holds a list of terrain types ... 
    /// the idea is that only 1 of each terrain type exists that 
    /// all instances refer back to
    /// </summary>
    public class TerrainTypeList: IEquatable<TerrainTypeList>
    {
        protected List<TerrainType> TerrainTypes;

        public TerrainTypeList()
        {
            TerrainTypes = new List<TerrainType>();
        }

        /// <summary>
        /// indexor: returns the terrotory type at a given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>the terrain type at a given index</returns>
        public TerrainType Get(int index)
        { return TerrainTypes[index]; }

        /// <summary>
        /// returns the list of territory types
        /// </summary>
        /// <returns>the list of all terrain types</returns>
        public List<TerrainType> GetList()
        {
            return TerrainTypes;
        }

        /// <summary>
        /// allows the addition of a territory type to te end of the list
        /// It must have an ID = Index
        /// </summary>
        /// <param name="theTerrainType">the terrain to be added to the list. Its ID must = the new items index</param>
        public void AddTerrainType(TerrainType theTerrainType)
        {

            if ((object)theTerrainType == null)
            { throw new NullReferenceException("Tried to add a null terrain Type"); }

            foreach (TerrainType t in TerrainTypes)
            {
                if (t.ID == theTerrainType.ID)
                { throw new Exception("Tried to add terrainType with already existing ID"); }
            }

            if (this.Contains(theTerrainType))
            { throw new Exception("Tred to add a terrain Type that is already in the list"); }

            TerrainTypes.Add(theTerrainType);           

        }

        public bool Contains(TerrainType theTerrainType)
        {
            foreach (TerrainType t in TerrainTypes)
            {
                if (t.Equals(theTerrainType))
                { return true; }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return TerrainTypes.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("TerrainTypes list contains " + TerrainTypes.Count + " elements");
            foreach (TerrainType t in TerrainTypes)
            {
                sb.AppendLine();
                sb.Append(t.ToString());
            }
            sb.AppendLine();
            sb.Append("End of TerrainType List");

            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            TerrainType t = obj as TerrainType;
            return this.Equals(t);
        }

        #region IEquatable<TerrainTypeList> Members

        public bool Equals(TerrainTypeList other)
        {
            if ((object)other == null)
            { return false; }

            if (TerrainTypes.Equals(other.TerrainTypes))
            { return true; }
            else { return false; }
        }

        #endregion
    }
}
