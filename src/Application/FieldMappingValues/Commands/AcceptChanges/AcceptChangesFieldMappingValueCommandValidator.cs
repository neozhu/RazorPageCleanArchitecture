// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.AcceptChanges;

public class AcceptChangesFieldMappingValueCommandValidator : AbstractValidator<AcceptChangesFieldMappingValuesCommand>
{
    public AcceptChangesFieldMappingValueCommandValidator()
    {

        RuleFor(v => v.Items)
             .NotNull()
             .NotEmpty();

        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.MappingRuleId).GreaterThan(0);
        });

    }
}

