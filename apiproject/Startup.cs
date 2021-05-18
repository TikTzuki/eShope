using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Text;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Data;
using Baseurl;
using DTO;
using usercustom;
using Models;
using Stripe;
using OrderService = apiproject.Services.OrderService;
using apiproject.Interfaces;

namespace apiproject
{
  public class Startup
  {
    readonly string AllowSpecificOrigins = "tiktzukiSpecific";
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<EcommerContext>(options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
      services.AddCors(options =>
      {
        options.AddPolicy(name: AllowSpecificOrigins,
        builder =>
        {
          // builder.WithOrigins("http://localhost:4201", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
          builder
          .WithOrigins("http://localhost:4201").AllowAnyHeader().AllowAnyMethod()
          .WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod()
          .WithOrigins("https://localhost:4201").AllowAnyHeader().AllowAnyMethod()
          .WithOrigins("https://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        });
      });
      services.AddControllers()
.AddNewtonsoftJson();
      services.AddHttpContextAccessor();
      services.AddSingleton<IrulService>(o =>
      {
        var ancessor = o.GetRequiredService<IHttpContextAccessor>();
        var request = ancessor.HttpContext.Request;
        var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
        return new urlService(uri);
      }

      );

      services.AddScoped<IOrderService, OrderService>();
      services.AddScoped<IProductService, Services.ProductService>();
      services.AddIdentity<Applicationuser, IdentityRole>()
        .AddEntityFrameworkStores<EcommerContext>()
        .AddDefaultTokenProviders();

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(options =>
      {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidAudience = Configuration["JWT:ValidAudience"],
          ValidIssuer = Configuration["JWT:ValidIssuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
        };
      });
    
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      StripeConfiguration.ApiKey = "sk_test_51IlDyBB8Uv4yEiL6hZ8anVINMmDyoc0oOrNZI3wtRrRrbBRL8GT03wSV2vAcPmSrXVN5jTxDuEbFSisb6pDbO92R00uD6uiXfc";
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();
      app.UseCors(AllowSpecificOrigins);
      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
