using API.Models;
using API.Validation;
using Domain;
using Domain.Enums;
using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;
using WebAPI.Models;

namespace API.Services
{
    public class SignUpService
    {
        public User SignUpFan(string name, string password, string email)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                if (userRepository.GetAll().Where(usr => usr.Email == email).Count() != 0)
                {
                    throw new InvalidModelException("Exista deja un utilizator cu aceasta adresa de email.");
                }
                var fanRepository = unitOfWork.GetRepository<Fan>();
                User user = new User { Name = name, Password = password, Email = email, Role=0 };
                UserValidator userValidator = new UserValidator();
                var checkResult = userValidator.Check(user);
                if (checkResult.Count != 0)
                {
                    throw new InvalidModelException(String.Join("\n", checkResult.ToArray()));
                }
                User addedUser = userRepository.Add(user);
                unitOfWork.Save();
                fanRepository.Add(new Fan { FanId = addedUser.Id });
                unitOfWork.Save();
                return addedUser;
            }
        }

        public User SignUpArtist(string name, string password, string email, List<GenreModelForSelector> genres)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                if (userRepository.GetAll().Where(usr => usr.Email == email).Count() != 0)
                {
                    throw new InvalidModelException("Exista deja un utilizator cu aceasta adresa de email.");
                }
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var genreRepository = unitOfWork.GetRepository<Genre>();
                User user = new User { Name = name, Password = password, Email = email, Role = 1 };
                UserValidator userValidator = new UserValidator();
                var checkResult = userValidator.Check(user);
                if (checkResult.Count != 0)
                {
                    throw new InvalidModelException(String.Join("\n", checkResult.ToArray()));
                }
                User addedUser = userRepository.Add(user);
                unitOfWork.Save();
                List<Genre> mappedGenres = new List<Genre>();
                foreach(var genre in genres)
                {
                    var foundGenre = genreRepository.Find(genre.id);
                    addedUser.Genres.Add(foundGenre);
                }
                FileOperations.SaveGenreDataToFile();
                artistRepository.Add(new Artist { ArtistId = addedUser.Id, PictureUrl="/images/user.jpg" });
                unitOfWork.Save();
                return addedUser;
            }
        }


        public void PostQuizAnswers(int fanId, List<ArtistLastFmModel> ratings, List<GenreModelForSelector> genres)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var genreRepository = unitOfWork.GetRepository<Genre>();
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                User foundUser = userRepository.Find(fanId);
                foreach(var genre in genres)
                {
                    var foundGenre = genreRepository.Find(genre.id);
                    foundUser.Genres.Add(foundGenre);
                }
                FileOperations.SaveGenreDataToFile();
                foreach (var rating in ratings)
                {
                    if (artistRepository.GetAll().Where(artist => artist.LastFmId == rating.ArtistId).ToList().Count == 0) 
                    {
                        var addedUser = userRepository.Add(new User {Name = rating.Name, Role = (int)RolesEnum.LASTFMARTIST});
                        unitOfWork.Save();
                        artistRepository.Add(new Artist { ArtistId = addedUser.Id, LastFmId = rating.ArtistId });
                        unitOfWork.Save();
                    }
                    var foundArtist  = artistRepository.GetAll().Where(artist => artist.LastFmId == rating.ArtistId).First();
                    ratingRepository.Add(new Rating { ArtistId = foundArtist.ArtistId, FanId = fanId, Score = rating.Score, Date=DateTime.Now });
                }
                unitOfWork.Save();

            }
        }
    }
}