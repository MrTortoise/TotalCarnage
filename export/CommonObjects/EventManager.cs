using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonObjects
{
	public enum MouseButtonState
	{
		Depressed,
		Pressed,
		Held
	}

	public class EventManager
	{
		#region Fields
		private MouseState mOldMouseState;
		private MouseState mCurrentMouseState;

		private KeyboardState mOldKeyboardState;
		private KeyboardState mCurrentKeyboardState;

		private List<Keys> mPressedKeys;
		private List<Keys> mHeldKeys;
		private List<Keys> mDepressedKeys;

		private MouseButtonState mLeftMouseButton;
		private MouseButtonState mMiddleMouseButton;
		private MouseButtonState mRightNouseButton;

		private int mNewScrolledDistance;

		private int mPrevMouseScrollWheel;
		private int mCurrentMouseScrollWheel;

		private Vector2 mOldMousePosition;
		private Vector2 mNewMousePosition;
		#endregion

		#region Constructor



		#endregion

		#region Methods

		protected void ProcessKeys()
		{
			mOldKeyboardState = mCurrentKeyboardState;
			mOldMouseState = mCurrentMouseState;

			mCurrentKeyboardState = Keyboard.GetState();
			mCurrentMouseState = Mouse.GetState();

			FindNewKeyPresses();

		}

		protected void FindNewKeyPresses()
		{
			// new key press is one that is not in held or new key presses
			List<Keys> heldKeys = new List<Keys>();
			List<Keys> newPressedKeys = new List<Keys>();
			List<Keys> depressedKeys = new List<Keys>();


			foreach (Keys k in mCurrentKeyboardState.GetPressedKeys())
			{
				// Test if key was already being held - if so add to new held list
				// if not then test is in the last pressed list - if so add to held list
				// if not then it is a new key press
				if (mHeldKeys.Contains(k))
				{
					heldKeys.Add(k);
				}
				else if (mPressedKeys.Contains(k))
				{
					heldKeys.Add(k);
				}
				else
				{
					newPressedKeys.Add(k);
				}
			}
			//need a list of all the old jkeys that were pressed in order to 
			//compare to current pressed keys
			// this will tell us which keys are now newly depressed
			List<Keys> allOldKeyPress = new List<Keys>();

			foreach (Keys k in mHeldKeys)
			{
				allOldKeyPress.Add(k);
			}
			foreach (Keys k in mPressedKeys)
			{
				allOldKeyPress.Add(k);
			}

			// if the oldKeysPressed contains a key that is not in current keys pressed
			// then we know its a newly depressed key
			// if its not held or newly pressed, then its depressed
			// if not currently presed then dd to depressed
			foreach (Keys k in allOldKeyPress)
			{
				bool Contains = false;
				foreach (Keys cK in mCurrentKeyboardState.GetPressedKeys())
				{
					if (cK == k)
						Contains = true;
				}
				if (Contains == false)
				{
					depressedKeys.Add(k);
				} 
			}

			//parse the mouse
			// have to do each contorl seperatley

			if (mCurrentMouseState.LeftButton == ButtonState.Pressed)
			{
				if (mLeftMouseButton == MouseButtonState.Depressed)
				{
					mLeftMouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mLeftMouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mLeftMouseButton == MouseButtonState.Held) || (mLeftMouseButton == MouseButtonState.Pressed))
				{
					mLeftMouseButton = MouseButtonState.Depressed;
				}												  
			}

			if (mCurrentMouseState.MiddleButton == ButtonState.Pressed)
			{
				if (mMiddleMouseButton == MouseButtonState.Depressed)
				{
					mMiddleMouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mMiddleMouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mMiddleMouseButton == MouseButtonState.Held) || (mMiddleMouseButton == MouseButtonState.Pressed))
				{
					mMiddleMouseButton = MouseButtonState.Depressed;
				}
			}

			if (mCurrentMouseState.RightButton == ButtonState.Pressed)
			{
				if (mRightNouseButton == MouseButtonState.Depressed)
				{
					mRightNouseButton = MouseButtonState.Pressed;
				}
				else
				{
					mRightNouseButton = MouseButtonState.Held;
				}
			}
			else
			{
				if ((mRightNouseButton == MouseButtonState.Held) || (mRightNouseButton == MouseButtonState.Pressed))
				{
					mRightNouseButton = MouseButtonState.Depressed;
				}
			}

			//ToDo: Mouse wheel and cursor position



			//todo: raise the events for the input

			mPressedKeys = newPressedKeys;
			mHeldKeys = heldKeys;
			mDepressedKeys = depressedKeys;
		}
			
			

		
		#endregion


		#region Events

		public EventHandler<InputEventArgs> MouseButtonPressed;
		public EventHandler<InputEventArgs> MouseButtonReleased;

		public EventHandler<InputEventArgs> MouseWheelScrolled;

		public EventHandler<InputEventArgs> KeyPressed;
		public EventHandler<InputEventArgs> KeyReleased;

		#region events Methods

		protected void RaiseEvent(EventHandler<InputEventArgs> theEvent)
		{
			//Copy to be threadsafe
			EventHandler<InputEventArgs> temp = theEvent;
			if (temp != null)
				temp(this, CreateInputEventArgs());
		}

		protected virtual InputEventArgs CreateInputEventArgs()
		{
			InputEventArgs retVal = new InputEventArgs();

			retVal.CurrentMouseScrollWheel = mCurrentMouseScrollWheel;

			retVal.LeftMouseButton = mLeftMouseButton;
			retVal.MiddleMouseButton = mMiddleMouseButton;
			retVal.RightNouseButton = mRightNouseButton;

			retVal.HeldKeys = mHeldKeys;
			retVal.NewPressedKeys = mPressedKeys;
			retVal.NewDepressedKeys = mDepressedKeys;

			retVal.NewMousePosition = mNewMousePosition;
			retVal.OldMousePosition = mOldMousePosition;

			return retVal;
		}
		#endregion

		#endregion





	}
}
