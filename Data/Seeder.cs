using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TradeApp.Data
{
    public static class Seeder
    {
        public static void Seed(UserManager<IdentityUser> _userManager,
            RoleManager<IdentityRole> _roleManager)
        {
            SeedRoles(_roleManager);
            SeedUsers(_userManager);
        }

        private static void SeedRoles(RoleManager<IdentityRole> _roleManager)
        {
            ICollection<string> _roles = new List<string>()
            {
                "Admin",
                "Customer"
            };

            foreach(var s in _roles)
            {
                if(_roleManager.RoleExistsAsync(s).Result)
                {
                    continue;
                }
                var role = new IdentityRole
                {
                    Name = s
                };
                var result = _roleManager.CreateAsync(role).Result;
            }

        }

        private static void SeedUsers(UserManager<IdentityUser> _userManager)
        {
            ICollection<UserInfo> _usersinfo = new List<UserInfo>()
            {
                new UserInfo()
                {
                    FName="Akhilesh",
                    LName = "Mishra",
                    UserName = "Admin_Akhi",
                    Email = "ashishmishra6510@gmail.com",
                    Password ="546ufgufg@@&4GDU",
                    Role = "Admin",
                },
                new UserInfo()
                {
                    FName="Ashish",
                    LName = "Mishra",
                    UserName = "Admin_Ashish",
                    Email = "ashishcurocity@gmail.com",
                    Password ="hfheUFH46^(*^(",
                    Role = "Admin",
                }
            };

            foreach(var userinfo in _usersinfo)
            {
                if (_userManager.FindByNameAsync(userinfo.UserName)
                    .Result != null) continue;

                var user = new User()
                {
                    UserName = userinfo.UserName,
                    FirstName = userinfo.FName,
                    LastName = userinfo.LName,
                    NormalizedUserName = userinfo.UserName.Normalize(),
                    Email = userinfo.Email,
                };
                
                var result = _userManager.CreateAsync(user, userinfo.Password).Result;
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(user, userinfo.Role.Normalize()).Wait();
                }
            }
        }
    }
}
