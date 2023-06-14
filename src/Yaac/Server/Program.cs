using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Yaac.Server;
using Yaac.Server.Data;
using Yaac.Server.Models;
using Yaac.Shared;

namespace Yaac;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<YaacDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString(nameof(YaacDbContext))));
        builder.Services.AddIdentity<YaacUser, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<YaacDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)))
            .Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = Constants.Models.MinimumPasswordLength;
        });

        builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var error = context.ModelState.Where(state => state.Value != null)
                                                .SelectMany(state => state.Value!.Errors)
                                                .Select(error => error.ErrorMessage)
                                                .First();
                return new BadRequestObjectResult(InvokedResult.Fail(error));
            };
        });

        builder.Services.AddOptions<AuthOptions>();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}