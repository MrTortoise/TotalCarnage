using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Text;

namespace CommonObjects.Controls
{
	public class ControlPositionDimsArgs : EventArgs 
	{
		public Vector2 Position;
		public Vector2 Dimensions;

		public ControlPositionDimsArgs(Vector2 thePosition, Vector2 theDims)
		{
			Position = thePosition;
			Dimensions = theDims;

		}

	}
}
