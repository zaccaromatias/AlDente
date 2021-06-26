using System;

namespace AlDente.Contracts.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {

        }

        public DomainException() : base()
        {

        }
    }
}
