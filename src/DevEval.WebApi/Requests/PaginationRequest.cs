using Microsoft.AspNetCore.Mvc;

namespace DevEval.WebApi.Requests
{
    public class PaginationRequest
    {
        [FromQuery(Name = "_page")]
        public int Page { get; set; }

        [FromQuery(Name = "_size")]
        public int Size { get; set; }

        [FromQuery(Name = "_order")]
        public string? Order { get; set; }
    }
}
