// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Create;

    public class CreateFieldMappingValueCommandValidator : AbstractValidator<CreateFieldMappingValueCommand>
    {
        public CreateFieldMappingValueCommandValidator()
        {

          RuleFor(v => v.MappingRuleId)
              .NotEmpty().NotEqual(0);

    }
    }

