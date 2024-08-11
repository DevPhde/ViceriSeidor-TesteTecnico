using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string error) : base(error) { }
    }
}
