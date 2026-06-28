# Corrections J4 - Pour le formateur

Document à conserver privé. Corrections des 2 mini-exos, de l'exo principal 1, et guide de correction du projet fil rouge.

---

## Mini exo 1 - Anatomie MVC

| Snippet | Type | Comportement runtime |
|---------|------|----------------------|
| 1 | Model | Classe du domaine. Le runtime ne fait rien tant qu'elle n'est pas instanciée par un controller ou un service. |
| 2 | Controller | Action exécutée sur GET `/Voitures` ou `/Voitures/Index`. Construit une liste, l'envoie à la view `Views/Voitures/Index.cshtml`. |
| 3 | View | Rendue par le runtime quand un controller appelle `View(...)`. Génère du HTML à partir du modèle reçu. |
| 4 | Controller | Action POST. ModelBinding rempli `form` depuis les champs HTML. Validation, sinon redirection vers Index. |
| 5 | Model (plus précisément ViewModel) | Classe destinée au binding d'un form. Les DataAnnotations pilotent la validation côté serveur. |

### Notes pédagogiques

- Le snippet 5 est piégeux : techniquement c'est un Model, mais spécifiquement un ViewModel destiné au binding. C'est l'occasion de rappeler la distinction.
- Sur le snippet 2, certains étudiants vont chercher "où est définie la view ?". Réponse : convention. `View(...)` cherche `Views/Voitures/Index.cshtml`.
- Sur le snippet 4, insister sur le fait que `ModelBinding` fonctionne sans qu'on écrive une ligne pour mapper.

---

## Exercice principal 1 - Premier controller

### Models/Voiture.cs

```csharp
public class Voiture
{
    public int Id { get; set; }
    public string Marque { get; set; }
    public string Modele { get; set; }
    public decimal TarifJournee { get; set; }
    public int NbPlaces { get; set; }
    public string TypeCarburant { get; set; }
}
```

### Controllers/VoituresController.cs

```csharp
public class VoituresController : Controller
{
    private static readonly List<Voiture> _voitures = new()
    {
        new Voiture { Id = 1, Marque = "Renault", Modele = "Clio", TarifJournee = 45, NbPlaces = 5, TypeCarburant = "Essence" },
        new Voiture { Id = 2, Marque = "Peugeot", Modele = "208", TarifJournee = 42, NbPlaces = 5, TypeCarburant = "Diesel" },
        new Voiture { Id = 3, Marque = "Tesla", Modele = "Model 3", TarifJournee = 95, NbPlaces = 5, TypeCarburant = "Electrique" }
    };

    public IActionResult Index() => View(_voitures);

    public IActionResult Detail(int id)
    {
        var voiture = _voitures.FirstOrDefault(v => v.Id == id);
        if (voiture == null) return NotFound();
        return View(voiture);
    }
}
```

### Views/Voitures/Index.cshtml

```html
@model List<Voiture>

<h1>Catalogue</h1>
<ul>
@foreach (var v in Model)
{
    <li>
        <a asp-action="Detail" asp-route-id="@v.Id">
            @v.Marque @v.Modele
        </a>
        - @v.TarifJournee EUR/jour
    </li>
}
</ul>
```

### Views/Voitures/Detail.cshtml

```html
@model Voiture

<h1>@Model.Marque @Model.Modele</h1>
<p>Carburant : @Model.TarifJournee</p>
<p>Places : @Model.NbPlaces</p>
<p>Tarif : @Model.TarifJournee EUR/jour</p>

<a asp-action="Index">Retour à la liste</a>
```

### Bonus : RechercherParMarque

```csharp
public IActionResult RechercherParMarque(string marque)
{
    var resultats = string.IsNullOrWhiteSpace(marque)
        ? _voitures
        : _voitures.Where(v => v.Marque.Equals(marque, StringComparison.OrdinalIgnoreCase)).ToList();
    return View("Index", resultats);
}
```

### Pièges fréquents

