using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using FoodDelivery.Data.Tables;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using FoodDelivery.Helpers;
using Microsoft.Extensions.FileProviders;
using FoodDelivery.Models; 
using Swashbuckle.AspNetCore.SwaggerGen;

namespace FoodDelivery
{
  public class Startup
  {
    public static string StoragePath;
    public static string ConnectionString;
    public static DefaultContractResolver DEFAULT_JSON_CONTRACT = new DefaultContractResolver {
      NamingStrategy = new CamelCaseNamingStrategy()
    };
    public static string JWT_SECRET_KEY;

    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public IConfiguration _configuration { get; }

    public static readonly ILoggerFactory MyLoggerFactory
  = LoggerFactory.Create(builder => { /*builder.AddConsole();*/ });
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      ConnectionString = _configuration.GetConnectionString("Default");
      JWT_SECRET_KEY = _configuration["JwtSecret"];

      ServiceLocator.Init(_configuration);
      ServiceLocator.ConfigureDependencies(services);

      ServiceLocator.AddDbContext<ApplicationDbContext>(services,
               opt => opt.UseNpgsql(ConnectionString, b => {
                 b.MigrationsAssembly(nameof(FoodDelivery));
                 b.UseNetTopologySuite();
               }));

      services.AddSwaggerGen(ConfigureSwaggerGen);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
    {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }
      else {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //app.UseHsts();
      }

      Migrate(serviceProvider);
      SeedingData(serviceProvider);

      var appConfig = new AppSettingsModel();
      _configuration.GetSection("Settings")
      .Bind(appConfig);
#if DEBUG
      StoragePath = appConfig.StoragePath;
#else
      StoragePath = appConfig.StoragePathLinux;

#endif
      FileHelper.TryExistOrCreate(StoragePath);

      app.UseCors(builder => {
        //builder.WithOrigins("http://localhost:5000")
        builder.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
        //.AllowCredentials();
      });

      app.UseStaticFiles(new StaticFileOptions {
        ServeUnknownFileTypes = true,
        FileProvider = new PhysicalFileProvider(StoragePath),
        RequestPath = "/storage"
      });

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseSwagger();
      app.UseEndpoints(endpoints => {
        endpoints.MapControllers();
      });

      app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Food Delivery API");
      });

    }

    private void ConfigureSwaggerGen(SwaggerGenOptions options)
    {
      options.SwaggerDoc("v1", new OpenApiInfo { Title = "FOOD DELIVERY API", Version = "v1" });
      options.EnableAnnotations();
      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",

      });

      options.IgnoreObsoleteProperties();

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
              In = ParameterLocation.Header,
            },
            new List<string>()
          }
        });

      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      options.IncludeXmlComments(xmlPath);
    }

    private void Migrate(IServiceProvider serviceProvider)
    {
      var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
      context.Database.Migrate();
    }

    private void SeedingData(IServiceProvider serviceProvider)
    {
      var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
      var manager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

      var entity = context.Roles.AsNoTracking().FirstOrDefault(s => s.Name == Roles.CLIENT);

      if (entity == null) {
        context.Roles.Add(new IdentityRole {
          Name = Roles.CLIENT,
          NormalizedName = Roles.CLIENT.ToUpper()
        });
      }

      entity = context.Roles.AsNoTracking().FirstOrDefault(s => s.Name == Roles.ADMIN);
      if (entity == null) {
        context.Roles.Add(new IdentityRole {
          Name = Roles.ADMIN,
          NormalizedName = Roles.ADMIN.ToUpper()
        });
      }

      var admin = context.Users.FirstOrDefault(x => x.Email == AppConst.ADMIN_EMAIL);
      if (admin == null) {
        admin = new ApplicationUser {
          Email = AppConst.ADMIN_EMAIL,
          UserName = AppConst.ADMIN_EMAIL,
          Name = Roles.ADMIN
        };

        var result = manager.CreateAsync(admin, "Anyany_1").Result;

        if (result.Succeeded) {
          manager.AddToRoleAsync(admin, Roles.ADMIN).Wait();
        }
      }

      entity = context.Roles.AsNoTracking().FirstOrDefault(s => s.Name == Roles.MANAGER);
      if (entity == null) {
        context.Roles.Add(new IdentityRole {
          Name = Roles.MANAGER,
          NormalizedName = Roles.MANAGER.ToUpper()
        });

        context.SaveChanges();
      }
    }
  }
}
