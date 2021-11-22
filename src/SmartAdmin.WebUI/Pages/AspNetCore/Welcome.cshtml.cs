using CleanArchitecture.Razor.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    [Authorize()]
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
            _logger.LogInformation("Home Page");
            _logger.LogError("Error");
            _logger.LogWarning("Warning");
            _logger.LogDebug("Debug");
            _logger.LogCritical("Critical");
            _logger.LogTrace("Trace");
            
            _diagnosticContext.Set("IndexCallCount", Interlocked.Increment(ref _callCount));
        }
    public  Task<JsonResult> OnGetFilter(string input)
    {
      return  Task.FromResult(new JsonResult(input));
    }
    public  Task<JsonResult> OnPost(string input)
    {
      return Task.FromResult(new JsonResult(string.Empty));
        }
  }
}
