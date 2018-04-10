using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Validation
{
    public class InvalidModelException : Exception
    {
        private string message;
        public InvalidModelException(string message) : base(message)
        {
            this.message = message;
        }
    }
}