using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;
using System.Threading;
using Custom.Interfaces;
using Custom.Exceptions;

namespace CommonObjects
{
    /// <summary>
    /// Holds the global texturelist for the tilesets
    /// all tiles/textures refer to this list
    /// It also is used to load the content on startup / device reinitialisation
    /// </summary>
    public class GeneralTextureList : IEquatable<GeneralTextureList>, 
        IEnumerable<GeneralTexture>, IEnumerator<GeneralTexture>, IAgroGarbageCollection 
    {       

        protected List<GeneralTexture> mTextures;
        protected int mTextureEnumerator = -1;

        private object TextureLock = new object();
        private object enumeratorLock = new object();
        protected bool mIsDisposed = false;

        protected  int mNoReferences = 0;
		protected object referenceLock = new object();

        #region Constructors
        public GeneralTextureList()
        {
            mTextures = new List<GeneralTexture>();
        }

        #endregion
        #region Properties

        public int Count
            { get { return mTextures.Count; } }


        /// <summary>
        /// reutrns the list of general textures
        /// </summary>
        /// <returns></returns>
        public List<GeneralTexture> AllTextures
        { get { return mTextures; } }

        /// <summary>
        /// When wiritng can only overwrite existing memebers
        /// </summary>
        /// <param name="index"></param>
        /// <returns>the general texture at the given index</returns>
        public GeneralTexture this[int index]
        {
            get 
            {                 
				return mTextures[index];
			}
            set
            {
                if ((object)value == null)
                { throw new ArgumentNullException("Tried to add null texture to list"); }

                if ((mTextures.Count >= index) || (index < 0))
                {
                    throw new ArgumentOutOfRangeException("Trying to overwrite non existant texture");
                }
                else
                {
                    try
                    {
                        Monitor.Enter(TextureLock);
                        mTextures[index] = value;
                    }
                    catch (Exception e)
                    {
                        throw e; 
                    }                        
                    finally
                    {
                        Monitor.Pulse(TextureLock);
                        Monitor.Exit(TextureLock);
                    }
                }
            }
        }

        #endregion

        #region public methods

        /// <summary>
        /// whenever an object adds a reference to this object this method should be called
        /// This is to facilitate Aggressive Garbage Collection
		/// <para>threadSafe</para>
        /// </summary>
        public void AddReference()
        {
			try
			{
				Monitor.Enter(referenceLock);
				mNoReferences++;
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				Monitor.Pulse(referenceLock);
				Monitor.Exit(referenceLock);
			}
        }

        /// <summary>
        /// whenever an object is finished with its reference to this object this method should be called
        /// This is to facilitate Aggressive Garbage Collection
		/// <para>threadSafe</para>
        /// </summary>
        public void RemoveReference()
        {
            if (mNoReferences > 0)
            {
				try
				{
					Monitor.Enter(referenceLock);
					mNoReferences--;
				}
				catch (Exception e)
				{
					throw e;
				}
				finally
				{
					Monitor.Pulse(referenceLock);
					Monitor.Exit(referenceLock);
				}
            }
        }

        /// <summary>
        /// Adds a general texture to the list
        /// The texture id must = the added items index
        /// </summary>
        /// <param name="Texture"></param>
		public void Add(GeneralTexture Texture)
		{
			if ((object)Texture == null)
			{ throw new ArgumentNullException("Tried to add a null texture to texture list"); }

			if (this.Contains(Texture))
			{ throw new ArgumentAlreadyExistsException("Tried to add General TExture to GeneralTexture List that was already in it"); }

			foreach (GeneralTexture t in mTextures)
			{
				if (t.ID == Texture.ID)
				{ throw new ArgumentAlreadyExistsException("Tried to add texture with existing ID"); }
			}

			Texture.AddReference();
			mTextures.Add(Texture);
		}

        /// <summary>
        /// gets the index of the texture with the specified ID
        /// returns -1 if none exist
        /// </summary>
        /// <param name="theID"></param>
        /// <returns></returns>
        public int GetIndexFromID(int theID)
        {
            //ToDo: come up with better search algorithms? - list not necesarily in id order          
            for (int x = 0; x < mTextures.Count; x++)
            {
                if (mTextures[x].ID == theID)
                { return x; }
            }
           return -1;            
        }

        /// <summary>
        /// Tests to see if there is a texture with the specified ID
        /// </summary>
        /// <param name="theID"></param>
        /// <returns></returns>
        public bool Contains(int theID)
        {  
            foreach (GeneralTexture t in mTextures)
            {
                if (t.ID == theID)
                { return true; }
            }

            return false;
        }

        /// <summary>
        /// Tests to see if the given texture exists in the list by value
        /// </summary>
        /// <param name="theTexture"></param>
        /// <returns></returns>
        public bool Contains(GeneralTexture theTexture)
        {
                foreach (GeneralTexture t in mTextures)
                {
                    if (t.Equals(theTexture))
                    { return  true; }
                }

            return false;
        }

        /// <summary>
        /// loads the textures int he list into the given graphics evbice
		/// <para>threadsafe</para>
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        public void Load(GraphicsDevice theGraphicsDevice)
        {
			foreach (GeneralTexture gt in mTextures)
			{
				gt.Load(theGraphicsDevice);
			}
        }

        public override int GetHashCode()
        {
            return mTextures.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("GeneralTextureList Contains " + mTextures.Count.ToString() + " elements ");
            foreach (GeneralTexture g in mTextures)
            {
                s.AppendLine();
                s.Append(" --> " + g.ToString());
            }
            s.AppendLine();
            s.Append("End of General Texture List");
            return s.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            GeneralTextureList temp = obj as GeneralTextureList;
            if (mTextures.Equals(temp))
            { return true; }
            else { return false; }
        }


        #region IEquatable<GeneralTextureList> Members

        public bool Equals(GeneralTextureList other)
        {
            if ((object)other == null)
            { return false; }

            if (other.mTextures.Count!=this.mTextures.Count)
            {return false;}

            bool retval = true;
            
            for (int x=0;x<this.mTextures.Count;x++)
            {
                if (this[x].Equals(other[x])==false)
                {retval=false;}
            }
            return retval;
        }

        #endregion
        #region IEnumerable<GeneralTexture> Members

        public IEnumerator<GeneralTexture> GetEnumerator()
        {
            return (IEnumerator<GeneralTexture>)this;
        }

        #endregion
        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion
        #region IEnumerator<GeneralTexture> Members

        public GeneralTexture Current
        {
            get
            {
                try
                {
                    Monitor.Enter(TextureLock);
                    return mTextures[mTextureEnumerator];
                }
                catch (Exception e)
                { throw e; }
                finally
                {
                    Monitor.Pulse(TextureLock);
                    Monitor.Exit(TextureLock);
                }
            }
        }

        #endregion
        #region IDisposable Members

        /// <summary>
        /// this will dispose of all the textures in the list also
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
				throw new GCObjectStillHasReferencesException("Tried to dispose of GeneralTextureList that stil lhas references", this);
			}
        }

        /// <summary>
        /// tries to dispose each generalTexture then clears the list
        /// if called manually
        /// </summary>
        /// <param name="val"></param>
        protected virtual void Dispose(bool val)
        {
			if (mIsDisposed == false)
			{
				foreach (GeneralTexture gt in mTextures)
				{
					gt.RemoveReference();
					if (val == true)
					{
						try
						{
							gt.Dispose();
						}
						catch (GCObjectStillHasReferencesException e)
						{
							throw new GCObjectStillHasReferencesException("Disposal of General Texture in GeneralTexture list Through Still has references exception", this, e);
						}

					}
				}
				mIsDisposed = true;
			}
        }

        ~GeneralTextureList()
        {
            Dispose(false);
        }

        #endregion
        #region IEnumerator Members

        object IEnumerator.Current
        {
            get
            {
                try
                {
                    Monitor.Enter(enumeratorLock);
                    return mTextures[mTextureEnumerator];
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    Monitor.Pulse(enumeratorLock);
                    Monitor.Exit(enumeratorLock);
                }

            }
        }

        public bool MoveNext()
        {
            try
            {
                Monitor.Enter(enumeratorLock);
                mTextureEnumerator++;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Monitor.Pulse(enumeratorLock);
                Monitor.Exit(enumeratorLock);
            }

            if (mTextureEnumerator >= mTextures.Count)
            { return false; }
            else { return true; }
        }

        public void Reset()
        {
            try
            {
                Monitor.Enter(enumeratorLock);
                mTextureEnumerator = -1;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                Monitor.Pulse(enumeratorLock);
                Monitor.Exit(enumeratorLock);
            }
        }

        #endregion

        #endregion






		#region IAgroGarbageCollection Members

		public bool IsDisposed
		{
			get { return mIsDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		#endregion
	}
}
