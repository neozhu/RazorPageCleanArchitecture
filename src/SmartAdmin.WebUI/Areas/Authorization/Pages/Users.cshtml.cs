using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Extensions;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Linq.Dynamic.Core;
using CleanArchitecture.Razor.Application.Common.Mappings;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CleanArchitecture.Razor.Application.Common.Models;

namespace SmartAdmin.WebUI.Areas.Authorization.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        [BindProperty]
        public RegisterModel RegisterFormModel { get; set; } = new();
        [BindProperty]
        public EditUserModel EditFormModel { get; set; } = new();
        [BindProperty]
        public ResetPasswordModel ResetFormModel { get; set; } = new();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UserModel> _localizer;

        public UserModel(
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IStringLocalizer<UserModel> localizer
            )
        {
            _userManager = userManager;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task OnGetAsync()
        {

        }
        public async Task<IActionResult> OnGetDataAsync(int page=1,int rows=15,string sort="UserName",string order="asc",string filterRules="") {
            var filters = PredicateBuilder.FromFilter<ApplicationUser>(filterRules);
            var data=await _userManager.Users.Where(filters)
                   .OrderBy($"{sort} {order}")
                   .PaginatedDataAsync(page, rows);
            return new JsonResult(data);
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            var user = new ApplicationUser {
                EmailConfirmed = true,
                IsActive = true,
                Site = RegisterFormModel.Site,
                DisplayName = RegisterFormModel.DisplayName,
                UserName = RegisterFormModel.UserName,
                Email = RegisterFormModel.Email,
                PhoneNumber = RegisterFormModel.PhoneNumber
                 };
            var result = await _userManager.CreateAsync(user, RegisterFormModel.Password);
            return new JsonResult(result.ToApplicationResult());
        }
        public async Task<IActionResult> OnPostEditAsync()
        {
            var user = await _userManager.FindByIdAsync(EditFormModel.Id);
            user.DisplayName=EditFormModel.DisplayName;
            user.PhoneNumber=EditFormModel.PhoneNumber;
            user.Email=EditFormModel.Email;
            user.Site=EditFormModel.Site;
            var result = await _userManager.UpdateAsync(user);
            return new JsonResult(result.ToApplicationResult());
        }
        public async Task<IActionResult> OnPostResetPasswordAsync()
        {
           
            var user = await _userManager.FindByIdAsync(ResetFormModel.Id);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, code, ResetFormModel.Password);
            
            return new JsonResult(result.ToApplicationResult());
        }
        public async Task<IActionResult> OnGetUnLockedAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user.LockoutEnd!=null && user.LockoutEnd <= System.DateTimeOffset.Now)
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, System.DateTimeOffset.MaxValue);
            }
            else
            {
                var result = await _userManager.SetLockoutEndDateAsync(user, System.DateTimeOffset.Now);
            }
            
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return new JsonResult(result.ToApplicationResult());
        }
        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] string[] id)
        {
            foreach(var key in id)
            {
                var user = await _userManager.FindByIdAsync(key);
                var result = await _userManager.DeleteAsync(user);
            }
            return new JsonResult(Result.Success());
        }
        public class RegisterModel
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
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }

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

         
        }
        public class EditUserModel
        {
            public string Id { get; set; }
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
            [Display(Name = "Phone Number")]
            public string PhoneNumber { get; set; }





        }
        public class ResetPasswordModel
        {
            [Required]
            public string Id { get; set; }
            [Required]
            public string UserName { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }
    }


}
