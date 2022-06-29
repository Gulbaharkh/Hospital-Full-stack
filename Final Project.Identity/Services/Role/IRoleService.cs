using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Identity.Services.Role
{
    public interface IRoleService
    {
        public Task CreateRolesAsync();
        public Task ChangeRoleAsync(string id, string isAdmin);
    }
}
