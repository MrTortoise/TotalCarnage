using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CommonObjects.Controls
{
	public class ControlSpriteBatchArgs
	{
		public SpriteBatch theSpriteBatch;
		public int NocontrolsDrawn = 0;

		public ControlSpriteBatchArgs(SpriteBatch theBatch)
		{
			theSpriteBatch = theBatch;
		}
	}
}
