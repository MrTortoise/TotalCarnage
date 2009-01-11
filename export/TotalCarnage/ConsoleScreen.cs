using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TotalCarnage
{
	/// <summary>
	/// This class is for the drop down console that is triggered by teh tilda key
	/// </summary>
	public class ConsoleScreen:GameScreen 
	{

		private Texture2D mBackGroundImage;	  		
		private SpriteFont mSpriteFont;

		private Rectangle mConsoleRectangle;

		protected List<string> mMessages;
		protected int mBuffer;

	

		public ConsoleScreen(  )
		{
			
			TransitionOnTime = TimeSpan.FromSeconds(1.5);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);

			IsPopup = true;

			//LoadContent();

		}

		/// <summary>
		/// sets the buffer size of the console in lines
		/// </summary>
		public int Buffer
		{
			get { return mBuffer; }
			set
			{
				if (value < 1)
					value = 1;
				mBuffer = value;
			}
		}
			

		public void WriteToConsole(string Message)
		{
			mMessages.Add(Message);
			if (mMessages.Count > mBuffer)
			{
				for (int i = 0; i < mMessages.Count; i++)
				{
					if (mMessages[i] != null)
					{
						mMessages.RemoveAt(i);
						return;
					}		   
				}
			}				   
		}



		public override void LoadContent()
		{
			ContentManager cm = ScreenManager.Game.Content;

			mConsoleRectangle=new Rectangle(0,0,ScreenManager.Game.GraphicsDevice.Viewport.Width, ScreenManager.Game.GraphicsDevice.Viewport.Height / 2);
			mSpriteFont = cm.Load<SpriteFont>("gamefont");
			mBackGroundImage = cm.Load<Texture2D>("StainlessSteel");

			
		}   

		public override void HandleInput(InputState input)
		{
			if (input.ToggleConsole == true)
			{
				if (ScreenState == ScreenState.Active)
					ScreenState = ScreenState.Hidden;
				else
					if (ScreenState == ScreenState.Hidden)
						ScreenState = ScreenState.Active;
			}
			
				
				


		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch Sprite = ScreenManager.SpriteBatch;
			Sprite.Begin();
			Sprite.Draw(mBackGroundImage, mConsoleRectangle, Color.White);
			Sprite.End();


		}

	}
}
