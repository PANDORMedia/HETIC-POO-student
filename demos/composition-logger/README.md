# Composition et logger

Cette démo, écrite en direct pendant le module sur la composition et SOLID, montre comment brancher un comportement (le logging) dans plusieurs services sans tout faire descendre par héritage. Les services reçoivent leur logger au lieu de le créer, ce qui illustre l'inversion de dépendance à la main, avant de la retrouver automatisée dans ASP.NET.

## Concepts illustrés
- Programmation par interface : un contrat `ILogger` qui déclare `Log(string)` sans imposer de mise en oeuvre.
- Polymorphisme : deux implémentations interchangeables, `ConsoleLogger` et `FileLogger`, vues à travers le même type `ILogger`.
- Composition plutôt qu'héritage : un service "a-un" logger (relation "a-un") au lieu d'"être" un logger.
- Injection par constructeur : la dépendance entre dans l'objet au moment de sa création.
- Inversion de dépendance (le D de SOLID) : les services dépendent de l'abstraction `ILogger`, jamais d'une classe concrète.
- Encapsulation : le logger est gardé dans un champ privé, exposé seulement aux sous-classes via une propriété `protected`.

## Lancer la démo
```bash
cd demos/composition-logger
dotnet run
```
Le programme crée un `UserService` en lui injectant un `ConsoleLogger`, puis appelle `Inscrire`. L'étudiant doit observer dans le terminal une ligne horodatée du type `[HH:mm:ss] Inscription : sean@pandor.media`. En remplaçant `new ConsoleLogger()` par `new FileLogger()` dans `Program.cs`, la même exécution écrit désormais dans un fichier `log.txt` sans qu'aucun service n'ait changé : c'est tout l'intérêt de la démonstration.

## Visite guidée du code
- `ILogger.cs` : l'interface, c'est-à-dire le contrat. Elle déclare la méthode `Log` que tout logger devra fournir. C'est l'abstraction dont dépend le reste du code.
- `ConsoleLogger.cs` : une implémentation concrète du contrat qui affiche le message horodaté dans la console. Porte le polymorphisme.
- `FileLogger.cs` : une seconde implémentation du même contrat qui écrit dans un fichier. Montre que deux classes deviennent interchangeables dès qu'elles respectent la même interface.
- `Classes.cs` : `BaseService` reçoit un `ILogger` par constructeur et le stocke (injection, inversion de dépendance, encapsulation). `UserService`, `OrderService` et `ReportService` en héritent et l'utilisent dans leurs méthodes métier.
- `Program.cs` : le point de composition. C'est le seul endroit qui choisit l'implémentation concrète (`new ConsoleLogger()`) et l'injecte dans un service.

## À retenir
- Une classe qui dépend d'une interface (et non d'une classe concrète) reste souple : on peut changer l'implémentation sans la modifier.
- Recevoir une dépendance par le constructeur plutôt que la créer soi-même rend le code réutilisable et testable (on peut injecter un faux logger).
- La composition ("a-un") évite les hiérarchies d'héritage rigides quand on veut juste ajouter un comportement.
- Le choix de l'implémentation concrète se fait en un seul endroit, le point de composition, ici la méthode `Main`.

## Leçons liées
- Interfaces : contrats et découplage
- Composition vs Héritage
- SOLID : LSP, ISP et DIP
- Injection de dépendances
