using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using Custom.Interfaces;

namespace CommonObjects
{
    /// <summary>
	/// This class essentially describes a sequence of coordinates that point to a cell in a GeneralTexture
	/// Also defines a number of loops it does in 1 complete cycle of animation.
    /// assumes that all items in the animation are the same size.
	/// <para>Remember to call Add References on this object everytime an object references it - and also to remove references 
	/// in order for the AgroGarbageCollection to work</para>
    /// </summary>
    public class TextureAnimation : IEquatable<TextureAnimation>, IAgroGarbageCollection  
    {
      
        protected  string mName;
        protected int mID;
        protected List<Vector2> mAnimationSequence;
        protected GeneralTexture mGenTex;

        protected int mNoFrames;
        protected int mNoLoops;       // -1 = infinite loop
        private int mNoReferences = 0;

        protected TimeSpan mUpdatePeriod;

        protected bool mIsDisposed = false;

		protected object referencesLock = new object();

        #region Constructors
		/// <summary>
		/// This object contains a path through the cells of a general texture.
		/// IT can point to one or many as defined by the list of vector2
		/// Creates a textureAnimation object which is then used to spawn the animation instances that the tiles then use
		/// </summary>
		/// <param name="theName"></param>
		/// <param name="theID"></param>
		/// <param name="theTexture"></param>
		/// <param name="NoLoops"></param>
		/// <param name="updatePeriod"></param>
        public TextureAnimation(string theName, int theID, GeneralTexture theTexture,  int NoLoops, TimeSpan updatePeriod)
        {
            if ((object)theTexture == null)
            {throw new ArgumentNullException ("tried to create animation with null Texture");}

            if (theName.Length==0)
            { throw new ArgumentNullException("cannot have empty string as name"); }

            if (NoLoops<-1)
            {throw new ArgumentOutOfRangeException("cannot have a negative no loops (bar -1 for infinite loop)");}

            if (theID<0)
            { throw new ArgumentOutOfRangeException("Cannot have a negative ID"); }            

            mName = theName;
            mID = theID;
            theTexture.AddReference();
            mGenTex = theTexture;
            
           
            mNoFrames = 0;

            mNoLoops=NoLoops;
            mAnimationSequence = new List<Vector2>();
            mUpdatePeriod = updatePeriod;
        }

