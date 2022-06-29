using Final_Project.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Identity.Services.Account
{
    public interface IAccountService
    {
        public Task SignOutAsync();
        public Task<SignInResult> LoginResultAsync(LoginModel loginmodel);
        public Task<IdentityResult> RegisterResultAsync(RegisterModel registerModel);
        public Task<IdentityResult> CreateAsync(CreateModel createModel);
        public Task<IdentityResult> UpdateAsync(EditModel editModel);
        public Task<IdentityResult> RemoveAsync(string id);
        public Task<AppUser> FindByEmailAsync(string email);
        public Task<AppUser> FindByIdAsync(string id);
        public Task ForgotPasswordEmailAsync(string email, string callback);
        public Task ConfirmedAccountEmailAsync(string email, string callback);
        public Task<string> GeneratePasswordTokenAsync(string email);
        public Task<string> GeneratePasswordResetTokenAsync(AppUser user);
        //Email Confirmation
        public Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
        public Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token);
        public Task<bool> IsEmailConfirmedAsync(AppUser user);

        public Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel);
        public Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string pass);
        public Task<AppUser> GetUser(ClaimsPrincipal user);
        public string GetUserId(ClaimsPrincipal user);
        public Task AddToRoleAsync(AppUser user, string name);
        public Task<bool> IsInRoleAsync(AppUser user, string role);

        public Task RemoveFromRoleAsync(AppUser user, string name);
  
        // Edit Datatable
        public Task<IdentityResult> UpdateDataAsync(EditModelData editModeldata);
    }
}
