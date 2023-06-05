namespace Data.Entities
{
    public class FilterProducts
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationModel PaginationModel => new() { Page = Page, RecordsPerPage = RecordsPerPage };
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public bool isAvalible { get; set; }
    }
}
