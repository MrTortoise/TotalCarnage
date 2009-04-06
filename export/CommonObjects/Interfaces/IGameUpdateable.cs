using System;

namespace CommonObjects
{
    public interface IGameUpdateable
    {
        void Update(UpdateArgs theUpdateArgs);

		bool IsActive
		{ get; }

		void SetActive(bool value);

    }
}
