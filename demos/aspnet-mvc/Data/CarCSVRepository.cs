using aspnet.Exportables;
using aspnet.Interfaces;
using aspnet.Models;

namespace aspnet.Data
{

    // Troisième implémentation de ICarRepository : ici les voitures sont lues et écrites
    // dans un fichier CSV. Même contrat que les autres dépôts, mais une source de données
    // totalement différente. Cela illustre concrètement le principe d'inversion de dépendance.
    public class CarCSVRepository : ICarRepository
    {
        private static List<Car> _cars = new List<Car>();

        public IEnumerable<Car> Cars {
            get => _cars;
        }

        // Au moment de la construction, on charge le contenu du fichier CSV en mémoire.
        public CarCSVRepository()
        {
            _cars = CSVWriter.ReadFile().ToList<Car>();
        }

        public bool Add(Car c)
        {
            // Calcul d'un nouvel identifiant via l'opérateur ternaire : si la liste contient
            // déjà des voitures, on prend le plus grand Id + 1, sinon on commence à 0.
            c.Id = _cars.Count > 0 ? _cars.Max(car => car.Id) + 1 : 0;
            Console.WriteLine("Adding car to repository...");
            _cars.Add(c);

            // On convertit chaque Car en ExportableCar (la sous-classe qui sait s'exporter en CSV).
            // C'est ici qu'intervient l'interface IExportable implémentée par ExportableCar.
            var listTemp = new List<ExportableCar>();
            foreach (var car in _cars)            {
                listTemp.Add(new ExportableCar(car));
            }


            Console.WriteLine("ListTemp:");
            foreach (var car in listTemp)            {
                Console.WriteLine(car.ExportAsCSV());
            }

            // On délègue l'écriture du fichier à la classe utilitaire csvwriter.
            CSVWriter.WriteToFile(listTemp);
            return true;
        }

        public bool Delete(Car c)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllCars()
        {
            return _cars as List<Car>;
        }
    }

}
