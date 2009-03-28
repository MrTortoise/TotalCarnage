using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects.Controls
{
	class ControlAddedEventArgs	   : EventArgs 
	{
		protected GameControl mControl;
		public GameControl Control
		{
			get { return mControl; }
			set { mControl = value; }
		}


		public ControlAddedEventArgs(GameControl theControl)
		{
			mControl = theControl;
		}

	}
}
