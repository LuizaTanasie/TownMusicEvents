using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;

namespace WebAPI.Services
{
    public class GenreController : ApiController
    {
        public IHttpActionResult Get()
        {
            var service = new GenreService();
            var genres = service.GetAllGenres();
            if (genres.Count == 0)
                return NotFound();
            return Ok(genres);
        }

        public IHttpActionResult GetGenresForArtist(int idArtist)
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
            var service = new GenreService();
            var genres = service.GetGenresForArtist(idArtist);
            return Ok(genres);
        }
    }
}