using MoviesApp.DTO;

namespace MoviesApp.Services.UserRepo
{
    public interface IUserService
    {
        bool RegisterUser(UserRegistration userRegistration);
        bool IsEmailAlreadyExist(string email);
        string LoginUser(UserLogin userLogin);
    }
}
