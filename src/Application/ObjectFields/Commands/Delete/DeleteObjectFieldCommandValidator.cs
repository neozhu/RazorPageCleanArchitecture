// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Delete;

    public class DeleteObjectFieldCommandValidator : AbstractValidator<DeleteObjectFieldCommand>
    {
        public DeleteObjectFieldCommandValidator()
        {
         
        RuleFor(v => v.Id).NotNull().GreaterThan(0);
  
        }
    }
    public class DeleteCheckedObjectFieldsCommandValidator : AbstractValidator<DeleteCheckedObjectFieldsCommand>
    {
        public DeleteCheckedObjectFieldsCommandValidator()
        {
        
          RuleFor(v => v.Id).NotNull().NotEmpty();

        }
    }

