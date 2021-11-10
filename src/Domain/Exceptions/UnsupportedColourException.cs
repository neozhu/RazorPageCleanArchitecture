// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;

namespace CleanArchitecture.Razor.Domain.Exceptions
{
    public class UnsupportedColourException : Exception
    {
        public UnsupportedColourException(string code)
            : base($"Colour \"{code}\" is unsupported.")
        {
        }
    }
}
