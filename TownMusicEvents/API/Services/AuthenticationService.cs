using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace WebAPI.Services
{
    public class AuthenticationService
    {
        public User LogIn(string email, string password)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var genreRepository = unitOfWork.GetRepository<User>();
                User foundUser = genreRepository.GetAll().Where(user => user.Email == email && user.Password == password).FirstOrDefault();
                return foundUser;
            }
        }
    }
}