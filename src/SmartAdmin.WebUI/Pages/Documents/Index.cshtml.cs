using CleanArchitecture.Razor.Application.Features.Documents.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Features.Documents.Commands.Delete;
using CleanArchitecture.Razor.Application.Features.Documents.Queries.Export;
using CleanArchitecture.Razor.Application.Features.Documents.Queries.PaginationQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.Features.DocumentTypes.Queries.PaginationQuery;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Application.Constants.Permission;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchitecture.Razor.Application.Common.Models;

namespace SmartAdmin.WebUI.Pages.Documents
{
    [Authorize(policy: Permissions.Documents.View)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditDocumentCommand Input { get; set; }
        [BindProperty]
        public IFormFile DocumentUploadedFile { get; set; }
        public SelectList DocumentTypes { get; set; }

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

            var request = new DocumentTypesGetAllQuery();
            var documentTypeDtos = await _mediator.Send(request);
            DocumentTypes = new SelectList(documentTypeDtos, "Id", "Name");
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] DocumentsWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (DocumentUploadedFile != null)
            {
                Input.UploadRequest = new  UploadRequest();
                Input.UploadRequest.FileName = DocumentUploadedFile.FileName;
                Input.UploadRequest.UploadType = CleanArchitecture.Razor.Domain.Enums.UploadType.Document;
                var stream = new MemoryStream();
                DocumentUploadedFile.CopyTo(stream);
                Input.UploadRequest.Data = stream.ToArray();
            }
            var result = await _mediator.Send(Input);
            return new JsonResult(result);

        }

        public async Task<IActionResult> OnPostDeleteCheckedAsync([FromBody] DeleteCheckedDocumentsCommand command)
        {
           
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteDocumentCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportDocumentsQuery command)
        {
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Documents"] + ".xlsx");
        }


    }
}
