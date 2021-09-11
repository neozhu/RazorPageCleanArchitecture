using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.SalesContracts.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.AddEdit
{
    public class AddEditSalesContractCommand: SalesContractDto,IRequest<Result>, IMapFrom<SalesContract>
    {
      
    }

    public class AddEditSalesContractCommandHandler : IRequestHandler<AddEditSalesContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<AddEditSalesContractCommandHandler> _localizer;
        public AddEditSalesContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<AddEditSalesContractCommandHandler> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(AddEditSalesContractCommand request, CancellationToken cancellationToken)
        {
  
            if (request.Id > 0)
            {
                var item = await _context.SalesContracts.FindAsync(new object[] { request.Id }, cancellationToken);
                item = _mapper.Map(request, item);
            }
            else
            {
                var item = _mapper.Map<SalesContract>(request);
                _context.SalesContracts.Add(item);
            }
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
