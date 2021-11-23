// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Domain.Events;

    public class MigrationTemplateFileDeletedEvent : DomainEvent
    {
        public MigrationTemplateFileDeletedEvent(MigrationTemplateFile item)
        {
            Item = item;
        }

        public MigrationTemplateFile Item { get; }
    }

