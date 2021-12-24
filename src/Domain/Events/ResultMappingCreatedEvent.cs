﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CleanArchitecture.Razor.Domain.Events;

    public class ResultMappingCreatedEvent : DomainEvent
    {
        public ResultMappingCreatedEvent(ResultMapping item)
        {
            Item = item;
        }

        public ResultMapping Item { get; }
    }
