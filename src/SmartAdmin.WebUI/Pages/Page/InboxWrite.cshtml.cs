using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class InboxWriteModel : PageModel
    {
        private readonly ILogger<InboxWriteModel> _logger;

        public InboxWriteModel(ILogger<InboxWriteModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
