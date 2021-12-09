using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Constants.Permission;
using CleanArchitecture.Razor.Application.Logs.Queries.PaginationQuery;
using CleanArchitecture.Razor.Application.Features.Logs.Queries.Export;
using CleanArchitecture.Razor.Application.Features.Logs.Queries.ChatData;

namespace AdminLTE.WebUI.Pages.Logs
{
    [Authorize(Policy = "Manager")]
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
        public async Task<IActionResult> OnGetDataAsync([FromQuery] LogsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportLogsQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Logs"] + ".xlsx");
        }

        public async Task<IActionResult> OnGetLogsTimeLineDataAsync([FromQuery] LogsTimeLineChatDataQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetLogsLevelDataAsync([FromQuery] LogsLevelChatDataQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

    }
}
