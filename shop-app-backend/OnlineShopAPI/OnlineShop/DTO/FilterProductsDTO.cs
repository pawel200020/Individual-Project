namespace OnlineShop.DTO
{
    public class FilterProductsDTO
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }

        public PaginationDTO PaginationDTO
        {
            get { return new PaginationDTO() {Page = Page, RecordsPerPage = RecordsPerPage}; }
        }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public bool isAvalible { get; set; }

    }
}
