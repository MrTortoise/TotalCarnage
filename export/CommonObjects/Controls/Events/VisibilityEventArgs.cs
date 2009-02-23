using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public class VisibilityEventArgs : EventArgs
    {
		protected bool mOldVisibility;
		protected bool mNewVisibility;

        public VisibilityEventArgs(bool theOldVisibility,bool theNewVisibility)
        {
			mOldVisibility = theOldVisibility;
			mNewVisibility = theNewVisibility;
        }

		public bool OldVisibility
		{
			get { return mOldVisibility; }
			set { mOldVisibility = value; }
		}
		public bool NewVisibility
		{
			get { return mNewVisibility; }
			set { mNewVisibility = value; }
		}
    }
}
