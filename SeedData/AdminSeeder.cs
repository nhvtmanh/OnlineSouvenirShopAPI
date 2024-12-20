using Microsoft.AspNetCore.Identity;
using OnlineSouvenirShopAPI.Models;

namespace OnlineSouvenirShopAPI.SeedData
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminUser(UserManager<AppUser> userManager)
        {
            var email = "admin@gmail.com";

            var adminUser = await userManager.FindByEmailAsync(email);
            if (adminUser == null)
            {
                var user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    FullName = "Online Souvenir Shop Admin",
                    Address = "VN",
                    AvatarUrl = "string",
                    Gender = false
                };
                string password = "Admin123@";
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
