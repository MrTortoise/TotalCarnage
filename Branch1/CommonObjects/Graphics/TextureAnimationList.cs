using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Custom.Interfaces;
using Custom.Exceptions;
using System.Threading;

namespace CommonObjects
{
	/// <summary>
	/// This is a container for TextureAnimations. 
	/// To be used for batch operations accross many TextureAnimations Eg Load
	/// </summary>
    public class TextureAnimationList : IEquatable<TextureAnimationList>, IAgroGarbageCollection 
    {
        #region fields
        protected List<TextureAnimation> mTextureAnimations;

        protected bool mIsdisposed;
        protected int mNoReferences = 0;

		protected object referencesLock = new object();


    #endregion

        
        public TextureAnimationList()
        {
            mTextureAnimations = new List<TextureAnimation>();
        }


        public List<TextureAnimation> AllAnimations
        { get { return mTextureAnimations; } }

		#region public methods
		public void AddAnimation(TextureAnimation theAnimation)
        {
            if ((object)theAnimation == null)
            { throw new ArgumentNullException ("Tried to add null TextureAnimation to TextureAnimationList"); }

            if (this.Contains(theAnimation))
            { throw new ArgumentAlreadyExistsException("Tried to Add already existing Texture Animation to a TextureAnimation List"); }
            
            theAnimation.AddReference();
            mTextureAnimations.Add(theAnimation);
        }

        public bool Contains(TextureAnimation theAnimation)
        {
            foreach (TextureAnimation t in mTextureAnimations)
            {
                if (t.Equals(theAnimation) == true)
                { return true; }
            }
            return false;
        }

        public int count()
        {
            return mTextureAnimations.Count;
		}
		#endregion
		#region Overidden members

		public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Texture Animation List Contains " + mTextureAnimations.Count().ToString() + " elements");

            foreach (TextureAnimation ta in mTextureAnimations)
            {
                sb.AppendLine();
                sb.Append(" -> " + ta.ToString());
            }
            sb.AppendLine();
            sb.Append("End of Texture Animation List");

            return sb.ToString();
        }

        public override int GetHashCode()
        {
            return mTextureAnimations.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            TextureAnimationList tal = obj as TextureAnimationList;

            if (this.Equals(tal))
            { return true; }
            else { return false; }
        }

		#endregion
        #region IEquatable<TextureAnimationList> Members

        public bool Equals(TextureAnimationList other)
        {
            if ((object)other == null)
            { return false; }

            if (mTextureAnimations.Equals(other.mTextureAnimations))
            { return true; }
            else { return false; }
        }

        #endregion
        #region IAgroGarbageCollection Members

        /// <summary>
        /// will dispose of all texture animations in its list
        /// </summary>
        public void Dispose()
        {
			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			else
			{ throw new GCObjectStillHasReferencesException("tried to dispose of TextureAnimation List but it still has refernces", this); }
        }

        protected virtual void Dispose(bool val)
        {
            if (mIsdisposed == false)
            {
                // if automatic GC then it will also look after the child objects
                if (val == true)
                {
                    foreach (TextureAnimation ta in mTextureAnimations)
                    {
                        ta.RemoveReference();
                        ta.Dispose();
                    }
                    mTextureAnimations = null;
                }                
                mIsdisposed = true;
            }
        }

        ~TextureAnimationList()
        {
            Dispose(false);
        }


        /// <summary>
        /// Everytime an object adds a reference to this object this method should be called
        /// Facilitates Aggressive Garabe Collection
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
        /// everytime an object containing a reference to this method is disposed
        /// or the reference is removed this method should be called
        /// Facilitiates Aggressive Garbage Collection
        /// </summary>
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
				{ throw e; }
				finally
				{
					Monitor.Pulse(referencesLock);
					Monitor.Exit(referencesLock);
				}
			}
			else
			{
				throw new GCNoReferencesToRemoveException("Tried to remove a reference to TextureAnimation List when none to remove", this);
			}
        }

		public bool IsDisposed
		{
			get { return mIsdisposed; }
		}


		public int NoReferences
		{
			get {return mNoReferences; }
		}


        #endregion



	}
}
