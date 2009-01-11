using System;

namespace Custom.Exceptions
{
    public class ArgumentAlreadyExistsException : ArgumentException 
    {
        public ArgumentAlreadyExistsException()
        {         }

        public ArgumentAlreadyExistsException(string message)
            :base(message)
        {}

        public ArgumentAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        { }


    }
}
