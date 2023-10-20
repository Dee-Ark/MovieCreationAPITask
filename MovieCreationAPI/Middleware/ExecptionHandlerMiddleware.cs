using System.Net;

namespace MovieCreationAPI.Middleware
{
    public class ExecptionHandlerMiddleware
    {
        private readonly ILogger<ExecptionHandlerMiddleware> logger;
        private readonly RequestDelegate requestDelegate;

        public ExecptionHandlerMiddleware(ILogger<ExecptionHandlerMiddleware> logger, RequestDelegate requestDelegate)
        {
            this.logger = logger;
            this.requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                logger.LogError(ex, $"{errorId} : {ex.Message}");

                //Reture object here
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    Message = "Something went wrong, Please, check back later..."
                };

                await httpContext.Response.WriteAsJsonAsync(error);
            }
        }
    }
}
