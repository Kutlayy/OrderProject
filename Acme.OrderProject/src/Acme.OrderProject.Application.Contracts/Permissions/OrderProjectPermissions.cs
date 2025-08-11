namespace Acme.OrderProject.Permissions
{
    public static class OrderProjectPermissions
    {
        public const string GroupName = "OrderProject";

        public static class Customers
        {
            public const string Default = GroupName + ".Customers";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Stocks
        {
            public const string Default = GroupName + ".Stocks";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
        }

        public static class Orders
        {
            public const string Default = GroupName + ".Orders";
            public const string Create = Default + ".Create";
            public const string Edit = Default + ".Edit";
            public const string Delete = Default + ".Delete";
            public const string Approve = Default + ".Approve";
        }
    }
}
