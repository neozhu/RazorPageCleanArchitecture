using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Constants.Permission;
using CleanArchitecture.Razor.Application.AuditTrails.Queries.PaginationQuery;
using CleanArchitecture.Razor.Application.Features.AuditTrails.Queries.Export;

namespace AdminLTE.WebUI.Pages.AuditTrails
{
    [Authorize()]
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;
        private readonly IStringLocalizer<IndexModel> _localizer;
        public IndexModel(
                ISender mediator,
            IStringLocalizer<IndexModel> localizer
            )
        {
            _mediator = mediator;
            _localizer = localizer;
        }
        public  Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] AuditTrailsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportAuditTrailsQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["AuditTrails"] + ".xlsx");
        }




    }
}
