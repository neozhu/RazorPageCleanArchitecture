// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Application.FieldMappingValues.Commands.Delete;

    public class DeleteFieldMappingValueCommandValidator : AbstractValidator<DeleteFieldMappingValueCommand>
    {
        public DeleteFieldMappingValueCommandValidator()
        {
           
           RuleFor(v => v.Id).NotNull().GreaterThan(0);
          
        }
    }
    public class DeleteCheckedFieldMappingValuesCommandValidator : AbstractValidator<DeleteCheckedFieldMappingValuesCommand>
    {
        public DeleteCheckedFieldMappingValuesCommandValidator()
        {
         
             RuleFor(v => v.Id).NotNull().NotEmpty();
         
        }
    }

