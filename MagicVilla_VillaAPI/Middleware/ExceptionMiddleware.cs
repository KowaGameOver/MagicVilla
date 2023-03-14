using MagicVilla_VillaAPI.ExceptionFiltering;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net;
using System.Text.Json;
using MagicVilla_VillaAPI.Errors;

namespace MagicVilla_VillaAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private HttpStatusCode code = HttpStatusCode.InternalServerError;
        private string result = string.Empty;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                switch (exception)
                {
                    case BadIdException:
                        code = HttpStatusCode.BadRequest;
                        result = JsonSerializer.Serialize("Bad ID");
                        break;
                    case NullEntityException:
                        code = HttpStatusCode.NotFound;
                        result = JsonSerializer.Serialize("Entity was not found");
                        break;
                    case EntityAlreadyExistException:
                        code = HttpStatusCode.NotFound;
                        result = JsonSerializer.Serialize("Entity already exist");
                        break;
                    default:
                        code = HttpStatusCode.InternalServerError;
                        result = JsonSerializer.Serialize("Unknown exception");
                        break;
                }
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)code;

                await httpContext.Response.WriteAsync(result);
            }
        }
    }
}
