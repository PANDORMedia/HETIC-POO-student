using aspnet.Interfaces;
using aspnet.Models;

namespace aspnet.Data
{

    // Implémentation factice ("dummy") du dépôt des voitures : aucune base de données,
    // juste une liste en mémoire avec des données pré-remplies. Très pratique pour tester
    // ou démarrer rapidement. Comme elle respecte ICarRepository, on peut la substituer à
    // CarSqlLiteRepository dans Program.cs sans rien changer aux contrôleurs : c'est tout
    // l'intérêt de programmer contre une interface (substituabilité / polymorphisme).
    public class CarDummyRepository : ICarRepository
    {
        // "Static" : la liste est partagée par toutes les instances de la classe et survit
        // entre les requêtes (utile ici pour conserver les ajouts en mémoire pendant l'exécution).
        private static List<Car> _cars = new List<Car>()
        {
            new Car()
            {
                Id = 0,
                // Brand = "Jaguar",
                // Model = "XF"
            },
            new Car()
            {
                Id = 1,
                // Brand = "VW",
                // Model = "Golf R32"
            }
        };
        public IEnumerable<Car> Cars {
            get => _cars;
        }

        // Pas de SaveChanges ici : on modifie simplement la liste en mémoire.
        public bool Add(Car c)
        {
            _cars.Add(c);
            return true;
        }

        public bool Delete(Car c)
        {
            _cars.Remove(c);
            return true;
        }

        public List<Car> GetAllCars()
        {
            return _cars as List<Car>;
        }
    }
}
