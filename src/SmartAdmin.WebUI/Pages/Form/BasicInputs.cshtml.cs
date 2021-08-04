using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class BasicInputsModel : PageModel
    {
        private readonly ILogger<BasicInputsModel> _logger;

        public BasicInputsModel(ILogger<BasicInputsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
