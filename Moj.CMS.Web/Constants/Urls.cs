using MudBlazor;
namespace Moj.CMS.Web.Constants
{
    public static class Urls
    {
        public static class Administration
        {
            public const string Tilte = "Administration";
            public static bool IsExpanded { get; set; }
            public static string[] SectionUrls => new[]
            {
                Hangfire.Href,
                Dashboard.Href,
                Logs.Href
            };

            public static class Hangfire
            {
                public const string Href = "/jobs";
                public static string Icon = Icons.Material.Outlined.Work;
                public static string Title = "Hangfire";
            }

            public static class Dashboard
            {
                public const string Href = "/";
                public static string Icon = Icons.Material.Outlined.Dashboard;
                public static string Title = "Dashboard";
            }

            public static class Logs
            {
                public const string Href = "/logs";
                public static string Icon = Icons.Material.Outlined.TrackChanges;
                public static string Title = "Logs";
            }
        }

        public static class Settings
        {
            public const string Tilte = "Settings";
            public static bool IsExpanded { get; set; }
            public static string[] SectionUrls => new[]
            {
                Lists.Href,
                Notify.Href
            };
            public static class Lists
            {
                public const string Href = "/settings/lists";
                public static string Icon = Icons.Material.Outlined.FormatListBulleted;
                public static string Title = "System Lists";
            }

            public static class Notify
            {
                public const string Href = "/settings/notifications";
                public static string Icon = Icons.Material.Outlined.Notifications;
                public static string Title = "Notifications";
            }
        }

        public static class General
        {
            public const string Tilte = "Cases Menu";
            public static bool IsExpanded { get; set; }
            public static string[] SectionUrls => new[]
            {
                Cases.Href,
                Promissories.Href,
                Parties.Href,
                Claims.Href,
                SadadInvoices.Href,
                AdjustmentReports.Href,
                Adjustments.Href,
                SeizingOrders.Href,
                Reports.Href,
                VIbans.Href
            };
            public static class Cases
            {
                public const string Href = "/cases";
                public const string ProfileHref = Href + "/profile/";
                public static string Icon = Icons.Material.Outlined.Gavel;
                public static string Title = "Cases";
            }

            public static class Promissories
            {
                public const string Href = "/Promissories";
                public static string Icon = Icons.Material.Outlined.Description;
                public static string Title = "Promissories";
            }

            public static class Parties
            {
                public const string Href = "/parties";
                public const string ProfileHref = Href + "/profile/";
                public static string Icon = Icons.Material.Outlined.Groups;
                public static string Title = "Parties";
            }

            public static class Claims
            {
                public const string Href = "/claims";
                public const string ProfileHref = Href + "/profile/";
                public static string Icon = Icons.Material.Outlined.RequestQuote;
                public static string Title = "Financial Claims";
            }

            public static class SadadInvoices
            {
                public const string Href = "/sadadInvoices";
                public const string ProfileHref = Href + "/profile/";
                public static string Icon = Icons.Material.Outlined.Receipt;
                public static string Title = "Sadad Invoices";
            }

            public static class AdjustmentReports
            {
                public const string Href = "/AdjustmentReports";
                public static string Icon = Icons.Material.Outlined.FileCopy;
                public static string Title = "Adjustment Reports";
            }

            public static class Adjustments
            {
                public const string Href = "/adjustments";
                public static string Icon = Icons.Material.Outlined.MiscellaneousServices;
                public static string Title = "Adjustments";
            }

            public static class SeizingOrders
            {
                public const string Href = "/seizing/orders";
                public static string Icon = Icons.Material.Outlined.MoneyOff;
                public static string Title = "Seizing Orders";
            }

            public static class Reports
            {
                public const string Href = "/reports";
                public static string Icon = Icons.Material.Outlined.Score;
                public static string Title = "Reports";
            }

            public static class VIbans
            {
                public const string Href = "/VIbans";
                public static string Icon = Icons.Outlined.AccountBalance;
                public static string Title = "VIbans";
            }

        }

        public static class UserAdministration
        {
            public const string Tilte = "User Administration";
            public static bool IsExpanded { get; set; }
            public static string[] SectionUrls => new[]
            {
                Users.Href,
                Roles.Href
            };
            public static class Users
            {
                public const string Href = "/users";
                public const string ProfileHref = Href + "/profile/";
                public const string UserRolesHref = Href + "/roles/";
                public static string Icon = Icons.Material.Outlined.Person;
                public static string Title = "Users";
            }

            public static class Roles
            {
                public const string Href = "/roles";
                public const string RolePermissions = Href + "/role-permissions/";
                public static string Icon = Icons.Material.Outlined.Security;
                public static string Title = "Roles";
            }
        }
    }
}



