using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Acme.OrderProject.Localization;

namespace Acme.OrderProject.Permissions;

public class OrderProjectPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var group = context.AddGroup(OrderProjectPermissions.GroupName, L("Permission:OrderProject"));

        // Customers
        var customers = group.AddPermission(OrderProjectPermissions.Customers.Default, L("Permission:Customers"));
        customers.AddChild(OrderProjectPermissions.Customers.Create, L("Permission:Customers.Create"));
        customers.AddChild(OrderProjectPermissions.Customers.Edit, L("Permission:Customers.Edit"));
        customers.AddChild(OrderProjectPermissions.Customers.Delete, L("Permission:Customers.Delete"));

        // Stocks
        var stocks = group.AddPermission(OrderProjectPermissions.Stocks.Default, L("Permission:Stocks"));
        stocks.AddChild(OrderProjectPermissions.Stocks.Create, L("Permission:Stocks.Create"));
        stocks.AddChild(OrderProjectPermissions.Stocks.Edit, L("Permission:Stocks.Edit"));
        stocks.AddChild(OrderProjectPermissions.Stocks.Delete, L("Permission:Stocks.Delete"));

        // Orders
        var orders = group.AddPermission(OrderProjectPermissions.Orders.Default, L("Permission:Orders"));
        orders.AddChild(OrderProjectPermissions.Orders.Create, L("Permission:Orders.Create"));
        orders.AddChild(OrderProjectPermissions.Orders.Edit, L("Permission:Orders.Edit"));
        orders.AddChild(OrderProjectPermissions.Orders.Delete, L("Permission:Orders.Delete"));
        orders.AddChild(OrderProjectPermissions.Orders.Approve, L("Permission:Orders.Approve"));
    }

    private static LocalizableString L(string name)
        => LocalizableString.Create<OrderProjectResource>(name);
}
