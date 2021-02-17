using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Repository
{
    public class AuthRepository : IAuthRepository
    {
        public readonly IAuthenticationContext authenticationContext;

        public AuthRepository(IAuthenticationContext _authenticationContext)
        {
            authenticationContext = _authenticationContext;
        }

        public User FindUserById(string userId)
        {
            return authenticationContext.Users.Find(userId);
        }

        public User LoginUser(string userId, string password)
        {
            var _user = authenticationContext.Users.FirstOrDefault(u => u.username == userId && u.Password == password);
            return _user;
        }

        public bool RegisterUser(User user)
        {
           authenticationContext.Users.Add(user);
           return authenticationContext.SaveChanges() > 0 ? true :  false ;
               
        }
    }
}
