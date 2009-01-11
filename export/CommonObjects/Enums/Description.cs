using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonObjects
{
    /// <summary>
    /// used to allow a descrition field on enums
    /// currently used by textures to specify path
    /// </summary>
    public class Description : Attribute
    {
        public string Text;
        public Description(string text)
        { Text = text; }
    }
}
