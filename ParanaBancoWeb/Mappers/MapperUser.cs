using ParanaBancoWeb.Models;
using ParanaBancoWeb.ViewModels;

namespace ParanaBancoWeb.Mappers
{
    public static class MapperUser
    {

        public static User ToUser(UserSignUp user)
        {
            return new User()
            {
                Email = user.Email,
                Nome = user.Nome
            };
        }
        public static UserViewModel ToUserViewModel(User user)
        {
            return new UserViewModel() {
                Id = user.Id,
                Email = user.Email,
                Nome = user.Nome
            };
        }

        public static User ToUser(UserViewModel user)
        {
            return new User()
            {
                Id = user.Id,
                Email = user.Email,
                Nome = user.Nome
            };
        }

        public static List<UserViewModel> ToUsersViewModel(List<User> users)
        {
            var usersViewModel = new List<UserViewModel>(); 

            foreach (var user in users)
            {
                usersViewModel.Add(new UserViewModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Nome = user.Nome
                });
            }

            return usersViewModel;
        }


    }
}
