using ViewModels.Pagination;

namespace ViewModels.Shop.Products
{
    public class FilterProductsViewModel
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationViewModel PaginationViewModel => new() {Page = Page, RecordsPerPage = RecordsPerPage};
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public bool isAvalible { get; set; }

    }
}