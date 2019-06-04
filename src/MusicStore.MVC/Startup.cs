using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MusicStore.MVC.Authorization;
using MusicStore.MVC.Models;
using MusicStore.MVC.Persistence.Data;
using MusicStore.MVC.Repository.Data;
using MusicStore.MVC.Services;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

namespace MusicStore.MVC
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      var appSetting = Configuration.Get<AppSettings>();

      services.Configure<AppSettings>(Configuration);
      services.AddDbContext<MusicStoreContext>(options =>
          options.UseSqlServer(appSetting.ConnectionStrings.DefaultConnection));

      services.AddTransient<IEmailSender, DemoEmailSender>();

      services.AddIdentity<User, IdentityRole>()
        .AddEntityFrameworkStores<MusicStoreContext>()
        .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;

        // Lockout settings.
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.AllowedForNewUsers = true;
        options.SignIn.RequireConfirmedEmail = true;
        // User settings.
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
        options.User.RequireUniqueEmail = true;
      });
      services.Configure<PasswordHasherOptions>(option =>
      {
        option.IterationCount = 15000;
      });
      services.ConfigureApplicationCookie(options =>
      {
        // Cookie settings
        options.Cookie.HttpOnly = false;
        // 10 days
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60 * 24 * 10);
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Users/AccessDenied";
        options.SlidingExpiration = true;
      });

      services.AddCors(o => o.AddPolicy("EnableCors", builder =>
      {
        builder
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
      }));

      services.AddAuthentication()
        .AddCookie()
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
         {
           options.SaveToken = true;
           options.RequireHttpsMetadata = false;
           options.TokenValidationParameters = new TokenValidationParameters
           {
             ValidIssuer = appSetting.Token.Issuer,
             ValidAudiences = appSetting.Token.Audience,
             ValidateIssuerSigningKey = true,
             IssuerSigningKey =
               new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting.Token.Key)),
             ValidateLifetime = true,
             ValidateIssuer = true,
             ValidateAudience = true,
             ClockSkew = TimeSpan.Zero
           };
         });
      services.AddAuthorization(options =>
      {
        options.AddPolicy("RequireAdministratorRole",
            policy => policy.RequireRole("Administrator"));
      });

      services.AddScoped<IAuthorizationHandler, OwenerResourseHandler>();
      // Admin handler
      services.AddSingleton<IAuthorizationHandler, AdministratorsHandler>();

      services.AddAutoMapper();

      services.AddScoped<IUnitOfWork, UnitOfWork>();

      services.AddMvc()
        .AddJsonOptions(opt =>
        {
          opt.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.All;
        })
        .ConfigureApiBehaviorOptions(options =>
        {
          options.SuppressMapClientErrors = true;
        })
        .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new Info { Title = "Music Store DEMO API", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();

      app.UseCors("EnableCors");

      app.UseStaticFiles();

      app.UseAuthentication();

      app.UseSwagger();

      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Store DEMO API");
      });

      app.UseMvc(routes =>
      {
        routes.MapRoute(
          name: "Users/{username}",
          "Profile/{username}",
          new { controller = "Users", action = "Profile", username = "" });

        routes.MapRoute(
          name: "default",
          template: "{controller=Songs}/{action=Index}/{id?}");
      });
    }
  }
}
