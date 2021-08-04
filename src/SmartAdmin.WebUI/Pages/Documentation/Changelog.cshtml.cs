using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class ChangelogModel : PageModel
    {
        private readonly ILogger<ChangelogModel> _logger;

        public ChangelogModel(ILogger<ChangelogModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
