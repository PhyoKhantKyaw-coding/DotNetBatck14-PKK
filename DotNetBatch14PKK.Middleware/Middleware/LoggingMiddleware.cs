using System.Text;

namespace DotNetBatch14PKK.Middleware.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request path
            Console.WriteLine("Request Path: " + context.Request.Path);

            // Log request body
            context.Request.EnableBuffering();
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var requestBody = await reader.ReadToEndAsync();
                Console.WriteLine("Request Body: " + requestBody);
                context.Request.Body.Position = 0;
            }

            // Capture and log response body
            var originalResponseBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                await _next(context);

                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                Console.WriteLine("Response Data: " + responseBody);
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }
    }
}
