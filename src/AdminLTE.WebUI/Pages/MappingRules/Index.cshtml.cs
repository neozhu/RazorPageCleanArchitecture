using CleanArchitecture.Razor.Application.MappingRules.Commands.AddEdit;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Delete;
using CleanArchitecture.Razor.Application.MappingRules.Queries.Export;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.MappingRules.Queries.Pagination;
using CleanArchitecture.Razor.Application.MigrationObjects.Queries.GetAll;
using CleanArchitecture.Razor.Application.MappingRules.Commands.Import;

namespace AdminLTE.WebUI.Pages.MappingRules
{
    [Authorize()]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditMappingRuleCommand Input { get; set; }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        [BindProperty]
        public IFormFile TemplateFile { get; set; }
        public SelectList MigrationObjects { get; set; }

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
        public async Task OnGetAsync()
        {

            var request = new GetAllMigrationObjectsQuery();
            var objectlist = await _mediator.Send(request);
            MigrationObjects = new SelectList(objectlist, "Name", "Name");
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] MappingRulesWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (TemplateFile != null)
            {
                Input.UploadRequest = new  UploadRequest();
                Input.UploadRequest.FileName = TemplateFile.FileName;
                Input.UploadRequest.UploadType = CleanArchitecture.Razor.Domain.Enums.UploadType.Document;
                var stream = new MemoryStream();
                TemplateFile.CopyTo(stream);
                Input.UploadRequest.Data = stream.ToArray();
            }
            var result = await _mediator.Send(Input);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedMappingRulesCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteMappingRuleCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportMappingRulesQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MappingRules"] + ".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateMappingRulesTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["MappingRules"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream = new MemoryStream();
            await UploadedFile.CopyToAsync(stream);
            var command = new ImportMappingRulesCommand()
            {
                FileName = UploadedFile.FileName,
                Data = stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

    }
}
