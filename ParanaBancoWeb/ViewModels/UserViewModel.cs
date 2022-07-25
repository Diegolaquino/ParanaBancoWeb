namespace ParanaBancoWeb.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Nome { get; set; }
    }

    public class UserSignUp
    {
        public string Email { get; set; }

        public string Nome { get; set; }
    }


}
