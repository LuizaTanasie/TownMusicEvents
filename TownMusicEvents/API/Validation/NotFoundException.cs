using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Validation
{
    public class NotFoundException : Exception
    {
        private string message;

        public NotFoundException(string message) : base(message)
        {
            this.message = message;
        }
    }
}