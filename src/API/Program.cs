using Microsoft.EntityFrameworkCore;
using API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(opt => opt.UseNpgsql(builder.Configuration["ConnectionStrings:Postgres"]));
builder.Logging.SetMinimumLevel(LogLevel.Information).ClearProviders();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var databaseContext = scope.ServiceProvider.GetRequiredService<Context>();
    databaseContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
