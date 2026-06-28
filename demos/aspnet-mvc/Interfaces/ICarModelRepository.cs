namespace aspnet.Data
{
    // Contrat pour le dépôt des modèles de voitures. Même principe que les autres interfaces :
    // on définit ce que doit savoir faire un dépôt de modèles, sans dire comment.
    public interface ICarModelRepository
    {
        public IEnumerable<CarModel> Models { get; }

        public bool Add(CarModel m);

        public bool Delete(CarModel m);

        public List<CarModel> GetAllModels();
    }
}
