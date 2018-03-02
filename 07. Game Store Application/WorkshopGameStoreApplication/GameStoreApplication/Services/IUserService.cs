namespace GameStoreApplication.Services
{
    using ViewModels.Account;
        
    public interface IUserService
    {
        bool Create(RegisterUserViewModel model);

        bool Find(LoginUserViewModel model);

        bool IsAdmin(string email);
    }
}
