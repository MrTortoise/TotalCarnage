using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// This object draws and updates an individual tile
    /// It also provides a base for derrived tile types 
    /// to provide their own animation Methods
    /// </summary>
    public class Tile : IGameUpdateable,ISpriteBatchDrawable,IEquatable<Tile>,IEnumerable, IEnumerator
    {
        private string mName;
        private int mID;

        protected  int mCurrentAnimationID;
        protected int mCurrentFrameID;
        protected int mNextAnimationID;

        protected int mDefaultAnimationID;

        protected List<TextureAnimation> mTextureAnimations;
        protected int mTextureCursor = -1;

        protected double mUpdatePeriod;
        protected double mTimeTillUpdate;

        protected bool mInDefaultAnimation; //ToDo: Decide if redundant in future (if not, then why in base class?)
        protected bool mUpdateable;

        protected float mScale = 1;

        #region Constructors

        /// <summary>
        /// The base textures have to be loaded before this object is created
        /// This constructor creates a tile with a single animation (more can be added later)
        /// This should really only be used as a base constructor
        /// </summary>
        /// <param name="theName"></param>
        /// <param name="theID"></param>
        /// <param name="theTextureAnimation"></param>
        /// <param name="theUpdatePeriod"></param>
        public Tile(string theName, int theID, TextureAnimation  theTextureAnimation, double theUpdatePeriod)
        {
            if ((object)theTextureAnimation == null)
            { throw new NullReferenceException("Tried to add a textureanimation that is null"); }

            mTextureAnimations = new List<TextureAnimation>();
            AddTextureAnimation(theTextureAnimation);

            mName = theName;
            mID = theID;
            mDefaultAnimationID=0;
            mCurrentAnimationID=mDefaultAnimationID;
            mCurrentFrameID=0;
            mInDefaultAnimation=true;
            mUpdatePeriod = theUpdatePeriod;            
        }

        /// <summary>
        /// The base textures have to be loaded before this object is created
        /// This constructor creates a tile with a List of Animations
        /// Should be really only be used as a base constructor 
        /// </summary>
        /// <param name="theName"></param>
        /// <param name="theID"></param>
        /// <param name="theDefaultAnimationID"></param>
        /// <param name="theTextureAnimations"></param>
        /// <param name="theUpdatePeriod"></param>
        public Tile(string theName, int theID, int theDefaultAnimationID, 
            List<TextureAnimation> theTextureAnimations, double theUpdatePeriod)
        {
            if ((object)theTextureAnimations == null)
            { throw new NullReferenceException("Tried to add a list of listoftextureanimationItem that is null"); }

            mTextureAnimations = new List<TextureAnimation>();

            foreach (TextureAnimation li in theTextureAnimations)
            {
                AddTextureAnimation(li);
            }

            DefaulAnimationID = theDefaultAnimationID;
            mCurrentAnimationID=DefaulAnimationID;
            mCurrentFrameID=0;
            mInDefaultAnimation=true;
            mUpdatePeriod = theUpdatePeriod;

            mName = theName;
            mID = theID;
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
        { get { return mID; } }

        /// <summary>
        /// get / set the default animation index (=ID)
        /// </summary>
        public int DefaulAnimationID
        {get{return mDefaultAnimationID;}
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
        /// readonly access to an individual ListofTextureAnimationItem
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public TextureAnimation this[int index]
        { get { return mTextureAnimations[index]; } }

        /// <summary>
        /// returns the scale the texture will be drawn at
        /// </summary>
        public float Scale
        { get { return mScale; } }       

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds a non null ListOfTextureAnimationItem to the Tile
        /// </summary>
        /// <param name="theItem"></param>
        public void AddTextureAnimation(TextureAnimation  theAnimation)
        {
            if ((object)theAnimation == null)
            { throw new NullReferenceException("Tried to add a null ListOfTextureAnimationItem"); }

            mTextureAnimations.Add(theAnimation);            
        }

        /// <summary>
        /// sets the index of the animation that will start once the current is finished
        /// </summary>
        /// <param name="index"></param>
        public void SetNextAnimation(int index)
        {
            if ((index > -1) && (index < mTextureAnimations.Count))
            {
                mNextAnimationID = index;
            }
            else
            { throw new OverflowException("index out of range"); }
        }

        /// <summary>
        /// Makes the tile calculate its drawing scale in order to be the specified width
        /// in pixels
        /// </summary>
        /// <param name="width"></param>
        public void SetScale(int width)
        {
            mScale = (float)width / (float)mTextureAnimations[0].Texture2D.Width;
        }

        #region OverRidden Members

        public override string ToString()
        {
            string retVal;
            retVal = "Tile Name: " + mName + ", ID: " + mID.ToString() + "/n";
            retVal = retVal + "   Contains the following Texture Animations /n";

            StringBuilder s = new StringBuilder();
            s.Append(retVal);

            foreach (TextureAnimation t in mTextureAnimations)
            {
                s.AppendLine();
                s.Append("Animation ID: " + t.ID.ToString() + ", Name: " + t.Name);
            }
            s.AppendLine();
            s.Append("End of Tile");
            return s.ToString();
        }

        public override int GetHashCode()
        {
            Int64 total;
            total = mID + mCurrentAnimationID + mCurrentFrameID + mDefaultAnimationID + mNextAnimationID;
            total = total + mName.GetHashCode() + mTextureAnimations.GetHashCode() + (int)mUpdatePeriod + (int)mScale;
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

        #region IGameUpdateable Members

        public virtual void Update(UpdateArgs theUpdateArgs)
        {
            // ToDo: some kind of command list?
            mTimeTillUpdate = mTimeTillUpdate - theUpdateArgs.GameTime.ElapsedGameTime.Milliseconds;

            if (mTimeTillUpdate < 0)
            {
                mTimeTillUpdate = mUpdatePeriod;
                IncrementFrame();
            }
        }

        #endregion

        #region ISpriteBatchDrawable Members

        public virtual void Draw(spriteBatchArgs theSpriteBatchArgs)
        {
            //theBatch.Draw(theTexture, position, null,theColor  ,0, Vector2.Zero,theScale  , SpriteEffects.None, mLayerDepth );
            theSpriteBatchArgs.propSpriteBatch.Draw(
                mTextureAnimations[mCurrentAnimationID].Texture2D,
                theSpriteBatchArgs.Position,
                mTextureAnimations[mCurrentAnimationID].GetRectangleForFrame(mCurrentFrameID),
                Color.White,
                theSpriteBatchArgs.Rotation,
                new Vector2(0, 0), new Vector2(mScale, mScale),
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
                (mID == other.mID)
                && (mName == other.mName)
                && (mDefaultAnimationID == other.mDefaultAnimationID)
                && (mTextureAnimations.Equals(other.mTextureAnimations))
                && (mUpdatePeriod == other.mUpdatePeriod)
                && (mUpdateable == other.mUpdateable)
                && (mScale == other.mScale)
                && (mCurrentAnimationID == other.mCurrentAnimationID)
                && (mNextAnimationID == other.mNextAnimationID)
                )
            { return true; }
            else { return false; }

        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)this;
        }

        #endregion

        #region IEnumerator Members

        public object Current
        {
            get { return mTextureAnimations[mTextureCursor]; }
        }

        public bool MoveNext()
        {
            mTextureCursor++;
            if (mTextureCursor >= mTextureAnimations.Count)
            {
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

        public void Dispose()
        {
            mTextureAnimations = null;
        }

        #endregion

#endregion

        #region Private Methods

        protected  void IncrementFrame()
        {
            if (mCurrentFrameID == (mTextureAnimations[mCurrentAnimationID].NoFrames - 1))
            {
                mCurrentAnimationID = mNextAnimationID;
                mNextAnimationID = mDefaultAnimationID;
                mCurrentFrameID = 0;
            }
            else
            {
                mCurrentFrameID = mCurrentFrameID + 1;
            }
        }


#endregion





     }
}
