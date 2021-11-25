using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Delete;
using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Import;
using CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Export;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.FieldMappingValues.Queries.Pagination;
using CleanArchitecture.Razor.Application.FieldMappingValues.Commands.AcceptChanges;
using CleanArchitecture.Razor.Application.ObjectFields.Queries.GetAll;
using CleanArchitecture.Razor.Application.MappingRules.Queries.GetAll;

namespace AdminLTE.WebUI.Pages.FieldMappingValues
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
        public async Task<IActionResult> OnGetDataAsync([FromQuery] FieldMappingValuesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync([FromBody] AcceptChangesFieldMappingValuesCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedFieldMappingValuesCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteFieldMappingValueCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportFieldMappingValuesQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["FieldMappingValues"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateFieldMappingValuesTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["FieldMappingValues"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream = new MemoryStream();
            await UploadedFile.CopyToAsync(stream);
            var command = new ImportFieldMappingValuesCommand()
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
