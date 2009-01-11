using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace TheGameEngine
{
    public abstract class Actor
    {
        #region Fields

        protected string mName = "Unnamed";
        
        protected Vector2 mPosition = Vector2.Zero;
        protected Vector2 mOldPosition = Vector2.Zero;
        protected Color mColor = Color.White;

        protected int mWidth;
        protected int mHeight;
        protected Vector2 mScale = Vector2.One;
        protected Vector2 mOrigin = Vector2.Zero;
        protected float mRotation;

        protected string mAssetName = "x";
        protected Texture2D mTexture;
        protected ContentManager mContentManager;
        protected float mLayerDepth;

        protected bool mVisible = true ;
        protected bool mEnabled = true;

        

        #endregion

        #region Properties
        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }
        public override string ToString()
        {
            return mName;
        }
        public float xPos
        {
            get { return mPosition.X; }
        }
        public float yPos
        {
            get { return mPosition.Y; }
        }
        public Vector2 Position
        { get { return mPosition; } }

        public int width
        { get { return mWidth; } }

        public int height
        { get { return mHeight; } }
        #endregion

        #region Constructor
        public Actor(string theAssetName,ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth)            
        {
            mAssetName = theAssetName;
            mWidth = theWidth;
            mHeight = theHeight;
            mLayerDepth = theLayerDepth;
            mContentManager = thecontentManager;
            LoadContent(mContentManager);
        }
        public Actor(string theAssetName, ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth, Vector2 thePosition)
            :this(theAssetName, thecontentManager, theWidth, theHeight, theLayerDepth)
        {
            mPosition=thePosition;           
            
        }
        public Actor(string theName, string theAssetName, ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth)
            :this(theAssetName, thecontentManager, theWidth, theHeight, theLayerDepth)
        {
            mName=theName;          
        }
        public Actor(string theName, string theAssetName, ContentManager thecontentManager, int theWidth, int theHeight, float theLayerDepth, Vector2 thePosition)
            :this(theAssetName, thecontentManager, theWidth, theHeight, theLayerDepth)
        {
            mName=theName;
            mPosition=thePosition;
                    }

        #endregion

        #region Methods
        public virtual void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;
            mTexture = mContentManager.Load<Texture2D>(mAssetName);
            mOrigin.X = (float)mTexture.Width / 2;
            mOrigin.Y = (float)mTexture.Height / 2;

            mScale.X = (float)mWidth / (float)mTexture.Width;
            mScale.Y = (float)mHeight / (float)mTexture.Height;
            Initialise();
        }

        protected virtual void Initialise() { }

       
        #endregion
    }
}
