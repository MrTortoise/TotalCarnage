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
		public Vector2 screenPosition = Vector2.Zero;
		public Vector2 screenDimensions;

		public FocusMessageArgs(EventManager theEventManager, Vector2 theMousePosition, Vector2 theScreenPosition, Vector2 theScreenDimensions)
		{
			eventManager = theEventManager;
			mousePosition = theMousePosition;
			screenPosition = theScreenPosition;
			screenDimensions = theScreenDimensions;
		}
	}
}
