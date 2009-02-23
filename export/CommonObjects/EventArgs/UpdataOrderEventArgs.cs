using System;

namespace CommonObjects
{

    public class UpdataOrderEventArgs : EventArgs
    {
        public int updateOrder;

        public UpdataOrderEventArgs(int theUpdataOrder)
        { updateOrder = theUpdataOrder; }
    }
}
