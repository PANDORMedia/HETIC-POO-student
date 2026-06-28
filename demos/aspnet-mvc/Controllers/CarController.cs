
using aspnet.Interfaces;
using aspnet.Models;
using aspnet.Models.ViewModels;
using aspnet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace aspnet.Controllers
{
    // Contrôleur des voitures (Car). Comme les autres, il hérite de Controller.
    public class CarController : Controller
    {
        private readonly ILogger<CarController> _logger;

        // Ce contrôleur dépend de deux abstractions : le dépôt des voitures et celui des modèles.
        // On a besoin des modèles pour proposer une liste déroulante lors de la création.
        private readonly ICarRepository _repo;
        private readonly ICarModelRepository _modelRepo;
        // Propriété calculée privée, en lecture seule : à chaque accès, elle interroge le dépôt.
        // Cela illustre l'encapsulation : on expose une donnée dérivée sans stocker de champ.
        private List<Car> cars
        {
            get
            {
                return _repo.GetAllCars();
            }
        }

        // Injection par constructeur des trois dépendances enregistrées dans Program.cs.
        public CarController(ILogger<CarController> logger, ICarRepository repo, ICarModelRepository modelRepo)
        {
            _logger = logger;
            _repo = repo;
            _modelRepo = modelRepo;
        }

        // GET /Car/Index : affiche toutes les voitures.
        public IActionResult Index()
        {

            return View(cars);
        }

        // GET /Car/Create : prépare la liste déroulante des modèles puis affiche le formulaire.
        public IActionResult Create()
        {
            SetModelSelectList();
            return View();
        }

        // POST /Car/Create : surcharge qui traite l'envoi du formulaire.
        [HttpPost]
        public IActionResult Create(CarCreateVM c)
        {
            // Si la validation échoue, on reconstruit la liste déroulante et on réaffiche le formulaire.
            if(!ModelState.IsValid)
            {
                SetModelSelectList();
                return View(c);
            }

            // Transformation du ViewModel (saisie) en entité Car (objet métier persistant).
            Car temp = new Car()
            {
                PlateNumber = c.PlateNumber,
                CarModelId = c.CarModelId,
            };
            _logger.Log(LogLevel.Debug, temp.PlateNumber + " created");
            _repo.Add(temp);

            return RedirectToAction("Index");
        }

        // Méthode privée utilitaire (helper) : évite de dupliquer le code dans les deux Create.
        // Elle construit une SelectList pour le menu déroulant des modèles dans la vue.
        private void SetModelSelectList()
        {
            // LINQ : on projette chaque modèle en un objet anonyme avec un libellé "Marque Modele".
            // On accède à m.CarBrand.Name grâce à la propriété de navigation entre les entités.
            var models = _modelRepo.GetAllModels()
                .Select(m => new
                {
                    m.Id,
                    Name = $"{m.CarBrand.Name} {m.Name}"
                });

            // ViewBag est un sac dynamique pour transmettre des données secondaires à la vue.
            ViewBag.Models = new SelectList(models, "Id", "Name");
        }
    }
}
