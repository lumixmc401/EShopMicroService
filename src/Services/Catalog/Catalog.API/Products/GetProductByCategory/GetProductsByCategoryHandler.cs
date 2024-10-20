using Marten;
using Marten.Linq.QueryHandlers;
using Microsoft.AspNetCore.Session;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductsByCategoryQuery(string Catalog):IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle
            (GetProductsByCategoryQuery query, CancellationToken cancellationToken)
        {
            var result = await session.Query<Product>()
                .Where(w => w.Category.Contains(query.Catalog))
                .ToListAsync(cancellationToken);
            return new GetProductsByCategoryResult(result);
        }
    }
}
