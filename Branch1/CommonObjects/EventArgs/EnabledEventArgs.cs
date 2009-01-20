using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    public class EnabledEventArgs : EventArgs
    {
        public bool enabled;

        public EnabledEventArgs(bool enabledState)
        {
            enabled = enabledState;
        }
    }
}
