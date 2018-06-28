using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    class Program
    {
        static void Main(string[] args)
        {
            GenresInit.InitializeGenresInDB();
            /*
            FileOperations.SaveGenreDataToFile();
            using (var unitOfWork = new UnitOfWork())
            {
                var genresRepository = unitOfWork.GetRepository<User>();
                User f = genresRepository.Find(57);
                MatlabRecommender.GetRecommendationsOnGenre(f);
                //genresInit.InitializeGenresInDB();
            }*/
        }
    }
}
