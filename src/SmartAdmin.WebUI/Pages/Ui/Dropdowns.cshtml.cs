using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class DropdownsModel : PageModel
    {
        private readonly ILogger<DropdownsModel> _logger;

        public DropdownsModel(ILogger<DropdownsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
