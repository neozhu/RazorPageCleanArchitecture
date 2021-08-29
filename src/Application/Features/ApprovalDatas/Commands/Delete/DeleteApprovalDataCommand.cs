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

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Delete
{
    public class DeleteApprovalDataCommand: IRequest<Result>
    {
      
    }
    public class DeleteCheckedApprovalDatasCommand : IRequest<Result>
    {
     
    }

    public class DeleteApprovalDataCommandHandler : 
                 IRequestHandler<DeleteApprovalDataCommand, Result>,
                 IRequestHandler<DeleteCheckedApprovalDatasCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<DeleteApprovalDataCommandHandler> _localizer;
        public DeleteApprovalDataCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<DeleteApprovalDataCommandHandler> localizer,
             IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }
        public async Task<Result> Handle(DeleteApprovalDataCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing DeleteApprovalDataCommandHandler method 
           throw new System.NotImplementedException();
        }

        public async Task<Result> Handle(DeleteCheckedApprovalDatasCommand request, CancellationToken cancellationToken)
        {
           //TODO:Implementing DeleteCheckedApprovalDatasCommandHandler method 
           throw new System.NotImplementedException();
        }
    }
}
