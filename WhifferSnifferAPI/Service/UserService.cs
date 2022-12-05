using WhifferSnifferAPI.IService;

namespace WhifferSnifferAPI.Service
{
    public class UserService : IUserService
    {
        public bool CheckUser(string username, string password)
        {
            return username.Equals("TeamOverengineered") && password.Equals("durstloescher1337!");
        }
    }
}
