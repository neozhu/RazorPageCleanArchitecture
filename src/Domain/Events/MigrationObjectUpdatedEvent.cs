// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Domain.Events;


    public class MigrationObjectUpdatedEvent : DomainEvent
    {
        public MigrationObjectUpdatedEvent(MigrationObject item)
        {
            Item = item;
        }

        public MigrationObject Item { get; }
    }

