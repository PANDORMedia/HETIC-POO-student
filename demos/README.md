# Démos de code

Les six projets ci-dessous ont été écrits en direct pendant le cours. Ils sont
volontairement simples et lisibles : chaque projet illustre un ou deux concepts
précis. Clone le dépôt, ouvre un dossier, lance-le, et lis le code en parallèle
de la leçon correspondante.

Tous les projets ciblent **.NET 8**. Pour les lancer :

```bash
cd demos/<nom-du-projet>
dotnet run        # applications console et web
```

Pour les projets web, `dotnet run` affiche une URL locale (par exemple
`http://localhost:5xxx`) à ouvrir dans le navigateur. Tu peux aussi utiliser
`dotnet watch` pour recharger automatiquement à chaque modification.

---

## Quel projet pour quel concept ?

### 1. [`gestionnaire-films`](gestionnaire-films) - console

Le fil rouge de la POO. Une classe de base **abstraite** `MediaItem` avec
validation dans les propriétés, trois sous-classes (`Movie`, `TVShow`,
`Documentary`) qui la spécialisent, une `Collection` qui stocke n'importe quel
média de façon **polymorphe**, et une méthode **générique** `FindByType<T>()`.
On y voit aussi les **interfaces** (`IExportable`, `IRateable`, `IStreamble`).

Concepts : héritage, classes abstraites, polymorphisme, interfaces, génériques, encapsulation.
Leçons : modules 1 et 2.

### 2. [`composition-logger`](composition-logger) - console

Comment brancher un comportement sans hériter de tout. Une interface `ILogger`,
deux implémentations (`ConsoleLogger`, `FileLogger`), et des services
(`UserService`, `OrderService`, `ReportService`) qui **reçoivent** leur logger
par le constructeur au lieu de le créer eux-mêmes. C'est la composition et
l'inversion de dépendance à la main, avant de les retrouver dans ASP.NET.

Concepts : composition vs héritage, programmation par interface, injection par constructeur.
Leçons : module 3 (composition, SOLID).

### 3. [`location-voiture`](location-voiture) - console

Une petite application complète : un menu en boucle, une hiérarchie de
véhicules (`Vehicule` puis `Car`, `Van`, `Motobike`), des **interfaces** de
capacité (`IServiceable`, `IAssurable`, `IExportable`), des **enums** (`Fuel`,
`MenuAction`) et un export **CSV**. Bon exemple de code organisé par
responsabilités (SOLID) sur un cas concret.

Concepts : héritage, interfaces, enums, séparation des responsabilités, export de données.
Leçons : modules 2 et 3.

### 4. [`aspnet-mvc`](aspnet-mvc) - web

La POO appliquée à une vraie application web : gérer un parc de voitures, leurs
marques et leurs modèles. Architecture **MVC**, **injection de dépendances**,
pattern **repository** (plusieurs implémentations interchangeables : SQLite via
Entity Framework Core, CSV, et une version factice), migrations EF.

Concepts : MVC, injection de dépendances, pattern repository, EF Core + SQLite.
Leçons : module 4.

### 5. [`minimal-api`](minimal-api) - web

Le point de départ pour découvrir ASP.NET Core sans les vues : une **API
minimale** qui expose un endpoint JSON. C'est le squelette à partir duquel on
construit, pour comprendre le pipeline HTTP et le routage.

Concepts : API minimale, endpoints, routage, sérialisation JSON.
Leçons : module 4.

### 6. [`minimal-api-2`](minimal-api-2) - web

Deuxième itération de l'API minimale : endpoints groupés (`MapGroup`), gestion
d'un identifiant dans l'URL, et réponses HTTP explicites (`Ok`, `NotFound`).

Concepts : groupes d'endpoints, paramètres de route, codes de réponse HTTP.
Leçons : module 4.

---

Chaque dossier contient son propre `README.md` avec le détail fichier par
fichier et ce que tu dois observer en lançant le projet.
