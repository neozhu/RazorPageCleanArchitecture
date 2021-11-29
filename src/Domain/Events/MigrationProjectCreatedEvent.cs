// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Domain.Events;

    public class MigrationProjectCreatedEvent : DomainEvent
    {
        public MigrationProjectCreatedEvent(MigrationProject item)
        {
            Item = item;
        }

        public MigrationProject Item { get; }
    }

