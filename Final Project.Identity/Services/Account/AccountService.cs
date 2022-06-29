using Final_Project.Identity.Helper;
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
        public class AccountService : IAccountService
        {
            private UserManager<AppUser> _userManager;
            private SignInManager<AppUser> _signinManager;
            private readonly IMailSender _mailSender;
            private IPasswordHasher<AppUser> _passwordHasher;
            public AccountService(UserManager<AppUser> userManager,
                                       SignInManager<AppUser> signinManager, IMailSender mailSender, IPasswordHasher<AppUser> passwordHasher)
            {
                _userManager = userManager;
                _signinManager = signinManager;
                _mailSender = mailSender;
                _passwordHasher = passwordHasher;

            }
            public async Task SignOutAsync()
            {
                await _signinManager.SignOutAsync();
            }

            public async Task<SignInResult> LoginResultAsync(LoginModel loginmodel)
            {
                AppUser user = FindByEmailAsync(loginmodel.Email).Result;
                if (user != null)
                {
                    SignInResult result = await _signinManager.PasswordSignInAsync(user,
                           loginmodel.Password, true, false);
                    return result;
                }
                return null;
            }

            public async Task<IdentityResult> RegisterResultAsync(RegisterModel registerModel)
            {
                AppUser user = new AppUser
                {
                    UserName = registerModel.Username,
                    Email = registerModel.Email,
                };
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, "User");
                    return result;
                }
                return null;
            }

            public async Task<AppUser> FindByEmailAsync(string email)
            {
                return await _userManager.FindByEmailAsync(email);
            }

            public async Task ForgotPasswordEmailAsync(string email, string callback)
            {

                var message = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Admin Panel",
                    Body = $"Reset password token : {callback}",
                    Attachments = null
                };
                await _mailSender.SendEmailAsync(message);
            }

            public async Task<string> GeneratePasswordTokenAsync(string email)
            {
                return await _userManager.GeneratePasswordResetTokenAsync(FindByEmailAsync(email).Result);
            }


            public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string pass)
            {
                return await _userManager.ResetPasswordAsync(user, token, pass);
            }

            public async Task<AppUser> GetUser(ClaimsPrincipal user)
            {
                return await GetCurrentUserAsync(user);
            }
            private Task<AppUser> GetCurrentUserAsync(ClaimsPrincipal user)
            {

                return _userManager.GetUserAsync(user);
            }

            public string GetUserId(ClaimsPrincipal user)
            {
                var id = _userManager.GetUserId(user);
                return id;
            }

            public async Task<AppUser> FindByIdAsync(string id)
            {
                return await _userManager.FindByIdAsync(id);
            }

            public async Task AddToRoleAsync(AppUser user, string name)
            {
                var result = await _userManager.AddToRoleAsync(user, name);
            }

            public async Task RemoveFromRoleAsync(AppUser user, string name)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, name);
            }


            public async Task<string> GeneratePasswordResetTokenAsync(AppUser user)
            {
                return await _userManager.GeneratePasswordResetTokenAsync(user);
            }

            public async Task<IdentityResult> CreateAsync(CreateModel createModel)
            {
                AppUser user = new AppUser
                {
                    Fullname = createModel.Fullname,
                    UserName = createModel.Email,
                    Email = createModel.Email,
                };
                var result = await _userManager.CreateAsync(user, createModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");

                }
                return result;
            }

            public async Task<IdentityResult> UpdateAsync(EditModel editModel)
            {
                AppUser user = await FindByIdAsync(editModel.Id);
                if (user != null)
                {
                    user.UserName = editModel.Email;
                    user.Email = editModel.Email;
                user.Fullname = editModel.Fullname;
                    /*if (editModel.IsAdmin == true)
                    {
                        if (IsInRoleAsync(user, "Admin").Result != true)
                        {
                            await AddToRoleAsync(user, "Admin");
                        }
                    }
                    else
                    {
                        if (IsInRoleAsync(user, "Admin").Result == true)
                        {
                            await RemoveFromRoleAsync(user, "Admin");
                        }
                    }*/
                    var result = await _userManager.UpdateAsync(user);

                    return result;

                }
                return null;
            }

            public async Task<IdentityResult> RemoveAsync(string id)
            {
            AppUser user = await FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        return result;
                    }
                }
                return null;
            }

            public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
            {
                return await _userManager.GenerateEmailConfirmationTokenAsync(user);
            }

            public async Task ConfirmedAccountEmailAsync(string email, string callback)
            {
                var message = new MailRequest
                {
                    ToEmail = email,
                    Subject = "Admin Panel",
                    Body = $"Please, confirm your email : {callback}",
                    Attachments = null
                };
                await _mailSender.SendEmailAsync(message);
            }

            public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
            {
                return await _userManager.ConfirmEmailAsync(user, token);
            }

            public Task<bool> IsEmailConfirmedAsync(AppUser user)
            {
                return _userManager.IsEmailConfirmedAsync(user);
            }


            public async Task<IdentityResult> ChangePasswordAsync(ChangePasswordModel changePasswordModel)
            {
                AppUser user = await FindByIdAsync(changePasswordModel.Id);
                IdentityResult result = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
                return result;
            }

            public async Task<bool> IsInRoleAsync(AppUser user, string role)
            {
                return await _userManager.IsInRoleAsync(user, role);
            }


            public async Task<IdentityResult> UpdateDataAsync(EditModelData editModelData)
            {
            AppUser user = await FindByIdAsync(editModelData.Id);
                if (user != null)
                {
                    user.UserName = editModelData.userName;
                    user.Email = editModelData.email;
                    if (editModelData.isAdmin == true)
                    {
                        if (IsInRoleAsync(user, "Librarian").Result != true)
                        {
                            await AddToRoleAsync(user, "Librarian");
                        }
                    }
                    else
                    {
                        if (IsInRoleAsync(user, "Librarian").Result == true)
                        {
                            await RemoveFromRoleAsync(user, "Librarian");
                        }
                    }
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return result;
                    }
                }
                return null;
            }
        }
}
