using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Form
{
    public class CheckboxRadioModel : PageModel
    {
        private readonly ILogger<CheckboxRadioModel> _logger;

        public CheckboxRadioModel(ILogger<CheckboxRadioModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
