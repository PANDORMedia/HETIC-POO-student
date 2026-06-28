using Microsoft.AspNetCore.Mvc;

namespace aspnet.Controllers
{
    // Contrôleur minimal de démonstration. "Foo" et "Bar" sont des noms génériques
    // utilisés pour montrer un exemple sans logique réelle.
    public class FooController : Controller
    {
        // GET /Foo/Bar : une action ne fait souvent que renvoyer la vue associée.
        // La convention de routage relie cette action à la vue Views/Foo/Bar.cshtml.
        public ActionResult Bar()
        {
            return View();
        }

    }
}
