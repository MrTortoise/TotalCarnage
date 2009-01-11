using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{

    public class UpdataOrderEventArgs : EventArgs
    {
        public int updateOrder;

        public UpdataOrderEventArgs(int theUpdataOrder)
        { updateOrder = theUpdataOrder; }
    }
}
