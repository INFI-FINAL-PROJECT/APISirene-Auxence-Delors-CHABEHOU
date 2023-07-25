using APISirene.Domain.Interfaces.InterfaceRepository;
using APISirene.Domain.Interfaces.InterfaceService;
using APISirene.Domain.Services;
using APISirene.Infrastructure.Data;
using APISirene.Infrastructure.Repository;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using OfficeOpenXml;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;


public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Récupération de la chaine de connexion MongoDB et du nom de la base de données depuis la configuration
        var connectionString = builder.Configuration.GetValue<string>("ConnectionStrings:MongoDb");
        var databaseName = builder.Configuration.GetValue<string>("MongoDbSettings:DatabaseName");

        // Création d'une instance de MongoClient en utilisant la chaine de connexion
        var client = new MongoClient(connectionString);

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        // Récupération de la base de données MongoDB
        var database = client.GetDatabase(databaseName);

        // Ajout de la base de données en tant que service singleton
        builder.Services.AddSingleton(database);

        // Ajout des services nécessaires à l'injection de dépendances
        builder.Services.AddScoped<APISirenneDbContext>();

        // Ajout des services nécessaires à l'injection de dépendances de Etablissement
        builder.Services.AddScoped<IEtablissementRepository, EtablissementRepository>();
        builder.Services.AddScoped<IEtablissementService, EtablissementService>();

        // Configuration de CORS pour autoriser les requêtes provenant de http://localhost:4200
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("_myAllowedOrigins",
                builder =>
                {
                    builder.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        // Configuration des contrôleurs
        builder.Services.AddControllers();

        // Configuration de Swagger
        builder.Services.AddSwaggerGen(c =>
        {
            // Configuration de l'information générale de l'API
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Sirene - V3",
                Description = "<h2>API Sirene donne accès aux informations concernant les entreprises et les établissements enregistrés au répertoire interadministratif Sirene depuis sa création en 1973, y compris les unités fermées.</h2>",
                Version = "1.0.0"
            });

            // Configuration du fichier XML de documentation
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
            app.UseHsts();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Sirene - V3");
            c.RoutePrefix = string.Empty;
            c.DocumentTitle = "Sirene - V3 App API";
            c.DefaultModelExpandDepth(-1);
            c.DefaultModelsExpandDepth(-1);
            c.DefaultModelRendering(ModelRendering.Example);
            c.DisplayRequestDuration();
            c.DocExpansion(DocExpansion.None);

            // Configuration de la recherche dans Swagger
            c.EnableDeepLinking();
            c.DocExpansion(DocExpansion.List);
            c.DefaultModelsExpandDepth(-1);
            c.DefaultModelRendering(ModelRendering.Example);
            c.DisplayRequestDuration();
            c.EnableFilter();
            c.ShowExtensions();
            c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
            c.SupportedSubmitMethods(new[] { SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete });
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("_myAllowedOrigins");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.Run();
    }
}