// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Delete;

    public class DeleteFieldValueMappingCommandValidator : AbstractValidator<DeleteFieldValueMappingCommand>
    {
        public DeleteFieldValueMappingCommandValidator()
        {
           
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
          
        }
    }
    public class DeleteCheckedFieldValueMappingsCommandValidator : AbstractValidator<DeleteCheckedFieldValueMappingsCommand>
    {
        public DeleteCheckedFieldValueMappingsCommandValidator()
        {
            
            RuleFor(v => v.Id).NotNull().NotEmpty();
         
        }
    }

