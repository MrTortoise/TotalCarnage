using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// does this list really need the diagonals?
    /// they cause mroe trouble than they are worth ... 
    /// holding 3 directino keys at once for example
    /// </summary>
    public enum eDirection
    {
        up = 1,
        upRight = 2,
        right = 3,
        downRight = 4,
        down = 5,
        downLeft = 6,
        Left = 7,
        upLeft = 8
    }
}
