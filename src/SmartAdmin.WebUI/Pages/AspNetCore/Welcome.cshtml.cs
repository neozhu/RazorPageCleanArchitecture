using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class WelcomeModel : PageModel
    {
        static int _callCount;
        private readonly ILogger<WelcomeModel> _logger;
        private readonly IDiagnosticContext _diagnosticContext;

        public WelcomeModel(ILogger<WelcomeModel> logger,
            IDiagnosticContext diagnosticContext)
        {
            _logger = logger;
            _diagnosticContext = diagnosticContext;
            
        }

        public void OnGet()
        {
            _logger.LogInformation("new163@163.com".ToMD5());
            _logger.LogInformation("Welcome.");
            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
        }
    public async Task<JsonResult> OnGetFilter(string input)
    {
      return new JsonResult(input);
    }
    public async Task<JsonResult> OnPost(string input)
    {
      return new JsonResult("");
    }
  }
}
