#region Using Statements


using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


#endregion

namespace CommonObjects.Components
{
    public class ConsoleMenu : DrawableGameComponent
    {
        #region Fields

		public event EventHandler<ConsoleVisibilityEventArgs> ConsoleVisibilityChanged;

		protected void OnVisibilityToggled()
		{
			if (ConsoleVisibilityChanged != null)
			{
				ConsoleVisibilityEventArgs cv = new ConsoleVisibilityEventArgs(active);
				ConsoleVisibilityChanged(this, cv);   
			} 			
		}



        // Graphics related fields
        private ContentManager content;
        //private Texture2D      texture;
        private SpriteBatch    batch;
        private SpriteFont     font;
		private ResolveTexture2D renderTargetTexture;

        // Menu related fields
        private bool         active;
        private int          width, height;
        private string       command = "", prefix = ">", line = "------------\n", logPrefix = "--> ", message = "Type HELP to begin";
        private List<string> log = new List<string>();

        // Input related fields
        private KeyboardState currKey, prevKey;
        private Keys[]        keys = { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F, Keys.G, Keys.H, Keys.I, Keys.J, Keys.K, Keys.L, Keys.M,
                                       Keys.N, Keys.O, Keys.P, Keys.Q, Keys.R, Keys.S, Keys.T, Keys.U, Keys.V, Keys.W, Keys.X, Keys.Y, Keys.Z };


        #endregion


        #region Initialization


        public ConsoleMenu(Game game)
            : base(game)
        {
            content = new ContentManager(game.Services, "Content");
        }


        public override void Initialize()
        {
            log.Add(message);


            base.Initialize();
        }


        #endregion


        #region Graphics Content

		protected override void LoadContent()
		{
			batch = new SpriteBatch(GraphicsDevice);
			font = content.Load<SpriteFont>("freesans");

			
			renderTargetTexture = new ResolveTexture2D(
				GraphicsDevice,
				GraphicsDevice.PresentationParameters.BackBufferWidth,
				GraphicsDevice.PresentationParameters.BackBufferHeight,
				1,
				GraphicsDevice.PresentationParameters.BackBufferFormat);

			width = GraphicsDevice.Viewport.Width;
			height = GraphicsDevice.Viewport.Height / 2;
			base.LoadContent();
		}

        #endregion


        #region Update and Draw


        public override void Update(GameTime gameTime)
        {
			
				CheckInput();
				base.Update(gameTime);
			
        }


        public override void Draw(GameTime gameTime)
        {
            if (active == true)
            {
                // Saves a copy of the current screen into the texture.  This allows the menus to be transparent
				

				GraphicsDevice.ResolveBackBuffer(renderTargetTexture);

                

                batch.Begin();
				// Draw the background
				batch.Draw(renderTargetTexture, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

                // Draw transparent menu
				batch.Draw(renderTargetTexture, new Rectangle(0, 0, width, height), new Rectangle(0, 0, width, height), Color.Gray);

                // Draw command string
                batch.DrawString(font, prefix + command, new Vector2(10.0f, height - (font.MeasureString(prefix).Y /* 2.0f*/) - 4.0f), Color.White);

                // Draw log
                for (int i = 0; i < log.Count; i++)
                    batch.DrawString(font, log[i], new Vector2(10.0f, height - 34 - font.MeasureString(prefix).Y * -i - font.MeasureString(prefix).Y * log.Count), Color.Silver);

                batch.End();

				
            }

            
            base.Draw(gameTime);
        }


        #endregion


        #region Helper Functions


        private void CheckInput()
        {
            prevKey = currKey;
            currKey = Keyboard.GetState();

            // Toggle the console menu on or off
            if (currKey.IsKeyDown(Keys.OemTilde) && prevKey.IsKeyUp(Keys.OemTilde))
            {
                active = !active;
				



                // Clears the log when the menu closes
                if (active == false)
                {
                    log.Clear();
                    log.Add(message);
                }
				OnVisibilityToggled();
            }

            if (active == true)
            {
                // Check input for alphabetical letters
                for (int i = 0; i < keys.Length; i++)
                {
                    if (currKey.IsKeyDown(keys[i]) && prevKey.IsKeyUp(keys[i]))
                    {
                        string letter = keys[i].ToString();

                        if (currKey.IsKeyUp(Keys.LeftShift) && currKey.IsKeyUp(Keys.RightShift))
                            letter = letter.ToLower();

                        command += letter;
                    }
                }

                // Check input for spacebar
                if (currKey.IsKeyDown(Keys.Space) && prevKey.IsKeyUp(Keys.Space) && command != "" && command[command.Length - 1].ToString() != " ")
                    command += " ";

                // Check input for backspace
                if (currKey.IsKeyDown(Keys.Back) && prevKey.IsKeyUp(Keys.Back) && command != "" && command != prefix)
                    command = command.Remove(command.Length - 1, 1);

                // Check input for enter
                if (currKey.IsKeyDown(Keys.Enter) && prevKey.IsKeyUp(Keys.Enter) && command != "")
                {
                    log.Add("");
                    log.Add(command);
                    ExecuteCommand();
                    command = "";
                }
            }
        }


        private void ExecuteCommand()
        {
            // Checking your command string can be done many different ways.  I decided to use StartsWith and EndsWith.
            // This allows you to use ignore casing so the user can enter "Help", "hELp", "helP", "HELP", etc. and the
            // command will still work.
            //
            // The reason I used EndsWith is so the string has to be exactly "Help" for example.  "Help bladhsdas" will
            // not work.
            //
            // The following commands are just examples.  You can change them however you want.


            if (command.StartsWith("Help", true, null) && command.EndsWith("Help", true, null))
            {
                log.Add(logPrefix + "COMMANDS - Displays a list of commands");
                log.Add(logPrefix + "COMPONENTS - Displays a list of components to use with the commands");
            }
            else if (command.StartsWith("Commands", true, null) && command.EndsWith("Commands", true, null))
            {
                log.Add(logPrefix + "TITLE string - Change the title to a user defined string");
                log.Add(logPrefix + "TOGGLE component - Turns the component on or off");
                log.Add(logPrefix + "QUIT - Exits the program");
            }
            else if (command.StartsWith("Components", true, null) && command.EndsWith("Components", true, null))
            {
                log.Add(logPrefix + "Background - Displays the sunset background");
                log.Add(logPrefix + "FPS - Displays the current frames per second");
            }
            else if (command.StartsWith("Title ", true, null))
            {
                Game.Window.Title = command.Remove(0, 6);
                log.Add(logPrefix + "Title updated");
				
            }
      /*      else if (command.StartsWith("Toggle background", true, null) && command.EndsWith("Toggle background", true, null))
            {
                Main.BGActive = !Main.BGActive;
                log.Add(logPrefix + "Background: " + (Main.BGActive ? "ON" : "OFF"));
            }
            else if (command.StartsWith("Toggle FPS", true, null) && command.EndsWith("Toggle FPS", true, null))
            {
                Main.FPS.Enabled = !Main.FPS.Enabled;
                Main.FPS.Visible = !Main.FPS.Visible;
                log.Add(logPrefix + "FPS: " + (Main.FPS.Visible ? "ON" : "OFF"));
            }
	   * */
            else if (command.StartsWith("Quit", true, null) && command.EndsWith("Quit", true, null))
                Game.Exit();
            else
                log.Add(logPrefix + "Command does not exist");
        }


        #endregion
    }
}