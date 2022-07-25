using Microsoft.EntityFrameworkCore;
using ParanaBancoWeb.Mappers;
using ParanaBancoWeb.Models;
using ParanaBancoWeb.ViewModels;

namespace ParanaBancoWeb.Application
{
    public class UsersApplication : IUsersApplication
    {
        private readonly ApplicationContext _context;
        public UsersApplication(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<UserViewModel> CreateUser(UserSignUp userView)
        {
            var user = MapperUser.ToUser(userView);
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return MapperUser.ToUserViewModel(user);
        }

        public Task DeleteUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if(user == null)
            {
                return Task.CompletedTask;
            }

            _context.Remove(user);
            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }

        public async Task<List<UserViewModel>> GetAll()
        {
            var users = await _context.Users.AsNoTracking().ToListAsync();
            return MapperUser.ToUsersViewModel(users);
        }

        public async Task<UserViewModel> GetUser(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return null;

            return MapperUser.ToUserViewModel(user);
        }

        public Task<UserViewModel> UpdateUser(UserViewModel userView)
        {
            var user = MapperUser.ToUser(userView);
            _context.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChangesAsync();

            return Task.FromResult(MapperUser.ToUserViewModel(user));
        }
    }
}
