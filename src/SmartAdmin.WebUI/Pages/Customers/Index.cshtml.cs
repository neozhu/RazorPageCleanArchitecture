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
        private readonly ISender _mediator;

        public IndexModel(
                ISender mediator
            )
        {
            _mediator = mediator;
        }
        public async Task OnGetAsync()
        {
            PageContext.SetRulesetForClientsideMessages("MyRuleset");
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

        public async Task<IActionResult> OnGetDeleteCheckedAsync([FromQuery]int[] id)
        {
            var command = new DeleteCheckedCommersCommand() { Id=id};
            var result= await _mediator.Send(command);
            return new JsonResult("");
        }
        public async Task<IActionResult> OnGetDeleteAsync([FromQuery]int id)
        {
            var command = new DeleteCommerCommand() { Id = id };
            var result = await _mediator.Send(command);
            return new JsonResult("");
        }
    }
}
