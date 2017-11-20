using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class GenreService
    {
        public List<GenreModelForSelector> GetAllCategories()
        {
            var genreModels = new List<GenreModelForSelector>();
            using (var unitOfWork = new UnitOfWork())
            {
                var genreRepository = unitOfWork.GetRepository<Genre>();
                foreach (var genre in genreRepository.GetAll())
                {
                    genreModels.Add(new GenreModelForSelector { id = genre.Id, value = genre.Name, label = genre.Name });
                }
                return genreModels;
            }
        }
    }
}