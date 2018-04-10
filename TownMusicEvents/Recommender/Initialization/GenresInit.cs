using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace Recommender
{
    public class GenresInit
    {
        public void InitializeGenresInDB()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var genresRepository = unitOfWork.GetRepository<Genre>();
                try
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\Recommender\Initialization\genres.json"))
                    {
                        String line = sr.ReadToEnd();
                        dynamic array = JsonConvert.DeserializeObject(line);
                        foreach (var item in array)
                        {
                            genresRepository.Add(new Genre { Name = item });
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                unitOfWork.Save();
            }
        }
        
                
    }
}