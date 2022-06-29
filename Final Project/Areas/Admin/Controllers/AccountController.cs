using Final_Project.Identity.Models;
using Final_Project.Identity.Services.Account;
using Final_Project.Identity.Services.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AccountController : Controller
    {
        //!nTh3dawgHs
        private IAccountService _accountService;
        private IRoleService _roleService;
        private UserManager<AppUser> _userManager;
        public AccountController(IAccountService accountService, UserManager<AppUser> usrMgr, IRoleService roleService)
        {
            _accountService = accountService;
            _roleService = roleService;
            _userManager = usrMgr;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login()
        
        {
            await _roleService.CreateRolesAsync();
            CreateModel model = new CreateModel()
            {
                Fullname = "Test",
                Email = "test@gmail.com",
                Username = "test@gmail.com",
                Password = "!nTh3dawgHs"
            };
            await _accountService.CreateAsync(model);
            await _accountService.SignOutAsync();
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginResultAsync(loginModel);

                if (result != null)
                {
                        if (result.Succeeded)
                        {
                            AppUser user = await _accountService.FindByEmailAsync(loginModel.Email);

                            return RedirectToAction("index", "dashboard");
                        
                        }

                  
                }
                else
                {
                    ModelState.AddModelError(nameof(LoginModel.Email),
                    "Invalid user or password");
                }
            }
            return View(loginModel);
        }

        [AllowAnonymous]
        public IActionResult SignOut()
        {
            _accountService.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterModel userModel)
        {
            if (ModelState.IsValid)
            {
                await _roleService.CreateRolesAsync();
                var result = _accountService.RegisterResultAsync(userModel);
                if (result != null)
                {
                    if (result.Result != null)
                    {
                        if (result.Result.Succeeded)
                        {
                            AppUser user = await _accountService.FindByEmailAsync(userModel.Email);
                            var token = await _accountService.GenerateEmailConfirmationTokenAsync(user);
                            var callback = Url.Action("ConfirmEmail", "Account", new { token, email = user.Email }, Request.Scheme);
                            await _accountService.ConfirmedAccountEmailAsync(user.Email, callback);
                            return RedirectToAction("Login", "Account");
                        }
                        else
                        {
                            foreach (var error in result.Result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userModel);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
                return View(forgotPasswordModel);
            var user = _accountService.FindByEmailAsync(forgotPasswordModel.Email).Result;
            if (user == null)
                return RedirectToAction(nameof(Register));
            var token = _accountService.GeneratePasswordResetTokenAsync(user).Result;
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);
            _accountService.ForgotPasswordEmailAsync(forgotPasswordModel.Email, callback);
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordModel { Token = token, Email = email };
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(ResetPasswordModel resetPasswordModel)
        {
            resetPasswordModel.Email = resetPasswordModel.Email ?? _accountService.GetUser(HttpContext.User).Result.Email;
            resetPasswordModel.Token = resetPasswordModel.Token ?? _accountService.GeneratePasswordTokenAsync(_accountService.GetUser(HttpContext.User).Result.Email).Result;
            if (!ModelState.IsValid)
                return View(resetPasswordModel);
            var user = _accountService.FindByEmailAsync(resetPasswordModel.Email).Result;
            if (user == null)
                return RedirectToAction("Login", "Account");
            var result = _accountService.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!result.Result.Succeeded)
            {
                foreach (var error in result.Result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return View(resetPasswordModel);
            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ChangePassword(string id)
        {
            var model = new ChangePasswordModel { Id = id };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ChangePasswordAsync(changePasswordModel);
                if (result != null)
                {

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {

                        ModelState.AddModelError(nameof(changePasswordModel.OldPassword), "Please enter correct password");

                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(changePasswordModel.OldPassword), "Please enter correct password");

                }

                return View(changePasswordModel);
            }
            return View(changePasswordModel);

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordUser(string id)
        {
            var user = _accountService.FindByIdAsync(id).Result;
            var token = _accountService.GeneratePasswordTokenAsync(user.Email).Result;
            var model = new ResetPasswordModel { Token = token, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task ChangeRole(string id, string isAdmin)
        {

            await _roleService.ChangeRoleAsync(id, isAdmin);
        }
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _accountService.FindByEmailAsync(email);
            if (user == null)
                return View("Error");

            var result = await _accountService.ConfirmEmailAsync(user, token);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateModel createModel)
        {
            if (ModelState.IsValid)
            {
                var result = _accountService.CreateAsync(createModel);
                if (result != null)
                {
                    if (result.Result != null)
                    {
                        if (result.Result.Succeeded)
                        {/*
                            AppUser user = await _accountService.FindByEmailAsync(createModel.Email);
                            var token = await _accountService.GenerateEmailConfirmationTokenAsync(user);
                            var callback = Url.Action("ConfirmEmail", "Email", new { token, email = user.Email }, Request.Scheme);
                            await _accountService.ConfirmedAccountEmailAsync(user.Email, callback);*/
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            foreach (var error in result.Result.Errors)
                            {
                                ModelState.AddModelError(createModel.Password, error.Description);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            return View(createModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Edit(string id)
        {
            var model = _accountService.FindByIdAsync(id).Result;
            EditModel editModel = new EditModel()
            {
                Id = model.Id,
                Fullname=   model.Fullname,
                Username = model.UserName,
                Email = model.Email,
            };
            return View(editModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditModel editModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.UpdateAsync(editModel);
            if (result != null)
                {
                    if (result != null)
                    {
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(Index));
                        }
                        else
                        {
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }


            return View(editModel);
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult EditData(EditModel editModelData)
        {
            if (ModelState.IsValid)
            {

                var result = _accountService.UpdateAsync(editModelData);
                if (result != null)
                {
                    if (result.Result != null)
                    {
                        if (result.Result.Succeeded)
                        {
                            var message = new[]
                            {
                                new {Successed = true}
                            };
                            return Json(message);
                        }
                        else
                        {
                            var message = new[]
                            {
                                new {
                                    Successed = false,
                                    Errors =result.Result.Errors,

                                }
                            };
                            foreach (var error in result.Result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                            return Json(message);
                        }
                    }
                }
                else
                {
                    var message = new[]
                            {
                                new {
                                    Successed = false,
                                    Errors =result.Result.Errors,

                                }
                            };
                    return Json(message);
                }
            }


            var jsonResult = new[]
                             {
                                new {Successed = false,
                                ModelErrors =ModelState.Keys.SelectMany(key=>ModelState[key].Errors) }
                            };
            return Json(jsonResult);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser appUser = await _accountService.FindByIdAsync(id);
            if (await _accountService.IsInRoleAsync(appUser, "Admin"))
            {
                return RedirectToAction(nameof(Index));
            }
            
            var result = _accountService.RemoveAsync(id);
            if (result != null)
            {
                if (result.Result != null)
                {
                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            List<AppUser> appUsers = await _userManager.Users.ToListAsync();
            return View(appUsers);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(AppUser appUser)
        //{

        //    bool isExist = await _userManager.AppUser.AnyAsync(c => c.Title.ToLower() == appUser.Fullname.ToLower());
        //    if (isExist)
        //    {
        //        ModelState.AddModelError("Title", "This info already exists!");
        //        return View();
        //    }
        //    await _context.AddRangeAsync(appUser);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        [AllowAnonymous]
        public async Task<IActionResult> Detail(string id)
        {

            if (id == null) return NotFound();
            AppUser appUser = await _accountService.FindByIdAsync(id) ;
            if (appUser == null) return NotFound();
            return View(appUser);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null) return NotFound();
        //    AppUser appUser = await _context.AppUser.FirstOrDefaultAsync(c => c.Id == id);
        //    if (appUser == null) return NotFound();
        //    return View(appUser);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? id)
        //{
        //    if (id == null) return NotFound();
        //    AppUser appUser = await _context.AppUser.FirstOrDefaultAsync(c => c.Id == id);
        //    if (appUser == null) return NotFound();
        //    _context.AppUser.Remove(appUser);
        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}
        //public async Task<IActionResult> Update(int? id)
        //{

        //    if (id == null) return NotFound();
        //    AppUser appUser = await _context.AppUser.FirstOrDefaultAsync(c => c.Id == id);
        //    if (appUser == null) return NotFound();
        //    return View(appUser);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Update(int? id, AppUser appUser)
        //{
        //    if (id == null) return NotFound();
        //    if (appUser == null) return NotFound();
        //    AppUser appUserIntroView = await _context.AppUser.FirstOrDefaultAsync(c => c.Id == id);
        //    if (!ModelState.IsValid)
        //    {
        //        return View(appUserIntroView);
        //    }
        //    AppUser appUserIntroDb = await _context.AppUser.FirstOrDefaultAsync(c => c.Title.ToLower().Trim() == appUser.Title.ToLower().Trim());
        //    if (appUserIntroDb != null && appUserIntroDb.Id != id)
        //    {
        //        ModelState.AddModelError("Title", " Already exist.");
        //        return View(aboutIntroView);
        //    }
        //    appUserIntroView.Fullname = appUser.Fullname;
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}


    }
}
