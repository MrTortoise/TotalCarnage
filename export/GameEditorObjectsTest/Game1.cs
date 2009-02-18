using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using CommonObjects.VectorDrawing;
using CommonObjects;
using CommonObjects.Controls;

namespace GameEditorObjectsTest
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Microsoft.Xna.Framework.Game
	{
		GraphicsDeviceManager mGraphics;
		SpriteBatch mSpriteBatch;
		EventManager mEventManager;
		ControlManager mControlManager;


		GameControl mControl;
		

		public Game1()
		{
			mGraphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			this.IsMouseVisible = true;
			base.Initialize();

			
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			mEventManager = new EventManager();
			mControlManager = new ControlManager(mEventManager);

			// Create a new SpriteBatch, which can be used to draw textures.
			mSpriteBatch = new SpriteBatch(GraphicsDevice);
			VectorDraw.GraphicsDevice = GraphicsDevice;
			VectorDraw.Load();

			mControl = new GameControl(0, "test");

			mControl.BackColor = Color.Wheat;
			mControl.BorderColor = Color.Black;
			mControl.Position = new Vector2(500, 100);
			mControl.Size = new Vector2(300, 75);

			mControlManager.AddControl(mControl);

			GameControl gc2 = new GameControl(1, "test2");

			gc2.BackColor = Color.Red;
			gc2.BorderColor = Color.Black;
			gc2.Position = new Vector2(10, 100);
			gc2.Size = new Vector2(300, 300);

			mControlManager.AddControl(gc2);

			

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			mEventManager.ProcessInput();
			// Allows the game to exit
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
				this.Exit();

			// TODO: Add your update logic here

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			mControlManager.Draw(new DrawingArgs(mGraphics.GraphicsDevice , new Camera(new Vector2(0, 0), mGraphics.GraphicsDevice )));

			/*spriteBatchArgs sba = new spriteBatchArgs(mSpriteBatch);

			mSpriteBatch.Begin();


			
			/*
			vectorDraw.DrawLine(new Vector2(100, 100), new Vector2(100, 0), Color.Black, sba);
			vectorDrawer.DrawRectangleEdge(new Vector2(100,300),new Vector2(150,100),Color.Black,1,sba);			
			vectorDrawer.DrawRectangleFilled(new Vector2(100, 500), new Vector2(150, 100), Color.Black, sba);

			mControl.Draw(sba);
			 * 
			 * 
			mSpriteBatch.End();
			   */

			

			// TODO: Add your drawing code here

			base.Draw(gameTime);
		}
	}
}
