using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CommonObjects.Controls
{
	public class FocusMessageArgs  :EventArgs 
	{
		
		public Vector2 mousePosition;
		public Camera Camera;

		public FocusMessageArgs(Vector2 theMousePosition, Camera theCamera)
		{
		
			mousePosition = theMousePosition;
			this.Camera = theCamera;
		}
	}
}
