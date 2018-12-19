using System.Net;

namespace AHHelperTools.DTOs
{
    public class WebAPIResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Description { get; set; }
        public string Data { get; set; }
    }
}
