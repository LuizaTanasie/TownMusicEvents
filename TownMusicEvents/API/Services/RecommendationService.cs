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
        public List<ArtistModel> GetRecommendedArtists(int fanId)
        {
            var artistModels = new List<ArtistModel>();
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var fanRepository = unitOfWork.GetRepository<Fan>();
                Fan foundFan = fanRepository.Find(fanId);
                var results = MatlabRecommender.GetRecommendations(foundFan, artistRepository.GetAll().ToList(), 4, 3);
                foreach (var result in results)
                {
                    artistModels.Add(new ArtistModel
                    {
                        ArtistId = result.ArtistId,
                        Name = result.User.Name,
                        PictureUrl = result.PictureUrl
                    });
                }
                return artistModels;
            }
        }
    }
}