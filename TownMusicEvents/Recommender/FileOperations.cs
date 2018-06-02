using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    public static class FileOperations
    {
        public static void SaveRatingDataToFile()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                using (StreamWriter writetext = new StreamWriter(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\rating-data.txt"))
                {
                    foreach (var r in ratingRepository.GetAll())
                    {
                        writetext.WriteLine(r.FanId + "," + r.ArtistId + "," + r.Score);
                    }
                }
            }
        }


        public static void AppendNewRatingLineToFile(int fanId, int artistId, int score)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var ratingRepository = unitOfWork.GetRepository<Rating>();
                using (StreamWriter writetext = new StreamWriter(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\rating-data.txt", true))
                {
                    writetext.WriteLine(fanId + "," + artistId + "," + score);
                }
            }
        }

        //!this is worng
        public static void AppendNewGenreLineToFile(User artist)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                using (StreamWriter writetext = new StreamWriter(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\rating-data.txt", true))
                {
                    foreach(Genre g in artist.Genres)
                    {
                        writetext.WriteLine(artist.Id+","+g.Id);
                    }
                }
            }
        }

        public static void SaveGenreDataToFile()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var genresRepository = unitOfWork.GetRepository<Genre>();
                using (StreamWriter writetext = new StreamWriter(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\genre-data.txt"))
                {
                    foreach (var u in userRepository.GetAll())
                    {
                        if (u.Role == (int)RolesEnum.ARTIST && u.Genres.Count!=0)
                        {
                            foreach (Genre g in genresRepository.GetAll())
                            {
                                if (u.Genres.Contains(g))
                                {
                                    writetext.WriteLine(u.Id + "," + g.Id + "," + 1);
                                }
                                else
                                {
                                    writetext.WriteLine(u.Id + "," + g.Id + "," + 0);
                                }
                                
                            }
                        }
                    }
                }
            }
        }

    }
}
