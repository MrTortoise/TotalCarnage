using System;

namespace Custom.Exceptions
{
    public class ArgumentsCombinationIllegalException : ArgumentException 
    {
        public ArgumentsCombinationIllegalException()
        {        }

        public ArgumentsCombinationIllegalException(string message)
            :base(message)
        {}

        public ArgumentsCombinationIllegalException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
