// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Customers.Commands.AddEdit;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Customers.Commands.Import
{
    public class ImportCustomersCommand : IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateCustomerTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName  { get;set; }
    }
    public class ImportCustomersCommandHandler :
        IRequestHandler<CreateCustomerTemplateCommand, byte[]>,
        IRequestHandler<ImportCustomersCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ImportCustomersCommandHandler> _localizer;
        private readonly IValidator<AddEditCustomerCommand> _addValidator;

        public ImportCustomersCommandHandler(
            IApplicationDbContext context,
            IMapper mapper,
            IExcelService excelService,
            IStringLocalizer<ImportCustomersCommandHandler> localizer,
            IValidator<AddEditCustomerCommand> addValidator
            )
        {
            _context = context;
            _mapper = mapper;
            _excelService = excelService;
            _localizer = localizer;
            _addValidator = addValidator;
        }
        public async Task<Result> Handle(ImportCustomersCommand request, CancellationToken cancellationToken)
        {
            var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, Customer, object>>
            {
                { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },
                { _localizer["Partner Type"], (row,item) => item.PartnerType = (PartnerType) Enum.Parse(typeof(PartnerType), row[_localizer["Partner Type"]]==null?"TP":row[_localizer["Partner Type"]].ToString(), true)  },
                { _localizer["Region"], (row,item) => item.Region =  row[_localizer["Region"]]?.ToString() },
                { _localizer["Sales"], (row,item) => item.Sales =  row[_localizer["Sales"]]?.ToString() },
                { _localizer["Address"], (row,item) => item.Address =  row[_localizer["Address"]].ToString() },
                { _localizer["Contact"], (row,item) => item.Contact =  row[_localizer["Contact"]]?.ToString() },
                { _localizer["Email"], (row,item) => item.Email =  row[_localizer["Email"]]?.ToString() },
                { _localizer["Phone Number"], (row,item) => item.PhoneNumber =  row[_localizer["Phone Number"]]?.ToString() },
                { _localizer["Contact2"], (row,item) => item.Contact2 =  row[_localizer["Contact2"]]?.ToString() },
                { _localizer["Email2"], (row,item) => item.Email2 =  row[_localizer["Email2"]]?.ToString() },
                { _localizer["Phone Number2"], (row,item) => item.PhoneNumber2 =  row[_localizer["Phone Number2"]]?.ToString() },
                { _localizer["Fax"], (row,item) => item.Fax =  row[_localizer["Fax"]]?.ToString() },
                { _localizer["Tax No"], (row,item) => item.TaxNo =  row[_localizer["Tax No"]]?.ToString() },
                { _localizer["Bank"], (row,item) => item.Bank =  row[_localizer["Bank"]]?.ToString() },
                { _localizer["Account No"], (row,item) => item.AccountNo =  row[_localizer["Account No"]]?.ToString() },
                { _localizer["Comments"], (row,item) => item.Comments =  row[_localizer["Comments"]]?.ToString() }
            }, _localizer["Customers"]);

            if (result.Succeeded)
            {
                var importItems = result.Data;
                var errors = new List<string>();
                var errorsOccurred = false;
                foreach (var item in importItems)
                {
                    var validationResult = await _addValidator.ValidateAsync(_mapper.Map<AddEditCustomerCommand>(item), cancellationToken);
                    if (validationResult.IsValid)
                    {
                        var exist = await _context.Customers.AnyAsync(x => x.Name==item.Name,cancellationToken);
                        if (!exist)
                        {
                            await _context.Customers.AddAsync(item, cancellationToken);
                        }
                    }
                    else
                    {
                        errorsOccurred = true;
                        errors.AddRange(validationResult.Errors.Select(e => $"{(!string.IsNullOrWhiteSpace(item.Name) ? $"{item.Name} - " : string.Empty)}{e.ErrorMessage}"));
                    }
                }

                if (errorsOccurred)
                {
                    return await Result.FailureAsync(errors);
                }

                await _context.SaveChangesAsync(cancellationToken);
                return await Result.SuccessAsync();
            }
            else
            {
                return await Result.FailureAsync(result.Errors);
            }
        }

        public async Task<byte[]> Handle(CreateCustomerTemplateCommand request, CancellationToken cancellationToken)
        {
            var fields = new string[] {
                _localizer["Name"],
                _localizer["Partner Type"],
                _localizer["Region"],
                _localizer["Sales"],
                _localizer["Address"],
                _localizer["Contact"],
                _localizer["Email"],
                _localizer["Phone Number"],
                _localizer["Email"],
                _localizer["Contact2"],
                _localizer["Phone Number2"],
                _localizer["Email2"],
                _localizer["Fax"],
                _localizer["Tax No"],
                _localizer["Bank"],
                _localizer["Account No"],
                _localizer["Comments"],
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["Customers"]);
            return result;
        }
    }
}
