using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Pagination;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.Queries.Export;

namespace SmartAdmin.WebUI.Pages.Approval
{
    [Authorize(policy: Permissions.Approval.View)]
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
        public   Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] ApprovalHistoriesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportApprovalDatasQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["ApprovalHistories"] + ".xlsx");
        }




    }
}
