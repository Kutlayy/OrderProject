using Acme.OrderProject;
using Acme.OrderProject.Customers;
using Acme.OrderProject.Stocks;
using AutoMapper;

namespace Acme.OrderProject;

public class OrderProjectApplicationAutoMapperProfile : Profile
{
    public OrderProjectApplicationAutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateUpdateCustomerDto, Customer>();
        CreateMap<Orders.Order, Orders.OrderDto>();
        CreateMap<Orders.OrderLine, Orders.OrderLineDto>();
        CreateMap<Orders.CreateOrderDto, Orders.Order>();
        CreateMap<Stock, StockDto>();
        CreateMap<CreateUpdateStockDto, Stock>();


        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
    }
}
