// Contrat décrivant la capacité "être assurable".
// On sépare encore une capacité métier d'un type concret : une voiture est assurable,
// un van (dans cette démo) ne l'est pas. Le fait d'implémenter ou non cette interface
// devient un choix de conception, pas une obligation imposée à tous les véhicules.
public interface IAssurable
{
    // La classe devra exposer une catégorie d'assurance (ex. Type de véhicule pour l'assureur).
    string InsuranceCategory { get; set; }

    // ... Et savoir calculer le prix de la prime. Le "comment" est laissé à chaque classe.
    decimal GetInsurancePrice();

}
