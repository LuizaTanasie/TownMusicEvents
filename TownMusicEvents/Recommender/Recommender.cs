using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    public class Recommender
    {
        public double CosineCorrelation(Fan activeFan, Fan anotherFan)
        {
            double numerator = 0;
            var allFavoredArtists = activeFan.Artists.Union(anotherFan.Artists);
            foreach (var artist in allFavoredArtists)
            {
                int activeFanRating = 0;
                int anotherFanRating = 0;
                if (activeFan.Artists.Contains(artist))
                {
                    activeFanRating = 1;
                }
                if (anotherFan.Artists.Contains(artist))
                {
                    anotherFanRating = 1;
                }
                numerator = numerator + activeFanRating * anotherFanRating;
            }
            double denominator = 0;
            double squareRoot1 = 0;
            foreach (var artist in allFavoredArtists)
            {
                int activeFanRating = 0;
                if (activeFan.Artists.Contains(artist))
                {
                    activeFanRating = 1;
                }
                squareRoot1 = squareRoot1 + activeFanRating * activeFanRating;
            }
            squareRoot1 = Math.Sqrt(squareRoot1);

            double squareRoot2 = 0;
            foreach (var artist in allFavoredArtists)
            {
                int anotherFanRating = 0;
                if (anotherFan.Artists.Contains(artist))
                {
                    anotherFanRating = 1;
                }
                squareRoot2 = squareRoot2 + anotherFanRating * anotherFanRating;
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

        public Dictionary<Fan, double> FindNearestKNeighbors(Fan activeFan, int k)
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

        public double CFpredictedPreference(Fan activeFan, Artist artist, int k)
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
                var term = CosineCorrelation(activeFan, pair.Key);
                if (pair.Key.Artists.Contains(artist))
                {
                    Rbi = 1;
                }
                term = term * (Rbi - 1); // 1-average rating of b
            }
            var result = 1 + 1 / neighborhoodPreferenceAbs + neighborhoodPreference; //first 1-average rating of a
            return result;

        }

    }
}
