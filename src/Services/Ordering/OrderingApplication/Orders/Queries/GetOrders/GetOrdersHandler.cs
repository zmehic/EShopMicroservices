
namespace OrderingApplication.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrderQuery query, CancellationToken cancellationToken)
        {
            var pageIndex = query.PaginationRequest.PageIndex;
            var pageSize = query.PaginationRequest.PageSize;

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);
            var orders = await dbContext.Orders.Include(o => o.OrderItems).AsNoTracking().Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return new GetOrdersResult(
                new PaginatedResult<OrderDTO>(pageIndex, pageSize, totalCount, orders.ToOrderDtoList()));

        }
    }
}
