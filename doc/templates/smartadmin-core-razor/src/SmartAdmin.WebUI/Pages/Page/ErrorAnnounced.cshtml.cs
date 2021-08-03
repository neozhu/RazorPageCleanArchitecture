using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ErrorAnnouncedModel : PageModel
    {
        private readonly ILogger<ErrorAnnouncedModel> _logger;

        public ErrorAnnouncedModel(ILogger<ErrorAnnouncedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
