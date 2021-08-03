using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class InputmaskModel : PageModel
    {
        private readonly ILogger<InputmaskModel> _logger;

        public InputmaskModel(ILogger<InputmaskModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
