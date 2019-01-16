using API.Models;
using Domain;
using Recommender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace API.Services
{
    public class RecommendationService
    {
        public List<RecommendedArtist> GetRecommendedArtists(int fanId)
        {
            var artistModels = new List<ArtistModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var fanRepository = unitOfWork.GetRepository<Fan>();
                Fan foundFan = fanRepository.Find(fanId);
                MatlabRecommender mr = new MatlabRecommender();
                var results = mr.GetRecommendations(foundFan, artistRepository.GetAll().ToList(), 4, 3);
                return results;
            }
        }
    }
}