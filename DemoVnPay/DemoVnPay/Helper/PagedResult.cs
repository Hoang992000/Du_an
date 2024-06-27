namespace DemoVnPay.Helper
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public bool HasPrevious => Page > 1;
        public bool HasNext => Page < TotalPages;
        public IQueryable<T> Data { get; set; }
    }
}
