using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserAuthentication.Models
{
    public class User
    {
        //[Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[JsonProperty( PropertyName = "userId")]

        //public string UserId { get; set; }


        [Key]
        [JsonProperty(PropertyName = "username")]
        public string username { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        //[JsonProperty(PropertyName = "firstName")]
        //public string FirstName { get; set; }

        //[JsonProperty(PropertyName = "lastName")]
        //public string LastName { get; set; }

        //[JsonProperty(PropertyName = "role")]
        //public string Role { get; set; }

        //[JsonProperty(PropertyName ="addedDate")]
        //public DateTime AddedDate { get; set; }
    }
}
