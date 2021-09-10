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

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Update
{
    public class UpdateSalesContractCommand: SalesContractDto,IRequest<Result>, IMapFrom<SalesContract>
    {
        
    }

    public class UpdateSalesContractCommandHandler : IRequestHandler<UpdateSalesContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateSalesContractCommandHandler> _localizer;
        public UpdateSalesContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateSalesContractCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateSalesContractCommand request, CancellationToken cancellationToken)
        {
           var item =await _context.SalesContracts.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
