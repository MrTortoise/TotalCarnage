using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace CommonObjects
{
    public class DrawingArgs
    {
        private GraphicsDevice mGraphicsDevice;
        private Camera mCamera;
        private SpriteBatch mSpriteBatch;
        //  public GameTime mGameTime;

        #region constructors


        public DrawingArgs(GraphicsDevice theGraphicsDevice, Camera theCamera)
        {
            if ((object)theGraphicsDevice == null)
            { throw new NullReferenceException("Tried to use a null GraphicsDevice in a drawing args"); }

            if ((object)theCamera == null)
            { throw new NullReferenceException("Tried to use a null Camera in a drawing args"); }

            mGraphicsDevice = theGraphicsDevice;
            mCamera = theCamera;
       //     mGameTime = theGameTime;
        }

        public DrawingArgs(SpriteBatch  theSpriteBatch, Camera theCamera)
        {
            if ((object)theSpriteBatch == null)
            { throw new NullReferenceException("Tried to use a null spritebatch in a drawing args"); }

            if ((object)theCamera == null)
            { throw new NullReferenceException("Tried to use a null Camera in a drawing args"); }

            mSpriteBatch  = theSpriteBatch;
            mCamera = theCamera;
            //     mGameTime = theGameTime;
        }

        #endregion

        #region properies

        public GraphicsDevice GraphicsDevice
        {
            get { return mGraphicsDevice; }
            set
            {
                if ((object)value == null)
                { throw new NullReferenceException("Tried to set GraphicsDevice to null in drawing args"); }
            }
        }

        public Camera Camera
        {
            get { return mCamera; }
            set
            {
                if ((object)Camera == null)
                { throw new NullReferenceException("Tried to set Camera to null in DrawingArgs"); }
            }
        }

        public SpriteBatch SpriteBatch
        {
            get { return mSpriteBatch; }
            set
            {
                if ((object)SpriteBatch == null)
                { throw new NullReferenceException("Tried to set SpriteBatch to Null in Drawing Args"); }
            }
        }
        #endregion

    }
}
