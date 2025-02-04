using Microsoft.AspNetCore.Mvc;

namespace DevEval.WebApi.Requests
{
    public class ProductQueryRequest : PaginationRequest
    {
        [FromQuery(Name = "category")]
        public string? Category { get; set; }

        [FromQuery(Name = "_minPrice")]
        public decimal? MinPrice { get; set; }

        [FromQuery(Name = "_maxPrice")]
        public decimal? MaxPrice { get; set; }
    }
}
