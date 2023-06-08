using ApiConciertosAWS.Data;
using ApiConciertosAWS.Helpers;
using ApiConciertosAWS.Models;
using ApiConciertosAWS.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ApiConciertosAWS;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {

        SecretAWS secret = HelperSecret.GetSecret().Result;
        string connectionString = secret.RDS;


        services.AddTransient<ConciertosRepository>();
        services.AddDbContext<ConciertosContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", x => x.AllowAnyOrigin());
        });
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Api Gateway Comics MySql 2023",
                Version = "V1"
            });
        });
        services.AddControllers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors(options => options.AllowAnyOrigin());

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint(url: "swagger/v1/swagger.json"
                , "ApiAWSComicsMySql 2023 v1");
            options.RoutePrefix = "";
        });


        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
            });
        });
    }
}