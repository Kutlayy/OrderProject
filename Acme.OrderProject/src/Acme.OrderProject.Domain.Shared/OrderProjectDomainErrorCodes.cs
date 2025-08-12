namespace Acme.OrderProject;

public static class OrderProjectDomainErrorCodes
{
    /* You can add your business exception error codes here, as constants */
    public const string RiskLimitExceeded = "OrderProject:RiskLimitExceeded";
    public const string InsufficientStock = "OrderProject:InsufficientStock";
    public const string CannotDeleteStockWithOrders = "CannotDeleteStockWithOrders";
}