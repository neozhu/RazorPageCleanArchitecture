using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Utilities
{
    public class SizingModel : PageModel
    {
        private readonly ILogger<SizingModel> _logger;

        public SizingModel(ILogger<SizingModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
