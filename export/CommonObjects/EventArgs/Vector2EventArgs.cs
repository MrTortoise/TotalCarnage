using System;
using Microsoft.Xna.Framework;

namespace CommonObjects
{
	class Vector2EventArgs	: EventArgs 
	{
		protected Vector2 mOldValue;
		protected Vector2 mNewValue;

		public Vector2EventArgs(Vector2 theOldValue, Vector2 theNewValue)
		{
			mOldValue = theOldValue;
			mNewValue = theNewValue;
		}

		public Vector2 OldValue
		{
			get {return  mOldValue; }
			set { mOldValue = value; }
		}

		public Vector2 NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}


	}
}
