using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementAPI.Exceptions
{
    public class DuplicateException :ApplicationException
    {
        public DuplicateException(string message) : base(message) { }
    }
}
    