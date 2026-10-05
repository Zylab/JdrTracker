using System.Text.Json.Serialization;

using JdrTracker.Application.Campaigns.Commands.CreateCampaign;
using JdrTracker.Application.Campaigns.Common;
using JdrTracker.Application.Common.Interfaces;
using JdrTracker.Infrastructure.Persistence;
using JdrTracker.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Enregistre le DbContext avec SQLite dans le conteneur d'injection de dépendances
//La chaîne de connexion vient de appsettings.json : l'Api décide du provider, l'Application l'ignore
builder.Services.AddDbContext<JdrTrackerDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Permet de convertir les enums en string pour l'affichage
builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

//On passe un type quelconque de l'Application pour désigner l'assembly à scanner
//MediatR y découvre automatiquement tous les handlers, présents et futurs
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateCampaignCommand).Assembly));

//Même principe : tous les profils de mapping de l'assembly sont enregistrés
builder.Services.AddAutoMapper(cfg =>
    cfg.AddMaps(typeof(CampaignMappingProfile).Assembly));

//Implémentations des contrats déclarés dans Application, à déclarer une par une
//Scoped = une instance par requête HTTP, pour que repository et unit of work
//partagent le même DbContext et du coup le meme scope
builder.Services.AddScoped<ICampaignRepository, CampaignRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
        options.SwaggerEndpoint("/openapi/v1.json", "JdrTracker API"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
