using Data.Entities;
namespace ShopCore.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationModel paginationModel)
        {
            return queryable.Skip((paginationModel.Page - 1) * paginationModel.RecordsPerPage)
                .Take(paginationModel.RecordsPerPage);
        }
    }
}