- Étudiant qui oublie `@model List<Voiture>` en haut de la vue Index, puis `@foreach (var v in Model)` ne compile pas.
- Confusion entre la liste statique et instance par requête. Faire remarquer que `static` ici est volontairement simple.
- Oubli du `NotFound()` quand l'id n'existe pas, retourne `null` à la view, qui crashe.
- Utilisation de `Html.ActionLink` au lieu des tag helpers `asp-action`. Les deux marchent, préférer asp-* qui est la convention moderne.

---

## Mini exo 2 - Lifetimes DI

| # | Service | Lifetime | Pourquoi |
|---|---------|----------|----------|
| 1 | IVoitureRepository (JSON unique en mémoire) | Singleton | Le repository porte le cache du catalogue. Une seule instance pour toute l'app garantit la cohérence et évite de relire le fichier à chaque requête. |
| 2 | IEmailService (SMTP stateless) | Transient ou Singleton | Stateless donc les deux marchent. Transient est plus sûr s'il porte une connexion SMTP éphémère. |
| 3 | ICurrentUserService | Scoped | L'utilisateur est lié à la requête en cours. Pas partagé entre requêtes. |
| 4 | ITarifCalculator stateless | Transient | Léger et stateless. Une instance neuve à chaque injection est sans coût. |
| 5 | IDbContext style EF | Scoped | Le tracking des changements doit vivre le temps d'une requête HTTP et être partagé entre les services d'une même requête. |

### Notes pédagogiques

- Le piège classique est de mettre `IDbContext` en Singleton : multi-threading sur les mêmes entités, corruption garantie.
- Mettre `IVoitureRepository` en Scoped marche aussi mais on relit le fichier à chaque requête. Acceptable pour démarrer.
- Insister sur la règle générale : en cas de doute, Scoped.

---

## Projet fil rouge - Guide de correction

### Vérifications par phase

**Phase 1 (Setup)**
- `Domain/` séparé du reste. Aucune annotation web (`[Required]`, `[HttpPost]`) sur les classes du domaine.
- `dotnet run` lance la page d'accueil sans erreur.

**Phase 2 (Lecture)**
- `VoituresController` avec `Index()` et `Detail(int id)`.
- `NotFound()` quand l'id n'existe pas.
- Views Razor utilisent `asp-action`, `asp-route-id`.
- Navigation depuis la home vers `/Voitures`.

**Phase 3 (Écriture)**
- `VoitureFormVm` distinct du domaine, annoté avec DataAnnotations.
- Actions Create/Edit/Delete avec versions GET et POST.
- `ModelState.IsValid` testé avant traitement.
- Redirection après POST, pas de re-render direct (pattern POST-Redirect-GET).
- Messages d'erreur affichés via `asp-validation-for`.

**Phase 4 (DI + persistance)**
- `IVoitureRepository` et `IVoitureService` injectés via constructeur.
- Aucune liste statique dans `VoituresController`.
- `VoitureRepositoryJson` lit `App_Data/voitures.json` au démarrage, réécrit à chaque modification.
- Lifetimes correctement choisis : Singleton pour le repository, Scoped pour le service.
- Test du redémarrage : ajouter une voiture, redémarrer le serveur, voir qu'elle est toujours là.

**Phase 5 (Réservation)**
- `ReservationsController` avec actions Reserver GET et POST.
- `ITarifCalculator` injecté.
- `VehiculeIndisponibleException` catché dans l'action, page d'erreur lisible affichée.

### Pièges fréquents à corriger en plénière

- Étudiant qui annote ses classes du domaine avec `[Required]`. C'est du couplage web vers domaine. Les annotations vont sur les ViewModels uniquement.
- Controller qui appelle `File.WriteAllText` directement : la couche repository n'a pas servi.
- Persistance qui s'écrase à chaque démarrage parce que le repository part toujours d'une liste vide.
- Action POST qui retourne `View()` au lieu de `RedirectToAction()` après un succès. Provoque le warning du navigateur "voulez-vous renvoyer le formulaire ?" si l'utilisateur recharge.
- Sérialisation du `Vehicule` abstract sans configuration : `System.Text.Json` plante. Solution simple : sérialiser uniquement `Voiture` à cette étape, ou ajouter `JsonDerivedType` en .NET 7+.
- Pas de `using` autour du `FileStream` dans le repository : fuite de handle.

