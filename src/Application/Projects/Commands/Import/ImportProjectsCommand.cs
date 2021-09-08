using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Projects.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using CleanArchitecture.Razor.Application.Projects.Commands.AddEdit;
using CleanArchitecture.Razor.Application.Projects.Commands.Create;

namespace CleanArchitecture.Razor.Application.Projects.Commands.Import
{
    public class ImportProjectsCommand: IRequest<Result>
    {
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
    public class CreateProjectsTemplateCommand : IRequest<byte[]>
    {
        public IEnumerable<string> Fields { get; set; }
        public string SheetName { get; set; }
    }

    public class ImportProjectsCommandHandler : 
                 IRequestHandler<CreateProjectsTemplateCommand, byte[]>,
                 IRequestHandler<ImportProjectsCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ImportProjectsCommandHandler> _localizer;
        private readonly IValidator<CreateProjectCommand> _addValidator;
        private readonly IExcelService _excelService;

        public ImportProjectsCommandHandler(
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ImportProjectsCommandHandler> localizer,
            IValidator<CreateProjectCommand> addValidator,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _addValidator = addValidator;
            _excelService = excelService;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ImportProjectsCommand request, CancellationToken cancellationToken)
        {
            var result = await _excelService.ImportAsync(request.Data, mappers: new Dictionary<string, Func<DataRow, Project, object>>
            {
                { _localizer["Name"], (row,item) => item.Name = row[_localizer["Name"]]?.ToString() },
                { _localizer["Status"], (row,item) => item.Status = row[_localizer["Status"]]?.ToString() },
                { _localizer["Description"], (row,item) => item.Description =  row[_localizer["Description"]]?.ToString() },
                { _localizer["Begin DateTime"], (row,item) => item.BeginDateTime = DateTime.Parse(row[_localizer["Begin DateTime"]].ToString()) },
                { _localizer["End DateTime"], (row,item) => item.EndDateTime = row.IsNull(_localizer["End DateTime"])?null:  DateTime.Parse(row[_localizer["End DateTime"]].ToString()) },
                { _localizer["Estimated Cost"], (row,item) => item.EstimatedCost = row.IsNull(_localizer["Estimated Cost"])?null: decimal.Parse(row[_localizer["Estimated Cost"]].ToString())},
                { _localizer["Actual Cost"], (row,item) => item.ActualCost = row.IsNull(_localizer["Actual Cost"])?null: decimal.Parse(row[_localizer["Actual Cost"]].ToString())},
                { _localizer["Contract Amount"], (row,item) => item.ContractAmount = row.IsNull(_localizer["Contract Amount"])?null:decimal.Parse(row[_localizer["Contract Amount"]].ToString()) },
                { _localizer["Receipt Amount"], (row,item) => item.ReceiptAmount = row.IsNull(_localizer["Receipt Amount"])?null:decimal.Parse(row[_localizer["Receipt Amount"]].ToString()) },
                { _localizer["Gross Margin"], (row,item) => item.GrossMargin = row.IsNull(_localizer["Gross Margin"])?null:decimal.Parse(row[_localizer["Gross Margin"]].ToString()) }

            }, _localizer["Projects"]);

            if (result.Succeeded)
            {
                var importItems = result.Data;
                var errors = new List<string>();
                var errorsOccurred = false;
                foreach (var item in importItems)
                {
                    var validationResult = await _addValidator.ValidateAsync(_mapper.Map<CreateProjectCommand>(item), cancellationToken);
                    if (validationResult.IsValid)
                    {
                        var exist = await _context.Projects.AnyAsync(x => x.Name == item.Name, cancellationToken);
                        if (!exist)
                        {
                            await _context.Projects.AddAsync(item, cancellationToken);
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
        public async Task<byte[]> Handle(CreateProjectsTemplateCommand request, CancellationToken cancellationToken)
        {
            //TODO:Implementing ImportProjectsCommandHandler method 
            var fields = new string[] {
                //TODO:Defines the title and order of the fields to be imported's template
                   _localizer["Name"],
                   _localizer["Status"],
                   _localizer["Description"],
                   _localizer["Begin DateTime"],
                   _localizer["End DateTime"],
                   _localizer["Actual Cost"],
                   _localizer["Contract Amount"],
                   _localizer["Receipt Amount"],
                   _localizer["Gross Margin"]
                };
            var result = await _excelService.CreateTemplateAsync(fields, _localizer["Projects"]);
            return result;
        }
    }
}
