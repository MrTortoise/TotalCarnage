using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using Custom.Interfaces;


namespace CommonObjects
{
    /// <summary>
	/// This is a map over a texture that defines a grid which is then referenced by a textureAnimation
    /// This is a generic non specific texture. Ir represents as many images/frames as cells.
    /// </summary>
    public class GeneralTexture: IGameLoadable , IEquatable<GeneralTexture>, IAgroGarbageCollection 
    {
        protected int mID;
        protected string mName;
        protected string mPath;
        protected Texture2D mTexture;

        protected int mColumns;
        protected int mColumnWidth;
        protected int mRows;
        protected int mRowHeight;

        protected bool mIsLoaded = false;
        protected bool mDisposed = false;

        protected object textureLock = new object();
		protected object referencesLock = new object();

        private int mNoReferences;       
             

        #region Constructor

		/// <summary>
		/// Creates a general texture
		/// </summary>
		/// <param name="ID">The unique id of the texture<para>Eg 1</para></param>
		/// <param name="Name">A name for the texture<para>Eg Wood</para></param>
		/// <param name="Path">the path of the texture file relative to the current directory<para>Eg "\Textures\ground_texture.jpg"</para></param>
		/// <param name="theNoColumns">the number of columns in the texture<para>Eg 2</para></param>
		/// <param name="theNoRows">the number of rows in the texture<para>Eg 1</para></param>
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
        { 
            get 
            {
                    return mTexture;
            }
        }           

		/// <summary>
		/// Get the numbe rof columns in the texture
		/// </summary>
        public int NoColumns
        { get { return mColumns; } }

		/// <summary>
		/// gets the number of rows in the texture
		/// </summary>
        public int NoRows
        { get { return mRows; } }

		/// <summary>
		/// gets the column width
		/// </summary>
        public int ColumnWidth
        { 
            get 
            {	 
                return mColumnWidth; 
            } 
        }

		/// <summary>
		/// gets the column height
		/// </summary>
        public int RowHeight
        { 
            get 
            {	  
                return mRowHeight; 
            } 
        }

        #endregion

        #region Public Methods	        

        /// <summary>
        /// loads the texture given a graphics device to load into
		/// <para>threadsafe</para>
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        public void Load(GraphicsDevice theGraphicsDevice)
        {
            if (mIsLoaded == false)
            {
				try
				{
					Monitor.Enter(textureLock);
					if (mIsLoaded == false)		  // could get loaded whilst getting lock
					{
						mTexture = Texture2D.FromFile(theGraphicsDevice, mPath);
						mIsLoaded = true;
						mColumnWidth = mTexture.Width / mColumns;
						mRowHeight = mTexture.Height / mRows;
					}
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					Monitor.Pulse(textureLock);
					Monitor.Enter(textureLock);
				}
            }
                
        }

        /// <summary>
        /// returns the contents of the General Texture
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
			if (mTexture != null)
			{
				return "General Texture Name : " + mName + ", ID : " + mID.ToString() +
					", Texture : " + mTexture.Name + ", Columns : " + mColumns.ToString()
					+ ", Rows : " + mRows.ToString();
			}
			else
			{
				return "General Texture Name : " + mName + ", ID : " + mID.ToString() +
					 ", Columns : " + mColumns.ToString()
					 + ", Rows : " + mRows.ToString();
			}
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

        #region #region IAgroGarbageCollection Members 

		/// <summary>
		/// Everytime the generalTexture is added to a class this should be called
		/// </summary>
		public void AddReference()
		{
			try
			{
				Monitor.Enter(referencesLock);
				mNoReferences++;
			}
			catch (Exception e)
			{ throw e; }
			finally
			{
				Monitor.Pulse(referencesLock);
				Monitor.Exit(referencesLock);
			}
		}

		/// <summary>
		/// Everytime the generalTexture no longer needed this should be called
		/// <para>Throws exception if no references remaining in counter</para>
		/// </summary>
		/// <exception cref="Custom.Interfaces.GCNoReferencesToRemoveException">Thrown when attempt to remove a reference and the counter is 0</exception>
		public void RemoveReference()
		{

			if (mNoReferences > 0)
			{
				try
				{
					Monitor.Enter(referencesLock);
					mNoReferences--;
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					Monitor.Pulse(referencesLock);
					Monitor.Exit(referencesLock);
				}
			}
			else
			{
				throw new GCNoReferencesToRemoveException("Tried to remove refernce to general Texture but count is 0");
			}
		}

        /// <summary>
        /// Only disposes if the manual reference count = 0
        /// </summary>
        public void Dispose()
        {
			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			else
			{
				throw new GCObjectStillHasReferencesException("Tried to dispose General Texture that still had references");
			}
        }
        
        /// <summary>
        /// If manually called then this forcably disposes the Texture2D object
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            { 
				//Todo make GC MultiThreaded
                mTexture.Dispose();               
            }
            mDisposed = true;
        }

        ~GeneralTexture()
        {
            Dispose(false);
        }  
		public bool IsDisposed
		{
			get { return mDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		#endregion
	}

}
