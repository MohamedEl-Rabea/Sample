using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Shared.Constants.Permission
{
    public class PermissionsProvider : IPermissionsProvider
    {
        private static Permission UsersPermissionsModule = new Permission
        {
            Code = "Permissions.Users",
            Name = "Users",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Users.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Users.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Users.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Users.View,
                    Name = "View",
                },
            }
        };
        private static Permission RolesPermissionsModule = new Permission
        {
            Code = "Permissions.Roles",
            Name = "Roles",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Roles.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Roles.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Roles.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Roles.View,
                    Name = "View",
                },
            }
        };
        private static Permission CasesPermissionsModule = new Permission
        {
            Code = "Permissions.Cases",
            Name = "Cases",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Cases.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Cases.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Cases.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Cases.View,
                    Name = "View",
                },
                new Permission
                {
                    Code = Permissions.Cases.ChangeCourtDetails,
                    Name = "Change Court Details",
                },
            }
        };
        private static Permission PartiesPermissionsModule = new Permission
        {
            Code = "Permissions.Parties",
            Name = "Parties",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Parties.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Parties.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Parties.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Parties.View,
                    Name = "View",
                },
            }
        };

        private static Permission ClaimsPermissionsModule = new Permission
        {
            Code = "Permissions.Claims",
            Name = "Claims",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Claims.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Claims.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Claims.Close,
                    Name = "Close",
                },
                new Permission
                {
                    Code = Permissions.Claims.AddNewDebt,
                    Name = "Add New Debt",
                },
                new Permission
                {
                    Code = Permissions.Claims.View,
                    Name = "View",
                },
                new Permission
                {
                    Code = Permissions.Claims.IncrementOrDecrement,
                    Name = "View",
                },
            }
        };
        private static Permission SadadInvoicePermissionsModule = new Permission
        {
            Code = "Permissions.SadadInvoice",
            Name = "SadadInvoice",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.SadadInvoice.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.SadadInvoice.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.SadadInvoice.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.SadadInvoice.View,
                    Name = "View",
                },
            }
        };
        private static Permission PromissoriesPermissionsModule = new Permission
        {
            Code = "Permissions.Promissories",
            Name = "Promissories",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Promissories.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Promissories.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Promissories.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Promissories.View,
                    Name = "View",
                },
            }
        };
        private static Permission AdjustmentsPermissionsModule = new Permission
        {
            Code = "Permissions.Adjustments",
            Name = "Adjustments",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Adjustments.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.Adjustments.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.Adjustments.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.Adjustments.View,
                    Name = "View",
                },
            }
        };
        private static Permission AdjustmentReportsPermissionsModule = new Permission
        {
            Code = "Permissions.AdjustmentReports",
            Name = "Adjustment Reports",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.AdjustmentReports.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.AdjustmentReports.Edit,
                    Name = "Update",
                },
                new Permission
                {
                    Code = Permissions.AdjustmentReports.Delete,
                    Name = "Delete",
                },
                new Permission
                {
                    Code = Permissions.AdjustmentReports.View,
                    Name = "View",
                },
            }
        };
        private static Permission VIbansPermissionsModule = new Permission
        {
            Code = "Permissions.VIbans",
            Name = "VIbans",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.VIbans.Create,
                    Name = "Create",
                },
                new Permission
                {
                    Code = Permissions.VIbans.View,
                    Name = "View"
                }
            }
        };
        private static Permission SettingsModule = new Permission
        {
            Code = "Permissions.Settings",
            Name = "Settings",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Settings.Lookups,
                    Name = "Manage Lookups",
                },
                new Permission
                {
                    Code = Permissions.Settings.Notifications,
                    Name = "Manage Notifications",
                }
            }
        };

        private static Permission DashboardModule = new Permission
        {
            Code = "Permissions.Dashboard",
            Name = "Dashboard",
            Permissions = new List<Permission>
            {
                new Permission
                {
                    Code = Permissions.Dashboard.View,
                    Name = "View",
                }
            }
        };


        public IEnumerable<Permission> GetAllPermissions()
        {
            return new List<Permission>()
            {
                UsersPermissionsModule,
                RolesPermissionsModule,
                CasesPermissionsModule,
                PartiesPermissionsModule,
                ClaimsPermissionsModule,
                SadadInvoicePermissionsModule,
                PromissoriesPermissionsModule,
                AdjustmentsPermissionsModule,
                AdjustmentReportsPermissionsModule,
                SettingsModule,
                DashboardModule,
                VIbansPermissionsModule
            };
        }
        public Permission GetPermission(string permissionCode)
        {
            Permission result = null;
            foreach (var permission in GetAllPermissions())
            {
                if (permission.Code == permissionCode)
                {
                    result = permission;
                    break;
                }
                else
                {
                    result = permission.Permissions.FirstOrDefault(p => p.Code == permissionCode);
                    break;
                }
            }
            return result;
        }
        public string GetPermissionParentCode(string permissionCode)
        {
            foreach (var permission in GetAllPermissions())
            {
                if (permission.Code == permissionCode)
                    return string.Empty;
                else if (permission.Permissions.FirstOrDefault(p => p.Code == permissionCode) != null)
                    return permission.Code;
            }
            return string.Empty;
        }
    }
}