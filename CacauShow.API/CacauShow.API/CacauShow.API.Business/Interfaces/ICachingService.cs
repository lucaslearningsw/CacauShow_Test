using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacauShow.API.Domain.Interfaces
{
    public interface ICachingRepository
    {
       Task<T> GetValue<T>(Guid id);

       Task<IEnumerable<T>> GetCollection<T>(string collectionKey);

       Task SetValue<T>(Guid id, T obj);

       Task SetCollection<T>(IEnumerable<T> collection, string collectionKey);

       Task DeleteValue<T>(Guid id);

    }
}
