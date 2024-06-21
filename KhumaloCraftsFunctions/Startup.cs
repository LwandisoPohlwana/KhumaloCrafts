using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Database_Layer.DataServices;
using System;
using Logic_Layer.Interfaces;
using Logic_Layer.Services;
using Microsoft.Azure.WebJobs.Extensions.DurableTask.ContextImplementations;

[assembly: FunctionsStartup(typeof(KhumaloCraftsFunctions.Startup))]

namespace KhumaloCraftsFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString")));

            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<IDurableFunctionsService, DurableFunctionsService>();
        }
    }
}