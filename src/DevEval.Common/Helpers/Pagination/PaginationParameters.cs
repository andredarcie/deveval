namespace DevEval.Common.Helpers.Pagination
{
    /// <summary>
    /// Represents pagination and sorting parameters.
    /// </summary>
    public class PaginationParameters
    {
        /// <summary>
        /// Gets or sets the page number for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the page size for pagination.
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Gets or sets the ordering string (e.g., "id desc, userId asc").
        /// </summary>
        public string? OrderBy { get; set; }
    }
}
