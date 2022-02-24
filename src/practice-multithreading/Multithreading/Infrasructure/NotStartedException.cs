using System;

namespace Multithreading
{
    internal sealed class NotStartedException : Exception
    {
        public NotStartedException(string serviceName) : base($"{serviceName} not started.")
        {
        }
    }
}
