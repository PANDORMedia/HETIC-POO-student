using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using aspnet.Models;

namespace aspnet.Controllers;

// Contrôleur de la page d'accueil. C'est lui qui répond à l'URL racine "/"
// car la route par défaut pointe vers Home/Index (voir Program.cs).
public class HomeController : Controller
{
    // Le logger est injecté par le constructeur, comme dans les autres contrôleurs.
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    // Chaque action publique correspond à une page. View() rend la vue du même nom.
    public IActionResult Index() // Home/Index
    {
        return View();
    }

    public IActionResult Privacy() // Home/Privacy
    {
        return View();
    }

    public IActionResult Toto()
    {
        return View();
    }

    // Action d'erreur. L'attribut [ResponseCache] désactive la mise en cache de la réponse.
    // On passe au ViewModel un identifiant de requête utile pour le diagnostic.
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // L'opérateur ?? Fournit une valeur de repli si Activity.Current?.Id est null.
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
