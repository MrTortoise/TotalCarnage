using System;


namespace CommonObjects
{
	public class IntEventArgs   : EventArgs 
	{
		protected int mOldValue;
		protected int mNewValue;

		public IntEventArgs(int theOldValue, int theNewValue)
		{
			mOldValue = theOldValue;
			mNewValue = theNewValue;
		}

		public int OldValue
		{
			get { return mOldValue; }
			set { mOldValue = value; }
		}

		public int NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}

	}
}
