using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ButtonsModel : PageModel
    {
        private readonly ILogger<ButtonsModel> _logger;

        public ButtonsModel(ILogger<ButtonsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
