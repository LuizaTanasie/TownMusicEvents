using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAPI.Services
{
    public class GenreController : ApiController
    {
        public IHttpActionResult Get()
        {
            var service = new GenreService();
            var genres = service.GetAllCategories();
            if (genres.Count == 0)
                return NotFound();
            return Ok(genres);
        }
    }
}