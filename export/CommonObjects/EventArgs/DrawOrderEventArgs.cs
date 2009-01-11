using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{

    public class DrawOrderEventArgs : EventArgs
    {
        public int drawOrder;

        public DrawOrderEventArgs(int theDrawOrder)
        {
            drawOrder = theDrawOrder;
        }
    }
}
