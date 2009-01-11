using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// This is a generic non specific texture. Specific textures reference individual textures via
    /// the texture list. This means that only 1 of each texture image is in memory
    /// </summary>
    public class GeneralTexture: IGameLoadable , IEquatable<GeneralTexture>
    {
        protected int mID;
        protected string mName;
        protected string mPath;
        protected Texture2D mTexture;

        protected int mColumns;
        protected int mColumnWidth;
        protected int mRows;
        protected int mRowHeight;
        // TODO Implement row and columns into the texture        

        #region Constructor

        public GeneralTexture(int ID, string Name, string Path, int theNoColumns, int theNoRows)
        {
            if (theNoColumns < 1)
            { theNoColumns = 0; }

            if (theNoRows < 0)
            { theNoRows = 0; }

            mID = ID;
            mName = Name;
            mPath = Path;

            mColumns = theNoColumns;
            mRows = theNoRows;
            
            //LoadTexture();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Returns the Id of the GeneralTexture
        /// </summary>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// Returns the Name of the general Texture
        /// </summary>
        public string Name
        { get { return mName; } }

        /// <summary>
        /// returns the file path of the texture
        /// </summary>
        public string Path
        { get { return mPath; } }

        /// <summary>
        /// returns the texture2d object that will be drawn
        /// </summary>
        public Texture2D Texture
        { get { return mTexture; } }

        public int NoColumns
        { get { return mColumns; } }

        public int NoRows
        { get { return mRows; } }

        public int ColumnWidth
        { get { return mColumnWidth; } }

        public int RowHeight
        { get { return mRowHeight; } }

        #endregion

        #region Public Methods

        /// <summary>
        /// loads the texture given a graphics device to load into
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        public void Load(GraphicsDevice theGraphicsDevice)
        {
            
            mTexture = Texture2D.FromFile(theGraphicsDevice, mPath);
            mColumnWidth = mTexture.Width / mColumns;
            mRowHeight = mTexture.Height / mRows;
        }

        /// <summary>
        /// returns the contents of the General Texture
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "General Texture Name : " + mName + ", ID : " +mID.ToString() + 
                ", Texture : " + mTexture.Name + ", Columns : " + mColumns.ToString() 
                +", Rows : " +mRows.ToString() ;
        }

        public override int GetHashCode()
        {
            int total;
            total = mID + mColumns + mRows + mTexture.GetHashCode();
            string hash = "";
            hash = mName.ToString() + mPath.ToString();
            total = total + hash.GetHashCode();
            return total.GetHashCode();            
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            GeneralTexture temp = obj as GeneralTexture;
            if (this.Equals(temp))
            { return true; }
            else { return false; }
        }

        #region IEquatable<GeneralTexture> Members

        public bool Equals(GeneralTexture other)
        {
            if ((object)other == null)
            { return false; }

            if (
                (mID == other.mID)
                && (mName == other.mName)
                && (mPath == other.mPath)
                && (mTexture.Equals(other.mTexture))
                && (mColumns == other.mColumns)
                && (mRows == other.mRows)
                )
            {
                return true;
            }
            else { return false; }
        }

        #endregion

        #endregion
    }

}
