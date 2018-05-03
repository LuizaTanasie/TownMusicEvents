using API.Models;
using API.Validation;
using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace API.Services
{
    public class RatingService
    {
        public Rating AddRating(int artistId, int fanId, int score)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                Rating foundRating = ratingRepository.GetAll().Where(r => r.ArtistId == artistId && r.FanId == fanId).FirstOrDefault();
                Rating newRating = null;
                if (foundRating != null)
                {
                    foundRating.Score = score;
                    foundRating.Date = DateTime.Now;
                    ratingRepository.Update(foundRating);
                    newRating = foundRating;  
                }
                else if (foundRating == null)
                {
                    newRating = ratingRepository.Add(new Rating { ArtistId = artistId, FanId = fanId, Score = score, Date = DateTime.Now });
                }
                unitOfWork.Save();
                return newRating;
            }

        }

        public RatingModel GetRating(int artistId, int fanId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                Rating foundRating = ratingRepository.GetAll().Where(r => r.ArtistId == artistId && r.FanId == fanId).FirstOrDefault();
                Rating newRating = null;
                if (foundRating != null)
                {
                    return new RatingModel { ArtistId = artistId, FanId = fanId, Score = foundRating.Score };
                }
                else
                {
                    return new RatingModel { ArtistId = artistId, FanId = fanId, Score = -1 };
                }
            }

        }

        public ArtistStatisticsModel GetNoOfGoodRatingsForArtist(int artistId, int when)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                if (when == (int)WhenEnum.LASTWEEK)
                {
                    var lastWeek = DateTime.Today.AddDays(-7);
                    int numberOfVisits = ratingRepository.GetAll().Where(r => r.ArtistId == artistId 
                    && r.Date >= lastWeek && r.Score>=3).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTMONTH)
                {
                    var lastMonth = DateTime.Today.AddDays(-30);
                    int numberOfVisits = ratingRepository.GetAll().Where(r => r.ArtistId == artistId
                    && r.Date >= lastMonth && r.Score >= 3).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                if (when == (int)WhenEnum.LASTYEAR)
                {
                    var lastYear = DateTime.Today.AddDays(-356);
                    int numberOfVisits = ratingRepository.GetAll().Where(r => r.ArtistId == artistId 
                    && r.Date >= lastYear && r.Score >= 3).Count();
                    return new ArtistStatisticsModel { ArtistId = artistId, HowMany = numberOfVisits, When = when };
                }
                throw new InvalidModelException("Invalid time period");
            }
        }
    }
}