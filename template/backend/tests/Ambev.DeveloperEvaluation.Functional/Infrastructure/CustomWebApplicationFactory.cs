using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Ambev.DeveloperEvaluation.Functional.Infrastructure;

using System.Linq;
using Ambev.DeveloperEvaluation.ORM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseContentRoot(GetContentRootPath());
        return base.CreateHost(builder);
    }

    private string GetContentRootPath()
    {
        var relativePath = @"src/Ambev.DeveloperEvaluation.WebApi/Ambev.DeveloperEvaluation.WebApi.csproj";
        return Path.GetFullPath(relativePath, Directory.GetCurrentDirectory());
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<DefaultContext>(options =>
            {
                options.UseInMemoryDatabase("TestSalesDb");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            db.Database.EnsureCreated();
        });
    }
    
}