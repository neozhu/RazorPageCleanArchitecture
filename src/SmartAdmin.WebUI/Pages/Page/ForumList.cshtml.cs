using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForumListModel : PageModel
    {
        private readonly ILogger<ForumListModel> _logger;

        public ForumListModel(ILogger<ForumListModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
