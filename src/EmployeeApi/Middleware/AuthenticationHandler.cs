using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;
using System;
using System.Linq;
using static Infrastructure.Employee.Constants;
using Microsoft.Extensions.Logging;
namespace EmployeeApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthenticationHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public AuthenticationHandler(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<AuthenticationHandler>();
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var headers = httpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value);
            var authorizationKey = headers.Keys.FirstOrDefault(i => i.Equals(AuthorizationKey, StringComparison.OrdinalIgnoreCase));
            if (authorizationKey != null)
            {
                headers.TryGetValue(authorizationKey, out var tokenValue);
            }  
            try
            {
               //Code to Authenticate token 
               //
               //
               if(_next != null)
                {
                    await _next(httpContext);
                }
            }
            catch(Exception e)
                {
                    httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    _logger.Log(LogLevel.Debug,e.Message,typeof(AuthenticationHandler));
                }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationHandlerExtensions
    {
        public static IApplicationBuilder UseAuthenticationHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationHandler>();
        }
    }
}
