using MediatR;
using Serilog.Events;
using Serilog;
using SkillSphere.Infrastructure.Consul;
using SkillSphere.Infrastructure.Security;
using SkillSphere.Infrastructure.UseCases.DI;
using SkillSphere.UserProfileManager.DataAccess;
using System.Reflection;
using SkillSphere.UserProfileManager.UseCases.UserProfiles.Commands.CreateProfile;
using SkillSphere.UserProfileManager.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using SkillSphere.Infrastructure.Security.AuthServices;
using SkillSphere.Infrastructure.Security.UserAccessor;
using SkillSphere.UserProfileManager.Core.Interfaces;
using SkillSphere.UserProfileManager.UseCases.Profiles;
using SkillSphere.UserProfileManager.API.Profiles;
using FluentValidation;
using SkillSphere.UserProfileManager.UseCases.Services;
using Microsoft.Extensions.FileProviders;

namespace SkillSphere.UserProfileManager.API;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .WriteTo.Async(a => a.Console())
        .WriteTo.Async(a => a.File("logs/UserProfilesWebAppLog.txt", rollingInterval: RollingInterval.Day))
        .CreateLogger();

        try
        {
            Log.Information("Starting up the application");
            var builder = ConfigureApp(args);
            await RunApp(builder);
        }
        catch (Exception exception)
        {
            Log.Fatal(exception, "An error occurred while app initialization");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static WebApplicationBuilder ConfigureApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.ClearProviders();
        builder.Host.UseSerilog();

        var services = builder.Services;

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        services.AddSwagger(xmlFilePath);

        services.AddHttpContextAccessor();

        services.AddConsul(builder.Configuration);

        services.AddJwtSettingsFromConsul(builder.Configuration["ConsulKey"]!);

        services.AddJwtBearerAuthentication();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        ConfigureDI(services, builder.Configuration);

        return builder;
    }

    private static void ConfigureDI(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(configuration["DatabaseConnection"]));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateProfileCommand).Assembly));
        services.AddAutoMapper(typeof(ControllerMappingProfile).Assembly, typeof(UserProfileMappingProfile).Assembly);

        services.AddHttpClient<IAuthorizationService, AuthorizationService>(client =>
        {
            client.BaseAddress = new Uri(configuration["AuthorizationService"]!);
        });


        services.AddScoped<IUserAccessor, UserAccessor>();

        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<IUserSkillRepository, UserSkillRepository>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IImageUploadService, ImageUploadService>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<CreateProfileCommandValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    private static async Task RunApp(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.ContentRootPath, @"..\..\..\SkillSphere.Files")),
            RequestPath = "/SkillSphere.Files"
        });

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors();

        app.UseMiddleware<ErrorExceptionHandler>();     
        app.UseAuthorization();       

        app.MapControllers();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            db.Database.Migrate();
        }

        await app.RunAsync();
    }
}