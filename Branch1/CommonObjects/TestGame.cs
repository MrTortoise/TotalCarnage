using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CommonObjects
{
    /// <summary>
    /// simply creates a game object with some accessors
    /// to suck references in order to test objects
    /// </summary>
    public class TestGame : Game 
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        GraphicsDevice device;

        public TestGame() 
        {
            graphics = new GraphicsDeviceManager(this);
            device = graphics.GraphicsDevice;
            Content.RootDirectory = "Content";
            Initialize();
            LoadContent();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>

            public GraphicsDeviceManager GraphicsDeviceManager
            {get{return graphics;}}

          //  public GraphicsDevice GraphicsDevice
          //  { get { return device; } }

            protected override void Initialize()
            {
                // TODO: Add your initialization logic here
                this.IsMouseVisible = true;
                base.Initialize();
            }

            protected override void LoadContent()
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                //  spriteBatch = new SpriteBatch(GraphicsDevice);

                base.LoadContent();
            }
        
    }
}
