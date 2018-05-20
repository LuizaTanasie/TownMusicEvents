using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    public static class RecommenderSystem
    {

        private static double ComputeAverageRatingFan(Fan fan)
        {
            if (fan.Ratings.Count == 0)
            {
                return 0;
            }
            var average = 0;
            foreach (var rating in fan.Ratings)
            {
                average += rating.Score;
            }
            average = average / fan.Ratings.Count;
            return average;
        }


        private static double CosineCorrelation(Fan activeFan, Fan anotherFan)
        {
            double numerator = 0;
            List<Artist> activeFanFavArtists = activeFan.Ratings.Select(r => r.Artist).ToList();
            List<Artist> anotherFanFavArtists = anotherFan.Ratings.Select(r => r.Artist).ToList();
            List<Artist> allRatedArtists = activeFanFavArtists.Union(anotherFanFavArtists).ToList();
            foreach (var artist in allRatedArtists)
            {
                int activeFanRating = 0;
                int anotherFanRating = 0;
                if (activeFanFavArtists.Contains(artist))
                {
                    activeFanRating = activeFan.Ratings.Where(r => r.ArtistId == artist.ArtistId).First().Score;
                }
                if (anotherFanFavArtists.Contains(artist))
                {
                    anotherFanRating = anotherFan.Ratings.Where(r => r.ArtistId == artist.ArtistId).First().Score;
                }
                numerator = numerator + (activeFanRating - ComputeAverageRatingFan(activeFan)) * (anotherFanRating - ComputeAverageRatingFan(anotherFan));
            }
            double denominator = 0;
            double squareRoot1 = 0;
            foreach (var artist in allRatedArtists)
            {
                int activeFanRating = 0;
                if (activeFanFavArtists.Contains(artist))
                {
                    activeFanRating = activeFan.Ratings.Where(r => r.ArtistId == artist.ArtistId).First().Score;
                }
                squareRoot1 = squareRoot1 + Math.Pow(activeFanRating - ComputeAverageRatingFan(activeFan),2);
            }
            squareRoot1 = Math.Sqrt(squareRoot1);

            double squareRoot2 = 0;
            foreach (var artist in allRatedArtists)
            {
                int anotherFanRating = 0;
                if (anotherFanFavArtists.Contains(artist))
                {
                    anotherFanRating = anotherFan.Ratings.Where(r => r.ArtistId == artist.ArtistId).First().Score;
                }
                squareRoot2 = squareRoot2 + Math.Pow(anotherFanRating - ComputeAverageRatingFan(anotherFan), 2);
            }
            squareRoot2 = Math.Sqrt(squareRoot1);

            denominator = squareRoot1 * squareRoot2;
            if (denominator == 0)
            {
                return 0;
            }
            double result = numerator / denominator;
            return result;
        }

        private static Dictionary<Fan, double> FindNearestKNeighbors(Fan activeFan, int k)
        {
            Dictionary<Fan, double> similarityToOthers = new Dictionary<Fan, double>();
            using (var unitOfWork = new UnitOfWork())
            {
                var fanRepository = unitOfWork.GetRepository<Fan>();
                foreach (Fan fan in fanRepository.GetAll())
                {
                    if (fan.FanId != activeFan.FanId)
                    {
                        similarityToOthers.Add(fan, CosineCorrelation(activeFan, fan));
                    }
                }
            }
            var sortedSimilarity = similarityToOthers.ToList().OrderByDescending(pair => pair.Value).ToList();
            return sortedSimilarity.Take(k).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private static double CFpredictedPreference(Fan activeFan, Artist artist, int k)
        {
            double neighborhoodPreferenceAbs = 0;
            var neighborhood = FindNearestKNeighbors(activeFan, k);
            foreach ( var pair in neighborhood)
            {
                neighborhoodPreferenceAbs += Math.Abs(CosineCorrelation(activeFan, pair.Key));
            }
            double neighborhoodPreference = 0;
            foreach (var pair in neighborhood)
            {
                double Rbi = 0;
                if (pair.Key.Ratings.Where(r=>r.Artist == artist).Count()!=0)
                {
                    Rbi = pair.Key.Ratings.Where(r => r.ArtistId == artist.ArtistId).First().Score;
                }
                neighborhoodPreference = neighborhoodPreference + CosineCorrelation(activeFan, pair.Key) * (Rbi - ComputeAverageRatingFan(pair.Key));
            }
            var result = ComputeAverageRatingFan(activeFan) + 1 / neighborhoodPreferenceAbs + neighborhoodPreference;
            return result;
        }

        public static List<Artist> GetRecommendations(Fan fan, List<Artist> artists, int neighborhoodSize, int noOfRecommendations)
        {
            Dictionary<Artist, double> predictions = new Dictionary<Artist, double>();
            List<Artist> recommendedArtists = new List<Artist>();
            foreach (Artist artist in artists)
            {
                if (fan.Ratings.Where(r => r.ArtistId == artist.ArtistId).Count() == 0)
                {
                    predictions.Add(artist, CFpredictedPreference(fan, artist, neighborhoodSize));
                }
            }
            var sortedPredictions = predictions.ToList().OrderByDescending(pair => pair.Value).ToList();
            foreach (var pred in sortedPredictions.Take(noOfRecommendations))
            {
                recommendedArtists.Add(pred.Key);
            }
            return recommendedArtists;
        }
            
    }
}
