using API.Services;
using API.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;

namespace API.Controllers
{
    public class UserController : ApiController
    {



        public IHttpActionResult PutNewPassword(int idUser, string oldPassword, string newPassword)
        {
            var headers = Request.Headers;
            if (!headers.Contains("token"))
            {
                return Ok(new { errorCode = "66", message = "unauthorized" });
            }
            if (headers.Contains("token"))
            {
                var token = headers.GetValues("token").First();
                var jwt = new JwtToken();
                if (!jwt.VerifyToken(token))
                {
                    return Ok(new { errorCode = "66", message = "unauthorized" });
                }
            }
            var service = new UserService();
            try
            {
                service.UpdatePassword(idUser,oldPassword,newPassword);
                return Ok(new { code = "200", message = "password updated" });
            }
            catch (NotFoundException ex)
            {
                return NotFound();
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}