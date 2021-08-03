# SmartAdmin for ASP.NET Core 3.1

SmartAdmin for ASP.NET Core is an open-source and cross-platform framework for building modern cloud based internet connected applications, such as web apps, IoT apps and mobile backends. SmartAdmin for ASP.NET Core apps can run on .NET Core or on the full .NET Framework. It was architected to provide an optimized development framework for apps that are deployed to the cloud or run on-premises. It consists of modular components with minimal overhead, so you retain flexibility while constructing your solutions. You can develop and run your SmartAdmin for ASP.NET Core apps cross-platform on Windows, Mac and Linux. [Learn more about ASP.NET Core](https://docs.microsoft.com/aspnet/core/).

## Get Started

Follow the **Getting Started** instructions in the SmartAdmin for ASP.NET Core **docs** folder or directly from the **ASP.NET Core 3.1** menu after launching the website locally.

Or use the following instructions to setup a local server running SmartAdmin for ASP.NET Core:

1. Download and install the **ASP.NET Core 3.1 SDK** here: https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x64-installer
    * You can verify the installation by opening a command line tool of your choice and running the following command: `dotnet --info`
    * If you get a message similar to: `dotnet is not recognized as an internal command`, then please try downloading the `32-bit` version of the ASP.NET Core 3.1 SDK
    * You can find it here: https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.102-windows-x86-installer
1. Download and install **SQL Server Express** here: https://go.microsoft.com/fwlink/?linkid=853017
    * Note: You can also install **LocalDB** as part of Visual Studio. During Visual Studio installation, select the **.NET desktop development** workload, which includes SQL Server Express LocalDB.
    * If you are going to SQL Express then the default connection string *value* in `appSettings.json` will need to be adjusted
    * The value and instructions are written in `Startup.cs` but also listed here for your convinience:
    * `"Server=localhost\\SQLEXPRESS;Database=aspnet-smartadmin;Trusted_Connection=True;MultipleActiveResultSets=true"`
1. Open a command prompt and ensure you are inside the directory containing the **ASP.NET Core 3.1 project sources** of SmartAdmin 4.0
    * This is the directory containing the `SmartAdminCore.sln` file
1. Run the following commands (in order) using the command line tool of your choice:
    * `dotnet build`
    * `dotnet publish` *(optional for localhost only deployment)*
    * `dotnet run --project ./src/SmartAdmin.WebUI/SmartAdmin.WebUI.csproj`
1. Launch your favorite browser and enter the following URL: https://localhost:5001, and try to login using the provided credentials
    * You may get a page mentioning that: 'Applying the existing migrations may resolve this issue'
    * Go ahead and click on the blue `Apply Migrations` button
1. If you receive a message stating: 'Invalid login attempt' then go ahead and Register the user
    * The username and password should be prefilled for demo purposes

> Note: You might get a security warning when browsing to your website, as the default `localhost` server will typically not have a trusted **localhost** SSL certificate!

Also check out the [.NET Homepage](https://www.microsoft.com/net) for released versions of .NET, getting started guides, and learning resources.

## How to Engage, Contribute, and Give Feedback

Some of the best ways to contribute are to try things out, file issues, join in design conversations, and make pull-requests.

* Download our latest builds
* Follow along with the development of SmartAdmin for ASP.NET Core:
  * [Roadmap](https://support.gotbootstrap.com/t/asp-net-core): The schedule and milestone themes for SmartAdmin for ASP.NET Core.
* Check out the [contributing](CONTRIBUTING.md) page to see the best places to log issues and start discussions.

## Reporting security issues and bugs

Security issues and bugs should be reported privately, via email, to SmartAdmin Security (SAC) at <secure@walapa.nl>. You should receive a response within 72 hours. If for some reason you do not, please follow up via email to ensure we received your original message.

## Code of conduct

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).  For more information, see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [smartadmin-next@walapa.nl](mailto:smartadmin-next@walapa.nl) with any additional questions or comments.
