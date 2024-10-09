using System.Net;

namespace Follow.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        //hata varsa hatayi global olcekte loglayacagim. yoksa devam et diyecegim
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        public static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            context.Response.ContentType = "application/json";
            int statusCode;

            switch (exception)
            {
                case UnauthorizedAccessException validationException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message =  statusCode == (int)HttpStatusCode.InternalServerError ? "Internal Server Error" : exception.Message
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
