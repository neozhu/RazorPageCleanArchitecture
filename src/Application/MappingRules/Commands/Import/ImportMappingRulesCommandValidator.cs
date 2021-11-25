// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.MappingRules.Commands.Import;

    public class ImportMappingRulesCommandValidator : AbstractValidator<ImportMappingRulesCommand>
    {
        public ImportMappingRulesCommandValidator()
        {
        
       RuleFor(v => v.Data)
              .NotNull()
              .NotEmpty();

    }
    }

