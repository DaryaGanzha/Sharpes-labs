using System;

namespace Isu.Tools
{
    public class IsuException : Exception
    {
        public IsuException()
        {
        }

        public IsuException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }

        public IsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}