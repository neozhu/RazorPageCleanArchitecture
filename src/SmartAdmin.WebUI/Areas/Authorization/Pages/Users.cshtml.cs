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

namespace SmartAdmin.WebUI.Areas.Authorization.Pages
{
    [Authorize]
    public class UserModel : PageModel
    {
        [BindProperty]
        public RegisterModel RegisterFormModel { get; set; } = new();
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

        public async Task<IActionResult> OnPostRegister()
        {
            var user = new ApplicationUser {
                EmailConfirmed = true,
                IsActive = true,
                Site = RegisterFormModel.Site,
                DisplayName = RegisterFormModel.DisplayName,
                UserName = RegisterFormModel.UserName,
                Email = RegisterFormModel.Email };
            var result = await _userManager.CreateAsync(user, RegisterFormModel.Password);
            return new JsonResult(result.ToApplicationResult());
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
    }


}
