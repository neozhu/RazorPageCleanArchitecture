using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.MappingRules.Queries.Pagination;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Download;
using CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Pagination;

namespace AdminLTE.WebUI.Pages.MappingRules
{
    [Authorize(Policy = "ProjectUsers")]
    public class OverviewModel : PageModel
    {


        private readonly ICurrentUserService _currentUserService;
        private readonly ISender _mediator;
        private readonly IStringLocalizer<OverviewModel> _localizer;

        public OverviewModel(
            ICurrentUserService currentUserService,
             ISender mediator,
            IStringLocalizer<OverviewModel> localizer
            )
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _localizer = localizer;
        }
        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnGetDataAsync([FromQuery] MappingRulesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnGetDownloadData(int mappingruleid,string mappingrulename)
        {
            var command = new DownloadMappingValueFileCommand();
            command.MappingRuleId = mappingruleid;
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"{mappingrulename}.xml");
        }

        public async Task<IActionResult> OnGetFieldMappingValuesAsync([FromQuery]FieldMappingValuesByMappingIdWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        

    }
}
