using CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Delete;
using CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.Import;
using CleanArchitecture.Razor.Application.MigrationTemplateFiles.Queries.Export;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.MigrationTemplateFiles.Queries.Pagination;
using CleanArchitecture.Razor.Application.MigrationTemplateFiles.Commands.AcceptChanges;
using CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;

namespace AdminLTE.WebUI.Pages.MigrationTemplateFiles
{
    [Authorize()]
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
        public async Task<IActionResult> OnGetDataAsync([FromQuery] MigrationTemplateFilesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync([FromBody] AcceptChangesMigrationTemplateFilesCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedMigrationTemplateFilesCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteMigrationTemplateFileCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportMigrationTemplateFilesQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MigrationTemplateFiles"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateMigrationTemplateFilesTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MigrationTemplateFiles"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream = new MemoryStream();
            await UploadedFile.CopyToAsync(stream);
            var command = new ImportMigrationTemplateFilesCommand()
            {
                FileName = UploadedFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetObjectFields(string q = "")
        {
            var request = new GetAllObjectFieldsWithKeyQuery() { Key = q ?? "" };
            var result= await _mediator.Send(request);
            return new JsonResult(result);
        }

    }
}
