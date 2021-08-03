using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class DropzoneModel : PageModel
    {
        private readonly ILogger<DropzoneModel> _logger;

        public DropzoneModel(ILogger<DropzoneModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