### Solutions de référence des points clés

**IVoitureRepository et VoitureRepositoryJson**

```csharp
public interface IVoitureRepository
{
    IEnumerable<Voiture> Lister();
    Voiture LirePar(int id);
    void Ajouter(Voiture voiture);
    void Mettre(int id, Voiture voiture);
    void Supprimer(int id);
}

public class VoitureRepositoryJson : IVoitureRepository
{
    private readonly string _path;
    private readonly List<Voiture> _voitures;

    public VoitureRepositoryJson(IWebHostEnvironment env)
    {
        var dataDir = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDir);
        _path = Path.Combine(dataDir, "voitures.json");

        if (File.Exists(_path))
        {
            var json = File.ReadAllText(_path);
            _voitures = JsonSerializer.Deserialize<List<Voiture>>(json) ?? new List<Voiture>();
        }
        else
        {
            _voitures = new List<Voiture>();
        }
    }

    public IEnumerable<Voiture> Lister() => _voitures;

    public Voiture LirePar(int id) => _voitures.FirstOrDefault(v => v.Id == id);

    public void Ajouter(Voiture voiture)
    {
        voiture.Id = _voitures.Count == 0 ? 1 : _voitures.Max(v => v.Id) + 1;
        _voitures.Add(voiture);
        Sauvegarder();
    }

    public void Mettre(int id, Voiture voiture)
    {
        var existante = LirePar(id);
        if (existante == null) return;
        var index = _voitures.IndexOf(existante);
        voiture.Id = id;
        _voitures[index] = voiture;
        Sauvegarder();
    }

    public void Supprimer(int id)
    {
        var existante = LirePar(id);
        if (existante == null) return;
        _voitures.Remove(existante);
        Sauvegarder();
    }

    private void Sauvegarder()
    {
        var json = JsonSerializer.Serialize(_voitures, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_path, json);
    }
}
```

**Program.cs**

```csharp
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IVoitureRepository, VoitureRepositoryJson>();
builder.Services.AddScoped<IVoitureService, VoitureService>();
builder.Services.AddTransient<ITarifCalculator, TarifStandard>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
```

**Action POST Create avec ModelState**

```csharp
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(VoitureFormVm form)
{
    if (!ModelState.IsValid)
        return View(form);

    var voiture = new Voiture
    {
        Marque = form.Marque,
        Modele = form.Modele,
        TarifJournee = form.TarifJournee,
        NbPlaces = form.NbPlaces,
        TypeCarburant = form.TypeCarburant
    };
    _service.Ajouter(voiture);
    return RedirectToAction(nameof(Index));
}
```

**Catch de l'exception métier dans ReservationsController**

```csharp
[HttpPost]
public IActionResult Reserver(ReservationFormVm form)
{
    if (!ModelState.IsValid)
        return View(form);

    try
    {
        var voiture = _service.LirePar(form.VoitureId);
        var reservation = new Reservation(form.Client, voiture, form.DateDebut, form.DateFin, _tarifCalculator);
        reservation.Confirmer();
        var montant = reservation.CalculerMontantTotal();
        return View("Confirmation", new ConfirmationVm { Reservation = reservation, Montant = montant });
    }
    catch (VehiculeIndisponibleException ex)
    {
        ModelState.AddModelError(string.Empty, ex.Message);
        return View(form);
    }
}
```

---

## Récap pédagogique J4

| Bloc | Exo | Concepts clés |
|------|-----|---------------|
| Intro ASP.NET | aucun | framework, anatomie projet, cycle HTTP |
| MVC | mini 1 + premier controller | routing, controller, view Razor, ViewModel, ModelBinding, validation |
| DI | mini 2 | service container, lifetimes, résolution en chaîne |
| Projet fil rouge | projet 5 phases | tout le bootcamp en une appli web |

### Bibliographie

- Documentation officielle ASP.NET Core sur learn.microsoft.com
- Pro ASP.NET Core MVC, Adam Freeman : référence complète
- Architecting Modern Web Applications with ASP.NET Core, Microsoft eBook gratuit
- Clean Architecture, Robert C. Martin : pour la couche Domain isolée
