// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.Create;

    public class CreateObjectFieldCommandValidator : AbstractValidator<CreateObjectFieldCommand>
    {
        public CreateObjectFieldCommandValidator()
        {
       
            RuleFor(v => v.Name)
                .MaximumLength(256)
                 .NotEmpty();
 
        }
    }

