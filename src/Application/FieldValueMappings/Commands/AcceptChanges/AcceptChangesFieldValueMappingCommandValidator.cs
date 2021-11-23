// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.AcceptChanges;

public class AcceptChangesFieldValueMappingCommandValidator : AbstractValidator<AcceptChangesFieldValueMappingsCommand>
{
    public AcceptChangesFieldValueMappingCommandValidator()
    {

        RuleFor(v => v.Items)
            .NotNull()
             .NotEmpty();
        RuleForEach(x => x.Items).ChildRules(fields =>
        {
            fields.RuleFor(x => x.ObjectFieldId).GreaterThan(0);
            fields.RuleFor(x => x.Legacy1).NotNull().NotEmpty().MaximumLength(256);
        });


    }
}

