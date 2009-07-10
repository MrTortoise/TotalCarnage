using System;

namespace CommonObjects
{
    public interface IGameUpdateable : IGameObject 
    {
        void Update(UpdateArgs theUpdateArgs);

		bool IsActive
		{ get; }

		void SetActive(bool value);

    }
}
