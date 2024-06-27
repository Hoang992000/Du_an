namespace DemoVnPay.Helper
{
    public class PaginationHelper
    {
        public static PagedResult<T> GetPagedData<T>(IQueryable<T> data, int page, int pageSize)
        {
            int totalItems = data.Count();
            int totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            data = data.Skip((page - 1) * pageSize).Take(pageSize);

            return new PagedResult<T>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                Page = page,
                PageSize = pageSize,
                Data = data
            };
        }
    }
}
