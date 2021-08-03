# SmartAdmin for ASP.NET Core 3.1 - Documentation

## Table of Contents

1. **[Introduction](introduction.md)**
1. **[Getting Started](getting-started.md)**
1. **[Site Structure](site-structure.md)**
1. **Solution Architecture**
1. **[Customizations](customizations.md)**
1. **[How To Contribute](howto-contribute.md)**
1. **[Licensing Information](licensing-information.md)**
1. **[Changelog](changelog.md)**

---

### Tooling

The SmartAdmin for ASP.NET Core Theme is targeting ASP.NET Core 3.1 and has been verified to work with Visual Studio 2019 Community, Professional and Enterprise. Depending how comfortable you are with your development tools, the project can also be used with [Visual Studio Code](https://code.visualstudio.com) and [Jetbrains Rider](https://www.jetbrains.com/rider).

> Note: ASP.NET Core 3.x does not support Visual Studio 2017. Although you could still use .NET Core 2.1 to host your project we high recommend you upgrade when this choice is available to you.

### Dependencies

SmartAdmin for ASP.NET Core relies on the following frameworks:

- **Bootstrap 4.5**: Responsive layouts on mobile devices and beyond
- **FontAwesome 5.13**: A vast library of scalable vector icons (Pro included!)
- **jQuery 3.5**: ubiquitous JavaScript library that supports all major browsers
- **SmartAdmin 4.5**: The heart and back-bone of this template

### Cloud services

The ASP.NET Core project was designed with hosting on the Cloud in mind and can easily be expanded to suit any cloud platform, such as [AWS](https://aws.amazon.com) or [Azure](https://azure.microsoft.com/en-us). The Live Preview of the website is hosted on Azure and the following services were used for the application when deploying to Azure:

- **Azure App Service**: For providing serverless hosting of the .NET Core web application (on Linux Unbuntu!)
- **SQL Serverless**: Serverless data storage of relational data used by the application when it is being used
- **Azure Storage**: Storage of uploaded data and/or static assets required by the application (optional, but recommended for static content)

> **Note:** An on-premise database scenario is also supported and can be just as easily configured by adjusting the connection string and data provider within the Startup of the Application!

Please see the [Changing the Data Store](customizations.md#Datastore) page for more details on how to configure this part of the the Application.

## Authentication and Authorization

SmartAdmin for ASP.NET Core uses **ASP.NET Core Identity** for providing support for common authentication scenario's. Not only does this give you an out of the box secure experience but also showcases how we were able to have these two frameworks seemlessly work together and co-exist as they were shaped to match the **SmartAdmin 4** Theme look and feel.

> **Hint:** Out of the box **ASP.NET Core Identity** uses a `string` for the primary key of the default schema tables. If you wish to change this to `int` please check the instructions on the [Customizations](Customizations.md) page.

All of this, resting comfortably on the back of [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/) which has been setup with Migration support to get you started on adding your own tables and entity classes!

Storing the data is not in any way limited to a specific data store, however the project was written with `SQLite` as the storage provider as this comes out of the box when generating a .NET Core project using the [.NET CLI](https://docs.microsoft.com/en-us/dotnet/core/tools). It will not impose any restriction on using cloud-based database services that provide SQL Server instances, MySql, or cloud specific data stores such as Aurora, Cosmos DB and Dynamo DB.

> **Note:** If you are uncomfortable using **EntityFramework Core** then using an alternative ORM such as [Dapper](https://stackexchange.github.io/Dapper) is certainly also possible, but **will** take time and additional customizations to get you started and progressed equally with what comes out-of-the-box as part of the template when the default setup is used.

> **Hint:** For more ORM options please see: [What are popular ORMs for C#](https://www.slant.co/topics/16667/~orms-for-c)

### Authorization

Most pages on the site are accessible anonymously, meaning, you do not have to login first in order to see the content of the pages. However, some pages **are** enabled to be viewable only by authenticated users. Typically this is done by specifying the `[Authorize]` attribute on either your **Controller** or **PageModel** class.

SmartAdmin for .NET Core makes use of the global filters to ensure that every page that does **not** have any attribute specified is marked for Authentication, as such that the user will have to login in order to see the content.

#### Startup.cs

```cs
services
    .AddControllersWithViews(options =>
    {
        var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
        options.Filters.Add(new AuthorizeFilter(policy));
    });
```

Here we can see that we are using Controllers and Views (MVC) and we build a specific policy to require authenticated users only by utilizing the `AuthorizationPolicyBuilder` class to build our required policy. We then add this to the `Filters` collection so that it is registered for each `Controller` class in the project.

#### Razor Pages

For Razor Pages this is done slightly different.

```cs
app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages().RequireAuthorization();
});
```

Here we simply add a call to `RequireAuthorization()` to provide the same type of functionality as shown in the MVC example above this one.

## Configuration

The project relies on configuration settings at runtime, such as whether to use a local database or a Azure SQL Database for data storage, whether to load sample data, default account information and/or determining which theme sections are visible by default. These setting are now stored in the `appSettings.json` file. However, doing this could make it easier to accidentally expose secrets and/or sensitive information, so please be aware of who has access to this information.

> **Important:** When you publish the project to Azure or any other hosting provider, you should take care to protect these values.

Previously the project included values for toggles features in the configuration through the use of an `ActionFilter`. After giving it some thought we decided it would be a better approach to provide these settings to you by using **strong-typed** setting classes instead.

### SmartSettings

The settings that the **SmartAdmin for .NET Core** application uses are mapped to the `SmartSettings.cs` class, located in the **Models** folder of the project.

```json
"SmartSettings": {
  "Version": "4.2.0",
  "App": "SmartAdmin",
  "AppName": "SmartAdmin WebApp",
  "AppFlavor": "ASP.NET Core 3.1",
  "AppFlavorSubscript": ".NET Core 3.1",
  "Theme": {
    "ThemeVersion": "4.4.5",
    "IconPrefix": "fal",
    "Logo": "logo.png",
    "User": "Dr. Codex Lantern",
    "Email": "drlantern@gotbootstrap.com",
    "Twitter": "codexlantern",
    "Avatar": "avatar-admin.png"
  },
  "Features": {
    "AppSidebar": true,
    "AppHeader": true,
    "AppLayoutShortcut": true,
    "AppFooter": true,
    "ShortcutMenu": true,
    "ChatInterface": true,
    "LayoutSettings": true
  }
},
```

The setting values in the root of the `SmartSettings` node contains the base settings used throughout the Application to provide the name and context. The `Theme` section includes settings that are specific to the rendering of the SmartAdmin Theme and assist with pre-filling the **Login** and **Register** page.

> **Warning:** Please do **not** use this approach for your own project! Never expose sensitive information, such as user credentials in the source of your pages!

The `Features` section contains the Theme features that can be toggled on or off in order to limit what is visible/accessible when the application is started. When you add more features to your project and/or if you wish to include more sections of the Theme, feel free to add them here.

#### Example

One of the page sections that you might be inclined to hide could be the **Settings** panel that is accessed from the cogwheel on the right side of the Site. Although you could still expose this as part of a Management Portal, typically you would not want to expose this to every visitor of your site, or maybe you do, but let's assume for now that you don't.

#### Using SmartSettings

The first part of exposing the `SmartSettings` class is done in `Startup.cs`

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.Configure<SmartSettings>(Configuration.GetSection(SmartSettings.SectionName));

    // yoink!
}
```

Here we tell .NET to create and inject an instance of the `SmartSettings` class and bind the (public) properties to the values found in the `SmartSettings` section of the `appsettings.json` file. The output is added to the `ServiceCollection` which serves as a container with all of the registered types and instances made available to the Application using [Dependency Injection](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1).

---

Now that the `SmartSettings` class is registered through [DI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-3.1) we can make it available globally by **injecting** it in the `_ViewImports.cshtml` file.

```cs
@using SmartAdmin.WebUI
@using SmartAdmin.WebUI.Extensions
@using SmartAdmin.WebUI.Models
@inject SmartSettings Settings // <--- Over here!
@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers
@addTagHelper *, SmartAdmin.WebUI
```

And to wrap it up we reference the `Settings` variable from the injection step earlier and access the `Features` property, which exposes each listed feature element from the JSON file as a `boolean` property.

```cs
@if (Settings.Features.LayoutSettings)
{
  <div>
    // ommitted for demonstration purpose
  </div>
}
```

The `if`-statament checks whether the **LayoutSettings** feature is enabled before showing the rendered content inside.

## Template Structure

The application makes heavy use of both [Sections](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/layout?view=aspnetcore-3.1#sections) and [Partials](https://docs.microsoft.com/en-us/aspnet/core/mvc/views/partial) which are an intricate part of the Razor engine for ASP.NET Core used by both MVC as well as Razor Pages. We ensured that the names of these components match those of the HTML Theme so that any information listed regarding these sections is still applicable and relevant to the .NET Core Flavor.

### Layout

The main layout of the Theme is defined in `/Views/Shared/_Layout.cshtml`.

```html
<!DOCTYPE html>
<partial name="_CopyrightHeader"/>
<html lang="en">
  <head>
    <partial name="_Head"/>
    @RenderSection("HeadBlock", required: false)
  </head>
  <body class="mod-bg-1 mod-nav-link @ViewBag.PreemptiveClass">
    <partial name="_ScriptsLoadingSaving"/>
    <div class="page-wrapper">
      <div class="page-inner">
        <partial name="_LeftPanel"/>
        <div class="page-content-wrapper">
          <partial name="_PageHeader"/>
            <main id="js-page-content" role="main" class="page-content">
                <partial name="_PageBreadcrumb"/>
                <div class="subheader">
                    <partial name="_PageHeading"/>
                    @RenderSection("SubheaderBlock", required: false)
                </div>
                @RenderBody()
            </main>
          <partial name="_PageContentOverlay"/>
          <partial name="_PageFooter"/>
          <partial name="_ShortcutModal"/>
          <partial name="_ColorProfileReference"/>
        </div>
      </div>
    </div>
    <partial name="_ShortcutMenu"/>
    <partial name="_ShortcutMessenger"/>
    <partial name="_PageSettings"/>
    <partial name="_GoogleAnalytics"/>
    <partial name="_ScriptsBasePlugins"/>
    @RenderSection("ScriptsBlock", required: false)
  </body>
</html>
```

### Partials

Each `<partial />` element can be controlled by their respective `Feature` setting in the `appsettings.json` file (See [Configuration](#Configuration)). Simply change `true` to `false` and the partial content will not be rendered. As an alternative, and/or when you simply do not wish to include a feature in its entirety, just remove the `partial` element from the `_Layout.cshtml` file and it will not be included.

### Sections

The application uses **Sections** to denote dynamic content that can be influenced from within each content page inside the **Views** and/or **Pages** folder of the project.

The following sections are available in `_Layout.cshtml`:

|Section|Purpose|Reference|Optional|
|---|---|---|:-:|
|HeadBlock|Contains the page specific CSS files that are required|`Line 6`|yes|
|SubheaderBlock|Contains the page specific heading content|`Line 22`|yes|
|ScriptsBlock|Contains the page specific scripts and/or plugins that are required|`Line 39`|yes|

This setup allows for referencing plugin specific CSS and JS files required by a page without having to include it in every page, even when it is not used at all. However, if you do have specific files that should be made available throughout the Application then you can use the `_Head.cshtml` file for styles and the `_ScriptsBasePlugins.cshtml` file for scripts.

### Content

The majority of the included files are a direct representation of the associated page in the HTML Theme of SmartAdmin. However, page specific **style** and/or **script** files are loaded in through the aforementioned **sections** defined in the `_Layout.cshtml` page.

#### /Views/FormPlugins/Select2.cshtml

```html
@section HeadBlock {
  <link rel="stylesheet" media="screen, print" href="~/css/formplugins/select2/select2.bundle.css">
}

<!-- // page specific content there -->

@section ScriptsBlock {
  <script src="~/js/formplugins/select2/select2.bundle.js"></script>
  <script>
    $(document).ready(function () {
        $('.select2').select2();
    });
  </script>
}
```

> **Note:** The above example is a snippet from the **FormPlugins/Select2** page and shows how the [Select2](https://select2.org) plugin specific stylesheet and script is included in order to properly render the example content.

### Routing

The routing between both the **MVC** and **Razor Pages** flavors is largely kept the same. This means that `/foo/bar` is routed to the same equivalent content page for both technologies. Routing for MVC is handled by the **Action** methods of each `Controller` class inside the **/Controllers/** folder. When Razor Pages is used however the route is determined based on conventions, and where needed can be specified as part of the `@page` directive on the first line of the respective `.cshtml` file.

> **Note:** Automatic highlighting of the current menu item is done by inspecting the current route; this applies to both the parent and child menu item. Although .NET Core has a [community taghelper](https://damienbod.com/2018/08/13/is-active-route-tag-helper-for-asp-net-mvc-core-with-razor-page-support) to assist with this, the usage for how the Theme menu is currently rendered makes it impractical to use.

---

Copyright &copy; 2020 by Walapa. All rights reserved. This documentation or any portion thereof
may not be reproduced or used in any manner whatsoever without the express written permission of the publisher except for the use of brief quotations in a review.
