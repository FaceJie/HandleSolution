﻿

namespace SharedHelper.Exceptions
{
    public abstract class DomainException: System.Exception
    {
        public DomainException(string message) : base(message)
        {}
    }
}
