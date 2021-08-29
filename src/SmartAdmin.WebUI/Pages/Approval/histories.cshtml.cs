using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Pagination;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Approve;

namespace SmartAdmin.WebUI.Pages.Approval
{
    [Authorize()]
    public class Histories : PageModel
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
        public Histories(
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
        public async Task<IActionResult> OnGetDataAsync([FromQuery] ApprovalHistoriesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        

      
        

    }
}
