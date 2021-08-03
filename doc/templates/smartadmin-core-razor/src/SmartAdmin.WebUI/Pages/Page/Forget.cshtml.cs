using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForgetModel : PageModel
    {
        private readonly ILogger<ForgetModel> _logger;

        public ForgetModel(ILogger<ForgetModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
