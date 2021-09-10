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

namespace CleanArchitecture.Razor.Application.SalesContracts.Commands.Create
{
    public class CreateSalesContractCommand: SalesContractDto,IRequest<Result>, IMapFrom<SalesContract>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<SalesContract, CreateSalesContractCommand>().ReverseMap();
        }
    }
    

    public class CreateSalesContractCommandHandler : IRequestHandler<CreateSalesContractCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateSalesContractCommand> _localizer;
        public CreateSalesContractCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateSalesContractCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateSalesContractCommand request, CancellationToken cancellationToken)
        {

           var item = _mapper.Map<SalesContract>(request);
            _context.SalesContracts.Add(item);
            await _context.SaveChangesAsync(cancellationToken);
            return  Result.Success();
        }
    }
}
