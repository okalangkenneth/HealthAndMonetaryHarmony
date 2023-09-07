using PangeaCyber.Net.Exceptions;

namespace HealthAndMonetaryHarmony.Middleware
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (PangeaAPIException ex)
            {
                // Handle Pangea specific exceptions here
                // Log the exception
                // Set the response for the client
            }
            catch (Exception ex)
            {
                // Handle other general exceptions here
                // Log the exception
                // Set a generic error response for the client
            }
        }
    }

}
