using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Wenlin.API.Authorization;
using Wenlin.Application;
using Wenlin.Authorization;
using Wenlin.Infrastructure;
using Wenlin.Persistence;

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
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IAuthorizationHandler, MustOwnImageHandler>();

        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
            configure.CacheProfiles.Add("240SecondsCacheProfile", new() { Duration = 240 });
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
                //            ValidationProblemDetails error = context.ModelState
                //.Where(e => e.Value.Errors.Count > 0)
                //.Select(e => new ValidationProblemDetails(context.ModelState)).FirstOrDefault();

                //            // Here you can add logging to you log file or to your Application Insights.
                //            // For example, using Serilog:
                //            // Log.Error($"{{@RequestPath}} received invalid message format: {{@Exception}}", 
                //            //   actionContext.HttpContext.Request.Path.Value, 
                //            //   error.Errors.Values);
                //            return new BadRequestObjectResult(error);

                // create a validation problem details object
                var problemDetailsFactory = context.HttpContext.RequestServices.GetRequiredService<ProblemDetailsFactory>();

                var validationProblemDetails = problemDetailsFactory.CreateValidationProblemDetails(context.HttpContext, context.ModelState);

                // add additional info not added by default
                validationProblemDetails.Detail = "See the errors field for details.";
                validationProblemDetails.Instance = context.HttpContext.Request.Path;

                // report invalid model state responses as validation issues
                validationProblemDetails.Type = "https://wenlin.net/modelvalidationproblem";
                validationProblemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                validationProblemDetails.Title = "One1 or more validation errors occurred.";

                return new UnprocessableEntityObjectResult(validationProblemDetails)
                {
                    ContentTypes = { "application/problem+json" }
                };
            };
        }) ;

        builder.Services.Configure<MvcOptions>(config =>
        {
            var newtonsoftJsonOutputFormatter = config.OutputFormatters.OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

            if (newtonsoftJsonOutputFormatter != null)
            {
                newtonsoftJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.wenlin.hateoas+json");
            }
        });

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
                    NameClaimType = "given_name",
                    RoleClaimType = "role",
                    ValidTypes = new[] { "at+jwt" }
                };
            });
            //.AddOAuth2Introspection(options =>
            //{
            //    options.Authority = "https//localhost:5003";
            //    options.ClientId = "wenlincoreapi";
            //    options.ClientSecret = "apisecret";
            //    options.NameClaimType = "given_name";
            //    options.RoleClaimType = "role";
            //});

        builder.Services.AddAuthorization(authorizationOptions =>
        {
            authorizationOptions.AddPolicy("UserCanAddImage", AuthorizationPolicies.CanAddImage());
            authorizationOptions.AddPolicy("ClientApplicationCanWrite", policyBuilder =>
            {
                policyBuilder.RequireClaim("scope", "wenlincoreapi.write");
            });
            authorizationOptions.AddPolicy("MustOwnImage", policyBuilder =>
            {
                policyBuilder.RequireAuthenticatedUser();
                policyBuilder.AddRequirements(new MustOwnImageRequirement());
            });
        });

        builder.Services.AddResponseCaching();

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

        app.UseResponseCaching();

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
