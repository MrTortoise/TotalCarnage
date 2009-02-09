using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonObjects
{
	class InputEventArgs : EventArgs 
	{
		public KeyboardState OldKeyboardState;
		public KeyboardState CurrentKeyboardState;			

		public List<Keys> NewPressedKeys;
		public List<Keys> HeldKeys;
		public List<Keys> NewDepressedKeys;

		public MouseButtonState LeftMouseButton;
		public MouseButtonState MiddleMouseButton;
		public MouseButtonState RightNouseButton;

		public int NewScrolledDistance;

		public int PrevMouseScrollWheel;
		public int CurrentMouseScrollWheel;

		public Vector2 OldMousePosition;
		public Vector2 NewMousePosition;

	}
}
