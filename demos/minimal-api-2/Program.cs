// Cet espace de noms apporte les outils de sérialisation JSON utilisés tout en bas du fichier :
// l'attribut [JsonSerializable] et la classe de base JsonSerializerContext.
// La sérialisation, c'est transformer un objet C# (qui vit en mémoire) en texte JSON envoyé au client.
using System.Text.Json.Serialization;

// Point d'entrée du programme : ce fichier utilise les "top-level statements".
// Il n'y a ni classe Program ni méthode Main écrites à la main ; le compilateur les génère pour nous.
// CreateSlimBuilder est une fabrique (méthode statique) : on l'appelle sans créer d'objet au préalable
// et elle nous rend un objet builder déjà configuré. La version "Slim" est une variante allégée de CreateBuilder,
// pensée pour des APIs minimales rapides, compatibles avec la compilation native AOT (voir PublishAot dans le .csproj).
// "args" représente les arguments passés en ligne de commande au lancement.
var builder = WebApplication.CreateSlimBuilder(args);

// On configure ici la façon dont l'API transforme les objets en JSON.
// builder.Services est le conteneur d'injection de dépendances (l'annuaire des services de l'application).
// On insère notre AppJsonSerializerContext (déclaré en bas du fichier) en tête de la chaine de résolveurs.
// Pourquoi ? Ce contexte est généré à la compilation ("source-generated JSON") : le code de sérialisation
// est écrit pour nous au moment du build au lieu d'être déduit par réflexion à l'exécution. C'est plus rapide
// et indispensable pour la compilation native AOT, qui interdit la réflexion dynamique.
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// La méthode Build verrouille la configuration et nous rend l'objet "app" : l'application web prête à être configurée.
var app = builder.Build();

// Jeu de données en mémoire qui sert de fausse base de données pour la démo.
// Chaque élément est une instance du record Todo (déclaré plus bas) : on appelle son constructeur
// en passant les valeurs dans l'ordre des paramètres (Id, Title, puis DueBy optionnel).
// "new(...)" est la forme abrégée de "new Todo(...)" : le type est déduit de celui du tableau.
// Construire des objets à partir d'un même modèle (le record) illustre la relation entre une classe et ses instances.
var sampleTodos = new Todo[] {
    new(1, "Walk the dog"),
    new(2, "Do the dishes", DateOnly.FromDateTime(DateTime.Now)),
    new(3, "Do the laundry", DateOnly.FromDateTime(DateTime.Now.AddDays(1))),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car", DateOnly.FromDateTime(DateTime.Now.AddDays(2)))
};

// MapGroup crée un "groupe d'endpoints" : toutes les routes définies ensuite sur todosApi
// partageront le préfixe d'URL "/todos". On factorise ainsi la partie commune de l'adresse
// au lieu de la répéter sur chaque route. todosApi est un RouteGroupBuilder : un objet
// auquel on rattache les routes du groupe. C'est de l'organisation du code par regroupement logique
// (toutes les opérations sur les todos réunies au même endroit).
var todosApi = app.MapGroup("/todos");

// Premier endpoint du groupe : GET sur "/todos/" (le préfixe du groupe suivi de "/").
// Le verbe HTTP GET sert à consulter des données. La lambda "() => sampleTodos" ne prend aucun paramètre
// et renvoie tout le tableau ; l'objet renvoyé est automatiquement sérialisé en JSON pour le client.
todosApi.MapGet("/", () => sampleTodos);

// Deuxième endpoint : GET sur "/todos/{id}". La partie "{id}" est un "paramètre de route" :
// un trou dans l'URL dont la valeur change à chaque appel (par exemple /todos/1, puis /todos/2 ...).
// ASP.NET Core lit ce segment de l'URL et le "lie" (model binding) au paramètre "int id" de la lambda,
// en convertissant tout seul le texte de l'URL en entier.
todosApi.MapGet("/{id}", (int id) =>
    // On cherche le premier todo dont l'Id correspond. FirstOrDefault renvoie l'élément trouvé,
    // ou la valeur par défaut (null pour un record) si aucun ne correspond.
    // Le motif "is { } todo" teste "n'est pas null" et, si c'est vrai, range le résultat dans la variable todo.
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        // Cas trouvé : on renvoie une réponse HTTP 200 OK contenant le todo (sérialisé en JSON).
        ? Results.Ok(todo)
        // Cas absent : on renvoie une réponse HTTP 404 Not Found, sans corps.
        // Choisir explicitement le code de réponse fait partie du contrat de l'API : 200 = succès, 404 = ressource introuvable.
        : Results.NotFound());

// Démarre le serveur web et bloque ici tant que l'application tourne.
// C'est la dernière instruction exécutée : tout ce qui suit n'est que déclaration de types.
app.Run();

// Un "record" est un type pensé pour transporter des données immuables (qui ne changent pas après la création).
// Cette syntaxe compacte (le constructeur primaire entre parenthèses) déclare d'un seul coup :
//   - quatre propriétés en lecture seule : Id, Title, DueBy et IsComplete,
//   - un constructeur qui reçoit ces valeurs et les affecte,
//   - et gratuitement l'égalité par valeur, un ToString lisible, etc.
// "string?" et "DateOnly?" signifient que ces valeurs peuvent être absentes (null), grâce au mode Nullable du .csproj.
// "= null" et "= false" sont des valeurs par défaut : on peut omettre ces arguments à la construction (voir sampleTodos).
// C'est un exemple d'encapsulation : le record regroupe des données liées au sein d'un même objet cohérent.
// Ici, Todo joue le rôle de "modèle" : il représente une chose du domaine métier, une tâche à faire.
public record Todo(int Id, string? Title, DateOnly? DueBy = null, bool IsComplete = false);

// L'attribut [JsonSerializable] déclare quel type devra être sérialisé en JSON (ici un tableau de Todo).
// Couplé à une classe partielle qui hérite de JsonSerializerContext, il déclenche la génération de code
// à la compilation : le générateur écrit pour nous le code qui transforme Todo en JSON et inversement.
// "partial" signifie que la classe est complétée par ce code généré automatiquement dans un autre fichier.
// "internal" limite la visibilité de cette classe à l'intérieur du projet (elle n'a pas besoin d'être publique).
[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
