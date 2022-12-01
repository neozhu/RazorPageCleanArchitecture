using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitecture.Application.IntegrationTests.Services;

using static Testing;
public class DateTimeServiceTest : TestBase
{
    [Test]
    public async Task ShouldGetDateTime()
    {
        var testservice = await GetRequiredService<IDateTime>();
        var result = testservice.Now;
        result.Should().As<DateTime>().Should().BeBefore(DateTime.Now);
    }
}
