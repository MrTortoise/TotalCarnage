using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Maths
{
    public class RotationManager
    {       

        public float GetAbsRotation(float theRotation)
        {
            if (theRotation < (-Math.PI))
            { theRotation = theRotation + (float)(Math.PI * 2); }

            if (theRotation > Math.PI)
            { theRotation = theRotation - (float)(Math.PI * 2); }

            return theRotation;
        }      


    }
}
