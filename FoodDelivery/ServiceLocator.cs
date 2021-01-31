using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FoodDelivery.Data.Tables;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using FoodDelivery.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FoodDelivery.Services.Admin;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FoodDelivery
{
  public static class ServiceLocator
  {
    private static IServiceProvider _serviceProvider;

    private static IConfiguration _configuration;

    public static void Init(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public static TService Resolve<TService>()
    {
      if (_serviceProvider == null) {
        var collection = new ServiceCollection();

        AddDbContext<ApplicationDbContext>(collection, opt => opt.UseInMemoryDatabase(nameof(ApplicationDbContext)).ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        ConfigureDependencies(collection);
      }

      return _serviceProvider.GetService<TService>();
    }

    public static TService ResolveRequired<TService>()
    {
      if (_serviceProvider == null) {
        var collection = new ServiceCollection();

        AddDbContext<ApplicationDbContext>(collection, opt => opt.UseInMemoryDatabase(nameof(ApplicationDbContext)).ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
        ConfigureDependencies(collection);
      }

      return _serviceProvider.GetRequiredService<TService>();
    }

    public static void ConfigureDependencies(IServiceCollection services)
    {
      _configuration ??= new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();

      services.AddAutoMapper(typeof(DefaultAutoMapperProfile));

      services.Configure<IdentityOptions>(x => {
        x.User.RequireUniqueEmail = true;
        x.Password.RequireDigit = true;
        x.Password.RequireLowercase = true;
        x.Password.RequireUppercase = true;
        x.Password.RequireNonAlphanumeric = false;
      });

      services.AddIdentity<ApplicationUser, IdentityRole>(option => {
        option.Password.RequireDigit = true;
        option.Password.RequireLowercase = true;
        option.Password.RequireNonAlphanumeric = false;
        option.Password.RequireUppercase = true;
      })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
      services.AddLogging();


      services.AddControllers()
      .AddJsonOptions(options => {
        options.JsonSerializerOptions.IgnoreNullValues = true;
      })
      .AddNewtonsoftJson(options => {
        options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
        options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        options.SerializerSettings.ContractResolver = new DefaultContractResolver {
          NamingStrategy = new CamelCaseNamingStrategy()
        };
      });

      services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
       .AddJwtBearer(x => {
         x.RequireHttpsMetadata = false;
         x.SaveToken = true;
         x.TokenValidationParameters = new TokenValidationParameters {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSecret"])),
           ValidateIssuer = false,
           ValidateAudience = false
         };
         x.Events = new JwtBearerEvents {
           OnMessageReceived = context => {
             var accessToken = context.Request.Query["access_token"];

             return Task.CompletedTask;
           }
         };
       });

      services.AddRouting(options => options.LowercaseUrls = true);
    
      services.AddSwaggerGenNewtonsoftSupport();

      services.AddScoped<IUserService, UserService>();
      services.AddScoped<IAddressService, AddressService>();
      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IRestaurantService, RestaurantService>();
      services.AddScoped<IReviewService, ReviewService>();
      services.AddScoped<IFavoriteDishService, FavoriteDishService>();
      services.AddScoped<IFavoriteRestaurantService, FavoriteRestaurantService>();
      services.AddScoped<IBasketService, BasketService>();
      services.AddScoped<IConnectionService, ConnectionService>();
      services.AddScoped<IAuthService, AuthService>();
      services.AddScoped<IInfoService, InfoService>();
      services.AddScoped<IStatisticsService, StatisticsService>();
      services.AddScoped<IAdminDishService, AdminDishService>();
      services.AddScoped<IAdminOrderService, AdminOrderService>();
      services.AddScoped<IAdminRestaurantService, AdminRestaurantService>();
      services.AddScoped<IAdminService, AdminService>();

      services.AddTransient<SessionService>();

      _serviceProvider = services.BuildServiceProvider();
    }

    public static void AddDbContext<TContext>(IServiceCollection services, Action<DbContextOptionsBuilder> builder)
        where TContext : DbContext
    {
      services.AddDbContext<TContext>(builder);
    }
  }
}
