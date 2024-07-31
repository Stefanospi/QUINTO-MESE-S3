using Microsoft.EntityFrameworkCore;
using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.Auth
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        public UserService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<User> Login(User user)
        {

            var userOn = await _dataContext.Users
                .Include(u => u.Role)
                .Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefaultAsync();

            if (userOn == null)
            {
                throw new Exception("Invalid username or password");
            }
            return userOn;
        }


        public async Task<User> Register(User user)
        {
            var role = await _dataContext.Roles.Where(r => r.IdRole == 2).FirstOrDefaultAsync();
            user.Role.Add(role);
            await _dataContext.Users.AddAsync(user);
            await _dataContext.SaveChangesAsync();
            return user;
        }
    }
}
