using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using Custom.Interfaces;
using Custom.Exceptions;

namespace CommonObjects
{
    //ToDo: add an initial time offset to the animation to allow ripple effects
	/// <summary>
	/// This class is created by a TextureAnimation in order for each element in a Tile to be unique. 
	/// <para>It is essentially a wrapper around the TextureAnimation to provide properties 
	/// that enable its animation to be independant. 
	/// This will be necessary as part of the ripple implementation</para>
	/// </summary>
    public class TextureAnimationInstance : 
        IEquatable<TextureAnimationInstance>, 
        IAgroGarbageCollection, 
        IGameUpdateable ,
        ICloneable 
    {

        public delegate void AnimationFinishedhandler(TextureAnimationInstance sender);
        public event AnimationFinishedhandler OnAnimationfinished;

		#region Fields
		protected TextureAnimation mAnimation;

        protected int mNoFrames;
        protected int mCurrentFrame = 0;

        protected int mDefaultFrame;

        protected int mNoLoops;       // -1 = infinite loop
        protected int mLoopCount = 0;

        protected TimeSpan mUpdatePeriod;
        protected TimeSpan mTimeTillUpdate;

        protected AnimationType mAnimationType;
        protected Vector2 mScale;

        protected bool mActive = false;
        

        private int mNoReferences = 0;
        protected bool mIsDisposed = false;

		protected object referencesLock = new object();
		protected object animLock = new object();
		#endregion

		#region constructor
		/// <summary>
        /// Allows creation of a TextureAnimationInstance from an existing general textureAnimation
        /// </summary>
        /// <param name="animation">the texture animation to use as a base</param>
        /// <param name="noFrames">0 = use TextureAnimation class - it must already have a frame</param>
        /// <param name="defaultFrame">can only be set to anything other than 0 if noframes == 1</param>
        /// <param name="noLoops">-2 indicates use TextureAnimation base value, -1 = infinite. Loop of 1 will repeat the animation twice</param>
        /// <param name="updatePeriod">update period in miliseconds</param>
        /// <param name="theAnimationType">the Type of the animation referenced by inheritors of tile</param>
        public TextureAnimationInstance(TextureAnimation animation, int noFrames, int defaultFrame,
            int noLoops, TimeSpan updatePeriod, AnimationType theAnimationType)
        {
            if ((object)animation == null)
            { throw new ArgumentNullException("Tried to create TextureanimationInstance with null animation"); }

            animation.AddReference();
            mAnimation = animation;

            if (noFrames == 0)
            { 
                if (mAnimation.NoFrames==0)
                { throw new ArgumentNullException("No frames in the texture animation"); }
                else
                {mNoFrames = mAnimation.NoFrames; }
            }
            else
            {
                if (noFrames > 0)
                {
                    if (noFrames > mAnimation.NoFrames)
                    { throw new ArgumentOutOfRangeException("cannot have more frames than the animation"); }
                    else
                    { mNoFrames = noFrames; }
                }
                else
                { throw new ArgumentOutOfRangeException("noFrames < 0"); }
            }

            if ((defaultFrame < 0)||(defaultFrame>=mNoFrames))
            { throw new ArgumentOutOfRangeException("default frame out of count range"); }
            else
            {
                if ((defaultFrame > 0) && (mNoFrames != 1))
                { throw new ArgumentsCombinationIllegalException("tried to set default frame in multi frame animation = no no"); }
                                
                mDefaultFrame = defaultFrame;
            }

            if (noLoops == -2)
            { mNoFrames = mAnimation.NoLoops; }
            else
            {
                if (noLoops > -2)
                {
                    if (noLoops > mAnimation.NoLoops)
                    { throw new ArgumentOutOfRangeException("cannot set no loops > base animation"); }
                    else
                    { mNoLoops = noLoops; }
                }
                else
                { throw new ArgumentOutOfRangeException("No loops cannot be < -2"); }
            }

            if ((noLoops != 1) && (mDefaultFrame != -1))
            { mNoLoops = 1; }

            if (updatePeriod == TimeSpan.MinValue)
            { mUpdatePeriod = mAnimation.UpdatePeriod; }
            else
            {
                if (mUpdatePeriod.Ticks  >= 0)
                { mUpdatePeriod = updatePeriod; }
                else
                { throw new ArgumentOutOfRangeException("update period cannot be < 0"); }
            }

            mTimeTillUpdate = mUpdatePeriod;
            mAnimationType = theAnimationType;

            if (mAnimationType == AnimationType.defaultAnimation)
            { mActive = true; }
        }

        #endregion

        #region properties

        public bool Active
        {
            get { return mActive; }
            set { mActive = value; }
        }

        /// <summary>
        /// Gets the current frame the animation is on
        /// </summary>
        public int CurrentFrame
        {
            get
            {
                try
                {
                    Monitor.Enter(animLock);
                    return mCurrentFrame;
                }
                catch (Exception e)
                { throw e; }
                finally
                {
                    Monitor.Pulse(animLock);
                    Monitor.Exit(animLock);
                }
            }
        }

        /// <summary>
        /// gets the numver of times the animation will loop
        /// -1 = infinite loop
        /// </summary>
        public int NoLoops
        { get { return mNoLoops; } }

        /// <summary>
        /// get the loop number that the animation is on
        /// </summary>
        public int CurrentLoop
        { get {
            try
            {
                Monitor.Enter(animLock);
                return mLoopCount;
            }
            catch (Exception e)
            { throw e; }
            finally
            {
                Monitor.Pulse(animLock);
                Monitor.Exit(animLock);
            }
            } }
        
        /// <summary>
        /// Gets the Number of Frames in the Animation
        /// </summary>
        public int NoFrames
        { get { return mNoFrames; } }

        /// <summary>
        /// gets the update period in a tmespan
        /// </summary>
        public TimeSpan  UpdatePeriod
        { get { return mUpdatePeriod; } }

        /// <summary>
        /// gets the time until the next update will execute 
        /// </summary>
        public TimeSpan TimeTillUpdate
        { get {
            try
            {
                Monitor.Enter(animLock);
                return mTimeTillUpdate;
            }
            catch (Exception e)
            { throw e; }
            finally
            {
                Monitor.Pulse(animLock);
                Monitor.Exit(animLock);
            }
        }
        }

        /// <summary>
        /// gets the type of the animation
        /// </summary>
        public AnimationType theAnimationType
        { get { return mAnimationType; } }

        /// <summary>
        /// gets the generic textureAnimation for this instance
        /// </summary>
        public TextureAnimation TextureAnimation
        { get 
        {

                return mAnimation;

        } 
        }

        /// <summary>
        /// gets the texture2D that forms the base for the animatino
        /// </summary>
        public Texture2D Texture2D
        { get { return mAnimation.Texture2D; } }

        /// <summary>
        /// gets the scaling value for this TextureAnimationInstance
        /// </summary>
        public Vector2 Scale
        { get { return mScale; } }

        #endregion

        #region Methods

       

        /// <summary>
        /// takes a vector2 representing the dims of the target rendering rectangle
        /// and uses this to set the scale of the texture based on column widths and 
        /// row heights of the general texture
        /// </summary>
        /// <param name="targetDims"></param>
        public void SetScale(Vector2 targetDims)
        {
            mScale.X = (float)targetDims.X / (float)mAnimation.GeneralTexture.ColumnWidth;
            mScale.Y = (float)targetDims.Y / (float)mAnimation.GeneralTexture.RowHeight;
        }

        /// <summary>
        /// given a frame in the animation this returns the Rectange representing that frame
        /// </summary>
        /// <returns></returns>
        public Rectangle  GetRectangleForCurrentFrame()
        { 
            return mAnimation.GetRectangleForFrame(CurrentFrame);
        }



        #endregion 
		#region overrided methods

		/// <summary>
		/// returns the Animations Name, ID and NoFrames
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			StringBuilder retVal = new StringBuilder();
			retVal.AppendLine("Texture Animation Instance No Frames : " + mNoFrames.ToString() + ", Current Frame: " +
				mCurrentFrame.ToString() + ", NoLoops: " + mNoLoops.ToString() + ", current Loop: " +
				mLoopCount.ToString() + ", Current Frame: " + mCurrentFrame.ToString());
			retVal.AppendLine("texture Animation Name: " + mAnimation.Name.ToString() + ", ID: " + mAnimation.ID.ToString());
			return retVal.ToString();
		}

