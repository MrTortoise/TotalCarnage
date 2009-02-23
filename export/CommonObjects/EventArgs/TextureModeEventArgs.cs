using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
	public class TextureModeEventArgs : EventArgs 
	{
		protected ETextureMode mOldValue;
		protected ETextureMode mNewValue;

		public TextureModeEventArgs(ETextureMode theOldValue, ETextureMode theNewVAlue)
		{
			mOldValue = theOldValue;
			mNewValue = theNewVAlue;
		}

		public ETextureMode OldValue
		{
			get { return mOldValue; }
			set { mOldValue = value; }
		}

		public ETextureMode NewValue
		{
			get { return mNewValue; }
			set { mNewValue = value; }
		}
		
	}
}
