using UserAuthentication.Exceptions;
using UserAuthentication.Models;
using UserAuthentication.Repository;
using System;

namespace UserAuthentication.Service
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository repository;

        public AuthService(IAuthRepository _repository)
        {
            this.repository = _repository;
        }
        public User LoginUser(string userId, string password)
        {
            try
            {
                var user = repository.LoginUser(userId, password);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    throw new UserNotFoundException($"User with this id {userId} and password {password} does not exist");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RegisterUser(User user)
        {
            try
            {
                var result = repository.RegisterUser(user);
                if (result)
                {
                    return result;
                }
                else
                {
                    throw new UserNotCreatedException($"User with this id {user.username} already exists");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsUserExists(string userId)
        {
            try
            {
                return repository.FindUserById(userId) != null ? true : false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
