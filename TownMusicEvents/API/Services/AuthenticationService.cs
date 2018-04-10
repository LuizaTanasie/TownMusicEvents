using Domain;
using Domain.Enums;
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
                var userRepository = unitOfWork.GetRepository<User>();
                User foundUser = userRepository.GetAll().Where(user => user.Email == email && user.Password == password && user.Role!=(int)RolesEnum.LASTFMARTIST).FirstOrDefault();
                return foundUser;
            }
        }
    }
}