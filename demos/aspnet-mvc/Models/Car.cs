
namespace aspnet.Models
{
    // Entité métier représentant une voiture physique (un exemplaire avec sa plaque).
    // Une classe regroupe des données (les propriétés) qui décrivent un objet du domaine.
    public class Car
    {
        // Clé primaire. La convention EF Core reconnaît automatiquement "Id" comme identifiant.
        public int Id {get; set;}

       // Propriétés auto-implémentées : { get; set; } génère un champ caché et ses accesseurs.
       // C'est de l'encapsulation : on accède à la donnée via la propriété, pas par un champ public.
       public string PlateNumber { get; set; }

       // Propriété CALCULÉE en lecture seule (pas de "set") : sa valeur est dérivée d'autres objets.
       // Brand n'est pas stockée, elle est recalculée à partir du modèle et de sa marque.
       public string Brand
        {
            get
            {
                // On traverse les relations : Car -> CarModel -> CarBrand -> Name.
                return CarModel.CarBrand.Name;
            }
        }

        // Autre propriété calculée : le nom du modèle, obtenu via la propriété de navigation.
        public string Model
        {
            get
            {
                return CarModel.Name;
            }
        }

       // Clé étrangère : stocke seulement l'identifiant du modèle lié (relation en base).
       public int CarModelId { get; set; }  // Foreign key to CarModel
       // Propriété de navigation : référence l'objet CarModel complet associé.
       // "= null!" signale au compilateur que la valeur sera fournie par EF Core (on assume non nul).
       public CarModel CarModel { get; set; } = null!; // Navigation property
    }
}
