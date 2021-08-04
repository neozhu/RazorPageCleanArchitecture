using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Miscellaneous
{
    public class LightgalleryModel : PageModel
    {
        private readonly ILogger<LightgalleryModel> _logger;

        public LightgalleryModel(ILogger<LightgalleryModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
