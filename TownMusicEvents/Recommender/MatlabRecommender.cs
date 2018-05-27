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
        public static void SaveDataToFile()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                using (StreamWriter writetext = new StreamWriter("rating-data.txt"))
                {
                    foreach (var r in ratingRepository.GetAll())
                    {
                        writetext.WriteLine(r.FanId + "," + r.ArtistId + "," + r.Score);
                    }
                }
            }
        }

        public static void AppendNewLineInFile(int fanId, int artistId, int score)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                using (StreamWriter writetext = new StreamWriter("rating-data.txt",true))
                {
                        writetext.WriteLine(fanId + "," + artistId + "," + score);
                }
            }
        }

        public static List<Artist> GetRecommendations(Fan fan, List<Artist> artists, int neighborhoodSize, int noOfRecommendations)
        {
            List<Artist> recommendedArtists = new List<Artist>();
            MLApp.MLApp matlab = new MLApp.MLApp();
            matlab.Execute(@"cd C:\Users\Luiza");
            object result = null;

            // Call the MATLAB function myfunc
            matlab.Feval("getRecommendations", 1, out result, fan.FanId,3);

            // Display result 
            object[] ids = result as object[];
            List<int> artistIds = ((IEnumerable)ids[0]).Cast<int>().ToList();

            foreach (var artistId in artistIds.Take(noOfRecommendations))
            {
                recommendedArtists.Add(artists.Where(a=>a.ArtistId==artistId && 
                a.User.Role == (int)RolesEnum.ARTIST).FirstOrDefault());
            }
            return recommendedArtists;
        }



    }
}
