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
    public class RatingController : ApiController
    {
        public IHttpActionResult GetRatingForAnArtist(int artistId, int fanId)
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
            var service = new RatingService();
            var rating = service.GetRating(artistId,fanId);
            return Ok(rating);
        }

        public IHttpActionResult Post([FromBody]RatingModel ratingModel)
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
                if (!jwt.VerifyTokenAndRole(token, (int)RolesEnum.FAN))
                {
                    return Ok(new { errorCode = "66", message = "unauthorized" });
                }
            }
            var service = new RatingService();
            try
            {
                var rating = service.AddRating(ratingModel.ArtistId, ratingModel.FanId, ratingModel.Score);
                return Ok(rating);
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

        public IHttpActionResult GetNoOfGoodRatingsForArtist(int artistId, int when)
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
            var service = new RatingService();
            try
            {
                var ratings = service.GetNoOfGoodRatingsForArtist(artistId, when);
                return Ok(ratings);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}