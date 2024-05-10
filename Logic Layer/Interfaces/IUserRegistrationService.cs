using System.Collections.Generic;
using System.Threading.Tasks;
using Database_Layer.DatabaseEntities;
using Database_Layer.DataServices;
using Logic_Layer.ViewModels;

namespace Logic_Layer.Interfaces
{
    public interface IUserRegistrationService
    {
        Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password);
        Task<bool> AuthenticateUserAsync(string email, string password);
        Task<bool> UserExistsAsync(string email);
        Task<int> GetUserIdByEmailAsync(string email);
    }
}
