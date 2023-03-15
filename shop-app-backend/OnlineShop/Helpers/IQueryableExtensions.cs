using OnlineShop.DTO;

namespace OnlineShop.Helpers
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationViewModel paginationViewModel)
        {
            return queryable.Skip((paginationViewModel.Page - 1) * paginationViewModel.RecordsPerPage)
                .Take(paginationViewModel.RecordsPerPage);
        }
    }
}
