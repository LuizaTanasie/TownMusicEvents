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
        public List<GenreModel> GetAllCategories()
        {
            var genreModels = new List<GenreModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var genreRepository = unitOfWork.GetRepository<Genre>();
                foreach (var genre in genreRepository.GetAll())
                {
                    genreModels.Add(new GenreModel { Id = genre.Id, Name = genre.Name });
                }
                return genreModels;
            }
        }
    }
}