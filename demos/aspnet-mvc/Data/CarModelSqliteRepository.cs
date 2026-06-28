using Microsoft.EntityFrameworkCore;

namespace aspnet.Data
{
    // Implémentation SQLite du dépôt des modèles. Respecte le contrat ICarModelRepository.
    public class CarModelSqliteRepository : ICarModelRepository
    {
        private readonly AppDbContext _context;

        public CarModelSqliteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Propriété à corps d'expression (=>) : Include charge aussi la marque de chaque modèle.
        public IEnumerable<CarModel> Models => _context.CarModels.Include(x => x.CarBrand);

        public bool Add(CarModel m)
        {
            _context.CarModels.Add(m);
            _context.SaveChanges();
            return true;
        }

        // Surcharge supplémentaire (suppression par nom), non requise par l'interface.
        public bool Delete(string m)
        {
            CarModel model = _context.CarModels.FirstOrDefault(x => x.Name == m);
            if (model == null)
                return false;

            _context.CarModels.Remove(model);
            _context.SaveChanges();
            return true;
        }

        // Version imposée par l'interface, laissée à implémenter (exemple de squelette de méthode).
        public bool Delete(CarModel m)
        {
            throw new NotImplementedException();
        }

        public List<CarModel> GetAllModels()
        {
            // On charge chaque modèle avec sa marque pour pouvoir afficher "Marque Modele" dans les vues.
            return _context.CarModels.Include(x => x.CarBrand).ToList();
        }
    }
}
