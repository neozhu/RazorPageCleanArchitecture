using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace CleanArchitecture.Razor.Infrastructure.Constants.Permission
{
    public static class Permissions
    {
        [DisplayName("Workflow")]
        [Description("Approval Permissions")]
        public static class Approval
        {
            public const string View = "Permissions.Approval.View";
            public const string Approve = "Permissions.Approval.Approve";
            public const string Histories = "Permissions.Approval.Histories";
            public const string Search = "Permissions.Approval.Search";
            public const string Export = "Permissions.Approval.Export";
        }
        [DisplayName("SalesContracts")]
        [Description("SalesContracts Permissions")]
        public static class SalesContracts
        {
            public const string View = "Permissions.SalesContracts.View";
            public const string Create = "Permissions.SalesContracts.Create";
            public const string Edit = "Permissions.SalesContracts.Edit";
            public const string Delete = "Permissions.SalesContracts.Delete";
            public const string Search = "Permissions.SalesContracts.Search";
            public const string Export = "Permissions.SalesContracts.Export";
            public const string Import = "Permissions.SalesContracts.Import";
            public const string ViewTerms = "Permissions.SalesContracts.ViewTerms";
            public const string AddTerms = "Permissions.SalesContracts.AddTerms";
            public const string DeleteTerms = "Permissions.SalesContracts.DeleteTerms";
            public const string UpdateTerms = "Permissions.SalesContracts.UpdateTerms";
        }
        [DisplayName("SalesContractDetails")]
        [Description("SalesContractDetails Permissions")]
        public static class SalesContractDetails
        {
            public const string View = "Permissions.SalesContractDetails.View";
            public const string Create = "Permissions.SalesContractDetails.Create";
            public const string Edit = "Permissions.SalesContractDetails.Edit";
            public const string Delete = "Permissions.SalesContractDetails.Delete";
            public const string Search = "Permissions.SalesContractDetails.Search";
            public const string Export = "Permissions.SalesContractDetails.Export";
            public const string Import = "Permissions.SalesContractDetails.Import";
        }
        [DisplayName("PurchaseContracts")]
        [Description("PurchaseContracts Permissions")]
        public static class PurchaseContracts
        {
            public const string View = "Permissions.PurchaseContracts.View";
            public const string Create = "Permissions.PurchaseContracts.Create";
            public const string Edit = "Permissions.PurchaseContracts.Edit";
            public const string Delete = "Permissions.PurchaseContracts.Delete";
            public const string Search = "Permissions.PurchaseContracts.Search";
            public const string Export = "Permissions.PurchaseContracts.Export";
            public const string Import = "Permissions.PurchaseContracts.Import";
            public const string ViewTerms = "Permissions.PurchaseContracts.ViewTerms";
            public const string AddTerms = "Permissions.PurchaseContracts.AddTerms";
            public const string DeleteTerms = "Permissions.PurchaseContracts.DeleteTerms";
            public const string UpdateTerms = "Permissions.PurchaseContracts.UpdateTerms";
        }
        [DisplayName("PurchaseContractDetails")]
        [Description("PurchaseContractDetails Permissions")]
        public static class PurchaseContractDetails
        {
            public const string View = "Permissions.PurchaseContractDetails.View";
            public const string Create = "Permissions.PurchaseContractDetails.Create";
            public const string Edit = "Permissions.PurchaseContractDetails.Edit";
            public const string Delete = "Permissions.PurchaseContractDetails.Delete";
            public const string Search = "Permissions.PurchaseContractDetails.Search";
            public const string Export = "Permissions.PurchaseContractDetails.Export";
            public const string Import = "Permissions.PurchaseContractDetails.Import";
        }
        [DisplayName("PurchaseOrders")]
        [Description("PurchaseOrders Permissions")]
        public static class PurchaseOrders
        {
            public const string View = "Permissions.PurchaseOrders.View";
            public const string Create = "Permissions.PurchaseOrders.Create";
            public const string Edit = "Permissions.PurchaseOrders.Edit";
            public const string Delete = "Permissions.PurchaseOrders.Delete";
            public const string Search = "Permissions.PurchaseOrders.Search";
            public const string Export = "Permissions.PurchaseOrders.Export";
            public const string Import = "Permissions.PurchaseOrders.Import";
        }
        [DisplayName("Projects")]
        [Description("Projects Permissions")]
        public static class Projects
        {
            public const string View = "Permissions.Projects.View";
            public const string Create = "Permissions.Projects.Create";
            public const string Edit = "Permissions.Projects.Edit";
            public const string Delete = "Permissions.Projects.Delete";
            public const string Search = "Permissions.Projects.Search";
            public const string Export = "Permissions.Projects.Export";
            public const string Import = "Permissions.Projects.Import";
        }
        [DisplayName("Products")]
        [Description("Products Permissions")]
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
            public const string Search = "Permissions.Products.Search";
            public const string Export = "Permissions.Products.Export";
            public const string Import = "Permissions.Products.Import";
        }
        [DisplayName("Customers")]
        [Description("Customers Permissions")]
        public static class Customers
        {
            public const string View = "Permissions.Customers.View";
            public const string Create = "Permissions.Customers.Create";
            public const string Edit = "Permissions.Customers.Edit";
            public const string Delete = "Permissions.Customers.Delete";
            public const string Search = "Permissions.Customers.Search";
            public const string Export = "Permissions.Customers.Export";
            public const string Import = "Permissions.Customers.Import";
        }

        [DisplayName("Categories")]
        [Description("Categories Permissions")]
        public static class Categories
        {
            public const string View = "Permissions.Categories.View";
            public const string Create = "Permissions.Categories.Create";
            public const string Edit = "Permissions.Categories.Edit";
            public const string Delete = "Permissions.Categories.Delete";
            public const string Search = "Permissions.Categories.Search";
            public const string Export = "Permissions.Categories.Export";
            public const string Import = "Permissions.Categories.Import";
        }

        [DisplayName("Documents")]
        [Description("Documents Permissions")]
        public static class Documents
        {
            public const string View = "Permissions.Documents.View";
            public const string Create = "Permissions.Documents.Create";
            public const string Edit = "Permissions.Documents.Edit";
            public const string Delete = "Permissions.Documents.Delete";
            public const string Search = "Permissions.Documents.Search";
            public const string Export = "Permissions.Documents.Export";
            public const string Import = "Permissions.Documents.Import";
            public const string Download = "Permissions.Documents.Download";
        }
        [DisplayName("DocumentTypes")]
        [Description("DocumentTypes Permissions")]
        public static class DocumentTypes
        {
            public const string View = "Permissions.DocumentTypes.View";
            public const string Create = "Permissions.DocumentTypes.Create";
            public const string Edit = "Permissions.DocumentTypes.Edit";
            public const string Delete = "Permissions.DocumentTypes.Delete";
            public const string Search = "Permissions.DocumentTypes.Search";
            public const string Export = "Permissions.Documents.Export";
            public const string Import = "Permissions.Categories.Import";
        }
        [DisplayName("Dictionaries")]
        [Description("Dictionaries Permissions")]
        public static class Dictionaries
        {
            public const string View = "Permissions.Dictionaries.View";
            public const string Create = "Permissions.Dictionaries.Create";
            public const string Edit = "Permissions.Dictionaries.Edit";
            public const string Delete = "Permissions.Dictionaries.Delete";
            public const string Search = "Permissions.Dictionaries.Search";
            public const string Export = "Permissions.Dictionaries.Export";
            public const string Import = "Permissions.Dictionaries.Import";
        }

        [DisplayName("Users")]
        [Description("Users Permissions")]
        public static class Users
        {
            public const string View = "Permissions.Users.View";
            public const string Create = "Permissions.Users.Create";
            public const string Edit = "Permissions.Users.Edit";
            public const string Delete = "Permissions.Users.Delete";
            public const string Search = "Permissions.Users.Search";
            public const string Import = "Permissions.Users.Import";
            public const string Export = "Permissions.Dictionaries.Export";
            public const string ManageRoles = "Permissions.Users.ManageRoles";
            public const string RestPassword = "Permissions.Users.RestPassword";
            public const string Active = "Permissions.Users.Active";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
            public const string Search = "Permissions.Roles.Search";
            public const string Export = "Permissions.Roles.Export";
            public const string Import = "Permissions.Roles.Import";
            public const string ManagePermissions = "Permissions.Roles.Permissions";
            public const string ManageNavigation = "Permissions.Roles.Navigation";
        }

        [DisplayName("Role Claims")]
        [Description("Role Claims Permissions")]
        public static class RoleClaims
        {
            public const string View = "Permissions.RoleClaims.View";
            public const string Create = "Permissions.RoleClaims.Create";
            public const string Edit = "Permissions.RoleClaims.Edit";
            public const string Delete = "Permissions.RoleClaims.Delete";
            public const string Search = "Permissions.RoleClaims.Search";
        }



        [DisplayName("Dashboards")]
        [Description("Dashboards Permissions")]
        public static class Dashboards
        {
            public const string View = "Permissions.Dashboards.View";
        }

        [DisplayName("Hangfire")]
        [Description("Hangfire Permissions")]
        public static class Hangfire
        {
            public const string View = "Permissions.Hangfire.View";
        }


        /// <summary>
        /// Returns a list of Permissions.
        /// </summary>
        /// <returns></returns>
        public static List<string> GetRegisteredPermissions()
        {
            var permissions = new List<string>();
            foreach (var prop in typeof(Permissions).GetNestedTypes().SelectMany(c => c.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)))
            {
                var propertyValue = prop.GetValue(null);
                if (propertyValue is not null)
                    permissions.Add(propertyValue.ToString());
            }
            return permissions;
        }


    }
}
