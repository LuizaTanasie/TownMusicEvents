using API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Security;

namespace API.Controllers
{
    public class RecommendationController : ApiController
    {
        public IHttpActionResult GetSearchResults(int fanId)
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
            var service = new RecommendationService();
            var artists = service.GetRecommendedArtists(fanId);
            if (artists.Count == 0)
                return NotFound();
            return Ok(artists);
        }
    }
}