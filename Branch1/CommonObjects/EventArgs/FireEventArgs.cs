using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// This will eventually hold the type of ammo (class)
    /// and the ammo modifier (class) this will be added to the
    /// fired shots manager.
    /// </summary>
    public class FireEventArgs : EventArgs
    {
        public int i = 1;
    }
}
