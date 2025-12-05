using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderTrackingApp.MigrationRunner;
using OrderTrackingApp.Persistence;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var connStr = context.Configuration["ConnectionStrings:SqlConnection"];
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connStr));
    })
    .Build();

using var scope = host.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

await ProductSeeder.SeedAsync(db);

