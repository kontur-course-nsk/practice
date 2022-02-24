using System;

namespace Multithreading
{
    internal sealed class AlreadyStartedException : Exception
    {
        public AlreadyStartedException(string serviceName) : base($"{serviceName} already started.")
        {
        }
    }
}
