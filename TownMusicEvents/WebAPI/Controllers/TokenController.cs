using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;

namespace WebAPI.Controllers
{
    public class TokenController : ApiController
    {
        public IHttpActionResult Get()
        {
            var headers = Request.Headers;
            if (!headers.Contains("token"))
            {
                return Ok(new { errorCode = "66", message = "not logged in" });
            }
            if (headers.Contains("token"))
            {
                var token = headers.GetValues("token").First();
                var jwt = new JwtToken();
                if (!jwt.VerifyToken(token))
                {
                    return Ok(new { errorCode = "66", message = "unauthorized" }); ;
                }
            }
            var payload = JwtToken.Decode(headers.GetValues("token").First());
            return Ok(new { errorCode = "0", message = payload });
        }

    }

}