using System;

namespace CommonObjects
{
	public interface IGraphicsUpdateable  : IGameObject 
	{
		void UpdateGraphics(GraphicsUpdateArgs  theArgs);
		bool IsGraphicsActive
		{ get; }

		void SetGraphicsActive(bool value);
	}
}
