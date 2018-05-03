using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TownMusicEvents;

namespace Recommender
{
    public class Test
    {
        public void TestPearson()
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var artistRepository = unitOfWork.GetRepository<Artist>();
                var userRepository = unitOfWork.GetRepository<User>();
                var u1 = userRepository.Add(new User { Name = "Luiza" });
                var u2 = userRepository.Add(new User { Name = "Vlad" });
                var u3 = userRepository.Add(new User { Name = "George" });
                var u4 = userRepository.Add(new User { Name = "Ioana" });

                var u5 = userRepository.Add(new User { Name = "Robin and the backstabbers" });
                var u6 = userRepository.Add(new User { Name = "The Kriptonite Sparks" });
                var u7 = userRepository.Add(new User { Name = "George" });
                var u8 = userRepository.Add(new User { Name = "Ioana" });

            }
        }
    }
}
