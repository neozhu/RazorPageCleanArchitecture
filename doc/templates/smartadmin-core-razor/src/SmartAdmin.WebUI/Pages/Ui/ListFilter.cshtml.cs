using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Ui
{
    public class ListFilterModel : PageModel
    {
        private readonly ILogger<ListFilterModel> _logger;

        public ListFilterModel(ILogger<ListFilterModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
