
using CacauShow.API.Domain.Models;


namespace CacauShow.API.Domain.Interfaces
{
    public interface IUserService
    {
        Task Create(User user);
        Task Update(User user, Guid id);
        Task Delete(Guid id);

        Task<User> GetById(Guid id);
        Task<IEnumerable<User>> GetAll();
        Task<User> Login(string UserName, string PassWord);

        Task Logoff();
    }
}
