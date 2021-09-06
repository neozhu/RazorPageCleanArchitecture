using System;
using System.Linq;
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
using WorkflowCore.Interface;

namespace CleanArchitecture.Razor.Application.Features.ApprovalDatas.Commands.Approve
{
    public class ApproveCommand : IRequest<Result>
    {
        public string WorkflowId { get; set; }
        public string Approver { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }

    }

    public class ApproveCommandHandler : IRequestHandler<ApproveCommand, Result>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ApproveCommandHandler> _localizer;
        private readonly IWorkflowHost _workflowHost;

        public ApproveCommandHandler(
            IApplicationDbContext context,
            IStringLocalizer<ApproveCommandHandler> localizer,
             //IWorkflowHost workflowHost,
            IMapper mapper
            )
        {
            _context = context;
            _localizer = localizer;
            //_workflowHost = workflowHost;
            _mapper = mapper;
        }
        public async Task<Result> Handle(ApproveCommand request, CancellationToken cancellationToken)
        {

            var actions = _workflowHost.GetOpenUserActions(request.WorkflowId);
            if (actions != null)
            {
                foreach (var act in actions)
                {
                    string key = act.Key;
                    string value = act.Options.Single(x => x.Value == request.Outcome).Value;
                    await _workflowHost.PublishUserAction(key, request.Approver, value);

                }
            }
            var approval = await _context.ApprovalDatas.FindAsync(request.WorkflowId);
            approval.Comments = request.Comments;
            approval.Status = "Finished";
            approval.ApprovedDateTime = DateTime.Now;
            approval.Outcome = request.Outcome;
            approval.Approver = request.Approver;
            await _context.SaveChangesAsync(cancellationToken);


            return Result.Success();

        }
    }
}
