namespace MedPedia.Common.Utilities.Extensions
{
    public static class QueryableExtensions
    {
        // public static async Task<List<T>> PaginateAsync<T>(this IQueryable<T> queryable, BaseGetDto baseGetDto, CancellationToken cancellationToken)
        // {
        //     return await queryable.Skip(baseGetDto.Offset).Take(baseGetDto.Limit).ToListAsync(cancellationToken);
        // }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
