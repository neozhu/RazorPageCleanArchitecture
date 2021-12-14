// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Delete;

    public class DeleteResultMappingCommandValidator : AbstractValidator<DeleteResultMappingCommand>
    {
        public DeleteResultMappingCommandValidator()
        {
      
       RuleFor(v => v.Id).NotNull().GreaterThan(0);
 
        }
    }
    public class DeleteCheckedResultMappingsCommandValidator : AbstractValidator<DeleteCheckedResultMappingsCommand>
    {
        public DeleteCheckedResultMappingsCommandValidator()
        {
   
          RuleFor(v => v.Id).NotNull().NotEmpty();
 
        }
    }

