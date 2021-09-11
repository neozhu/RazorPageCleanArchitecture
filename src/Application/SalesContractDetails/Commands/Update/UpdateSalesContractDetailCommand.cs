using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.SalesContractDetails.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.SalesContractDetails.Commands.Update
{
    public class UpdateSalesContractDetailCommand: SalesContractDetailDto,IRequest<Result>, IMapFrom<SalesContractDetail>
    {
        
    }

    public class UpdateSalesContractDetailCommandHandler : IRequestHandler<UpdateSalesContractDetailCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<UpdateSalesContractDetailCommandHandler> _localizer;
        public UpdateSalesContractDetailCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<UpdateSalesContractDetailCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(UpdateSalesContractDetailCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing UpdateSalesContractDetailCommandHandler method 
           var item =await _context.SalesContractDetails.FindAsync( new object[] { request.Id }, cancellationToken);
           if (item != null)
           {
                item = _mapper.Map(request, item);
                await _context.SaveChangesAsync(cancellationToken);
           }
           return Result.Success();
        }
    }
}
