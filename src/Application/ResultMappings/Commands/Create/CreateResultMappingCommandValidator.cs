// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.ResultMappings.Commands.Create;

public class CreateResultMappingCommandValidator : AbstractValidator<CreateResultMappingCommand>
{
    public CreateResultMappingCommandValidator()
    {
        RuleFor(v => v.Name)
        .MaximumLength(256)
        .NotEmpty();
        RuleFor(v => v.FieldParameters)
            .NotNull()
            .NotEmpty();
    }
}

