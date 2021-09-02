using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Documents.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Documents.Commands.Delete;
using CleanArchitecture.Razor.Application.Documents.Queries.Export;
using CleanArchitecture.Razor.Application.Documents.Queries.PaginationQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using CleanArchitecture.Razor.Application.DocumentTypes.DTOs;
using CleanArchitecture.Razor.Application.DocumentTypes.Queries.PaginationQuery;
using Microsoft.AspNetCore.Authorization;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SmartAdmin.WebUI.Pages.Documents
{
    [Authorize(policy: Permissions.Documents.View)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditDocumentCommand Input { get; set; }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }
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
            try
            {
                if (UploadedFile != null)
                {
                    Input.UploadRequest = new CleanArchitecture.Razor.Application.Models.UploadRequest();
                    Input.UploadRequest.FileName = UploadedFile.FileName;
                    Input.UploadRequest.UploadType = CleanArchitecture.Razor.Domain.Enums.UploadType.Document;
                    var stream = new MemoryStream();
                    UploadedFile.CopyTo(stream);
                    Input.UploadRequest.Data = stream.ToArray();
                }
                var result = await _mediator.Send(Input);
                return new JsonResult(result);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => $"{ string.Join(",", x.Value) }");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(string.Join(",", errors));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedDocumentsCommand() { Id = id };
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
            var result =await _mediator.Send(command);
            return  File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Documents"]+".xlsx");
        }
        

    }
}
