using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Tables
{
    public class GenerateStyleModel : PageModel
    {
        private readonly ILogger<GenerateStyleModel> _logger;

        public GenerateStyleModel(ILogger<GenerateStyleModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
