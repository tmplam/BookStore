using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ApplicationRoles.Admin)]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserEmailStore<IdentityUser> _emailStore;
        private readonly IUserStore<IdentityUser> _userStore;
        private readonly ILogger<UserController> _logger;
        private readonly IEmailSender _emailSender;

        public UserController(IUnitOfWork unitOfWork, 
            UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            IUserStore<IdentityUser> userStore,
            ILogger<UserController> logger,
            IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ApplicationUser> users = await _unitOfWork.ApplicationUser.GetAllAsync(includeProperties: "Company");

            foreach (var user in users)
            {
                user.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            }
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userCreateVM = new UserCreateVM
            {
                RoleList = _roleManager.Roles.Select(role => new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Name
                }),
                CompanyList = (await _unitOfWork.Company.GetAllAsync()).Select(company => new SelectListItem
                {
                    Text = company.Name,
                    Value = company.Id.ToString()
                })
            };
            return View(userCreateVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateVM userCreateVM)
        {
            userCreateVM.RoleList = _roleManager.Roles.Select(role => new SelectListItem
            {
                Text = role.Name,
                Value = role.Name
            });
            userCreateVM.CompanyList = (await _unitOfWork.Company.GetAllAsync()).Select(company => new SelectListItem
            {
                Text = company.Name,
                Value = company.Id.ToString()
            });

            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                if (userCreateVM.Role == ApplicationRoles.Company && userCreateVM.CompanyId == null)
                {
                    TempData["errorMessage"] = "Please select a company";

                    return View(userCreateVM);
                }

                user.Name = userCreateVM.Name;
                user.CompanyId = userCreateVM.CompanyId;
                await _userStore.SetUserNameAsync(user, userCreateVM.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, userCreateVM.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, userCreateVM.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (!string.IsNullOrEmpty(userCreateVM.Role))
                    {
                        await _userManager.AddToRoleAsync(user, userCreateVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Individual);
                    }

                    TempData["successMessage"] = "Create user successfully";

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = Url.Content("~/") },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(userCreateVM.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    //{
                    //    return RedirectToPage("RegisterConfirmation", new { email = userCreateVM.Email, returnUrl = Url.Content("~/") });
                    //}
                    //else
                    //{
                    //    await _signInManager.SignInAsync(user, isPersistent: false);
                    //    return LocalRedirect(returnUrl);
                    //}
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    
                    return View(userCreateVM);
                }
            }
            else
            {
                TempData["errorMessage"] = "Information is not correct";

                return View(userCreateVM);
            }

        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> RoleManagement([FromQuery] string userId)
        {
            var user = await _unitOfWork.ApplicationUser.GetAsync(user => user.Id == userId, includeProperties: "Company");
            if (user != null)
            {
                user.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                RoleManagementVM roleManagementVM = new RoleManagementVM()
                {
                    ApplicationUser = user,
                    RoleList = _roleManager.Roles.Select(role => new SelectListItem
                    {
                        Text = role.Name,
                        Value = role.Name
                    }),
                    CompanyList = (await _unitOfWork.Company.GetAllAsync()).Select(company => new SelectListItem
                    {
                        Text = company.Name,
                        Value = company.Id.ToString()
                    })
                };

                return View(roleManagementVM);
            }
            else
            {
                TempData["errorMessage"] = "User not exists!";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RoleManagement(RoleManagementVM roleManagementVM)
        {
            var user = await _unitOfWork.ApplicationUser.GetAsync(user => user.Id == roleManagementVM.ApplicationUser.Id, 
                includeProperties: "Company");

            if (user != null)
            {
                user.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

                if (user.Role != roleManagementVM.ApplicationUser.Role)
                {
                    if (roleManagementVM.ApplicationUser.Role == ApplicationRoles.Company)
                    {
                        if (roleManagementVM.ApplicationUser.CompanyId == null)
                        {
                            TempData["errorMessage"] = "Please choose a company!";
                            roleManagementVM = new RoleManagementVM()
                            {
                                ApplicationUser = roleManagementVM.ApplicationUser,
                                RoleList = _roleManager.Roles.Select(role => new SelectListItem
                                {
                                    Text = role.Name,
                                    Value = role.Name
                                }),
                                CompanyList = (await _unitOfWork.Company.GetAllAsync()).Select(company => new SelectListItem
                                {
                                    Text = company.Name,
                                    Value = company.Id.ToString()
                                })
                            };
                            return View(roleManagementVM);
                        }
                        else
                        {
                            user.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                        }
                    }
                    else if (user.Role == ApplicationRoles.Company)
                    {
                        user.Company = null;
                        user.CompanyId = null;
                    }

                    _unitOfWork.ApplicationUser.Update(user);
                    await _unitOfWork.SaveChangesAsync();

                    await _userManager.RemoveFromRoleAsync(user, user.Role);
                    await _userManager.AddToRoleAsync(user, roleManagementVM.ApplicationUser.Role);
                } 
                else if (user.Role == ApplicationRoles.Company 
                    && roleManagementVM.ApplicationUser.CompanyId != null 
                    && user.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
                {
                    user.CompanyId = roleManagementVM.ApplicationUser.CompanyId;

                    _unitOfWork.ApplicationUser.Update(user);
                    await _unitOfWork.SaveChangesAsync();
                }

                TempData["successMessage"] = "Update successfully!";

                return LocalRedirect($"/Admin/User/RoleManagement?userId={user.Id}");
            }
            else
            {
                TempData["errorMessage"] = "User not exists!";
                return RedirectToAction("Index");
            }
        }

        private IUserEmailStore<IdentityUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<IdentityUser>)_userStore;
        }


        #region API CALLS

        [HttpPost]
        public async Task<IActionResult> ToggleLockout([FromBody] string userId)
        {
            var user = await _unitOfWork.ApplicationUser.GetAsync(user => user.Id == userId);

            if (user == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }

            if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.UtcNow)
            {
                // Handle unlock user
                user.LockoutEnd = DateTimeOffset.UtcNow;
            }
            else
            {
                // Handle lock user
                user.LockoutEnd = DateTimeOffset.UtcNow.AddYears(100);
            }

            _unitOfWork.ApplicationUser.Update(user);
            await _unitOfWork.SaveChangesAsync();

            return Json(new { success = true, message = "Locking/Unlocking successfully!!" });
        }

        #endregion
    }
}
