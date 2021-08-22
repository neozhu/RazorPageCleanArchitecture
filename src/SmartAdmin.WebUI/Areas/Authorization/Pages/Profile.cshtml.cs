using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Models;
using CleanArchitecture.Razor.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Areas.Authorization.Pages
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly ILogger<ProfileModel> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUploadService _uploadService;

        [BindProperty]
        public string ProfilePictureData { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        public ProfileModel(ILogger<ProfileModel> logger,
            ICurrentUserService currentUserService,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUploadService uploadService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
            _userManager = userManager;
            _signInManager = signInManager;
            _uploadService = uploadService;
            UserId = currentUserService.UserId;
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ProfilePictureData.Contains("data:image"))
            {
                ProfilePictureData = ProfilePictureData.Substring(ProfilePictureData.LastIndexOf(',') + 1);
            }

            var uploadrequet = new UploadRequest()
            {
                Data = Convert.FromBase64String(ProfilePictureData),
                FileName = Guid.NewGuid().ToString() +".png",
                UploadType = CleanArchitecture.Razor.Domain.Enums.UploadType.ProfilePicture
            };
            var result = _uploadService.UploadAsync(uploadrequet);
            var user=await _userManager.FindByIdAsync(UserId);
            user.ProfilePictureDataUrl = result.Replace("\\","/");
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            return  RedirectToPage("./profile"); ;
        }
    }
}
