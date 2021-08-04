using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.Page
{
    public class ForumThreadsModel : PageModel
    {
        private readonly ILogger<ForumThreadsModel> _logger;

        public ForumThreadsModel(ILogger<ForumThreadsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
