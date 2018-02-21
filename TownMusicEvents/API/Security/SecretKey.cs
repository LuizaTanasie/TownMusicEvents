using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Security
{
    public class SecretKey
    {
        private readonly String secretKey = "licenta";

        public string GetSecretKey()
        {
            return this.secretKey;
        }
    }
}