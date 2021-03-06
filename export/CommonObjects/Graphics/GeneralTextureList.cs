﻿using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;
using System.Threading;
using Custom.Interfaces;
using Custom.Exceptions;
using System.Xml.Linq;
using System.Linq;

namespace CommonObjects.Graphics
{
    /// <summary>
    /// Holds the global texturelist for the tilesets
    /// all tiles/textures refer to this list
    /// It also is used to load the content on startup / device reinitialisation
    /// </summary>
    public class GeneralTextureList : IEquatable<GeneralTextureList>,  ILoadXML,  IEnumerable<GeneralTexture>, IEnumerator<GeneralTexture>, IAgroGarbageCollection 
    {       

        protected Dictionary<int,GeneralTexture> mTextures = new Dictionary<int,GeneralTexture>();
        protected int mTextureEnumerator = -1;

        private object TextureLock = new object();
        private object enumeratorLock = new object();
        protected bool mIsDisposed = false;

        protected  int mNoReferences = 0;
		protected object referenceLock = new object();

        #region Constructors
        public GeneralTextureList()
        {
            
        }

        #endregion
        #region Properties

        public Dictionary<int, GeneralTexture> Textures
        {
            get
            {
                return mTextures;
            }

        }

        public int Count
            { get { return mTextures.Count; } }

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
		/// Gets the general texture in the list with the corresponding ID
		/// <para>returns null if not found</para>
		/// </summary>
		/// <param name="ID"></param>
		/// <returns>The GeneralTexture with the ID
		/// <para>if none found then returns null</para></returns>
		public GeneralTexture GetByID(int ID)
		{
			return mTextures[ID];
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

			
			if (!mTextures.ContainsKey(Texture.ID))
			{
                if (mTextures.ContainsValue(Texture))
                {
                    ArgumentAlreadyExistsException a = new ArgumentAlreadyExistsException("Tried to add General TExture to GeneralTexture List that was already in it");
                    a.Data.Add("textureID", Texture.ID.ToString());
                    a.Data.Add("textureName", Texture.Name.ToString());
                    throw a;
                }

				Texture.AddReference();
				mTextures.Add(Texture.ID, Texture);
			}    			
		}


        /// <summary>
        /// loads the textures int he list into the given graphics evbice
		/// <para>threadsafe</para>
        /// </summary>
        /// <param name="theGraphicsDevice"></param>
        public void Load()
        {
			foreach (GeneralTexture gt in mTextures.Values)
			{
				gt.Load();
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
            foreach (GeneralTexture g in mTextures.Values)
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
				throw new GCObjectStillHasReferencesException("Tried to dispose of GeneralTextureList that stil lhas references");
			}
		}

		/// <summary>
		/// tries to dispose each generalTexture then clears the list
		/// if called manually
		/// <para>threadsafe</para>
		/// </summary>
		/// <param name="val"></param>
		protected virtual void Dispose(bool val)
		{
			if (mIsDisposed == false)
			{
				foreach (GeneralTexture gt in mTextures.Values)
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
							throw new GCObjectStillHasReferencesException("Disposal of General Texture in GeneralTexture list Through Still has references exception",  e);
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

		public bool IsDisposed
		{
			get { return mIsDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		#endregion

        #region ILoadXML Members

        protected bool mIsXMLLoaded;
        public bool IsXMLLoaded
        {
            get { return mIsXMLLoaded; }
        }

        public void LoadFromXML(XDocument  theXML)
        {
            var generalTextures = from i in theXML.Descendants("texture")
                                  select new GeneralTexture(Convert.ToInt32(i.Element("id").Value),
                                      (string)i.Element("name").Value.ToString(),
                                      (string)i.Element("path").Value.ToString(),
                                      Convert.ToInt32(i.Element("noColumns").Value),
                                      Convert.ToInt32(i.Element("noRows").Value));

            foreach (GeneralTexture gt in generalTextures)
            {
                this.Add(gt);
            }

            mIsXMLLoaded = true;
        }

        public void LoadFromXMLFile(string thePath)
        {
            XDocument  data = XDocument.Load(thePath);

            this.LoadFromXML(data);
        }

        #endregion
    }
}
