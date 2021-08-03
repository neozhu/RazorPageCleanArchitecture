using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Documentation
{
    public class SolutionArchitectureModel : PageModel
    {
        private readonly ILogger<SolutionArchitectureModel> _logger;

        public SolutionArchitectureModel(ILogger<SolutionArchitectureModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
