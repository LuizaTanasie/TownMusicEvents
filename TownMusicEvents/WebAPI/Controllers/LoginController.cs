using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    public class LoginController : ApiController
    {

        private JwtToken _authToken;

    
        [HttpPost]
        public IHttpActionResult Login([FromBody] dynamic user)
        {

                var email = user.email.ToString();
                var password = user.password.ToString();
                
                var authService = new AuthenticationService();
                var foundUser = authService.LogIn(email,password);

                if (foundUser == null)
                {
                    return BadRequest();
                }
                _authToken = new JwtToken();
                var token = _authToken.CreateJwt("MusicApp", foundUser.Id, foundUser.Name,
                    foundUser.Role,  10000);

                return Ok( token );
            


        }


    }
}