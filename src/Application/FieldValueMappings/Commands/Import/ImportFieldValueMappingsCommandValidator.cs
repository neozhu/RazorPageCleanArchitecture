// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldValueMappings.Commands.Import;

    public class ImportFieldValueMappingsCommandValidator : AbstractValidator<ImportFieldValueMappingsCommand>
    {
        public ImportFieldValueMappingsCommandValidator()
        {
           
            RuleFor(v => v.Data)
                 .NotNull()
                 .NotEmpty();
           
        }
    }