        public TextureAnimation(string theName, int theID, GeneralTexture theTexture,  int NoLoops, 
            TimeSpan updatePeriod, List<Vector2> theAnimationSequence)
        {
            if ((object)theTexture == null)
            { throw new ArgumentNullException("tried to create animation with null Texture"); }

            if (theAnimationSequence.Count < 1)
            { throw new ArgumentNullException("Tried to construct Texture Animation with an empty vector2 list"); }

            if (theID<0)
            { throw new ArgumentOutOfRangeException("Cannot have a negative ID"); }

            if (theName.Length==0)
            { throw new ArgumentNullException("cannot have empty string as name"); }

            if (NoLoops<-1)
            { throw new ArgumentOutOfRangeException("Cannot have noLoops less than -1"); }            

            mName = theName;
            mID = theID;
            theTexture.AddReference();
            mGenTex = theTexture;
           
            mNoLoops=NoLoops;
            mUpdatePeriod = updatePeriod;

            foreach (Vector2 v in theAnimationSequence)
            {
                AddFrame(v);
            }           

        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the Name of the Texture Animation
        /// </summary>
        public string Name
        { get { return mName; } }

        /// <summary>
        /// Gets the ID of the TextureAnimation
        /// </summary>
        public int ID
        { get { return mID; }
        /*    
			set
            {
                mID = value;
            }
		 */
        }

        /// <summary>
        /// Gets the Number of Frames in the Animation
        /// </summary>
        public int NoFrames
        {get { return mNoFrames;}}

        /// <summary>
        /// Gets the Number of loops in the Animation
        /// </summary>
        public int NoLoops
        { get { return mNoLoops; } }

        /// <summary>
        /// gets the Texture2D for the Animation
        /// </summary>
        public Texture2D Texture2D
        { get { return mGenTex.Texture; } }

        /// <summary>
        /// gets he general texture for the TextureAnimation
        /// </summary>
        public GeneralTexture GeneralTexture
        { get { return mGenTex; } }

        /// <summary>
        /// gets the update period in miliseconds
        /// </summary>
        public TimeSpan UpdatePeriod
        { get { return mUpdatePeriod; } }

        /// <summary>
        /// gets the animation frame at the specified index
        /// </summary>
        /// <param name="index">the animation frame index to return</param>
        /// <returns></returns>
        public Vector2 this[int index]
        {
            get
            {
                if ((index > 0) && (index < mAnimationSequence.Count))
                {return mAnimationSequence[index];}
                else
                { throw new OverflowException("index out of range"); }
            }
        }
            

        #endregion

  

#region Methods

      


        /// <summary>
        /// allows adding a frame in the form of a 
        /// coordinate of the sprite sheet
        /// </summary>
        /// <param name="theVector"></param>
        public void AddFrame(Vector2 theVector)
        {
            if ((object)theVector == null)
            { throw new NullReferenceException("Tried to add null vector2 to the animation list"); }

            if (theVector.X > (mGenTex.NoColumns-1))
            { throw new OverflowException("Tried to add a frame outside of frame grid"); }

            if (theVector.X < 0)
            { throw new OverflowException("Tried to add a frame outside of frame grid"); }

            if (theVector.Y  > (mGenTex.NoRows-1) )
            { throw new OverflowException("Tried to add a frame outside of frame grid"); }

            if (theVector.Y < 0)
            { throw new OverflowException("Tried to add a frame outside of frame grid"); }


                mAnimationSequence.Add(theVector);


            mNoFrames ++;
        }

        /// <summary>
        /// given a frame in the animation this returns the Rectange representing that frame
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Rectangle  GetRectangleForFrame(int frameIndex)
        {
            Vector2 CurrentCell;

                CurrentCell = mAnimationSequence[frameIndex];


            CurrentCell.X = CurrentCell.X * mGenTex.ColumnWidth;
            CurrentCell.Y = CurrentCell.Y * mGenTex.RowHeight;

            Rectangle retVal;
            retVal.Height = mGenTex.RowHeight;
            retVal.Width = mGenTex.ColumnWidth;
            retVal.X = (int)CurrentCell.X;
            retVal.Y = (int)CurrentCell.Y;

            return retVal;
        }

        /// <summary>
        /// creates a TextureanimationInstance from itself given the required paramaters
        /// </summary>
        /// <param name="noFrames">0 = use textureanimation default</param>
        /// <param name="defaultFrame">if no frames = 1 then this specifies which frame to stay on</param>
        /// <param name="noLoops">-1 = infinite, -2= use texture animation</param>
        /// <param name="updatePeriod">must be > -1 in miliseconds</param>
        /// <returns></returns>
        public TextureAnimationInstance CreateNewInstance(int noFrames, int defaultFrame,
            int noLoops, TimeSpan updatePeriod, AnimationType theAnimationType)
        {

                return new TextureAnimationInstance(this, noFrames, defaultFrame, noLoops, updatePeriod, theAnimationType);

        }

        #region Overridden Members
        /// <summary>
        /// returns the Animations Name, ID and NoFrames
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder  retVal = new StringBuilder();
            retVal.Append("Texture Animation Name : " + mName + ", ID : " + mID.ToString() + " No Frames : " + NoFrames.ToString());
            retVal.AppendLine();
            retVal.Append("  -> " + mGenTex.ToString());

            return retVal.ToString();
        }

        public override int GetHashCode()
        {
            Int64 temp;
            temp = mName.GetHashCode() + mID.GetHashCode() + mAnimationSequence.GetHashCode()
                + mGenTex.GetHashCode() + mNoFrames.GetHashCode() +mNoLoops.GetHashCode();
            return temp.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            { return false; }

            TextureAnimation temp = obj as TextureAnimation;
            if (this.Equals(temp))
            { return true; }
            else { return false; }
        }
#endregion
        #region IEquatable<TextureAnimation> Members

        public bool Equals(TextureAnimation other)
        {
            if ((object)other == null)
            { return false; }

            if (
                (mID == other.mID)
                && (mName == other.mName)
                && (mAnimationSequence.Equals(other.mAnimationSequence))
                && (mGenTex.Equals(other.mGenTex))
                && (mNoFrames == other.mNoFrames)
                && (mNoLoops==other.mNoLoops)
                && (mUpdatePeriod==other.mUpdatePeriod)
                )
            {
                return true;
            }
            else { return false; }
        }


        #endregion
  
#endregion  

		#region IAgroGarbageCollection Members

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
				throw new GCNoReferencesToRemoveException("Tried to remove reference to TextureAnimation that had no references", this);
			}
		}

		public bool IsDisposed
		{
			get { return mIsDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		/// <summary>
		/// Disposes of itself and attempts to dispose anything attached
		/// </summary>
        public void Dispose()
        {
			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			else
			{ throw new GCObjectStillHasReferencesException("Tried to dispose of Texture Animation, but it still has references", this); }
        }

        private void Dispose(bool disposed)
        {
            if (!mIsDisposed)
            {
                if (disposed)
                {
					//ToDO: enforce checking of references before trying to GC
                    mAnimationSequence.Clear();
                    mGenTex.RemoveReference();
                    mGenTex.Dispose();
                }
                mIsDisposed = true;
            }
        }

        ~TextureAnimation()
        {
            Dispose(false);
        }





		#endregion
	}
}
