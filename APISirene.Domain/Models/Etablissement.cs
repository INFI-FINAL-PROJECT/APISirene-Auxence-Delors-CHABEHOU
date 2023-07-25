using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace APISirene.Domain.Models
{
    public class Etablissement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("score")]
        public int Score { get; set; }

        [BsonElement("siren")]
        public string Siren { get; set; }

        [BsonElement("nic")]
        public string Nic { get; set; }

        [BsonElement("siret")]
        public string Siret { get; set; }

        [BsonElement("statutDiffusionEtablissement")]
        public string StatutDiffusionEtablissement { get; set; }

        [BsonElement("dateCreationEtablissement")]
        public string DateCreationEtablissement { get; set; }

        [BsonElement("trancheEffectifsEtablissement")]
        public string TrancheEffectifsEtablissement { get; set; }

        [BsonElement("anneeEffectifsEtablissement")]
        public string AnneeEffectifsEtablissement { get; set; }

        [BsonElement("activitePrincipaleRegistreMetiersEtablissement")]
        public string ActivitePrincipaleRegistreMetiersEtablissement { get; set; }

        [BsonElement("dateDernierTraitementEtablissement")]
        public string DateDernierTraitementEtablissement { get; set; }

        [BsonElement("etablissementSiege")]
        public bool EtablissementSiege { get; set; }

        [BsonElement("nombrePeriodesEtablissement")]
        public int NombrePeriodesEtablissement { get; set; }

        [BsonElement("uniteLegale")]
        public UniteLegaleDetails UniteLegale { get; set; }

        [BsonElement("adresseEtablissement")]
        public AdresseEtablissement AdresseEtablissement { get; set; }

        [BsonElement("adresse2Etablissement")]
        public Adresse2Etablissement Adresse2Etablissement { get; set; }

        [BsonElement("periodesEtablissement")]
        public PeriodeEtablissement[] PeriodesEtablissement { get; set; }
    }

    public class UniteLegaleDetails
    {
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

        [BsonElement("etatAdministratifUniteLegale")]
        public string EtatAdministratifUniteLegale { get; set; }

        [BsonElement("nomUniteLegale")]
        public string NomUniteLegale { get; set; }

        [BsonElement("denominationUniteLegale")]
        public string DenominationUniteLegale { get; set; }

        [BsonElement("denominationUsuelle1UniteLegale")]
        public string DenominationUsuelle1UniteLegale { get; set; }

        [BsonElement("denominationUsuelle2UniteLegale")]
        public string DenominationUsuelle2UniteLegale { get; set; }

        [BsonElement("denominationUsuelle3UniteLegale")]
        public string DenominationUsuelle3UniteLegale { get; set; }

        [BsonElement("activitePrincipaleUniteLegale")]
        public string ActivitePrincipaleUniteLegale { get; set; }

        [BsonElement("categorieJuridiqueUniteLegale")]
        public string CategorieJuridiqueUniteLegale { get; set; }

        [BsonElement("nicSiegeUniteLegale")]
        public string NicSiegeUniteLegale { get; set; }

        [BsonElement("nomenclatureActivitePrincipaleUniteLegale")]
        public string NomenclatureActivitePrincipaleUniteLegale { get; set; }

        [BsonElement("nomUsageUniteLegale")]
        public string NomUsageUniteLegale { get; set; }

        [BsonElement("economieSocialeSolidaireUniteLegale")]
        public string EconomieSocialeSolidaireUniteLegale { get; set; }

        [BsonElement("societeMissionUniteLegale")]
        public string SocieteMissionUniteLegale { get; set; }

        [BsonElement("caractereEmployeurUniteLegale")]
        public string CaractereEmployeurUniteLegale { get; set; }
    }

    public class AdresseEtablissement
    {
        [BsonElement("complementAdresseEtablissement")]
        public string ComplementAdresseEtablissement { get; set; }

        [BsonElement("numeroVoieEtablissement")]
        public string NumeroVoieEtablissement { get; set; }

        [BsonElement("indiceRepetitionEtablissement")]
        public string IndiceRepetitionEtablissement { get; set; }

        [BsonElement("typeVoieEtablissement")]
        public string TypeVoieEtablissement { get; set; }

        [BsonElement("libelleVoieEtablissement")]
        public string LibelleVoieEtablissement { get; set; }

        [BsonElement("codePostalEtablissement")]
        public string CodePostalEtablissement { get; set; }

        [BsonElement("libelleCommuneEtablissement")]
        public string LibelleCommuneEtablissement { get; set; }

        [BsonElement("libelleCommuneEtrangerEtablissement")]
        public string LibelleCommuneEtrangerEtablissement { get; set; }

        [BsonElement("distributionSpecialeEtablissement")]
        public string DistributionSpecialeEtablissement { get; set; }

        [BsonElement("codeCommuneEtablissement")]
        public string CodeCommuneEtablissement { get; set; }

        [BsonElement("codeCedexEtablissement")]
        public string CodeCedexEtablissement { get; set; }

        [BsonElement("libelleCedexEtablissement")]
        public string LibelleCedexEtablissement { get; set; }

        [BsonElement("codePaysEtrangerEtablissement")]
        public string CodePaysEtrangerEtablissement { get; set; }

        [BsonElement("libellePaysEtrangerEtablissement")]
        public string LibellePaysEtrangerEtablissement { get; set; }
    }

    public class Adresse2Etablissement
    {
        [BsonElement("complementAdresse2Etablissement")]
        public string ComplementAdresse2Etablissement { get; set; }

        [BsonElement("numeroVoie2Etablissement")]
        public string NumeroVoie2Etablissement { get; set; }

        [BsonElement("indiceRepetition2Etablissement")]
        public string IndiceRepetition2Etablissement { get; set; }

        [BsonElement("typeVoie2Etablissement")]
        public string TypeVoie2Etablissement { get; set; }

        [BsonElement("libelleVoie2Etablissement")]
        public string LibelleVoie2Etablissement { get; set; }

        [BsonElement("codePostal2Etablissement")]
        public string CodePostal2Etablissement { get; set; }

        [BsonElement("libelleCommune2Etablissement")]
        public string LibelleCommune2Etablissement { get; set; }

        [BsonElement("libelleCommuneEtranger2Etablissement")]
        public string LibelleCommuneEtranger2Etablissement { get; set; }

        [BsonElement("distributionSpeciale2Etablissement")]
        public string DistributionSpeciale2Etablissement { get; set; }

        [BsonElement("codeCommune2Etablissement")]
        public string CodeCommune2Etablissement { get; set; }

        [BsonElement("codeCedex2Etablissement")]
        public string CodeCedex2Etablissement { get; set; }

        [BsonElement("libelleCedex2Etablissement")]
        public string LibelleCedex2Etablissement { get; set; }

        [BsonElement("codePaysEtranger2Etablissement")]
        public string CodePaysEtranger2Etablissement { get; set; }

        [BsonElement("libellePaysEtranger2Etablissement")]
        public string LibellePaysEtranger2Etablissement { get; set; }
    }

    public class PeriodeEtablissement
    {
        [BsonElement("dateFin")]
        public string DateFin { get; set; }

        [BsonElement("dateDebut")]
        public string DateDebut { get; set; }

        [BsonElement("etatAdministratifEtablissement")]
        public string EtatAdministratifEtablissement { get; set; }

        [BsonElement("changementEtatAdministratifEtablissement")]
        public bool ChangementEtatAdministratifEtablissement { get; set; }

        [BsonElement("enseigne1Etablissement")]
        public string Enseigne1Etablissement { get; set; }

        [BsonElement("enseigne2Etablissement")]
        public string Enseigne2Etablissement { get; set; }

        [BsonElement("enseigne3Etablissement")]
        public string Enseigne3Etablissement { get; set; }

        [BsonElement("changementEnseigneEtablissement")]
        public bool ChangementEnseigneEtablissement { get; set; }

        [BsonElement("denominationUsuelleEtablissement")]
        public string DenominationUsuelleEtablissement { get; set; }

        [BsonElement("changementDenominationUsuelleEtablissement")]
        public bool ChangementDenominationUsuelleEtablissement { get; set; }

        [BsonElement("activitePrincipaleEtablissement")]
        public string ActivitePrincipaleEtablissement { get; set; }

        [BsonElement("nomenclatureActivitePrincipaleEtablissement")]
        public string NomenclatureActivitePrincipaleEtablissement { get; set; }

        [BsonElement("changementActivitePrincipaleEtablissement")]
        public bool ChangementActivitePrincipaleEtablissement { get; set; }

        [BsonElement("caractereEmployeurEtablissement")]
        public string CaractereEmployeurEtablissement { get; set; }

        [BsonElement("changementCaractereEmployeurEtablissement")]
        public bool ChangementCaractereEmployeurEtablissement { get; set; }
    }

    public class Facette
    {
        [BsonElement("nom")]
        public string Nom { get; set; }

        [BsonElement("manquants")]
        public int Manquants { get; set; }

        [BsonElement("total")]
        public int Total { get; set; }

        [BsonElement("modalites")]
        public int Modalites { get; set; }

        [BsonElement("avant")]
        public int Avant { get; set; }

        [BsonElement("apres")]
        public int Apres { get; set; }

        [BsonElement("entre")]
        public int Entre { get; set; }

        [BsonElement("comptages")]
        public List<Comptage> Comptages { get; set; }

        [BsonElement("facettes")]
        public List<Facette> Facettes { get; set; }
    }


    public class Comptage
    {
        [BsonElement("valeur")]
        public BsonDocument Valeur { get; set; }

        [BsonElement("nombre")]
        public int Nombre { get; set; }

        [BsonElement("facettes")]
        public List<Facette> Facettes { get; set; }
    }

    public class Root
    {
        public int TotalResults { get; set; }
        public int ItemsPerPage { get; set; }
        public int Page { get; set; }
        public List<Etablissement> Etablissements { get; set; }
    }

    public class SireneApiResponse
    {
        public SireneApiHeader header { get; set; }
        public IEnumerable<Etablissement> etablissements { get; set; }
    }

    public class SireneApiHeader
    {
        public int statut { get; set; }
        public string message { get; set; }
        public int total { get; set; }
        public int debut { get; set; }
        public int nombre { get; set; }
    }
}
