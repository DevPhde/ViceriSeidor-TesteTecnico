using System;
using System.Collections.Generic;
using System.Text;

namespace SuperHeroes.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string error) : base(error) { }
    }
}
