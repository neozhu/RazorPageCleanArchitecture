// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Update;

    public class UpdateFieldMappingValueCommandValidator : AbstractValidator<UpdateFieldMappingValueCommand>
    {
        public UpdateFieldMappingValueCommandValidator()
        {
        RuleFor(v => v.MappingRuleId)
           .NotEmpty().GreaterThan(0);

    }
    }

