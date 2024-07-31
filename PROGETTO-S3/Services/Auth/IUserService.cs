using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.Auth
{
    public interface IUserService
    {
        public Task<User> Login(User user);
        public Task<User> Register(User user);

    }
}
