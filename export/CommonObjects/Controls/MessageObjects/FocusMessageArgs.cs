using System;
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

		public FocusMessageArgs(Vector2 theMousePosition)
		{									 		
			mousePosition = theMousePosition;			
		}
	}
}
