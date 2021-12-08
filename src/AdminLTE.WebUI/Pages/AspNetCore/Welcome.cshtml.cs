using CleanArchitecture.Razor.Application.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace AdminLTE.WebUI.Pages.AspNetCore
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
            _logger.LogInformation("Welcome.");
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
