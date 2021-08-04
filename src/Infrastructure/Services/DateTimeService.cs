using CleanArchitecture.Razor.Application.Common.Interfaces;
using System;

namespace CleanArchitecture.Razor.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