		public override int GetHashCode()
		{
			Int64 temp;
			temp = mNoFrames.GetHashCode() + mNoLoops.GetHashCode() + mUpdatePeriod.GetHashCode() + mAnimation.GetHashCode();
			return temp.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{ return false; }

			TextureAnimationInstance temp = obj as TextureAnimationInstance;
			if (this.Equals(temp))
			{ return true; }
			else { return false; }
		}

		#endregion

		#region IGameUpdateable Members

		public void Update(UpdateArgs theUpdateArgs)
		{
			// if only 1 frame then no point in updating
			if (mNoFrames != 1)
			{
				if (mActive == true)
				{
					//apply the timewarp                
					TimeSpan timepassed = TimeSpan.FromTicks((long)(theUpdateArgs.GameTime.ElapsedGameTime.Ticks * theUpdateArgs.TimeScale));
					try
					{
						Monitor.Enter(animLock);
						mTimeTillUpdate = mTimeTillUpdate - timepassed;
						// if due for an update
						if (mTimeTillUpdate.Milliseconds <= 0)
						{


							mTimeTillUpdate = mUpdatePeriod;
							mCurrentFrame++;
							if (mCurrentFrame >= mNoFrames)
							{
								// if the animation is an infinite loop we want i to report as finished as the tile
								// decides wether to start this animation over again.
								if ((mNoLoops > 0) && (mLoopCount < mNoLoops))
								{
									mCurrentFrame = mDefaultFrame;
									mLoopCount++;
								}
								else
								{
									if (OnAnimationfinished != null)
									{
										OnAnimationfinished(this);
									}
									Reset();
								}
							}

						}
					}
					catch (Exception e)
					{ throw e; }
					finally
					{
						Monitor.Pulse(animLock);
						Monitor.Exit(animLock);
					}
				}
			}
		}

