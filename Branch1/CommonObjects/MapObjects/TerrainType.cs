using System;
using System.Collections.Generic;

using System.Text;

namespace CommonObjects
{
   public class TerrainType : IEquatable<TerrainType>
    {
        protected int mID;
        protected string mName;
        protected bool mPassable;
        protected float mMoveModifier;

        public TerrainType(int ID, string Name, bool Passable, float MoveModifier)
        {
            mID = ID;
            mName = Name;
            mPassable = Passable;
            mMoveModifier = MoveModifier;
        }

        #region Properties
        /// <summary>
       /// returns the ID of the TerrainType
       /// </summary>
        public int ID
        { get { return mID; } }

       /// <summary>
       /// Returns the Name of the TerrainType
       /// </summary>
        public string Name
        { get { return mName; } }

       /// <summary>
       /// returns wether or not the terrain is passable
       /// </summary>       
        public bool Passable
        { get { return mPassable; } }

       /// <summary>
       /// returns the move multiplier for the territory
       /// later maybe adapted to be a vector2
       /// </summary>
        public float MoveModifier
        { get { return mMoveModifier; } }

        #endregion

        /// <summary>
       /// overides ToString() to return the 
       /// TerrainTypeName
       /// </summary>
       /// <returns>The Name</returns>
        public override string ToString()
        {
            return " Terrain Type ID: " + mID.ToString()+ ", Name: " + mName +", Passable: " + mPassable.ToString() + ", MoveModifier: " + mMoveModifier.ToString();

        }

        public override int GetHashCode()
        {
            Int64 temp;
            temp = mID + mName.GetHashCode() + mPassable.GetHashCode() + mMoveModifier.GetHashCode();
            return temp.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            TerrainType t = obj as TerrainType;
            return this.Equals(t);
            
        }

        #region IEquatable<TerrainType> Members

        public bool Equals(TerrainType other)
        {
            if ((object)other == null)
            { return false; }

            if (
                (mID == other.mID)
                && (mName == other.mName)
                && (mPassable == other.mPassable)
                && (mMoveModifier == other.mMoveModifier)
                )
            { return true; }
            else { return false; }

        }

        #endregion
    }
}
