namespace ECommerceManagement.API.Extensions
{
    public static class PaginationExtension
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, int offset, int limit)
        {
            return queryable.Skip(offset).Take(limit);
        }

        public static IEnumerable<T> Paginate<T>(this IEnumerable<T> enumarable,  int offset, int limit)
        {
            return enumarable.Skip(offset).Take(limit);
        }
    }
}
