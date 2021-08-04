using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.FormPlugins
{
    public class NouisliderModel : PageModel
    {
        private readonly ILogger<NouisliderModel> _logger;

        public NouisliderModel(ILogger<NouisliderModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
