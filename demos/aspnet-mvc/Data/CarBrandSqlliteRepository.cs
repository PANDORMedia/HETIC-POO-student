
namespace aspnet.Data
{
    // Implémentation SQLite du dépôt des marques. Respecte le contrat ICarBrandRepository.
    public class CarBrandSqlLiteRepository : ICarBrandRepository
    {
        // Référence vers le contexte EF Core, injectée par le constructeur.
        public readonly AppDbContext _context;

        public CarBrandSqlLiteRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarBrand> Brands {
            get => _context.CarBrands;
        }

        public bool Add(CarBrand b)
        {
            _context.CarBrands.Add(b);
            _context.SaveChanges();
            return true;
        }

        // SURCHARGE de Delete : cette version supprime par NOM (string). Même nom de méthode,
        // paramètre différent. Elle n'est pas imposée par l'interface, c'est un service en plus.
        public bool Delete(string b)
        {
            // LINQ : on cherche la première marque dont le nom correspond, ou null si aucune.
            CarBrand temp = _context.CarBrands.Where(x => x.Name == b).FirstOrDefault();
            if (temp != null)
            {
                _context.CarBrands.Remove(temp);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        // Deuxième surcharge de Delete, celle exigée par l'interface (supprime par entité).
        // Volontairement non implémentée ici : lever NotImplementedException signale "pas encore codé".
        public bool Delete(CarBrand b)
        {
            throw new NotImplementedException();
        }

        public List<CarBrand> GetAllBrands()
        {
            return _context.CarBrands.ToList();
        }
    }
}
