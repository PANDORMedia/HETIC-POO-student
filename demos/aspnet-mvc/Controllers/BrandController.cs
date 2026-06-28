using aspnet.Data;
using aspnet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    // Un contrôleur gère les marques de voitures (CarBrand).
    // Il hérite de la classe de base Controller fournie par ASP.NET : grâce à cet
    // héritage, il récupère gratuitement des méthodes comme View() ou RedirectToAction().
    public class BrandController : Controller
            {
        // "Readonly" : ces références sont fixées une fois pour toutes dans le constructeur.
        // _Logger sert à tracer des messages ; _repo est l'accès aux données.
        private readonly ILogger<BrandController> _logger;
        // On dépend de l'interface ICarBrandRepository, pas d'une classe concrète.
        // C'est l'abstraction : le contrôleur ne sait pas si les marques viennent de SQLite,
        // d'un CSV ou d'ailleurs. Cela rend le code testable et interchangeable.
        private readonly ICarBrandRepository _repo;

        // Injection de dépendances par constructeur : ASP.NET crée le contrôleur et lui
        // "injecte" automatiquement un logger et un repository (ceux enregistrés dans Program.cs).
        public BrandController(ILogger<BrandController> logger, ICarBrandRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        // Action accessible via l'URL /Brand/Index : affiche la liste des marques.
        // View(brands) passe la liste à la vue, qui sert de Modèle à afficher.
        public IActionResult Index()
        {
            var brands = _repo.GetAllBrands();

            return View(brands);
        }

        // Affiche le formulaire de création (GET /Brand/Create). Pas de données à fournir.
        public IActionResult Create()
        {
            return View();
        }

        // Deuxième méthode Create : c'est de la surcharge (overload), même nom mais
        // signature différente. L'attribut [HttpPost] indique qu'elle répond à l'envoi
        // du formulaire. Le paramètre est un ViewModel (données saisies par l'utilisateur).
        [HttpPost]
        public IActionResult Create(CarBrandCreateVM carBrandCreateVM)
        {
            // ModelState.IsValid vérifie les règles de validation déclarées dans le ViewModel
            // (par exemple [Required]). Si la saisie est invalide, on réaffiche le formulaire.
            if (!ModelState.IsValid)
                return View(carBrandCreateVM);

            // On convertit le ViewModel en entité métier CarBrand avant de l'enregistrer.
            // Le ViewModel et l'entité sont volontairement deux classes distinctes (séparation des rôles).
            _repo.Add(new CarBrand() { Name = carBrandCreateVM.Name });
            _logger.Log(LogLevel.Debug, carBrandCreateVM.Name + " created");

            // Après une création réussie, on redirige vers la liste (patron Post-Redirect-Get).
            return RedirectToAction("Index");
        }

    }
}
