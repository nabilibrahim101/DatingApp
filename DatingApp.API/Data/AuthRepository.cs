using System;
using System.Threading.Tasks;
using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        //Now this particular repository going to be responsible for actually querying our database via entity framework.
        // And in here is where we're gonna inject our data context.
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> Login(string username, string password)
        {
            //FirstOrDefaultAsync will return null if it didn't find the user.
            //But "FirstAsync" will return an exception if it didn't find the user.
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            //if we return "null" from this method then our controller will return (401 Unauthorized)
            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;
            
            return user;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i<computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            // we are using the out keyword because we need to send it as reference not as values
            // when it gets updated inside the 'CreatePasswordHsh' method it will also be updated here in the variables as well.
            CreatePasswordHsh(password, out passwordHash, out passwordSalt); 

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHsh(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; // random generated key.
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            
        }
        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }

        /*
            Now that we have our repository we need to tell our application about it and we want to make this repository
            available to other classes in our application.
            So that means we're going to need to add it to our services container and then we'll be able to inject
            our repository into other classes in our app.
         */
    }
}