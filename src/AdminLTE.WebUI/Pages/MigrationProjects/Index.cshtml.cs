using CleanArchitecture.Razor.Application.MigrationProjects.Commands.Delete;
using CleanArchitecture.Razor.Application.MigrationProjects.Commands.Import;
using CleanArchitecture.Razor.Application.MigrationProjects.Queries.Export;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.MigrationProjects.Queries.Pagination;
using CleanArchitecture.Razor.Application.MigrationProjects.Commands.AcceptChanges;
using CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;
using CleanArchitecture.Razor.Application.MappingRules.Queries.GetAll;

namespace AdminLTE.WebUI.Pages.MigrationProjects
{
    [Authorize(Policy = "Manager")]
    public class IndexModel : PageModel
    {

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

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
        public Task OnGetAsync()
        {
            return Task.CompletedTask;
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] MigrationProjectsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync([FromBody] AcceptChangesMigrationProjectsCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedMigrationProjectsCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteMigrationProjectCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportMigrationProjectsQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MigrationProjects"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateMigrationProjectsTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MigrationProjects"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream = new MemoryStream();
            await UploadedFile.CopyToAsync(stream);
            var command = new ImportMigrationProjectsCommand()
            {
                FileName = UploadedFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetMappingRules(string q = "")
        {
            var request = new GetAllMappingRulesWithKeyQuery() { Key = q ?? "" };
            var result= await _mediator.Send(request);
            return new JsonResult(result);
        }

    }
}
