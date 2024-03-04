using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CacauShow.API.Data.Context;
using CacauShow.API.Domain.Interfaces;
using CacauShow.API.Domain.Models;

namespace CacauShow.API.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext db) : base(db)
        {

        }

        
    }
}
