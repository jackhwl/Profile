using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using Wenlin.Application;
using Wenlin.Infrastructure;
using Wenlin.Persistence;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Wenlin.API;

internal static class StartupHelperExtensions
{
    // Add services to the container
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        // Add configuration sources
        if (builder.Environment.IsDevelopment())
        {
            builder.Configuration.AddUserSecrets<Program>();
        }

        // Add services to the container.
        string connectionString = builder.Configuration.GetConnectionString("WenlinConnection");
        var emailSettings = builder.Configuration.GetSection("EmailSettings");
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(emailSettings);
        builder.Services.AddPersistenceServices(connectionString);

        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddNewtonsoftJson(setupAction =>
        {
            setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        })
        .AddXmlDataContractSerializerFormatters()
        .ConfigureApiBehaviorOptions(setupAction =>
        {
            setupAction.InvalidModelStateResponseFactory = context =>
            {
                // create a validation problem details object
                var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                // add additional info not added by default
                validationProblemDetails.Detail = "See the errors field for details.";
                validationProblemDetails.Instance = context.HttpContext.Request.Path;

                // report invalid model state responses as validation issues
                validationProblemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
                validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                validationProblemDetails.Title = "One or more validation errors occurred.";

                return new UnprocessableEntityObjectResult(validationProblemDetails) 
                { 
                    ContentTypes= { "application/problem+json"}
                };
            };
        }) ;;

        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        });

        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://localhost:5003";
                options.Audience = "wenlincoreapi";
                options.TokenValidationParameters = new()
                {
                    ValidTypes = new[] { "at+jwt" }
                };
            });

        return builder.Build();
    }

    // Configure the request/response pipelien
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            //app.UseSwagger();
            //app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
            //        var exception = errorFeature.Error;

            //        var problemDetails = new ProblemDetails 
            //        { 
            //            Instance = $"urn:myorganization:error:{Guid.NewGuid()}" 
            //        };

            //        if (exception is BadHttpRequestException badHttpRequestException)
            //        {
            //            problemDetails.Title = "Invalid request";
            //            problemDetails.Status = (int)typeof(BadHttpRequestException).GetProperty("StatusCode", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(badHttpRequestException);
            //            problemDetails.Detail = badHttpRequestException.Message;
            //        }
            //        else
            //        {
            //            problemDetails.Title = "An unexpected error occurred!";
            //            problemDetails.Status = 500;
            //            problemDetails.Detail = exception.Message;
            //        }

            //        context.Response.StatusCode = problemDetails.Status.Value;
            //        context.Response.WriteJson(problemDetails, "application/problem+json");
            //    });
            //});
        }
        else
        {
            // logger to DB
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var errorFeature = context.Features.Get<IExceptionHandlerFeature>();
                    //var exception = errorFeature.Error;

                    var problemDetails = new ProblemDetails
                    {
                        Title = "An unexpected error occurred!",
                        Status = 500,
                        Detail = "An unexpected fault happened. Try again later.",
                        Instance = context.Request.Path
                    };

                    context.Response.ContentType = "application/problem+json";
                    await context.Response.WriteAsJsonAsync(problemDetails);
                });
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseCors("Open");

        app.MapControllers();

        return app;
    }
}

public static class HttpExtensions
{
    private static readonly JsonSerializer Serializer = new JsonSerializer
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    public static void WriteJson<T>(this HttpResponse response, T obj, string contentType = "application/json")
    {
        response.ContentType = contentType;
        using (var writer = new HttpResponseStreamWriter(response.Body, Encoding.UTF8))
        {
            using (var jsonWriter = new JsonTextWriter(writer))
            {
                jsonWriter.CloseOutput = false;
                jsonWriter.AutoCompleteOnClose = false;

                Serializer.Serialize(jsonWriter, obj);
            }
        }
    }
}
