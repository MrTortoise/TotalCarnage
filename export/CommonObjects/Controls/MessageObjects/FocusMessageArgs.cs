using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CommonObjects.Controls
{
	public class FocusMessageArgs  :EventArgs 
	{
		public EventManager eventManager;
		public Vector2 mousePosition;

		public FocusMessageArgs(EventManager theEventManager, Vector2 theMousePosition)
		{
			eventManager = theEventManager;
			mousePosition = theMousePosition; 
		}
	}
}
