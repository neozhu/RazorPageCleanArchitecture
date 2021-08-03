using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class ValidationModel : PageModel
    {
        private readonly ILogger<ValidationModel> _logger;

        public ValidationModel(ILogger<ValidationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
