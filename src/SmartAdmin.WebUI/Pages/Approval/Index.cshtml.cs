using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Pagination;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Approve;
using CleanArchitecture.Razor.Application.Constants.Permission;

namespace SmartAdmin.WebUI.Pages.Approval
{
    [Authorize(policy: Permissions.Approval.View)]
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
        public   Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] ApprovalDatasWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPost()
        {
            var request = new ApproveCommand() {
                 Approver= this.Approver,
                 Outcome=this.Outcome,
                 WorkflowId=this.WorkflowId,
                 Comments=this.Comments
                };

            var result = await _mediator.Send(request);
            return new JsonResult(result);
        }

      
        

    }
}
