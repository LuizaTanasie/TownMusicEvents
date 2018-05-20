using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace RecommenderTest
{
    public class SetUpDatabase
    {
        public void SetUpFans()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var fanRepository = unitOfWork.GetRepository<Fan>();
                try
                {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\RecommenderTest\usersha1-profile.tsv"))
                    {
                        String line = sr.ReadToEnd();
                        var attributes = line.Split('\t');
                        User fanUser = userRepository.Add(new User { });
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void SetUpRatings()
        {
                using (var unitOfWork = new UnitOfWork())
                {
                    var userRepository = unitOfWork.GetRepository<User>();
                    var artistRepository = unitOfWork.GetRepository<Artist>();
                try
                    {
                    using (StreamReader sr = new StreamReader(@"C:\Users\Luiza\Source\Repos\TME\TownMusicEvents\RecommenderTest\usersha1-artmbid-artname-plays.tsv"))
                    {
                        String line = sr.ReadToEnd();
                        var attributes = line.Split('\t');
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
