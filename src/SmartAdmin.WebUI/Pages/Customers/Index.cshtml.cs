using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Customers.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Customers.Commands.Delete;
using CleanArchitecture.Razor.Application.Customers.Commands.Import;
using CleanArchitecture.Razor.Application.Customers.Queries.Export;
using CleanArchitecture.Razor.Application.Customers.Queries.PaginationQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartAdmin.WebUI.Extensions;

namespace SmartAdmin.WebUI.Pages.Customers
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditCustomerCommand Input { get; set; }
        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        private readonly ICurrentUserService _currentUserService;
        private readonly ISender _mediator;
        private readonly IStringLocalizer<IndexModel> _localizer;

        public IndexModel(
            ICurrentUserService currentUserService,
                ISender mediator,
            IStringLocalizer<IndexModel> localizer
            )
        {
            _currentUserService = currentUserService;
            _mediator = mediator;
            _localizer = localizer;
            var email = _currentUserService.UserId;
        }
        public async Task OnGetAsync()
        {
            
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] CustomersWithPaginationQuery command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
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
            var command = new DeleteCheckedCommersCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteCommerCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportCustomersQuery command)
        {
            var result =await _mediator.Send(command);
            return  File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Customers"]+".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateCustomerTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["Customers"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            var stream=new MemoryStream();
            await  UploadedFile.CopyToAsync(stream);
            var command = new ImportCustomersCommand() {
                 FileName=UploadedFile.FileName,
                 Data= stream.ToArray()
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

    }
}
