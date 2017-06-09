namespace Demo.Common.Web.Infrastructure
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    public class StatusCodeTextPlainActionResult : IHttpActionResult
    {
        public StatusCodeTextPlainActionResult(string message, HttpRequestMessage request, HttpStatusCode statusCode)
        {
            Message = message;
            Request = request;
            StatusCode = statusCode;
        }

        public string Message { get; }

        public HttpRequestMessage Request { get; }

        public HttpStatusCode StatusCode { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }

        public HttpResponseMessage Execute()
        {
            return new HttpResponseMessage(StatusCode)
            {
                Content = new StringContent(Message),
                RequestMessage = Request
            };
        }
    }
}
