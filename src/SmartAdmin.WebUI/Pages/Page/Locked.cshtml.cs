using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class LockedModel : PageModel
    {
        private readonly ILogger<LockedModel> _logger;

        public LockedModel(ILogger<LockedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
