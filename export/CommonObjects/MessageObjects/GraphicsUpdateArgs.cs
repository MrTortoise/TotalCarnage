using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace CommonObjects
{
	public class GraphicsUpdateArgs
	{
		public GameTime time;
		public float timeScale;

		public GraphicsUpdateArgs(GameTime theTime, float theTimeScale)
		{
			time = theTime;
			timeScale = theTimeScale;
		}
	}
}
