

using APISirene.Domain.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace APISirene.Infrastructure.Data
{
    public class APISirenneDbContext
    {
        private readonly IMongoDatabase _database;

        public APISirenneDbContext(MongoClient mongoClient, IConfiguration configuration, string databaseName)
        {
            // Récupère le nom des collections à partir de la configuration
            var etablissementCollectionName = configuration.GetValue<string>("MongoDbSettings:EtablissementCollectionName");

            // Initialise la connexion à la base de données MongoDB avec le nom de la base de données spécifié
            _database = mongoClient.GetDatabase(databaseName);

            // Récupère la collection des utilisateurs
            Etablissement = _database.GetCollection<Etablissement>(etablissementCollectionName);
        }

        public APISirenneDbContext()
        {
            // Utilisé pour les tests

            // Chaîne de connexion à la base de données MongoDB locale
            var connectionString = "mongodb://localhost:27017";

            // Nom de la base de données
            var databaseName = "APISireneDB";

            // Nom des collections
            var usersCollectionName = "Etablissement";

            // Initialise le client MongoDB avec la chaîne de connexion
            var client = new MongoClient(connectionString);

            // Initialise la connexion à la base de données
            _database = client.GetDatabase(databaseName);


            Etablissement = _database.GetCollection<Etablissement>(usersCollectionName);
        }

        public IMongoCollection<Etablissement> Etablissement { get; }
    }
}
