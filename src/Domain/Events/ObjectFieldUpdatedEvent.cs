// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Domain.Events;


    public class ObjectFieldUpdatedEvent : DomainEvent
    {
        public ObjectFieldUpdatedEvent(ObjectField item)
        {
            Item = item;
        }

        public ObjectField Item { get; }
    }

