using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Exceptions;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Customers.Commands;
using CleanArchitecture.Razor.Application.Customers.Queries;
using CleanArchitecture.Razor.Application.Models;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace SmartAdmin.WebUI.Pages.Customers
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public AddEditCustomerCommand Input { get; set; }
        private readonly ISender mediator;

        public IndexModel(
                ISender _mediator
            )
        {
            mediator = _mediator;
        }
        public async Task OnGetAsync()
        {
            PageContext.SetRulesetForClientsideMessages("MyRuleset");
        }
        public async Task<IActionResult> OnGetDataAsync([FromQuery] CustomersWithPaginationQuery command)
        {
            var result = await mediator.Send(command);
            return new JsonResult(result);
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var result = await mediator.Send(Input);
                return new JsonResult(result);
            }
            catch(ValidationException ex)
            {
                var errors = ex.Errors.Select(x =>   $"{ string.Join(",", x.Value) }" );
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(string.Join(",",errors));
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new JsonResult(ex.Message);
            }
        }
    }
}
