namespace aspnet.Data
{
    // Contrat pour le dépôt des marques. Les contrôleurs dépendront de cette abstraction,
    // pas de la classe concrète CarBrandSqlLiteRepository.
    public interface ICarBrandRepository
    {
        public IEnumerable<CarBrand> Brands { get; }

        public bool Add(CarBrand b);

        // Suppression à partir d'une entité CarBrand.
        public bool Delete(CarBrand b);

        public List<CarBrand> GetAllBrands();
    }
}
