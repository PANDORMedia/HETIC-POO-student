# Application web ASP.NET Core MVC (location de voitures)

Cette démo applique la POO à une vraie application web : gérer un parc de voitures avec leurs marques et leurs modèles. Elle a été construite en classe pour relier l'architecture MVC, l'injection de dépendances et le pattern repository sur un même exemple concret, en stockant les données dans SQLite via Entity Framework Core.

## Concepts illustrés
- Architecture MVC : les contrôleurs (`Controllers/`) reçoivent les requêtes, les entités (`Models/`) portent les données, les vues (`Views/`) affichent le résultat.
- Injection de dépendances : les services sont enregistrés une fois dans `Program.cs`, puis injectés automatiquement dans le constructeur de chaque contrôleur.
- Programmation contre une interface : les contrôleurs dépendent des abstractions (`ICarRepository`, `ICarBrandRepository`, `ICarModelRepository`), jamais des classes concrètes.
- Pattern repository avec implémentations interchangeables : `CarSqlLiteRepository`, `CarCSVRepository` et `CarDummyRepository` respectent le même contrat et sont substituables sans toucher aux contrôleurs.
- EF Core et SQLite : `AppDbContext` mappe les entités C# sur des tables, avec clés étrangères et propriétés de navigation.
- ViewModels : `CarCreateVM`, `CarModelCreateVM` et `CarBrandCreateVM` séparent les données du formulaire des entités métier et portent la validation (`[Required]`, `[Range]`).
- Héritage et capacités : `ExportableCar` hérite de `Car` tout en implémentant l'interface `IExportable`.
- Énumérations : `Fuel` restreint le type de carburant à un ensemble fermé de valeurs nommées.

## Lancer la démo
```bash
cd demos/aspnet-mvc
dotnet run
```
Au démarrage, le serveur web s'allume et affiche une URL locale (par exemple `http://localhost:5xxx`) à ouvrir dans le navigateur. La base SQLite `rental.db` est créée à partir des migrations EF Core. L'étudiant doit observer le parcours complet : créer une marque, puis un modèle rattaché à cette marque, puis une voiture rattachée à ce modèle, et voir les listes se mettre à jour à chaque ajout.

## Visite guidée du code
- `Program.cs` : point d'entrée. Enregistre les services (MVC, le `AppDbContext` SQLite et les trois repositories) dans le conteneur d'injection, puis configure le pipeline HTTP et le routage. C'est ici qu'on choisit quelle implémentation concrète répond à chaque interface.
- `Controllers/HomeController.cs` : le contrôleur de la page d'accueil, branché sur la route par défaut. Montre la structure minimale d'un contrôleur (actions, `View()`, page d'erreur).
- `Controllers/BrandController.cs` : CRUD des marques. Illustre l'injection par constructeur, la surcharge de l'action `Create` (GET et POST) et la conversion ViewModel vers entité.
- `Controllers/ModelController.cs` : CRUD des modèles. Dépend de deux repositories à la fois et alimente une liste déroulante des marques via `ViewBag`.
- `Controllers/CarController.cs` : CRUD des voitures. Combine deux abstractions et construit la liste déroulante des modèles avec LINQ et les propriétés de navigation.
- `Controllers/FooController.cs` : contrôleur minimal de démonstration, sans logique réelle.
- `Models/Car.cs` : entité voiture. Montre les clés étrangères, les propriétés de navigation et des propriétés calculées en lecture seule (`Brand`, `Model`).
- `Models/CarModel.cs` : entité modèle, au centre de la relation marque vers voiture (relations un-vers-plusieurs dans les deux sens).
- `Models/CarBrand.cs` : entité marque, qui possède une liste de modèles.
- `Models/ErrorViewModel.cs` : petit ViewModel de la page d'erreur, avec une propriété calculée à corps d'expression.
- `Models/ViewModels/CarCreateVM.cs`, `CarModelCreateVM.cs`, `CarBrandCreateVM.cs` : objets de transport entre formulaire et contrôleur, porteurs des règles de validation par attributs.
- `Models/Exportables/ExportableCar.cs` : sous-classe de `Car` qui implémente `IExportable`. Exemple d'héritage combiné à l'implémentation d'interface et de méthode statique.
- `Interfaces/ICarRepository.cs`, `ICarBrandRepository.cs`, `ICarModelRepository.cs` : les contrats des dépôts. C'est l'abstraction sur laquelle reposent l'injection de dépendances et l'interchangeabilité.
- `Interfaces/IExportable.cs` : interface de capacité à une seule méthode (`ExportAsCSV`).
- `Data/CarSqlliteRepository.cs`, `CarModelSqliteRepository.cs`, `CarBrandSqlliteRepository.cs` : implémentations SQLite qui délèguent le travail à `AppDbContext` (`Include` pour charger les objets liés).
- `Data/CarCSVRepository.cs` : implémentation alternative qui lit et écrit un fichier CSV au lieu d'une base, pour montrer qu'on change de stockage sans changer les contrôleurs.
- `Data/CarDummyRepository.cs` : implémentation factice en mémoire, pratique pour tester rapidement.
- `Data/Context/AppDbContext.cs` : le contexte EF Core. Héritage de `DbContext`, déclaration des `DbSet`, redéfinition de `OnModelCreating` pour nommer les tables.
- `Enums/Fuel.cs` : énumération des types de carburant.
- `Tools/CSVWriter.cs` : classe utilitaire statique qui isole la lecture et l'écriture de fichiers CSV, à l'écart du métier.

## À retenir
- Dépendre d'une interface plutôt que d'une classe concrète rend le code interchangeable : changer une seule ligne dans `Program.cs` suffit pour passer de SQLite à CSV ou à une version factice.
- L'injection de dépendances par constructeur, configurée dans `Program.cs`, est de la composition : le contrôleur reçoit ses outils au lieu de les créer lui-même.
- Un ViewModel n'est pas une entité : il n'expose que les champs saisis par l'utilisateur et porte la validation, ce qui protège les entités métier.
- Avec EF Core, les propriétés de navigation relient les objets entre eux, mais il faut souvent les charger explicitement avec `Include`.

## Leçons liées
- Intro ASP.NET Core MVC
- ASP.NET Core MVC
- Models & Controllers
- Injection de dépendances
- Interfaces : contrats & découplage
- Composition vs Héritage
