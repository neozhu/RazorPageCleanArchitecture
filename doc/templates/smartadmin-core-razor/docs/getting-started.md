# SmartAdmin for ASP.NET Core 3.1 - Documentation

## Table of Contents

1. **[Introduction](introduction.md)**
1. **Getting Started**
1. **[Site Structure](site-structure.md)**
1. **[Solution Architecture](solution-architecture.md)**
1. **[Customizations](customizations.md)**
1. **[How To Contribute](howto-contribute.md)**
1. **[Licensing Information](licensing-information.md)**
1. **[Changelog](changelog.md)**

---

Please use the quick instructions below to load the project and start exploring its reach set of features. If you wish to change the default database we have provided you with a quick instruction on howto switch it out for another one.

- For more instructions please refer to the **Customization** menu item under **Documentation**
- If you wish to learn more about the site structure please to the **Site Structure** menu item under **Documentation**
- Dying to know what was changed/added in this latest update of SmartAdmin for .NET Core? Then pleas head over to the **Changelog** menu item under **Documentation**

## Using Visual Studio 2019 to load the Solution

1. Navigate to the folder containing the extracted project package and double-click the SmartAdminCore.sln file
   - **Note:** You can no longer use Visual Studio 2017 for .NET Core 3.1 projects, for working with .NET Core 2.1 you will need to down-target the framework to `netcoreapp2.1` and then you can still use Visual Studio 2017, but we highly recommend upgrading to the latest version.
   - **Warning:** ASP.NET Core 2.2 is no longer supported by Microsoft and as such it is no longer included as part of the package.
1. Once the project is loaded press `CTRL+F5` to launch the website using IIS Express.
   - The site has been configured to use **SSL/HTTPS** by default.
1. Should you be presented with the **Login** page, simply press **Secure Login** to enter the website using the default user.
   - The default credentials should be pre-filled for you already.
   - If you are presented with an **Invalid login** error then please click **Register** to add the default account.

## Running SmartAdmin for .NET Core

> Note: If your computer is already provisioned with the necessary .NET prerequisites, then feel free to skip ahead to **step 3**.

1. Download and install the ASP.NET Core 3.1 SDK here: <https://dotnet.microsoft.com/download/dotnet-core/3.1>
   - You can verify the installation by opening a command line tool of your choice and running the following command: dotnet --info
   - If you get a message similar to: dotnet is not recognized as an internal command, then please try downloading the 32-bit version of the ASP.NET Core 3.1 SDK
   - You can find it here: <https://dotnet.microsoft.com/download/dotnet-core/3.1>
1. SmartAdmin for ASP.NET Core 3.1 comes built-in with **SqlLite** as part of EntityFramework Core (No additional installation is required to use this as your data store)
   - Note: You can download and install **SQL Server Express** here: <https://go.microsoft.com/fwlink/?linkid=853017>.
   - Note: You can also install **LocalDB** as part of Visual Studio. During Visual Studio installation, select the **.NET desktop development** workload, which includes **SQL Server Express LocalDB**.
   - If you are going to use **SQL Express** then the default connection string value in `appSettings.json` will need to be adjusted
   - The value and instructions are written in Startup.cs but also listed here for your convenience:
   - `"Server=localhost\\SQLEXPRESS;Database=aspnet-smartadmin;Trusted_Connection=True;MultipleActiveResultSets=true"`
1. Open a command prompt and ensure you are inside the directory containing the **ASP.NET Core 3.1 project sources** of SmartAdmin for .NET Core
   - This is the directory containing the `SmartAdminCore.sln` file
1. Run the following commands (in order) using the command line tool of your choice:
   - `dotnet build` (this triggers `dotnet restore` as well)
   - `dotnet publish` (**optional** unless you require a specific deployment)
   - `dotnet run --project ./src/SmartAdmin.WebUI`
   - **Note:** You can also use the `watch` command to automatically reload your website when you change a file
   - `dotnet watch --project ./src/SmartAdmin.WebUI run`
1. Launch your favorite browser and enter the following URL: `https://localhost:5001`, and try to login using the provided credentials
   - You may get a page mentioning that: 'Applying the existing migrations may resolve this issue'
   - Go ahead and click on the blue `Apply Migrations` button
1. If you receive a message stating: '**Invalid login attempt**' then go ahead and **Register** the default user
   - The username and password should be pre-filled for demo purposes

---

Copyright &copy; 2020 by Walapa. All rights reserved. This documentation or any portion thereof
may not be reproduced or used in any manner whatsoever without the express written permission of the publisher except for the use of brief quotations in a review.
