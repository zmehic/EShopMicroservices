using BuildingBlocks.Pagination;

namespace OrderingApplication.Orders.Queries.GetOrders
{
    public record GetOrderQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;

    public record GetOrdersResult(PaginatedResult<OrderDTO> Orders);
}
