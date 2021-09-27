using System.Net;
using Api.ResponseWrapper;
using Application.Common.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NewCustomerActivationProcess.Exception
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlobalExceptionFilter
    {

        /// <summary>
        /// Configures the exception handler.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="logger">The logger.</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, Serilog.ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    IExceptionHandlerFeature contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        logger.Information($"Something went wrong: {contextFeature.Error}");

                        if (contextFeature.Error.GetType() == typeof(ValidationException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            var error = contextFeature.Error as ValidationException;
                            await context.Response.WriteAsync(
                                JsonConvert.SerializeObject(error?.Errors.ToResponse(false, "Validation error"), new JsonSerializerSettings
                                {
                                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                                }));
                        }
                        else if (contextFeature.Error.GetType() == typeof(AppException))
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            await context.Response.WriteAsync(JsonConvert.SerializeObject(contextFeature.Error.Message.ToResponse(false), new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                        else
                        {
                            await context.Response.WriteAsync(JsonConvert.SerializeObject("Server Error".ToResponse(false, "Something went wrong"), new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver()
                            }));
                        }
                    }
                });
            });
        }
    }
}