using aspnet.Interfaces;
using aspnet.Models;
using Microsoft.EntityFrameworkCore;

namespace aspnet.Data
{
    // Implémentation CONCRÈTE de ICarRepository qui stocke les voitures dans SQLite via EF Core.
    // ": ICarRepository" signifie "cette classe respecte le contrat ICarRepository".
    // C'est l'une des implémentations interchangeables (à comparer avec CarCSVRepository et CarDummyRepository).
    public class CarSqlLiteRepository : ICarRepository
    {
        // Le contexte EF Core est lui-même injecté par le conteneur (voir AddDbContext dans Program.cs).
        // Cette classe DÉLÈGUE le travail de base de données à _context : c'est de la composition.
        private readonly AppDbContext _context;

        public CarSqlLiteRepository(AppDbContext context)
        {
            _context = context;
        }

        // get => ... est une syntaxe abrégée pour un accesseur en lecture.
        public IEnumerable<Car> Cars {
            get => _context.Cars;
        }

        // Ajoute une voiture puis SaveChanges() écrit réellement les changements en base.
        public bool Add(Car c)
        {
            _context.Cars.Add(c);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(Car c)
        {
            _context.Cars.Remove(c);
            _context.SaveChanges();
            return true;
        }

        public List<Car> GetAllCars()
        {
            // Include / ThenInclude demandent à EF Core de charger aussi les objets liés
            // (le modèle de chaque voiture, puis la marque de chaque modèle) en une seule requête.
            // Sans cela, les propriétés de navigation seraient nulles (chargement différé non actif ici).
            return _context.Cars
                .Include(c => c.CarModel)
                .ThenInclude(m => m.CarBrand)
                .ToList();
        }
    }
}
