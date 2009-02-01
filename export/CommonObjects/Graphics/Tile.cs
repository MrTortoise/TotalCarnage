using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using Custom.Interfaces;
using Custom.Exceptions;
using Custom.Maths;

namespace CommonObjects
{
    /// <summary>	/// 
    /// This object draws and updates an individual tile
    /// It also provides a base for derrived tile types 
    /// to provide their own animation Methods
	/// Basically contains the data necessary for animation state management
    /// </summary>
	public class Tile : ISpriteBatchDrawable, IEquatable<Tile>, IEnumerable<TextureAnimationInstance>,
		IEnumerator<TextureAnimationInstance>, IAgroGarbageCollection, ICloneable
	{
		//ToDo Multithread references. Are you really sure this needs to be multi threaded? - small loss, big gain?
		#region Fields
		protected  string mName;
		protected  int mID;

		protected int mCurrentAnimationID;
		protected int mNextAnimationID = -1;

		protected int mDefaultAnimationID;

		protected List<TextureAnimationInstance> mTextureAnimations = new List<TextureAnimationInstance>();
		protected int mTextureCursor = -1;

		protected float mScale = 1;
		protected float mRotation = 0;

		protected bool mVisible;

		protected bool mIsDisposed = false;
		protected int mNoReferences = 0;

		protected object animLock = new object();
		protected object ACLock = new object();
		protected object TCLock = new object();
		#endregion

		#region Constructors


		public Tile(string theName, int theID)
		{
			mID = theID;
			mName = theName;
		}
		/// <summary>
		/// The base textures have to be loaded before this object is created
		/// This constructor creates a tile with a single animation (more can be added later)
		/// This should really only be used as a base constructor
		/// </summary>
		/// <param name="theName">the name to be given to the tile: currently not really used - intended for the editor</param>
		/// <param name="theID">the Id of the tile: not currently used really - intended for the editor</param>
		/// <param name="theTextureAnimation">the textureanimation object - will use this by effectivley cloning the origional object</param>
		/// <param name="theUpdatePeriod">a timespan representing the objects update period: efectivley acts as 1/fps</param>
		/// <param name="theRotation">the initial rotation offset - currently there is no angular velocity / period implementation, derivation is intended to achieve this</param> 
		/// <param name="animationType">the animation type according to the enumeration</param>
		public Tile(string theName, int theID, TextureAnimation theTextureAnimation, int Noframes, int defaultFrame, int NoLoops,
			TimeSpan theUpdatePeriod, float theRotation, AnimationType animationType)
		{
			if ((object)theTextureAnimation == null)
			{ throw new ArgumentNullException("Tried to add a textureanimation that is null"); }

			if (theName.Length == 0)
			{ throw new ArgumentNullException("Cannot have empty string as Tile Name"); }

			if (theID < 0)
			{ throw new ArgumentOutOfRangeException("Cannot have negative ID"); }

			if (theUpdatePeriod.Ticks < 0)
			{ throw new ArgumentOutOfRangeException("Cannot have negative update period"); }

			mTextureAnimations = new List<TextureAnimationInstance>();
			AddTextureAnimation(theTextureAnimation, Noframes, defaultFrame, NoLoops, theUpdatePeriod, animationType);

			mName = theName;
			mID = theID;
			mDefaultAnimationID = 0;
			mCurrentAnimationID = mDefaultAnimationID;

			RotationManager temp = new RotationManager();
			mRotation = temp.GetAbsRotation(theRotation);
		}


		#endregion

		#region Properties

		/// <summary>
		/// readonly name of the Tile
		/// </summary>
		public string Name
		{ get { return mName; } }

		/// <summary>
		/// readonly ID of the Tile
		/// </summary>
		public int ID
		{
			get { return mID; }
			set { mID = value; }
		}

		/// <summary>
		/// get / set the default animation index (=ID)
		/// </summary>
		public int DefaulAnimationID
		{
			get
			{
				return mDefaultAnimationID;
			}
			set
			{
				if (value > mTextureAnimations.Count)
				{
					throw new OverflowException("Tried to set a default animationID for non existent animation");
				}

				if (value < 0)
				{ value = 0; }

				mDefaultAnimationID = value;
			}
		}

		/// <summary>
		/// readonly access to an individual TextureAnimationList
		/// </summary>
		/// <param name="index">this is the index in the collection and does not refer to ID</param>
		/// <returns></returns>
		public TextureAnimationInstance this[int index]
		{
			get
			{
				try
				{
					Monitor.Enter(animLock);
					return mTextureAnimations[index];
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
		/// returns the scale the texture will be drawn at
		/// </summary>
		public float Scale
		{ get { return mScale; } }

		/// <summary>
		/// Returns the Rotation of the Texture
		/// </summary>
		public float Rotation
		{
			get
			{ return mRotation; }
			set
			{
				RotationManager temp = new RotationManager();
				mRotation = temp.GetAbsRotation(value);
			}
		}

		/// <summary>
		/// gets / sets next animatino id
		/// this is the animation to be shown once the current animation finishes
		/// -1 = default
		/// </summary>
		public int NextAnimationID
		{
			get
			{
				try
				{
					Monitor.Enter(ACLock);
					return mNextAnimationID;
				}
				catch (Exception e)
				{ throw e; }
				finally
				{
					Monitor.Pulse(ACLock);
					Monitor.Exit(ACLock);
				}
			}
			set
			{
				if (value < -1)
				{ throw new OverflowException("cannot set next animation index less than -1"); }
				try
				{
					Monitor.Enter(ACLock);
					if (value > (mTextureAnimations.Count - 1))
					{ throw new OverflowException("cannot set next animation index grater than the total no"); }

					mNextAnimationID = value;
				}
				catch (Exception e)
				{ throw e; }
				finally
				{
					Monitor.Pulse(ACLock);
					Monitor.Exit(ACLock);
				}
			}
		}

		public bool Visible
		{
			get { return mVisible; }
			set { mVisible = value; }
		}

		public List<TextureAnimationInstance> animations
		{ get { return mTextureAnimations; } }


		#endregion

		#region Public Methods

		/// <summary>
		/// Allows adding a generic textureanimation along with the info required to instantiate it
		/// </summary>
		/// <param name="theAnimation">the texture animation</param>
		/// <param name="noFrames">0 = use TextureAnimation class - it must already have a frame</param>
		/// <param name="defaultFrame">can only be set to anything other than -1 if noframes == 1</param>
		/// <param name="noLoops">-2 indicates use TextureAnimation base value, -1 = infinite. Loop of 1 will repeat the animation twice</param>
		/// <param name="updatePeriod">update period in miliseconds</param>
		/// <param name="theAnimationType">the Type of the animation referenced by inheritors of tile</param>
		public void AddTextureAnimation(TextureAnimation theAnimation, int noFrames, int defaultFrame,
			int noLoops, TimeSpan updatePeriod, AnimationType theAnimationType)
		{
			if ((object)theAnimation == null)
			{ throw new ArgumentNullException("Tried to add a null textureanimation"); }
			try
			{
				Monitor.Enter(animLock);
				foreach (TextureAnimationInstance t in mTextureAnimations)
				{
					if (t.theAnimationType == theAnimationType)
					{ throw new ArgumentAlreadyExistsException("Animation Type already exists for this tile"); }
				}

				TextureAnimationInstance temp = new TextureAnimationInstance(theAnimation, noFrames, defaultFrame,
					noLoops, updatePeriod, theAnimationType);

				if (this.Contains(temp))
				{ throw new ArgumentAlreadyExistsException("The AnimationInstance is already in the collection"); }
				temp.AddReference();
				mTextureAnimations.Add(temp);
				Subscribe(temp);
			}
			catch (Exception e)
			{ throw e; }
			finally
			{
				Monitor.Pulse(animLock);
				Monitor.Exit(animLock);
			}
		}

		/// <summary>
		/// tests to see if the collection already contains the TextureAnimationInstance by Value
		/// </summary>
		/// <param name="theAnimation"></param>
		/// <returns></returns>
		public bool Contains(TextureAnimationInstance theAnimation)
		{

			foreach (TextureAnimationInstance t in mTextureAnimations)
			{
				if (t.Equals(theAnimation))
				{ return true; }
			}
			return false;

		}


		/// <summary>
		/// Makes the tile calculate its drawing scale in order to be the specified width
		/// in pixels
		/// </summary>
		/// <param name="width">in pixels</param>
		public void SetScale(Vector2 target)
		{
			foreach (TextureAnimationInstance t in mTextureAnimations)
			{
				t.SetScale(target);
			}
		}



		#region OverRidden Members

		public override string ToString()
		{
			string retVal;
			retVal = "Tile Name: " + mName + ", ID: " + mID.ToString() + "/n";
			retVal = retVal + "   Contains the following Texture Animations /n";

			StringBuilder s = new StringBuilder();
			s.Append(retVal);

			foreach (TextureAnimationInstance t in mTextureAnimations)
			{	 
				// this could be broke			
				s.AppendLine(t.ToString());// + t.Name);
			}
			s.AppendLine();
			s.Append("End of Tile");
			return s.ToString();
		}

		public override int GetHashCode()
		{
			//ToDo: check all relevant items are in here
			Int64 total;
			total = mID + mCurrentAnimationID + mDefaultAnimationID + mNextAnimationID;
			total = total + mName.GetHashCode() + mTextureAnimations.GetHashCode() + (int)mRotation + (int)mScale;

			return total.GetHashCode();
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{ return false; }

			Tile t = obj as Tile;

			if (this.Equals(t))
			{ return true; }
			else { return false; }
		}

		#endregion


		#region ISpriteBatchDrawable Members

		public virtual void Draw(spriteBatchArgs theSpriteBatchArgs)
		{
			// test to see if the tile is visible
			Rectangle currentFrameRectange = mTextureAnimations[mCurrentAnimationID].GetRectangleForCurrentFrame();

			
			float theRotation;
			if (theSpriteBatchArgs.IsRotationSet == true)
			{
				theRotation = theSpriteBatchArgs.Rotation;
			}
			else { theRotation = mRotation; }  
			//theBatch.Draw(theTexture, position, null,theColor  ,0, Vector2.Zero,theScale  , SpriteEffects.None, mLayerDepth );
			theSpriteBatchArgs.SpriteBatch.Draw(
				mTextureAnimations[mCurrentAnimationID].Texture2D,
				theSpriteBatchArgs.Position,
				currentFrameRectange,
				Color.White,
				theRotation,
				new Vector2(0, 0), //ToDo: [rpvide option of origin of the tile as its center point 
				mTextureAnimations[mCurrentAnimationID].Scale,
				SpriteEffects.None,
				theSpriteBatchArgs.LayerDepth);
		}


		#endregion
		#region IEquatable<Tile> Members

		public bool Equals(Tile other)
		{
			if ((object)other == null)
			{ return false; }

			if (
				//ToDo: Check that all relevant Items are in here
				(mID == other.mID)
				&& (mName == other.mName)
				&& (mDefaultAnimationID == other.mDefaultAnimationID)
				&& (mTextureAnimations.Equals(other.mTextureAnimations))
				&& (mScale == other.mScale)
				&& (mCurrentAnimationID == other.mCurrentAnimationID)
				&& (mNextAnimationID == other.mNextAnimationID)
				&& (mRotation == other.mRotation)
				)
			{ return true; }
			else { return false; }

		}

		#endregion
		#region IEnumerable<TextureAnimationInstance> Members

		IEnumerator<TextureAnimationInstance> IEnumerable<TextureAnimationInstance>.GetEnumerator()
		{
			return (IEnumerator<TextureAnimationInstance>)this;
		}

		#endregion
		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return (IEnumerator)this;
		}

		#endregion
		#region IEnumerator<TextureAnimationInstance> Members

		TextureAnimationInstance IEnumerator<TextureAnimationInstance>.Current
		{
			get { return mTextureAnimations[mTextureCursor]; }
		}

		#endregion
		#region IEnumerator Members
		//ToDo: Implement cursor multithreading
		public object Current
		{
			get { return mTextureAnimations[mTextureCursor]; }
		}


		public bool MoveNext()
		{
			mTextureCursor++;
			if (mTextureCursor >= mTextureAnimations.Count)
			{
				Reset();
				return false;

			}
			else { return true; }

		}

		public void Reset()
		{
			mTextureCursor = -1; ;
		}

		#endregion
		#region IDisposable Members

		public virtual void Dispose()
		{

			if (mNoReferences == 0)
			{
				Dispose(true);
				GC.SuppressFinalize(this);				
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!mIsDisposed)
			{
				if (disposing)
				{
					foreach (TextureAnimationInstance tai in mTextureAnimations)
					{
						tai.RemoveReference();						
						tai.Dispose();
						
					}
					mTextureAnimations = null;
				}
				mIsDisposed = true;
			}
		}

		~Tile()
		{
			Dispose(false);
		}


		#endregion
		#region	ICloneable Members

		public virtual object Clone()
		{
			Tile temp = new Tile(mName, mID);
			foreach (TextureAnimationInstance ta in mTextureAnimations)
			{
				temp.AddTextureAnimation(ta.TextureAnimation, 0, 0, 0, TimeSpan.FromMilliseconds(500), AnimationType.defaultAnimation);
			}
			return (object)temp;
		}

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

		public void AddReference()
		{
			mNoReferences++;
		}

		public void RemoveReference()
		{
			if (mNoReferences > 0)
			{ mNoReferences--; }
		}

		#endregion

		#endregion



		protected virtual void Subscribe(TextureAnimationInstance textureAnimationInstance)
		{
			textureAnimationInstance.OnAnimationfinished += new TextureAnimationInstance.AnimationFinishedhandler(textureAnimationInstance_OnAnimationfinished);
		}

		public virtual void textureAnimationInstance_OnAnimationfinished(TextureAnimationInstance textureAnimationInstance)
		{
			//check that the animation is the ctive one - it is possible to share animations 
			if (textureAnimationInstance.Equals(mTextureAnimations[mCurrentAnimationID]))
			{
				textureAnimationInstance.Active = false;
				//need to move to the next animation
				if (mNextAnimationID > -1) // check if next animation has been set
				{
					mCurrentAnimationID = mNextAnimationID;
					mNextAnimationID = -1;
				}
				else // if not an infinite looping animation use the default animation
				{
					if (mTextureAnimations[mCurrentAnimationID].NoLoops != -1)
					{
						mCurrentAnimationID = mDefaultAnimationID;
					}
				}
				mTextureAnimations[mCurrentAnimationID].Reset();
				mTextureAnimations[mCurrentAnimationID].Active = true;
			}
		}


	}
}

