namespace Data.Entities
{
    public class PaginationModel
    {
        public int Page { get; set; } = 1;
        private int _recordsPerPage = 10;
        private const int MaxRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get => _recordsPerPage;
            set => _recordsPerPage = (value > MaxRecordsPerPage) ? MaxRecordsPerPage : value;
        }
    }
}