		public void Reset()
		{
			try
			{
				Monitor.Enter(animLock);
				mTimeTillUpdate = mUpdatePeriod;
				mCurrentFrame = mDefaultFrame;
				mLoopCount = 0;
			}
			catch (Exception e)
			{ throw e; }
			finally
			{
				Monitor.Pulse(animLock);
				Monitor.Exit(animLock);
			}

		}

		#endregion
		#region IEquatable<textureAnimationInstance> Members

		public bool Equals(TextureAnimationInstance other)
		{
			if ((object)other == null)
			{ return false; }

			if ((mAnimation.Equals(other.mAnimation))
				&& (mLoopCount == other.mLoopCount)
				&& (mNoFrames == other.mNoFrames)
				&& (mNoLoops == other.mNoLoops)
				&& (mUpdatePeriod == other.mUpdatePeriod)
				&& (mCurrentFrame == other.mCurrentFrame)
				)
			{ return true; }
			else { return false; }

		}

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
			{
				throw e;
			}
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
				{ throw e; }
				finally
				{
					Monitor.Pulse(referencesLock);
					Monitor.Exit(referencesLock);
				}
			}
			else
			{
				throw new GCNoReferencesToRemoveException("Tried to remove non existant reference in TextureAnimationInstance");
			}

		}

		public bool IsDisposed
		{
			get { return IsDisposed; }
		}

		public int NoReferences
		{
			get { return mNoReferences; }
		}

		public void Dispose()
		{
			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		private void Dispose(bool disposing)
		{
			if (!mIsDisposed)
			{
				if (disposing)
				{
					mAnimation.RemoveReference();
					mAnimation.Dispose();
				}
				mIsDisposed = true;
			}
		}

		~TextureAnimationInstance()
		{
			Dispose(false);
		}


		#endregion
		#region	ICloneable Members

		public object Clone()
		{
			TextureAnimationInstance temp = new TextureAnimationInstance(mAnimation, 0, 0, 0, TimeSpan.FromMilliseconds(500), AnimationType.defaultAnimation);
			return (object)temp;
		}



		#endregion	  

        
	}
}


/*
 *         #region IEnumerable<Vector2> Members

        public IEnumerator<Vector2> GetEnumerator()
        {
            return (IEnumerator<Vector2>)this;
        }
        #endregion
        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion
        #region IEnumerator<Vector2> Members

        public Vector2 Current
        {
            get { return mAnimation[mCurrentFrame]; }
        }

        #endregion
        #region IEnumerator Members

        object IEnumerator.Current
        {
            get { return mAnimation[mCurrentFrame]; }
        }

        /// <summary>
        /// iterates the frame, manages the loop counter 
        /// returns false when all loops have completed
        /// </summary>
        /// <returns></returns>
        public bool MoveNext()
        {
            mCurrentFrame++;
            if (mCurrentFrame < mNoFrames)
            { return true; }
            else
            {
                if ((mNoLoops > 0) && (mLoopCount < mNoLoops))
                {
                    mCurrentFrame = mDefaultFrame;
                    mLoopCount++;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// resets both the current frame and the current loop couters
        /// if no frames =0 and default frame <> 0 then current = default
        /// </summary>
        public void Reset()
        {
            mCurrentFrame = mDefaultFrame;                        
            mLoopCount = 0;
        }

        #endregion
 * 
 * */
