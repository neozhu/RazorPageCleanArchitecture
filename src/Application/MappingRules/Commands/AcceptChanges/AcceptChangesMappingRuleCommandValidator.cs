// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.AcceptChanges;

public class AcceptChangesMappingRuleCommandValidator : AbstractValidator<AcceptChangesMappingRulesCommand>
{
    public AcceptChangesMappingRuleCommandValidator()
    {

        RuleFor(v => v.Items)
               .NotNull()
               .NotEmpty();


    }
}

