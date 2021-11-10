using CleanArchitecture.Razor.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace SmartAdmin.WebUI.Pages.AspNetCore
{
    public class InteractiveModel : PageModel
    {
        private readonly ILogger<InteractiveModel> _logger;
        private readonly ApplicationDbContext _context;

        public string DbVersion { get; set; }

        public InteractiveModel(ApplicationDbContext context, ILogger<InteractiveModel> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {
            DbVersion = _context.Database.ProviderName;
        }
    }
}
