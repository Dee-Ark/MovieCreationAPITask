using Microsoft.EntityFrameworkCore;
using MovieCreationAPI.Data;
using MovieCreationAPI.Interface;
using MovieCreationAPI.Mapping;
using MovieCreationAPI.Middleware;
using MovieCreationAPI.Repository;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/MovieAPI_log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Warning()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("MovieConnection")));


builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.Services.AddAutoMapper(typeof(Automapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExecptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
