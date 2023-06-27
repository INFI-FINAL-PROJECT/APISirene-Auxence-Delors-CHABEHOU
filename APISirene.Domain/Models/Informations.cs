using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISirene.Domain.Models
{
    public class Informations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("etatService")]
        public string ServiceState { get; set; }

        [BsonElement("etatsDesServices")]
        public List<ServiceStateInfo> ServiceStates { get; set; }

        [BsonElement("versionService")]
        public string ServiceVersion { get; set; }

        [BsonElement("journalDesModifications")]
        public string ChangeLog { get; set; }

        [BsonElement("datesDernieresMisesAJourDesDonnees")]
        public List<DataUpdateInfo> DataUpdates { get; set; }
    }

    public class ServiceStateInfo
    {
        [BsonElement("Collection")]
        public string CollectionName { get; set; }

        [BsonElement("etatCollection")]
        public string CollectionState { get; set; }
    }

    public class DataUpdateInfo
    {
        [BsonElement("collection")]
        public string CollectionName { get; set; }

        [BsonElement("dateDerniereMiseADisposition")]
        public string LastDataAvailabilityDate { get; set; }

        [BsonElement("dateDernierTraitementMaximum")]
        public string LastMaxProcessingDate { get; set; }

        [BsonElement("dateDernierTraitementDeMasse")]
        public string LastBulkProcessingDate { get; set; }
    }
}
