using FootballLeague.API.MappingConfiguration;
using FootballLeague.API.Services;
using FootballLeague.API.Services.Contracts;
using FootballLeague.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<FootballLeagueDbContext>(options =>
       options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<IMatchesService, MatchesService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();