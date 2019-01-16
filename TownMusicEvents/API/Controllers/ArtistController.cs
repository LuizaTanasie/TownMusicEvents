using API.Models;
using API.Services;
using API.Validation;
using Domain.Enums;
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
            var rnd = new Random();
            var artists = service.GetAllArtists().OrderBy(item => rnd.Next()).ToList();
            if (artists.Count == 0)
                return NotFound();
            return Ok(artists);
        }

        [Route("api/artist/search")]
        public IHttpActionResult GetSearchResults(string artistName)
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
            var artists = service.SearchForArtist(artistName);
            if (artists.Count == 0)
                return NotFound();
            return Ok(artists);
        }

        public IHttpActionResult GetRatedArtistsByAFan(int fanId)
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
            var artists = service.GetRatedArtistsByAFan(fanId);
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

        public IHttpActionResult Put([FromBody]ArtistModel artistModel)
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
                if (!jwt.VerifyTokenAndRole(token,(int)RolesEnum.ARTIST))
                {
                    return Ok(new { errorCode = "66", message = "unauthorized" });
                }
            }
            var service = new ArtistService();
            try
            {
                var artist = service.UpdateArtist(artistModel);
                return Ok(artist);
            }
            catch(NotFoundException ex)
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