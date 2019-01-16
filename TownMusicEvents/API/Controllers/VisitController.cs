using API.Models;
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
    public class VisitController :  ApiController
    {
        public IHttpActionResult Post([FromBody]VisitModel model)
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
            var service = new VisitService();
            try
            {
                var visit = service.AddVisit(model.ArtistId, model.FanId, model.HasClickedALink);
                return Ok(new VisitModel
                {
                    ArtistId = visit.ArtistId,
                    HasClickedALink = visit.HasClickedALink,
                    FanId = visit.FanId,
                    Date = visit.Date
                });
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        public IHttpActionResult GetNumberOfVisitsForArtist(int artistId, int whenVisited)
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
            var service = new VisitService();
            try
            {
                var visits = service.GetNumberOfVisitsForArtist(artistId, whenVisited);
                return Ok(visits);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }
  
        }

        public IHttpActionResult GetNumberOfClickedLinksForArtist(int artistId, int whenClicked)
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
            var service = new VisitService();
            try
            {
                var links = service.GetNumberOfClickedLinksForArtist(artistId, whenClicked);
                return Ok(links);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}