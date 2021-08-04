using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Docs
{
    public class CommunitySupportModel : PageModel
    {
        private readonly ILogger<CommunitySupportModel> _logger;

        public CommunitySupportModel(ILogger<CommunitySupportModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
