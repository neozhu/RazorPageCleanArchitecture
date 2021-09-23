using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Pagination;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Export;
using CleanArchitecture.Razor.Application.AuditTrails.Queries.PaginationQuery;

namespace SmartAdmin.WebUI.Pages.AuditTrails
{
    [Authorize(policy: Permissions.AuditTrails.View)]
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;
        private readonly IStringLocalizer<IndexModel> _localizer;
        [BindProperty]
        public string WorkflowId { get; set; }
        [BindProperty]
        public string Comments { get; set; }
        [BindProperty]
        public string Outcome { get; set; }
        [BindProperty]
        public string Approver { get; set; }
        public IndexModel(
                ISender mediator,
            IStringLocalizer<IndexModel> localizer
            )
        {
            _mediator = mediator;
            _localizer = localizer;
        }
        public async Task OnGetAsync()
        {
            
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] AuditTrailsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
         




    }
}
