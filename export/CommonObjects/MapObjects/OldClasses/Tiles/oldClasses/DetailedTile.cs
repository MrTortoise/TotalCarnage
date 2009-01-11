using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace CommonObjects
{
    /// <summary>
    /// this will control the animation of the general tile and its texture
    /// does this by specifying source rectangle
    /// I know this is useless now
    /// </summary> 
    public class DetailedTile 
    {
        protected int mID;
        protected string mName;
        protected GeneralTile  mGeneralTile;
        protected Vector2 mSourcerectangle;



        public DetailedTile(int ID, string Name, GeneralTile theGeneralTile)
        {
            mID = ID;
            mName = Name;
            mGeneralTile = theGeneralTile;
            // TODO source rectange - needs calculating
        }

        /// <summary>
        /// Returns the id of the Tile
        /// </summary>
        public int ID
        { get { return mID; } }

        /// <summary>
        /// returns the name of the Tile
        /// </summary>
        public string Name
        { get { return mName; } }

        /// <summary>
        /// gets the general tile for the detailsed tile
        /// </summary>
        /// <returns></returns>
        public GeneralTile Get()
        {
            return mGeneralTile;
        }

        /// <summary>
        /// updates the tile (does any animation)
        /// </summary>
        /// <param name="theGameTime"></param>
        public void Update(GameTime theGameTime)
        {
            // TODO - iterates the animations
        }

        /// <summary>
        /// loads the initial animation state of the tile
        /// will also tellt he textures to load properly?
        /// </summary>
        /// <param name="theContentManager"></param>
        public void Load(GraphicsDevice theGraphicsDevice)
        {
            mGeneralTile.Load(theGraphicsDevice);
            
            // TODO - sets up the initial animation state and parameters
            // TODO - establish wether detailedTile.Load needs to load textures also

        }
        
    


}
}
