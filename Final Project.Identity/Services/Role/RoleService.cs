
using Final_Project.Identity.Enums;
using Final_Project.Identity.Models;
using Final_Project.Identity.Services.Account;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

 namespace Final_Project.Identity.Services.Role
{
    public class RoleService : IRoleService
    {
        private IAccountService _accountService;
        private RoleManager<IdentityRole> _roleManager;
        public RoleService(IAccountService accountService,
                                   RoleManager<IdentityRole> roleManager)
        {
            _accountService = accountService;
            _roleManager = roleManager;

        }
        private async Task CreateRoleAsync(string name)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(name));
                var message = result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public async Task CreateRolesAsync()
        {
            foreach (var name in Enum.GetNames(typeof(RolesEnum)))
            {
                await CreateRoleAsync(name);
            }
        }

        public async Task ChangeRoleAsync(string id, string isAdmin)
        {
            AppUser user = await _accountService.FindByIdAsync(id);
            if (user != null)
            {
                if (isAdmin == "true")
                {

                    await _accountService.RemoveFromRoleAsync(user, "Librarian");
                    await _accountService.AddToRoleAsync(user, "User");

                }
                else
                {
                    try
                    {
                        await _accountService.AddToRoleAsync(user, "Librarian");
                        await _accountService.RemoveFromRoleAsync(user, "User");
                    }
                    catch (Exception ex)
                    {

                        throw;
                    }


                }
            }
        }
    }
}
