using System;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace CommonObjects
{
    public class ListOfTextureAnimationItem
    {
        private int mID;
        private string mName;
        private int mFrameRate;
        private TextureAnimation mTextureAnimation;

        #region Constructor

        public ListOfTextureAnimationItem(int theID, string theName, int theFrameRate, TextureAnimation theTextureAnimation)
        {
            if ((object)theTextureAnimation == null)
            {
                throw new NullReferenceException("Tried to create ListofTextureAnimation with a null Texture Animation");
            }

            if (theFrameRate < 0)
            { theFrameRate = 0; }

            mID = theID;
            mName = theName;
            mFrameRate = theFrameRate;
            mTextureAnimation = theTextureAnimation;

        }

        #endregion

        #region properties

        public int ID
        { get { return mID; } }

        public string Name
        {get { return mName; }}


        public int FrameRate
        { get { return mFrameRate; } }

 //       public TextureAnimation TextureAnimation
 //       { get { return mTextureAnimation; } }

        public Texture2D Texture2D
        { get { return mTextureAnimation.Texture2D; } }

        public int NoFrames
        { get { return mTextureAnimation.NoFrames; } }

        #endregion

        #region Methods

        public Rectangle GetSourceRectangle(int frame)
        {
            return mTextureAnimation.GetRectangleForFrame(frame);
        }

        #endregion


    }
}
