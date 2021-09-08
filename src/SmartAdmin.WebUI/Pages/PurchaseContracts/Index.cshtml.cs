using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Interfaces.Identity;
using CleanArchitecture.Razor.Application.Customers.Queries.GetAll;
using CleanArchitecture.Razor.Application.Products.Queries.GetAll;
using CleanArchitecture.Razor.Application.Projects.Queries.GetAll;
using CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.AddEdit;
using CleanArchitecture.Razor.Application.PurchaseContractDetails.Commands.Delete;
using CleanArchitecture.Razor.Application.PurchaseContractDetails.Queries.Specify;
using CleanArchitecture.Razor.Application.PurchaseContracts.Commands.AddEdit;
using CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Delete;
using CleanArchitecture.Razor.Application.PurchaseContracts.Commands.Import;
using CleanArchitecture.Razor.Application.PurchaseContracts.Queries.Export;
using CleanArchitecture.Razor.Application.PurchaseContracts.Queries.Pagination;
using CleanArchitecture.Razor.Infrastructure.Constants.Permission;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using SmartAdmin.WebUI.Extensions;

namespace SmartAdmin.WebUI.Pages.PurchaseContracts
{
    [Authorize(policy: Permissions.PurchaseContracts.View)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditPurchaseContractCommand Input { get; set; }
    

        [BindProperty]
        public IFormFile UploadedFile { get; set; }
        [BindProperty]
        public int PurchaseContractId { get; set; }

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
        public async Task<IActionResult> OnGetDataAsync([FromQuery] PurchaseContractsWithPaginationQuery command)
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

        public async Task<IActionResult> OnPostSaveTermsAsync([FromBody] AddEditPurchaseContractDetailCommand command)
        {
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery] int[] id)
        {
            var command = new DeleteCheckedPurchaseContractsCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery] int id)
        {
            var command = new DeletePurchaseContractCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<FileResult> OnPostExportAsync([FromBody] ExportPurchaseContractsQuery command)
        {
            var result =await _mediator.Send(command);
            return  File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["PurchaseContracts"]+".xlsx");
        }
        public async Task<FileResult> OnGetCreateTemplate()
        {
            var command = new CreatePurchaseContractsTemplateCommand();
            var result = await _mediator.Send(command);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", _localizer["PurchaseContracts"] + ".xlsx");
        }
        public async Task<IActionResult> OnPostImportAsync()
        {
            try
            {
                var stream = new MemoryStream();
                await UploadedFile.CopyToAsync(stream);
                var command = new ImportPurchaseContractsCommand()
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
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(string.Join(",", errors));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(ex.Message);
            }
        }

        public async Task<IActionResult> OnGetPurchaseTermsAsync(int id)
        {
            var command = new GetPurchaseContractDetailsByPurchaseIdQuery()
            { 
               PurchaseContractId= id
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnGetDeleteTermsAsync(int id)
        {
            var command = new DeletePurchaseContractDetailCommand()
            {
                 Id = id
            };
            var result = await _mediator.Send(command);
            return new JsonResult(result);
        }
    }
}
