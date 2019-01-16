using API.Models;
using API.Validation;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TownMusicEvents;

namespace API.Services
{
    public class UserService
    {

        public void UpdatePassword(int id, string oldPassword, string newPassword)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var user = userRepository.Find(id);
                if (user == null)
                {
                    throw new NotFoundException("User not in database");
                }
                if (user.Password == oldPassword)
                {
                    user.Password = newPassword;
                    userRepository.Update(user);
                    unitOfWork.Save();
                }
                else throw new InvalidModelException("Parola veche introdusa este incorecta.");
                PasswordValidator passwordValidator = new PasswordValidator();
                var checkResult = passwordValidator.Check(newPassword);
                if (checkResult.Count != 0)
                {
                    throw new InvalidModelException(String.Join("\n", checkResult.ToArray()));
                }
            }
        }

        public UserModel Get(int id)
        {

            using (var unitOfWork = new UnitOfWork())
            {
                var userRepository = unitOfWork.GetRepository<User>();
                var user = userRepository.Find(id);
                if (user == null)
                {
                    return null;
                }
                return new UserModel
                {
                    Id=user.Id,
                    Name=user.Name,
                    Email=user.Email
                };

            }
        }

    }
}
