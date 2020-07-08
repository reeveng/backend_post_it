using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppsAPI.Models;

namespace WebAppsAPI.Data
{
    public class DataInit
    {
        private readonly UserContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public DataInit(UserContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                User user1 = new User { Email = "govaertr@gmail.com", FirstName = "Ree", LastName = "Govaert" };

                await CreateUser(user1.Email, "YeayBijnaG1SchoolMeer@");

                User user2 = new User { Email = "randomGuy@gmail.com", FirstName = "Random", LastName = "Guy" };

                await CreateUser(user2.Email, "YeayBijnaG1SchoolMeer@");

                //User user3 = new User { Email = "Web4@gmail.com", FirstName = "Web4", LastName = "Web4" };
                //await CreateUser(user3.Email, "gelukkiggeennetbeans@");
                _dbContext.SaveChanges();
            }

        }
        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
