﻿using API.Models;
using API.Services;
using API.Validation;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Security;

namespace API.Controllers
{
    public class SignUpController : ApiController
    {

        [HttpPost]
        public IHttpActionResult SignUpFan([FromBody] User user)
        {
            var signUpService = new SignUpService();
            try
            {
                User addedUser = signUpService.SignUpFan(user.Name, user.Password, user.Email);
                var _authToken = new JwtToken();
                var token = _authToken.CreateJwt("MusicApp", addedUser.Id, addedUser.Name,
                    addedUser.Role, 10000);
                return Ok(token);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/signup/artist")]
        public IHttpActionResult SignUpArtist([FromBody] dynamic artist)
        {
            var signUpService = new SignUpService();
            try
            {
                List<GenreModelForSelector> genres = artist.genres.ToObject<List<GenreModelForSelector>>();
                User addedUser = signUpService.SignUpArtist(artist.Name.ToString(), artist.Password.ToString(),
                    artist.Email.ToString(), genres);
                var _authToken = new JwtToken();
                var token = _authToken.CreateJwt("MusicApp", addedUser.Id, addedUser.Name,
                    addedUser.Role, 10000);
                return Ok(token);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("api/signup/quiz")]
        public IHttpActionResult PostQuizAnswers([FromBody] dynamic answers)
        {
            var headers = Request.Headers;
            var token = headers.GetValues("token").First();
            JwtToken jwt = new JwtToken();
            int fanId = jwt.GetIdFromToken(token);

            List<ArtistLastFmModel> ratings = answers.ratings.ToObject<List<ArtistLastFmModel>>();
            List<GenreModelForSelector> genres = answers.genres.ToObject<List<GenreModelForSelector>>();
            try
            {
                var signUpService = new SignUpService();
                signUpService.PostQuizAnswers(fanId, ratings, genres);
            }
            catch (InvalidModelException ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(new { code = "200", message = "Quiz successful" });
           
        }
    }
}