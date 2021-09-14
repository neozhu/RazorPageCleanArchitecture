using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Customers.Queries.GetAll;
using CleanArchitecture.Razor.Application.Products.Queries.GetAll;
using CleanArchitecture.Razor.Application.Projects.Queries.GetAll;
using CleanArchitecture.Razor.Application.SalesContractDetails.Commands.AddEdit;
using CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Delete;
using CleanArchitecture.Razor.Application.SalesContractDetails.Queries.Specify;
using CleanArchitecture.Razor.Application.SalesContracts.Commands.AddEdit;
using CleanArchitecture.Razor.Application.SalesContracts.Commands.Delete;
using CleanArchitecture.Razor.Application.SalesContracts.Commands.Import;
using CleanArchitecture.Razor.Application.SalesContracts.Queries.Export;
using CleanArchitecture.Razor.Application.SalesContracts.Queries.Pagination;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SmartAdmin.WebUI.Extensions;

namespace SmartAdmin.WebUI.Pages.SalesContracts
{
    [Authorize(policy: Permissions.SalesContracts.View)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditSalesContractCommand Input { get; set; }
    

        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        [BindProperty]
        public int SalesContractId { get; set; }

        public SelectList Projects { get; set; }
        public SelectList Customers { get; set; } 

        private readonly IIdentityService _identityService;
        private readonly IAuthorizationService _authorizationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISender _mediator;
        private readonly IStringLocalizer<IndexModel> _localizer;

        public IndexModel(
           IIdentityService identityService,
            IAuthorizationService authorizationService,
            ICurrentUserService currentUserService,
            ISender mediator,
            IStringLocalizer<IndexModel> localizer
            )
        {
            _identityService = identityService;
            _authorizationService = authorizationService;
            _currentUserService = currentUserService;
            _mediator = mediator;
            _localizer = localizer;
            var email = _currentUserService.UserId;
        }

        public async Task OnGetAsync()
        {
            var requestp = new GetAllProjectsQuery();
            var projects = await _mediator.Send(requestp);
            Projects = new SelectList(projects, "Id", "Name");
            var requestc = new GetAllCustomersQuery();
            var customers = await _mediator.Send(requestc);
            Customers = new SelectList(customers, "Id", "Name");
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] SalesContractsWithPaginationQuery command)
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
                return BadRequest(Result.Failure(errors));
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Failure(new string[] { ex.Message }));
            }
        }

       

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedSalesContractsCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeleteSalesContractCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportSalesContractsQuery command)
        {
            var result =await _mediator.Send(command);
            return  File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["SalesContracts"]+".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreateSalesContractsTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["SalesContracts"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            try
            {
                var stream = new MemoryStream();
                await UploadedFile.CopyToAsync(stream);
                var command = new ImportSalesContractsCommand()
                {
                    FileName = UploadedFile.FileName,
                    Data = stream.ToArray()
                };
                var result = await _mediator.Send(command);
                return new JsonResult(result);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(x => $"{ string.Join(",", x.Value) }");
                return BadRequest(Result.Failure(errors));
            }
            catch (Exception ex)
            {
                return BadRequest(Result.Failure(new string[] { ex.Message }));
            }
        }


        public async Task<IActionResult> OnPostSaveSalesContractTermsAsync([FromBody] AddEditSalesContractDetailCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetSalesContractTermsAsync(int id)
        {
            var command = new GetSalesContractDetailsByContractIdQuery()
            { 
               SalesContractId= id
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetDeleteSalesContractTermsAsync(int id)
        {
            var command = new DeleteSalesContractDetailCommand()
            {
                 Id = id
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
    }
}
