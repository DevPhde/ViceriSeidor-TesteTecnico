using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.Exceptions
{
    public class ConflictException : Exception
    {
        public ConflictException(string error) : base(error) { }
    }
}
