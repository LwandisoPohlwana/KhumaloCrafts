using Logic_Layer.Interfaces;
using Database_Layer.DatabaseEntities;
using Database_Layer.DataServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Logic_Layer.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly ApplicationDbContext _context;

        public UserRegistrationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(string firstName, string lastName, string email, string password)
        {
            // Check if the email is already in use
            if (await _context.Users.AnyAsync(u => u.Email == email))
            {
                // Email is already registered
                return false;
            }

            // Create a new user
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password  // Note: You should properly hash and salt the password
            };

            // Add the user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Registration successful
            return true;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
        public async Task<bool> AuthenticateUserAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user != null;
        }
        public async Task<int> GetUserIdByEmailAsync(string email)
        {
            // Query the database to find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                // Return the user ID if the user is found
                return user.UserId;
            }
            else
            {
                // Handle the case where the user is not found
                // For example, you could return a default user ID or throw an exception
                throw new InvalidOperationException("Unable to retrieve the user ID.");
            }
        }
    }
}
