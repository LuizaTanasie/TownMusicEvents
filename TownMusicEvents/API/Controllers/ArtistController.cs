using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;

namespace API.Controllers
{
    public class ArtistController : ApiController
    {
        public IHttpActionResult Get()
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
            var service = new ArtistService();
            var artists = service.GetAllArtists();
            if (artists.Count == 0)
                return NotFound();
            return Ok(artists);
        }

        public IHttpActionResult Get(int id)
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
            var service = new ArtistService();
            var artist = service.GetArtist(id);
            if (artist == null)
                return NotFound();
            return Ok(artist);
        }
    }
}