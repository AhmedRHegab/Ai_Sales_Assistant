using Propulse.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Propulse.Core.Interfaces;
using Propulse.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://+:{port}");

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? "Data Source=/app/data/propulse.db";
builder.Services.AddDbContext<PropulseDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddScoped<IWhatsAppService, TwilioWhatsAppService>();
builder.Services.AddScoped<IAiService, OpenAiService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PropulseDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
