using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;
using VeiculosWeb.DataAccess;
using VeiculosWeb.Domain.Base;
using VeiculosWeb.Domain.Enum;
using VeiculosWeb.Infrastructure.Repository;
using VeiculosWeb.Infrastructure.Service;
using VeiculosWeb.Persistence;
using VeiculosWeb.Service;
using VeiculosWeb.Utils;

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

            builder.Services.AddScoped<SessionMiddleware>();

            InjectUserDependencies(builder);

            InjectServiceDependencies(builder);
            InjectRepositoryDependencies(builder);

            SetupAuthentication(builder, configuration);

            builder.Services.AddSession();

            builder.Services.AddControllers()
                            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

            builder.Services.AddOutputCache(x =>
            {
                x.AddPolicy("CacheImmutableResponse", OutputCachePolicy.Instance);
            });

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
            app.UseOutputCache();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<SessionMiddleware>();

            app.MapControllers();

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
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICarRepository, CarRepository>();
            builder.Services.AddScoped<IColorRepository, ColorRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<IFeatureRepository, FeatureRepository>();
            builder.Services.AddScoped<IGearboxRepository, GearboxRepository>();
            builder.Services.AddScoped<IImageRepository, ImageRepository>();
            builder.Services.AddScoped<IStateRepository, StateRepository>();
            builder.Services.AddScoped<IFuelRepository, FuelRepository>();
            builder.Services.AddScoped<IModelRepository, ModelRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void InjectServiceDependencies(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ICarService, CarService>();
            builder.Services.AddScoped<IColorService, ColorService>();
            builder.Services.AddScoped<ICityService, CityService>();
            builder.Services.AddScoped<IFeatureService, FeatureService>();
            builder.Services.AddScoped<IGearboxService, GearboxService>();
            builder.Services.AddScoped<IStateService, StateService>();
            builder.Services.AddScoped<IFuelService, FuelService>();
            builder.Services.AddScoped<IModelService, ModelService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
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