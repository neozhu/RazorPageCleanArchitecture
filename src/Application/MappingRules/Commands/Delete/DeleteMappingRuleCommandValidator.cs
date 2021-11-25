// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Delete;

    public class DeleteMappingRuleCommandValidator : AbstractValidator<DeleteMappingRuleCommand>
    {
        public DeleteMappingRuleCommandValidator()
        {
 
           RuleFor(v => v.Id).NotNull().GreaterThan(0);

        }
    }
    public class DeleteCheckedMappingRulesCommandValidator : AbstractValidator<DeleteCheckedMappingRulesCommand>
    {
        public DeleteCheckedMappingRulesCommandValidator()
        {

            RuleFor(v => v.Id).NotNull().NotEmpty();

        }
    }

