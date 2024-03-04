using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacauShow.API.Domain.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
    }
}
