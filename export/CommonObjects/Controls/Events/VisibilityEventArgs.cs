using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public class VisibilityEventArgs : EventArgs
    {
        public bool visibility;

        public VisibilityEventArgs(bool theVisibility)
        {
            visibility = theVisibility;
        }
    }
}
