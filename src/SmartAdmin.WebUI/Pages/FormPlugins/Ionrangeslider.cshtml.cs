using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class IonrangesliderModel : PageModel
    {
        private readonly ILogger<IonrangesliderModel> _logger;

        public IonrangesliderModel(ILogger<IonrangesliderModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
