namespace MovieApi.Helpers
{
    public class QueryObject
    {
        public string? Title { get; set; } = null;
        public string? Genre { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;

        private int _pageNumber { get; set; } = 1;
        private int _pageSize { get; set; } = 20;
        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value > 0 ? value : 1;
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > 0 ? value : 20;
        }
    }
}
