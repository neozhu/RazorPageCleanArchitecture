using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SmartAdmin.WebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
  public class RegisterModel : PageModel
  {
    private readonly IEmailSender _emailSender;
    private readonly ILogger<RegisterModel> _logger;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<RegisterModel> logger,
        IEmailSender emailSender)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
    }

    [BindProperty] public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    public void OnGet(string returnUrl = null) => ReturnUrl = returnUrl;

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");
      if (ModelState.IsValid)
      {
        var user = new ApplicationUser { EmailConfirmed=true,
            IsActive=true,
            Site=Input.Site,
            DisplayName=Input.DisplayName,
            UserName = Input.UserName,
            Email = Input.Email,
            ProfilePictureDataUrl = $"https://www.gravatar.com/avatar/{ Input.Email.ToMD5() }?s=120&d=retro"
        };
        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
          _logger.LogInformation("User created a new account with password.");

          //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
          //var callbackUrl = Url.Page("/Account/ConfirmEmail", null, new { userId = user.Id, code }, Request.Scheme);

          //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
          //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

          await _signInManager.SignInAsync(user, false);
          return LocalRedirect(returnUrl);
        }

        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      // If we got this far, something failed, redisplay form
      return Page();
    }

    public class InputModel
    {
      [Display(Name = "Site")]
      [Required]
      public string Site { get; set; }
      [Display(Name = "User Name")]
      [Required]
      public string UserName { get; set; }
      [Required]
      [Display(Name = "Display Name")]
      public string DisplayName { get; set; }


      [Required]
      [EmailAddress]
      [Display(Name = "Email")]
      public string Email { get; set; }

      [Required]
      [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
      public string ConfirmPassword { get; set; }

      [Required]
      [Display(Name = "I agree to terms & conditions")]
      public bool AgreeToTerms { get; set; }

      [Display(Name = "Sign up for newsletters")]
      public bool SignUp { get; set; }
    }
  }
}
