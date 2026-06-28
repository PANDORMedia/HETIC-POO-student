using aspnet.Models;

namespace aspnet.Interfaces
{

    // Une interface est un CONTRAT : elle déclare des membres sans les implémenter.
    // Toute classe qui déclare "implémenter" ICarRepository devra fournir tout ce qui suit.
    // C'est ce qui permet d'avoir plusieurs dépôts interchangeables (SQLite, CSV, factice)
    // tous vus de la même manière par les contrôleurs. C'est la base du polymorphisme.
    public interface ICarRepository
    {
        // Propriété en lecture seule exposant la collection de voitures.
        public IEnumerable<Car> Cars { get; }

        // Opérations de base attendues d'un dépôt (style CRUD : créer, lire, supprimer).
        public bool Add(Car c);

        public bool Delete(Car c);

        public List<Car> GetAllCars();

    }
}
