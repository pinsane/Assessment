using Microsoft.EntityFrameworkCore;

namespace SensorApi.Core
{
    /// <summary>
    /// Defines an interface for a service that can apply pagination and optional mapping to a query.
    /// </summary>
    public interface IFilteredQuery
    {
        /// <summary>
        /// Paginates the query and applies an optional mapping function to each item.
        /// </summary>
        /// <typeparam name="TResult">The type of the items returned from the query.</typeparam>
        /// <param name="query">The query to paginate and map.</param>
        /// <param name="request">The pagination request containing the page number and page size.</param>
        /// <param name="mapper">An optional mapping function to transform each item in the query.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Page{TResult}"/> containing the paginated and possibly mapped items.</returns>
        Task<Page<TResult>> ToPageListAsync<TResult>(IQueryable<TResult> query, PagedRequest request, Func<TResult, TResult>? mapper = null) where TResult : class, new();
    }

    /// <summary>
    /// Implementation of <see cref="IFilteredQuery"/> that handles pagination and optional mapping of query results.
    /// </summary>
    public class FilteredQuery : IFilteredQuery
    {
        /// <summary>
        /// Paginates the provided query and applies an optional mapping function to each item.
        /// </summary>
        /// <typeparam name="TResult">The type of the items returned from the query.</typeparam>
        /// <param name="query">The query to paginate and map.</param>
        /// <param name="request">The pagination request containing the page number and page size.</param>
        /// <param name="mapper">An optional mapping function to transform each item in the query.</param>
        /// <returns>A task representing the asynchronous operation, with a <see cref="Page{TResult}"/> containing the paginated and possibly mapped items.</returns>
        public async Task<Page<TResult>> ToPageListAsync<TResult>(
            IQueryable<TResult> query,
            PagedRequest request,
            Func<TResult, TResult>? mapper = null
        ) where TResult : class, new()
        {
            // Validate page number and page size
            if (request.Page < 1)
                throw new ArgumentException("Page number must be greater than or equal to 1", nameof(request.Page));

            if (request.PageSize < 1)
                throw new ArgumentException("Page size must be greater than or equal to 1", nameof(request.PageSize));

            // Calculate total count of items in the query and total pages based on the page size
            var totalCount = await query.CountAsync(); // Use async method for better performance
            var totalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize); // Total pages is the ceiling of totalCount / pageSize

            // Apply pagination by skipping and taking the correct number of items based on the current page and page size
            var itemsQuery = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize);

            // Execute the query and materialize the result into a list
            var itemsList = await itemsQuery.ToListAsync();

            // If a mapper is provided, apply the mapping function to each item; 
            // Otherwise, assume TResult is the same as the source type and return the items as they are
            var items = mapper != null
                ? itemsList.Select(mapper).ToList() // Apply the mapping function if provided
                : itemsList; // Use the items as they are if no mapping is provided

            // Return the paginated result as a Page containing the items, page number, page size, and total count
            return new Page<TResult>(items, request.Page, request.PageSize, totalCount);
        }
    }
}