// ÉTAPE 1 : on crée le "builder" (le constructeur de l'application).
// Ce fichier utilise les "top-level statements" : il n'y a ni classe Program ni méthode Main visibles,
// le compilateur les génère pour nous. Le code que l'on écrit ici est donc le véritable point d'entrée du programme.
// WebApplication.CreateBuilder est une méthode statique (une fabrique) : on l'appelle sans créer d'objet au préalable,
// et elle nous rend un objet builder déjà configuré (lecture de appsettings.json, variables d'environnement, journalisation...).
// "args" représente les arguments passés en ligne de commande au lancement.
var builder = WebApplication.CreateBuilder(args);

// ÉTAPE 2 : on enregistre des "services" dans le conteneur d'injection de dépendances.
// Le conteneur est un annuaire d'objets : on lui déclare ici les outils dont l'application aura besoin,
// et il saura les fournir (les "injecter") plus tard à qui les demande. C'est le principe d'inversion de contrôle.
// builder.Services est cet annuaire ; AddEndpointsApiExplorer et AddSwaggerGen y ajoutent de quoi documenter l'API.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ÉTAPE 3 : on demande au builder de construire l'application finale.
// La méthode Build verrouille la configuration et nous rend l'objet "app" : c'est l'application web prête à être configurée.
// On ne touche plus aux services après ce point ; on passe maintenant à la configuration du pipeline.
var app = builder.Build();

// ÉTAPE 4 : on configure le "pipeline HTTP", c'est-à-dire la chaine des étapes que va traverser chaque requête.
// Chaque app.UseXxx ajoute un "middleware" : un maillon qui inspecte ou transforme la requête avant de passer au suivant.
// L'ordre des appels compte : les requêtes traversent les middlewares dans l'ordre où ils sont déclarés ici.

// On n'active Swagger (la page web qui documente et teste l'API) que lorsque l'on développe.
// app.Environment expose l'environnement courant ; IsDevelopment renvoie vrai si ASPNETCORE_ENVIRONMENT vaut "Development".
// Cela évite d'exposer cette documentation en production.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware qui redirige automatiquement les requêtes HTTP vers HTTPS (la version sécurisée du protocole).
app.UseHttpsRedirection();

// Un tableau de chaines de caractères : les libellés météo possibles que l'on tirera au hasard.
// "new[]" laisse le compilateur déduire tout seul le type du tableau (ici string[]) à partir des valeurs fournies.
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// ÉTAPE 5 : on déclare un "endpoint", c'est-à-dire une route que l'API sait gérer.
// app.MapGet associe l'URL "/weatherforecast" avec le verbe HTTP GET (la consultation de données).
// Le deuxième argument est une fonction lambda "() => { ... }" : du code anonyme exécuté à chaque appel de cette route.
// Ici la lambda ne prend aucun paramètre et renvoie la liste des prévisions.
app.MapGet("/weatherforecast", () =>
{
    // Enumerable.Range(1, 5) génère les nombres 1, 2, 3, 4, 5.
    // .Select(index => ...) est une opération LINQ : elle transforme chaque nombre en un objet WeatherForecast.
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        // On instancie ici un objet WeatherForecast en passant ses trois valeurs au constructeur (positionnées dans l'ordre).
        // C'est de la construction d'objet : à partir du modèle (le record), on crée une instance concrète remplie de données.
        new WeatherForecast
        (
            // La date du jour plus "index" jours, convertie en DateOnly (une date sans heure).
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            // Une température aléatoire entre -20 (inclus) et 55 (exclu).
            Random.Shared.Next(-20, 55),
            // Un libellé tiré au hasard dans le tableau summaries.
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        // .ToArray() matérialise la séquence LINQ en un vrai tableau d'objets WeatherForecast.
        .ToArray();
    // L'objet renvoyé sera automatiquement converti (sérialisé) en JSON pour être envoyé au client.
    return forecast;
})
// .WithName donne un nom interne à la route, utile pour la documentation et pour générer des liens.
.WithName("GetWeatherForecast")
// .WithOpenApi déclare cette route dans le descriptif OpenAPI/Swagger.
.WithOpenApi();

// ÉTAPE 6 : on démarre l'application. app.Run lance le serveur web et bloque ici tant que l'application tourne.
// C'est la dernière instruction exécutée : tout ce qui suit dans le fichier ne sont que des déclarations de types.
app.Run();

// Un "record" est un type de référence particulier, pensé pour transporter des données immuables (qui ne changent pas).
// Cette syntaxe compacte (le "constructeur primaire" entre parenthèses) déclare à la fois :
//   - trois propriétés en lecture seule : Date, TemperatureC et Summary,
//   - un constructeur qui reçoit ces trois valeurs et les affecte,
//   - et gratuitement l'égalité par valeur, un ToString lisible, etc.
// string? signifie que Summary peut valoir null (le "?" autorise l'absence de valeur), grâce au mode Nullable activé dans le .csproj.
// C'est un exemple d'encapsulation : le record regroupe des données liées au sein d'un même objet cohérent.
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    // Propriété calculée : sa valeur n'est pas stockée, elle est recalculée à chaque lecture à partir de TemperatureC.
    // La flèche "=>" définit un corps d'expression (équivaut à un get { return ...; } en plus court).
    // Ici on convertit les degrés Celsius en Fahrenheit ; aucune donnée supplémentaire n'est conservée en mémoire.
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
