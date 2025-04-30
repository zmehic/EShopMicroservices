
namespace CatalogAPI.Products.GetProductsByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(x=>x.Category.Contains(query.Category)).ToListAsync();

            return new GetProductsByCategoryResult(products);
        }
    }
}
