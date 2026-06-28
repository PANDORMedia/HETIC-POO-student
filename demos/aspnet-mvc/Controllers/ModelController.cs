using aspnet.Data;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aspnet.Controllers
{
    // Contrôleur des modèles de voitures (CarModel), par exemple "Golf" ou "Clio".
    public class ModelController : Controller
    {
        private readonly ILogger<ModelController> _logger;
        // Un modèle appartient à une marque : ce contrôleur a donc besoin des deux dépôts.
        private readonly ICarModelRepository _repo;
        private readonly ICarBrandRepository _brandRepo;

        // Les trois dépendances sont injectées automatiquement par le conteneur.
        public ModelController(ILogger<ModelController> logger, ICarModelRepository repo, ICarBrandRepository brandRepo)
        {
            _logger = logger;
            _repo = repo;
            _brandRepo = brandRepo;
        }

        // GET /Model/Index : liste tous les modèles.
        public IActionResult Index()
        {
            var models = _repo.GetAllModels();
            return View(models);
        }

        // GET /Model/Create : prépare la liste déroulante des marques puis affiche le formulaire.
        public IActionResult Create()
        {
            SetBrandSelectList();
            return View();
        }

        // POST /Model/Create : surcharge qui traite la soumission du formulaire de création.
        [HttpPost]
        public IActionResult Create(CarModelCreateVM m)
        {
            if (!ModelState.IsValid)
            {
                SetBrandSelectList();
                return View(m);
            }

            // Conversion du ViewModel en entité CarModel avant l'enregistrement via le dépôt.
            _repo.Add(new CarModel() { Name = m.Name, DailyPrice = m.DailyPrice, NbSeats = m.NbSeats, Fuel = m.Fuel, CarBrandId = m.CarBrandId });
            _logger.Log(LogLevel.Debug, m.Name + " created");

            return RedirectToAction("Index");
        }

        // Helper privé : remplit ViewBag.Brands avec la liste des marques pour le menu déroulant.
        private void SetBrandSelectList()
        {
            ViewBag.Brands = new SelectList(_brandRepo.GetAllBrands(), "Id", "Name");
        }
    }
}
