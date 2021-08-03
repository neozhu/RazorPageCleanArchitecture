# SmartAdmin for ASP.NET Core 3.1 - Documentation

## Table of Contents

1. **[Introduction](introduction.md)**
1. **[Getting Started](getting-started.md)**
1. **[Site Structure](site-structure.md)**
1. **[Solution Architecture](solution-architecture.md)**
1. **Customizations**
1. **[How To Contribute](howto-contribute.md)**
1. **[Licensing Information](licensing-information.md)**
1. **[Changelog](changelog.md)**

---

### Customizations

Listed below are some of the most common and/or most voted instructions for making modifications to the SmartAdmin for .NET Core project. These instructions are not mandatory to be executed for your project, so please read them carefully before deciding to execute one of them, you will not unlock any hidden achievements (yet!). If they are applicable they should allow you to achieve the result you were after.

Is the instruction not working properly for you and/or you believe we overlooked a crucial step? Please let us know on the [Support Forum](https://support.gotbootstrap.com/t/asp-net-core) so we can address it and ensure it is fixed for the next update! Your help and feedback is always appreciated.

---

#### Instructions

1. [Switch ASP.NET Core Identity to use int for Keys](#SwitchASP.NETCoreIdentitytouseintforKeys)
1. [Renaming the Application and Project](#RenamingtheApplicationandProject)
1. [Using MySQL/Aurora as the data store](#UsingMySQL/AuroraastheDataStore)

## Switch ASP.NET Core Identity to use int for Keys

Changing the primary key from `string` to `int` consists of 4 main steps:

1. Creating our own **User** and **Role** identity classes
1. Instructing our `DbContext` implementation to use these custom classes
1. Registering everything with EntityFramework Core
1. Updating the (existing) data store schema

### ApplicationUser and ApplicationRole

First lets create the `ApplicationUser` and `ApplicationRole` class. We do not need to specify any other properties or methods since our only intent is to change the primary key of these two classes.

```cs
public class ApplicationUser : IdentityUser<int>
{
}

public class ApplicationRole : IdentityRole<int>
{
}
```

We ensure that the implementation specifies that we want to use `int` as our primary key type (`TKey`).

> **Hint:** Unless you want to change the primary key across **all** of the Identity classes the above examples are sufficient to get your started. For **full** instructions on changing the primary key please refer to the documentation link at the end of this instruction.

### Adjusting ApplicationDbContext

Now let's open the `ApplicationDbContext` class located in the **Data** folder of the project and adjust the implementation to use our newly created classes.

```cs
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
```

### Registration with EntityFramework

Open up the `Startup.cs` file in the root of the **SmartAdmin.WebUI** project, and look for these lines of code:

```cs
services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

Change it to match the following:

```cs
services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoleManager<RoleManager<ApplicationRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

This instructs ASP.NET Identity that our newly created classes are used and exposed throughout. We also overwrite the type for the `RoleManager` so that **Claims** are added to the user after login using the new `ApplicationUser` and `ApplicationRole` types.

### Update the Identity Schema

Since the ASP.NET Identity tables already exist with a different primary key, we will need to make some additional changes. The easiest route to take here is to just delete the existing database. Once the Application is started again the database will be re-created with the correct schema and you can then add the default user user again using the **Registration** page.

If you cannot delete the database, then we will need to run code first migrations to change the tables. However, by taking this route the new integer primary key will not be set up as a SQL IDENTITY property in the database. You will have to manually set the Id column as an IDENTITY using SQL Management Studio or other tooling/script.

---

And that's it! Both `ApplicationUser` and `ApplicationRole` are now using `int` as their primary keys and SmartAdmin for .NET Core can be used as before!

#### Reference

> If you wish to change more than just the **User** and **Role** you can find out more information about this topic on the [Change Primary Key](https://docs.microsoft.com/en-us/aspnet/identity/overview/extensibility/change-primary-key-for-users-in-aspnet-identity) page, as part of the **ASP.NET Identity** section of the official documentation.

## Renaming the Application and Project

Very often and not uncommon to do as one of the first steps after you have choosen the Edition you want to use as the basis for your project is to ensure that it is fully renamed, or whitelabeled, using the company and/or project name that you are working on.

The steps below aim to provide you with the required instructions to rename all project related folder, files and namespaces and to adjust the application settings to give your project a head start.

The following steps will need to be performed:

1. Renaming the Solution and Project(s)
1. Update Code References
1. Update Application Settings
1. Rename Project Directory

### Renaming Solution and Project

> **Note:** Using the instructions below you would replace `Acme` with the name of your choice, we used this value for the sake of the example and instruction

Assuming you have the project open in Visual Studio 2019:

- Right-click on the Solution and press the `F2` key to rename it (e.g. `Acme`)
  - This creates a solution file called `Acme.sln` in the same directory
- Right-click on the Project and press the `F2` key to rename it (e.g. `Acme.WebUI`)
  - This creates a project file called `Acme.WebUI.csproj` in the same directory
  - The root namespace of the project is also changed to `Acme.WebUI`

### Update Code References

Now we need to update the existing files to use the new namespace.

1. With Visual Studio still open press the key combination: `SHFT+CTRL+H`
1. Enter the following values on the the **Replace in Files** dialog
   - Enter the search term `SmartAdmin`
   - Enter the replacement term `Acme`
   - Restrict the search to `*.cs` files
   - Select the **Match Case** checkbox
   - Click on **Replace All**
1. Enter the following values on the the **Replace in Files** dialog
   - Enter the search term `using SmartAdmin`
   - Enter the replacement term `using Acme`
   - Restrict the search to `*.cshtml` files
   - Select the **Match Case** checkbox
   - Click on **Replace All**
1. Enter the following values on the the **Replace in Files** dialog
   - Enter the search term `SmartAdmin.WebUI`
   - Enter the replacement term `Acme.WebUI`
   - Restrict the search to `*.cshtml` files
   - Select the **Match Case** checkbox
   - Click on **Replace All**

### Update Application Settings

1. Open the file `appsettings.json` located in the root of the `Acme.WebUI` project
1. Press the key combination: `SHFT+H`
   - Replace `SmartAdmin` with `Acme`
   - Click on **Replace All**
1. Build and Launch the website
   - Press `SHFT+CTRL+B` to build the Application and check for faulty output
   - Press `CTRL+F5` to launch the Application in IIS Express

### Rename Project Directory

Unfortunately this does not give us a complete rename operation as the initial folder of the **WebUI** project will still be named `SmartAdmin.WebUI`. In order to address this we will need to carry out a small set of last steps.

1. Close Visual Studio 2019
1. Use explorer (or any other file manager program) to open up the SmartAd...I mean Acme folder
1. Locate the `./src/SmartAdmin.WebUI` folder and rename it to `Acme.WebUI`
1. Open the `Acme.sln` file in the root of the project package folder in your favorite editor
1. Press `CTRL+H` and replace `SmartAdmin` with `Acme`

That should be it! Your project is now fully renamed to use the name you prefer! It would be nice if Visual Studio would go the extra mile to ensure renaming is less cumbersome but luckily this is usually only a one time effort.

## Using MySQL/Aurora as the data store

In the interactive instructions we already demonstrated how a few simple steps allowed you to use SQL Server instead of SQLite as the data store. With other data stores out there we felt it would be helpful to also describe the steps required to use MySql/Aurora instead. Aurora is a data store created by Amazon AWS and is based on top of MySql and will work using the same instructions.

The following steps are required to switch the data provider to MySql

1. Choose the correct NuGet package
1. Switch the Data Provider to MySql
1. Adjust the connection string

### Choosing the right NuGet package

At the time of writing Oracle has yet to publish a .NET Core 3.x compatible driver package. We would still recommend using this package when it is released, but for the time being we are required to choose a different package in order to support MySql on .NET Core 3.1.

1. Right-click your Solution and choose: **Manage NuGet packages for Solution**
1. Make sure the **Browse** tab is selected and search for the `Pomelo.EntityFrameworkCore.MySql` package
1. Add the package to the `Acme.WebU` erhh...the `SmartAdmin.WebUI` project
1. Open the `Startup.cs` file in the root of the project
   - Replace `services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));`
   - With: `services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));`
1. Open the `appsettings.json` file in the root of the project
   - Change the connection string value to a MySql compatible connection string
   - e.g. `server=[DB-Server Name];port=3306;database=[DB-Name];uid=[User-ID];password=[Password]`

In case you are getting any errors and/or issues with mismatching types, you will need to drop the **Migrations** folder inside the project and create a new migration

1. Open the **Package Manager Console**
1. Type: `Add-Migration InitialCreate`
1. Then type: `Update-Database`

This should ensure that the data store is initialized using proper MySql schema types and updates the snapshot of the model accordingly.

> Please see the [MySql and .NET Core 3.0 Identity](https://www.c-sharpcorner.com/article/using-asp-net-core-3-0-identity-with-mysql/) article for more details and information.

---

Copyright &copy; 2020 by Walapa. All rights reserved.
This documentation or any portion thereof may not be reproduced or used in any manner whatsoever without the express written permission of the publisher except for the use of brief quotations in a review.
