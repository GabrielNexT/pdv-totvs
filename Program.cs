using Microsoft.EntityFrameworkCore;
using System.Data;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var services = builder.Services;

string dbUrl = builder.Configuration.GetConnectionString("Default");

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddControllers();

services.AddDbContext<ChangeContext>(options => options.UseNpgsql(dbUrl));

services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(dbUrl));

services.AddScoped<IChangeService, ChangeService>();
services.AddScoped<IChangeRepository, ChangeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
  var tmpServices = scope.ServiceProvider;

  var context = tmpServices.GetRequiredService<ChangeContext>();
  context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();