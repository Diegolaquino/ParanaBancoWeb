using ParanaBancoWeb.ViewModels;

namespace ParanaBancoWeb.Application
{
    public interface IUsersApplication
    {
        public Task<UserViewModel> GetUser(string email);

        public Task<UserViewModel> UpdateUser(UserViewModel user);

        public Task<List<UserViewModel>> GetAll();

        public Task DeleteUser(string email);

        public Task<UserViewModel> CreateUser(UserSignUp user);
        
    }
}