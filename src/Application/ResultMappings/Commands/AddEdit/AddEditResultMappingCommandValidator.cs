// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.AddEdit;

public class AddEditResultMappingCommandValidator : AbstractValidator<AddEditResultMappingCommand>
{
    public AddEditResultMappingCommandValidator()
    {

        RuleFor(v => v.Name)
           .MaximumLength(256)
           .NotEmpty();
        RuleFor(v => v.LegacySystem)
           .MaximumLength(256)
           .NotEmpty();
        RuleFor(v => v.RelevantMock)
          .MaximumLength(256)
          .NotEmpty();
   

    }
}

