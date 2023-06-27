

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace APISirene.Domain.Models
{
    public class UniteLegale
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }

        [BsonElement("siren")]
        public string Siren { get; set; }

        [BsonElement("statutDiffusionUniteLegale")]
        public string StatutDiffusionUniteLegale { get; set; }

        [BsonElement("unitePurgeeUniteLegale")]
        public bool UnitePurgeeUniteLegale { get; set; }

        [BsonElement("dateCreationUniteLegale")]
        public string DateCreationUniteLegale { get; set; }

        [BsonElement("sigleUniteLegale")]
        public string SigleUniteLegale { get; set; }

        [BsonElement("sexeUniteLegale")]
        public string SexeUniteLegale { get; set; }

        [BsonElement("prenom1UniteLegale")]
        public string Prenom1UniteLegale { get; set; }

        [BsonElement("prenom2UniteLegale")]
        public string Prenom2UniteLegale { get; set; }

        [BsonElement("prenom3UniteLegale")]
        public string Prenom3UniteLegale { get; set; }

        [BsonElement("prenom4UniteLegale")]
        public string Prenom4UniteLegale { get; set; }

        [BsonElement("prenomUsuelUniteLegale")]
        public string PrenomUsuelUniteLegale { get; set; }

        [BsonElement("pseudonymeUniteLegale")]
        public string PseudonymeUniteLegale { get; set; }

        [BsonElement("identifiantAssociationUniteLegale")]
        public string IdentifiantAssociationUniteLegale { get; set; }

        [BsonElement("trancheEffectifsUniteLegale")]
        public string TrancheEffectifsUniteLegale { get; set; }

        [BsonElement("anneeEffectifsUniteLegale")]
        public string AnneeEffectifsUniteLegale { get; set; }

        [BsonElement("dateDernierTraitementUniteLegale")]
        public string DateDernierTraitementUniteLegale { get; set; }

        [BsonElement("nombrePeriodesUniteLegale")]
        public int NombrePeriodesUniteLegale { get; set; }

        [BsonElement("categorieEntreprise")]
        public string CategorieEntreprise { get; set; }

        [BsonElement("anneeCategorieEntreprise")]
        public string AnneeCategorieEntreprise { get; set; }

        [BsonElement("periodesUniteLegale")]
        public List<PeriodeUniteLegale> PeriodesUniteLegale { get; set; }
    }

    public class PeriodeUniteLegale
    {
        [BsonElement("dateFin")]
        public string DateFin { get; set; }

        [BsonElement("dateDebut")]
        public string DateDebut { get; set; }

        [BsonElement("etatAdministratifUniteLegale")]
        public string EtatAdministratifUniteLegale { get; set; }

        [BsonElement("changementEtatAdministratifUniteLegale")]
        public bool ChangementEtatAdministratifUniteLegale { get; set; }

        [BsonElement("nomUniteLegale")]
        public string NomUniteLegale { get; set; }

        [BsonElement("changementNomUniteLegale")]
        public bool ChangementNomUniteLegale { get; set; }

        [BsonElement("nomUsageUniteLegale")]
        public string NomUsageUniteLegale { get; set; }

        [BsonElement("changementNomUsageUniteLegale")]
        public bool ChangementNomUsageUniteLegale { get; set; }

        [BsonElement("denominationUniteLegale")]
        public string DenominationUniteLegale { get; set; }

        [BsonElement("changementDenominationUniteLegale")]
        public bool ChangementDenominationUniteLegale { get; set; }

        [BsonElement("denominationUsuelle1UniteLegale")]
        public string DenominationUsuelle1UniteLegale { get; set; }

        [BsonElement("denominationUsuelle2UniteLegale")]
        public string DenominationUsuelle2UniteLegale { get; set; }

        [BsonElement("denominationUsuelle3UniteLegale")]
        public string DenominationUsuelle3UniteLegale { get; set; }

        [BsonElement("changementDenominationUsuelleUniteLegale")]
        public bool ChangementDenominationUsuelleUniteLegale { get; set; }

        [BsonElement("categorieJuridiqueUniteLegale")]
        public string CategorieJuridiqueUniteLegale { get; set; }

        [BsonElement("changementCategorieJuridiqueUniteLegale")]
        public bool ChangementCategorieJuridiqueUniteLegale { get; set; }

        [BsonElement("activitePrincipaleUniteLegale")]
        public string ActivitePrincipaleUniteLegale { get; set; }

        [BsonElement("nomenclatureActivitePrincipaleUniteLegale")]
        public string NomenclatureActivitePrincipaleUniteLegale { get; set; }

        [BsonElement("changementActivitePrincipaleUniteLegale")]
        public bool ChangementActivitePrincipaleUniteLegale { get; set; }

        [BsonElement("nicSiegeUniteLegale")]
        public string NicSiegeUniteLegale { get; set; }

        [BsonElement("changementNicSiegeUniteLegale")]
        public bool ChangementNicSiegeUniteLegale { get; set; }

        [BsonElement("economieSocialeSolidaireUniteLegale")]
        public string EconomieSocialeSolidaireUniteLegale { get; set; }

        [BsonElement("changementEconomieSocialeSolidaireUniteLegale")]
        public bool ChangementEconomieSocialeSolidaireUniteLegale { get; set; }

        [BsonElement("societeMissionUniteLegale")]
        public string SocieteMissionUniteLegale { get; set; }

        [BsonElement("changementSocieteMissionUniteLegale")]
        public bool ChangementSocieteMissionUniteLegale { get; set; }

        [BsonElement("caractereEmployeurUniteLegale")]
        public string CaractereEmployeurUniteLegale { get; set; }

        [BsonElement("changementCaractereEmployeurUniteLegale")]
        public bool ChangementCaractereEmployeurUniteLegale { get; set; }
    }
}
