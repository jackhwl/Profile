﻿using Wenlin.Infrastructure;

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
        builder.Services.AddInfrastructure(connectionString);

        builder.Services.AddControllers(configure =>
        {
            configure.ReturnHttpNotAcceptable = true;
        })
        .AddXmlDataContractSerializerFormatters();

        //// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
        }
        else
        {
            // logger to DB
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsync("An unexpected fault happened. Try again later.");
                });
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}
