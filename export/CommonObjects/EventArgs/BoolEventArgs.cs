using System;

namespace CommonObjects
{
	public class BoolEventArgs : EventArgs 
		
	{

		protected bool mOldValue;
		protected bool mNewValue;

		public BoolEventArgs(bool theOldValue, bool theNewValue)
		{
			mOldValue = theOldValue;
			mNewValue = theNewValue;
		}

		public bool OldValue
		{
			get { return mOldValue; }
			set { mOldValue = value; }
		}

		public bool NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}

	}
}
