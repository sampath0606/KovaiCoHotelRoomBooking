﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserAuthentication.Models;

namespace UserAuthentication.Service
{
    public interface IAuthService
    {
        bool RegisterUser(User user);
        User LoginUser(string userId, string password);
        bool IsUserExists(string userId);
    }
}
