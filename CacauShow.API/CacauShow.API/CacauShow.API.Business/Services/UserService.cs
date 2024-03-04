using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CacauShow.API.Domain.Interfaces;
using CacauShow.API.Domain.Models;

namespace CacauShow.API.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICachingRepository _cachingRepository;
        private const string CACHE_COLLECTION_KEY = "AllUser";


        public UserService(IUserRepository userRepo, ICachingRepository cachingRepository)
        {
            _userRepository = userRepo;
            _cachingRepository = cachingRepository;
        }
        public async Task Create(User user)
        {

            var useremail = _userRepository.Find(u => u.UserEmail == user.UserEmail).Result.FirstOrDefault();

            if (useremail != null)
            {
                throw new Exception("Já existe usuário cadastro com esse email");
            }

            await _userRepository.CreateAsync(user);

           //TOD: call caching to add in list

        }

        public async Task Update(User user, Guid id)
        {

            var userdb = _userRepository.Find(u => u.Id == id).Result.FirstOrDefault();

            if (userdb == null)
            {
                throw new Exception("Usuário não encotrado");
            }

            var userWithEmailChange = _userRepository.Find(u => u.UserEmail == user.UserEmail && u.Id != id).Result.FirstOrDefault();

            if (userWithEmailChange != null)
            {
                throw new Exception("Já existe usuário cadastro com esse email");
            }

            userdb.UserEmail = user.UserEmail;
            userdb.Name = user.Name;
            userdb.City = user.City;


            await _userRepository.UpdateAsync(userdb);

            await _cachingRepository.SetValue(userdb.Id, userdb);
        }

        public async Task Delete(Guid id)
        {
            await _userRepository.DeleteAsync(id);
            await _cachingRepository.DeleteValue<User>(id);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _cachingRepository.GetValue<User>(id);

            if (user is null)
            {
                user = await _userRepository.GetById(id);
                await _cachingRepository.SetValue(id, user);
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _cachingRepository.GetCollection<User>(CACHE_COLLECTION_KEY);

            if (users is null || !users.Any())
            {
                users = await _userRepository.GetAllAsync();
                await _cachingRepository.SetCollection(users, CACHE_COLLECTION_KEY);
            }

            return await _userRepository.GetAllAsync();
        }

        public async Task<User> Login(string userName, string password)
        {
            return _userRepository.Find(u => u.UserEmail == userName && u.Password == password).Result.FirstOrDefault();
        }

        public Task Logoff()
        {
            throw new NotImplementedException();
        }
    }
}
