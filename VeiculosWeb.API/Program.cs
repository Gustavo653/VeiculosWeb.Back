using Hangfire;
using Hangfire.PostgreSql;
using VeiculosWeb.DataAccess;
using VeiculosWeb.DataAccess.Driver;
using VeiculosWeb.DataAccess.Paramedic;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Repository.Driver;
using VeiculosWeb.Infrastructure.Repository.Paramedic;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Infrastructure.Service.Driver;
using VeiculosWeb.Infrastructure.Service.Paramedic;
using VeiculosWeb.Persistence;
using VeiculosWeb.Service;
using VeiculosWeb.Service.Driver;
using VeiculosWeb.Service.Paramedic;
using VeiculosWeb.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

namespace VeiculosWeb.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine("logs", "log.txt"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 10,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {CorrelationId} {Level:u3} {Username} {Message:lj}{Exception}{NewLine}")
            .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Host.UseSerilog();

            string databaseVeiculosWeb = Environment.GetEnvironmentVariable("DatabaseConnection") ?? configuration.GetConnectionString("DatabaseConnection")!;

            Log.Information("Início dos parâmetros da aplicação \n");
            Log.Information($"(DatabaseConnection) String de conexao com banco de dados para VeiculosWeb: \n{databaseVeiculosWeb} \n");
            Log.Information("Fim dos parâmetros da aplicação \n");

            builder.Services.AddDbContext<VeiculosWebContext>(x =>
            {
                x.UseNpgsql(databaseVeiculosWeb);
                if (builder.Environment.IsDevelopment())
                {
                    x.EnableSensitiveDataLogging();
                    x.EnableDetailedErrors();
                }
            });

            builder.Services.AddHttpLogging(x =>
            {
                x.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
            });

            builder.Services.AddScoped<TenantMiddleware>();

            InjectUserDependencies(builder);

            InjectServiceDependencies(builder);
            InjectRepositoryDependencies(builder);

            SetupAuthentication(builder, configuration);

            builder.Services.AddSession();

            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
                    )
                    .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );

            builder.Services.AddEndpointsApiExplorer();

            SetupSwaggerGen(builder);

            builder.Services.AddCors();

            builder.Services.AddHangfire(x =>
            {
                x.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(databaseVeiculosWeb));
            });

            builder.Services.AddHangfireServer(x => x.WorkerCount = 1);

            builder.Services.AddMvc();
            builder.Services.AddRouting();

            builder.Services.AddHealthChecks().AddCheck<WarmHealthCheck>("WarmHealthCheck");

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<VeiculosWebContext>();
                db.Database.Migrate();
                SeedAdminUser(scope.ServiceProvider).Wait();
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() },
            });

            app.UseCors(corsPolicyBuilder =>
            {
                corsPolicyBuilder.AllowAnyMethod()
                       .AllowAnyOrigin()
                       .AllowAnyHeader();
            });

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TenantMiddleware>();

            app.MapControllers();

            app.MapHealthChecks("/health");

            app.Run();
        }

        private static void SetupAuthentication(WebApplicationBuilder builder, ConfigurationManager configuration)
        {
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("TokenKey")!)),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        private static void SetupSwaggerGen(IHostApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "VeiculosWeb.API", Version = "v1" });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando Bearer.
                                Entre com 'Bearer ' [espaço] então coloque seu token.
                                Exemplo: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        private static void InjectUserDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<VeiculosWebContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

            builder.Services.AddScoped<UserManager<User>>();
        }

        private static void InjectRepositoryDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAmbulanceRepository, AmbulanceRepository>();
            builder.Services.AddScoped<IDriverCategoryRepository, DriverCategoryRepository>();
            builder.Services.AddScoped<IDriverItemRepository, DriverItemRepository>();
            builder.Services.AddScoped<IDriverChecklistCheckedItemRepository, DriverChecklistCheckedItemRepository>();
            builder.Services.AddScoped<IDriverChecklistRepository, DriverChecklistRepository>();
            builder.Services.AddScoped<IDriverChecklistReviewRepository, DriverChecklistReviewRepository>();
            builder.Services.AddScoped<IDriverChecklistItemRepository, DriverChecklistItemRepository>();
            builder.Services.AddScoped<IParamedicChecklistReplacedItemRepository, ParamedicChecklistReplacedItemRepository>();
            builder.Services.AddScoped<IParamedicCategoryRepository, ParamedicCategoryRepository>();
            builder.Services.AddScoped<IParamedicChecklistRepository, ParamedicChecklistRepository>();
            builder.Services.AddScoped<IParamedicChecklistItemRepository, ParamedicChecklistItemRepository>();
            builder.Services.AddScoped<IParamedicChecklistReviewRepository, ParamedicChecklistReviewRepository>();
            builder.Services.AddScoped<IParamedicItemRepository, ParamedicItemRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITenantRepository, TenantRepository>();
        }

        private static void InjectServiceDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IAmbulanceService, AmbulanceService>();
            builder.Services.AddScoped<IDriverCategoryService, DriverCategoryService>();
            builder.Services.AddScoped<IDriverItemService, DriverItemService>();
            builder.Services.AddScoped<IDriverChecklistService, DriverChecklistService>();
            builder.Services.AddScoped<IDriverChecklistReviewService, DriverChecklistReviewService>();
            builder.Services.AddScoped<IParamedicCategoryService, ParamedicCategoryService>();
            builder.Services.AddScoped<IParamedicItemService, ParamedicItemService>();
            builder.Services.AddScoped<IParamedicChecklistService, ParamedicChecklistService>();
            builder.Services.AddScoped<IParamedicChecklistReviewService, ParamedicChecklistReviewService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<ITenantService, TenantService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IGoogleCloudStorageService, GoogleCloudStorageService>();
        }

        private static async Task SeedAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            const string adminEmail = "admin@admin.com";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            var user = new User { Name = "Admin", UserName = "admin", Email = adminEmail, Role = RoleName.Admin, EmailConfirmed = true };
            if (adminUser == null)
                await userManager.CreateAsync(user, "Admin@123");
        }
    }
}