// Les directives using importent des espaces de noms (namespaces) : on rend
// ainsi visibles les classes définies ailleurs (nos repositories, nos interfaces, EF Core).
using aspnet.Data;
using aspnet.Interfaces;
using Microsoft.EntityFrameworkCore;

// Point d'entrée de l'application. Le "builder" sert à configurer l'application
// avant de la démarrer : on y enregistre les services et les options.
var builder = WebApplication.CreateBuilder(args);

// On active le patron MVC (Modèle - Vue - Contrôleur) : les contrôleurs et les vues.
builder.Services.AddControllersWithViews();

// Enregistrement du contexte de base de données EF Core (notre AppDbContext)
// en lui indiquant d'utiliser SQLite et la chaîne de connexion "Default" de la configuration.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Coeur de l'injection de dépendances.
// On dit au conteneur : "chaque fois qu'une classe réclame l'interface ICarRepository,
// fournis-lui une instance de CarSqlLiteRepository". Le code qui consomme l'abstraction
// (l'interface) ignore totalement l'implémentation concrète : c'est l'inversion de dépendance.
// Pour passer du stockage SQLite à un autre stockage (CSV, factice), il suffirait de
// changer la classe à droite ici, sans toucher aux contrôleurs. C'est le polymorphisme en action.
// AddScoped = durée de vie "Scoped" : une seule instance par requête HTTP entrante.
builder.Services.AddScoped<ICarRepository, CarSqlLiteRepository>();
builder.Services.AddScoped<ICarBrandRepository, CarBrandSqlLiteRepository>();
builder.Services.AddScoped<ICarModelRepository, CarModelSqliteRepository>();

// Build() construit l'application finale à partir de toute la configuration ci-dessus.
var app = builder.Build();

// Configuration du "pipeline" : la suite ordonnée d'étapes (middlewares) que
// traverse chaque requête HTTP. L'ordre des appels ci-dessous a de l'importance.
// En dehors du mode développement, on active une page d'erreur générique et HSTS.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Redirige automatiquement les requêtes HTTP vers HTTPS.
app.UseHttpsRedirection();
// Autorise le service des fichiers statiques (CSS, JS, images du dossier wwwroot).
app.UseStaticFiles();

// Active le routage : associer une URL entrante à un contrôleur et une action.
app.UseRouting();

// Étape d'autorisation (vérification des droits d'accès).
app.UseAuthorization();

// Définit la convention d'URL par défaut : /Contrôleur/Action/Id.
// Sans précision, on tombe sur HomeController.Index (le {id?} est optionnel).
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Démarre le serveur web et bloque ici tant que l'application tourne.
app.Run();
