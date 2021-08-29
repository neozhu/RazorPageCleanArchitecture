using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using CleanArchitecture.Razor.Application.Common.Mappings;
using CleanArchitecture.Razor.Application.Common.Models;
using CleanArchitecture.Razor.Application.Features.ApprovalDatas.DTOs;
using CleanArchitecture.Razor.Domain.Entities;
using CleanArchitecture.Razor.Domain.Events;
using MediatR;
using Microsoft.Extensions.Localization;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Create
{
    public class CreateApprovalDataCommand: ApprovalDataDto,IRequest<Result>
    {
      
    }
    

    public class CreateApprovalDataCommandHandler : IRequestHandler<CreateApprovalDataCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CreateApprovalDataCommand> _localizer;
        public CreateApprovalDataCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<CreateApprovalDataCommand> localizer,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(CreateApprovalDataCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing CreateApprovalDataCommandHandler method 
           throw new System.NotImplementedException();
        }
    }
}
