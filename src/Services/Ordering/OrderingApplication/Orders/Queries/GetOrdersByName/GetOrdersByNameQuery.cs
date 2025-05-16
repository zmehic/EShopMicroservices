﻿namespace OrderingApplication.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQuery(string OrderName) : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDTO> Orders);
}
