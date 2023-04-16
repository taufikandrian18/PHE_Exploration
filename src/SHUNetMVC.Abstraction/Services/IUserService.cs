using SHUNetMVC.Abstraction.Model.Response;
using System.Threading.Tasks;

namespace SHUNetMVC.Abstraction.Services
{
    public interface IUserService
    {
        string GetCurrentUserName();
        Task<User> GetCurrentUserInfo();
        Task<User> GetUserInfo(string username);
    }
}
