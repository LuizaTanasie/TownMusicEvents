using Domain;
using Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    public static class MatlabRecommender
    {

        public static List<RecommendedArtist> GetRecommendationsOnRatings(Fan fan, List<Artist> artists, int neighborhoodSize, int noOfRecommendations)
        {
            List<Artist> recommendedArtists = new List<Artist>();
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd C:\Users\Luiza");
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Feval("getRecommendations", 1, out result, fan.FanId,3);

            // Display result 
            object[] res = result as object[];
            object arr = res[0];

            double[,] pairs = (double[,])arr;
            List<RecommendedArtist> recommendations = new List<RecommendedArtist>();           

            for (int i = 0; i < pairs.Length/2; i++)
            {
                var artist = artists.Where(a => a.User.Role == (int)RolesEnum.ARTIST && a.ArtistId == pairs[i, 0]).FirstOrDefault();
                if (artist != null && pairs[i, 1]>=3)
                {
                    recommendations.Add(new RecommendedArtist
                    {
                        Id = artist.ArtistId,
                        Name = artist.User.Name,
                        Score = pairs[i, 1]*20,
                        PictureUrl = artist.PictureUrl,
                        Why = "Acest artist iti este recomandat deoarece alte persoane cu preferinte similare alor tale il apreciaza."+
                    "Valoarea estimata a notei tale pentru acest artist este " + Math.Round(pairs[i, 1], 2)+"."
                    });
                }
            }
            return recommendations.Take(noOfRecommendations).ToList();
        }

        public static List<RecommendedArtist> GetRecommendations(Fan fan, List<Artist> artists, int neighborhoodSize, int noOfRecommendations)
        {
            List<RecommendedArtist> recommendedArtists = new List<RecommendedArtist>();
            var byRating = GetRecommendationsOnRatings(fan, artists, neighborhoodSize, noOfRecommendations);
            var byGenres = GetRecommendationsOnGenre(fan.User, artists,noOfRecommendations);
            if (fan.Ratings.Count == 0 && fan.User.Genres.Count!=0 && byRating.Count!=0)
            {
                return byGenres;
            }
            else if (fan.Ratings.Count != 0 && fan.User.Genres.Count == 0 && byGenres.Count!=0)
            {
                return byRating;
            }
            if ((fan.Ratings.Count == 0 && fan.User.Genres.Count == 0) || (byGenres==null || byRating==null))
            {
                throw new Exception("Ne pare rau, nu avem suficiente date pentru a-ti recomanda artisti relevanti.");
            }
            recommendedArtists = byRating;
            foreach(var reco in byGenres)
            {
                var found = recommendedArtists.Find(r => r.Id == reco.Id);
                if (found!=null)
                {
                    found.Why += '\n' + reco.Why;
                    if (found.Score< reco.Score)
                    {
                        found.Score = reco.Score;
                    }
                }
                else
                {
                    recommendedArtists.Add(reco);
                }
            }
            recommendedArtists = recommendedArtists.OrderByDescending(x => x.Score).ToList();
            return recommendedArtists;
        }

        public static List<RecommendedArtist> GetRecommendationsOnGenre(User fan, List<Artist> artists, int noOfRecommendations)
        {
            String genresTxt = "";

            using (var unitOfWork = new UnitOfWork())
            {
                var genresRepository = unitOfWork.GetRepository<Genre>();
                foreach (Genre g in genresRepository.GetAll())
                {
                    if (fan.Genres.Where(x=>x.Id==g.Id).Count()!=0)
                    {
                        genresTxt+= fan.Id + "," + g.Id + "," + 1+";";
                    }
                    else
                    {
                        genresTxt += fan.Id + "," + g.Id + "," + 0 +";";
                    }
                }
            }
            List<Artist> recommendedArtists = new List<Artist>();
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd C:\Users\Luiza");
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Feval("genreSimilarity", 1, out result, genresTxt);

            // Display result 
            object[] res = result as object[];
            object arr = res[0];

            double[,] pairs = (double[,])arr;
            List<RecommendedArtist> recommendations = new List<RecommendedArtist>(); 
            for (int i = 0; i < pairs.Length/2; i++)
            {
                var artist = artists.Where(a => a.User.Role == (int)RolesEnum.ARTIST && a.ArtistId == pairs[i,0]).FirstOrDefault();
                if (artist != null && pairs[i, 1] * 100 > 10 && artist.Ratings.Where(r=>r.FanId==fan.Id).Count()==0) 
                {
                    recommendations.Add(new RecommendedArtist
                    {
                        Id = artist.ArtistId,
                        Name = artist.User.Name,
                        Score = pairs[i, 1]*100,
                        PictureUrl = artist.PictureUrl,
                        Why = "Acest artist iti este recomandat deoarece genurile tale muzicale preferate se potrivesc cu ale sale" +
                        " in proportie de " + Math.Round(pairs[i, 1] * 100, 2) + "%."
                    });
                }
            }
            return recommendations.Take(noOfRecommendations).ToList();
        }



    }

}
