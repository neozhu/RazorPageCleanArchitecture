using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        private readonly ILogger<LogoutModel> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        [BindProperty]
        public InputModel Input { get; set; }
        [BindProperty]
        [Required]
        public string UserName { get; set; }
        [BindProperty]
        public string ReturnUrl { get; set; }
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
            public string ProfilePictureDataUrl { get; set; }
        }

        public LockoutModel(
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<LogoutModel> logger)
        {
            _currentUserService = currentUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task OnGetAsync(string userName="", string returnUrl="")
        {
            UserName= userName;
            ReturnUrl = returnUrl;
            if (!string.IsNullOrEmpty(UserName))
            {
                var user = await _userManager.FindByNameAsync(UserName);
                if (user != null)
                {
                    Input = new InputModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        ProfilePictureDataUrl = user.ProfilePictureDataUrl,
                    };
                }
            }


            await _signInManager.SignOutAsync();

            _logger.LogInformation("User logged out.");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var userName = Input.UserName;
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Not found user.");
                    return Page();
                }
                var lockoutresult = await _userManager.SetLockoutEndDateAsync(user, System.DateTimeOffset.Now.AddMinutes(-1));
                if (lockoutresult.Succeeded)
                {
                    var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, true, lockoutOnFailure: true);

                    if (result.Succeeded)
                    {
                        await _userManager.AddLoginAsync(user, new UserLoginInfo("UserNamePassword", user.Id, "Account/Lockout"));
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = true });
                    }
                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout", new { userName = Input.UserName, ReturnUrl = returnUrl });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return Page();
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, lockoutresult.Errors.First().Description);
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
