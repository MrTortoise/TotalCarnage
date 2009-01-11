using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// assumes that all items in the animation are the same size.
    /// </summary>
    public class TextureAnimation : IEquatable<TextureAnimation>
    {
        private string mName;
        private int mID;
        private List<Vector2> mAnimationSequence; //ToDo: ensure all items are the same size
        private GeneralTexture mGenTex;
        private int mNoFrames;
        private bool mIsAnimated;

        #region Constructors

        public TextureAnimation(string theName, int theID, GeneralTexture theTexture, bool Animated)
        {
            if ((object)theTexture == null)
            {
                throw new NullReferenceException("tried to create animation with null Texture");
            }

            mName = theName;
            mID = theID;
            mGenTex = theTexture;
            mIsAnimated = Animated;
            mNoFrames = 0;
            mAnimationSequence = new List<Vector2>();
        }

        public TextureAnimation(string theName, int theID, GeneralTexture theTexture, bool Animated, List<Vector2> theAnimationSequence)
        {
            if ((object)theTexture == null)
            {
                throw new NullReferenceException("tried to create animation with null Texture");
            }

            if (theAnimationSequence.Count < 1)
            {
                throw new Exception("Tried to construct Texture Animation with an empty vector2 list");
            }

            mName = theName;
            mID = theID;
            mGenTex = theTexture;
            mIsAnimated = Animated;

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
        { get { return mID; } }

        /// <summary>
        /// Gets the Number of Frames in the Animation
        /// </summary>
        public int NoFrames
        {get { return mNoFrames;}}

        /// <summary>
        /// Gets wether or not the animation is animated
        /// </summary>
        public bool IsAnimated
        {get{return mIsAnimated;}}

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
        public Rectangle  GetRectangleForFrame(int index)
        {
            if ((index < 0) || (index > (mAnimationSequence.Count - 1)))
            {
                throw new OverflowException("index out of range");
            }

            Vector2 CurrentCell = mAnimationSequence[index];

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
                + mGenTex.GetHashCode() + mNoFrames.GetHashCode() + IsAnimated.GetHashCode();
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
                && (mIsAnimated == other.mIsAnimated)
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
