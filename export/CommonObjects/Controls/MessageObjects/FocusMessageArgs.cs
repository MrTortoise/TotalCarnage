﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CommonObjects.Controls
{
	public class FocusMessageArgs  :EventArgs 
	{
		/// <summary>
		/// This is the mouse position after adjustment to scale to 1000 : aspect ratio * 1000
		/// </summary>
		public Vector2 mousePosition;
		public ControlManager controlManager;
		public Vector2 viewPosition;
		public Vector2 viewArea;

		public FocusMessageArgs(Vector2 theMousePosition, ControlManager theManager,Vector2 theViewPosition,Vector2 theViewArea)
		{									 		
			mousePosition = theMousePosition;
			controlManager = theManager;
			viewPosition = theViewPosition;
			viewArea = theViewArea;
		}
	}
}
