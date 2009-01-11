using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// Holds the global texturelist for the tilesets
    /// all tiles/textures refer to this list
    /// It also is used to load the content on startup / device reinitialisation
    /// </summary>
    public class GeneralTextureList : IEquatable<GeneralTextureList>, 
        IEnumerable<GeneralTexture>, IEnumerator<GeneralTexture>
    {

        protected List<GeneralTexture> mTextures;
        protected int mTextureEnumerator = -1;

            public GeneralTextureList()
        {
            mTextures = new List<GeneralTexture>();
        }

        /// <summary>
        /// Adds a general texture tot he list
        /// The texture id must = the added items index
        /// </summary>
        /// <param name="Texture"></param>
        public void Add(GeneralTexture Texture)
        {
            if ((object)Texture == null)
            { throw new NullReferenceException("Tried to add a null texture to texture list"); }

            if (mTextures.Contains(Texture))
            { throw new Exception("Tried to add General TExture to GeneralTexture List that was already in it"); }
            
            if (mTextures.Count == Texture.ID)
            {
                mTextures.Add(Texture);
            }
            else
            {
                throw new Exception("Tried to add texture with bad ID");
            }            
        }

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
            get { return mTextures[index]; }
            set
            {
                if ((object)value == null)
                { throw new NullReferenceException("Tried to add null texture to list"); }

                if ((mTextures.Count  >= index) || (index < 0))
                {
                    throw new Exception("Trying to overwrite non existant texture");
                }
                else
                {
                    mTextures[index] = value;
                }
            }
        }

        /// <summary>
        /// loads the textures int he list into the given graphics evbice
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

            if (mTextures.Equals(other.mTextures))
            { return true; }
            else { return false; }
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
            get { return mTextures[mTextureEnumerator]; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            mTextures = null;
        }

        #endregion

        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return mTextures[mTextureEnumerator]; }
        }

        public bool MoveNext()
        {
            mTextureEnumerator++;
            if (mTextureEnumerator >= mTextures.Count)
            { return false; }
            else { return true; }
        }

        public void Reset()
        {
            mTextureEnumerator = -1;
        }

        #endregion


    }
}
