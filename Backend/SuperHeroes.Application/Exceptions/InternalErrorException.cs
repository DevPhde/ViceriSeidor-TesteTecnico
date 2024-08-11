using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException(string error) : base(error) { }
    }
}
