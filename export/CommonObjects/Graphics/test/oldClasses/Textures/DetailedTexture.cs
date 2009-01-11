using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
    /// <summary>
    /// This is a specific texture that will be drawn
    /// will comtain the paramaters efeecting how a general texture willb e drawn
    /// eg rate of frame switching
    /// </summary>
    public class DetailedTexture : IGameLoadable 
    {
        protected int mID;
        protected GeneralTexture mTexture;
        protected Color mColor;     // controls transpanrency   
        protected string mName;

        // TODO Implement a function to cycle through the various images (aka rows/columns / source rectangles)
        // TODO Figure out how multiple anoimation paths would ahve to be handled ... texture state management?

        public DetailedTexture(int ID, string Name, GeneralTexture  theTexture, Color theColor)
        {
            mID = ID;
            mName = Name;
            mTexture = theTexture;
            mColor = theColor;                 
        }

        /// <summary>
        /// returns the Id of te DetailedTexture
        /// </summary>
        /// <returns>The Id as int</returns>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// Returns the GeneralTexture that the DetailedTexture Refers To
        /// </summary>
        /// <returns>GeneralTexture</returns>
        public GeneralTexture Texture
        { get { return mTexture; } }

        /// <summary>
        /// returns the colour value for the texture (holds alpha channel also)
        /// </summary>
        /// <returns>Color</returns>
        public Color Color
        { get { return mColor; } }

        /// <summary>
        /// retrns the name of the detailed texture
        /// </summary>
        /// <returns>the name as string</returns>
        public string Name
        { get { return mName; } }

        /// <summary>
        /// returns the name of the detailed texture
        /// </summary>
        /// <returns>the name</returns>
        public override string ToString()
        {
            return Name;
        }



        #region IGameLoadable Members

        public void Load(GraphicsDevice theGraphicsDevice)
        {
            mTexture.Load(theGraphicsDevice);
        }

        #endregion
    }
}
