using BlogApi.Middlewares;
using DataAccess;
using DataAccess.Repositories.Imp;
using DataAccess.Repositories;

using Microsoft.EntityFrameworkCore;
using Serilog;
using Business.Services;
using Business.Services.Imp;
using Business.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting Server.");

    var builder = WebApplication.CreateBuilder(args);


    builder.Host.UseSerilog((context , loggerConfiguration) =>
    {
        loggerConfiguration.WriteTo.Console();
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);
    }
    );
    // Add services to the container.

    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.AddFluentValidationAutoValidation();
    builder.Services.AddValidatorsFromAssemblyContaining<UserDTOValidator>();

    builder.Services.AddScoped<IImageRepository, ImageRepository>();
    builder.Services.AddTransient<IUserRepository, UserRepository>();


    builder.Services.AddScoped<IImageService, ImageService>();
    builder.Services.AddTransient<IUserService, UserService>();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();


    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "server terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}