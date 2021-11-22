// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ObjectFields.Commands.AddEdit;

    public class AddEditObjectFieldCommandValidator : AbstractValidator<AddEditObjectFieldCommand>
    {
        public AddEditObjectFieldCommandValidator()
        {

            RuleFor(v => v.Name)
                 .MaximumLength(256)
                 .NotEmpty();
  
        }
    }

