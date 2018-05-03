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
        public List<GenreModelForSelector> GetAllGenres()
        {
            var genreModels = new List<GenreModelForSelector>();
            using (var unitOfWork = new UnitOfWork())
            {
                var genreRepository = unitOfWork.GetRepository<Genre>();
                foreach (var genre in genreRepository.GetAll())
                {
                    genreModels.Add(item: new GenreModelForSelector { id = genre.Id, value = genre.Name, label = genre.Name });
                }
                return genreModels;
            }
        }

        public List<GenreModelForSelector> GetGenresForArtist(int artistId)
        {
            var genreModels = new List<GenreModelForSelector>();
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var artist = artistRepository.Find(artistId);
                var genres = artist.User.Genres;
                foreach (var genre in genres)
                {
                    genreModels.Add(item: new GenreModelForSelector { id = genre.Id, value = genre.Name, label = genre.Name });
                }
                return genreModels;
            }
        }

        public List<GenreModelForSelector> GetGenresForFan(int fanId)
        {
            var genreModels = new List<GenreModelForSelector>();
            using (var unitOfWork = new UnitOfWork())
            {
                var fanRepository = unitOfWork.GetRepository<Fan>();
                var fan = fanRepository.Find(fanId);
                var genres = fan.User.Genres;
                foreach (var genre in genres)
                {
                    genreModels.Add(item: new GenreModelForSelector { id = genre.Id, value = genre.Name, label = genre.Name });
                }
                return genreModels;
            }
        }
    }
}