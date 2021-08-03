using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class MarkdownModel : PageModel
    {
        private readonly ILogger<MarkdownModel> _logger;

        public MarkdownModel(ILogger<MarkdownModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
