# API minimale ASP.NET Core

Cette démo montre comment exposer une petite API web avec l'approche "minimal API" d'ASP.NET Core, sans contrôleurs ni structure lourde. Elle a été écrite en classe pour découvrir, en partant du modèle de projet par défaut, comment une requête HTTP devient un objet C# sérialisé en JSON.

## Concepts illustrés
- Le record `WeatherForecast` : un type immuable créé avec un constructeur primaire (données regroupées, égalité par valeur).
- La propriété calculée `TemperatureF` : une valeur dérivée, recalculée à la lecture et jamais stockée.
- L'encapsulation de données liées au sein d'un même objet cohérent.
- La construction d'objets (instanciation via `new`) pour remplir un modèle avec des données.
- L'injection de dépendances : enregistrer des services dans un conteneur (`builder.Services`).
- Le type nullable `string?` (mode `Nullable` activé) pour autoriser une valeur absente.

## Lancer la démo
```bash
cd demos/minimal-api
dotnet run
```
Au démarrage, le serveur web s'allume et affiche une URL locale (par exemple `http://localhost:5184`). En environnement de développement, la page Swagger s'ouvre : l'étudiant peut y déclencher la route `GET /weatherforecast` et observer la réponse, un tableau JSON de cinq prévisions météo avec des dates, des températures et des libellés tirés au hasard.

## Visite guidée du code
- `Program.cs` : tout le programme tient dans ce seul fichier grâce aux "top-level statements". Il enchaine les six étapes du cycle de vie : création du builder, enregistrement des services, construction de l'application, configuration du pipeline HTTP (les middlewares), déclaration de l'endpoint `MapGet`, puis démarrage du serveur. Il déclare aussi le record `WeatherForecast` et sa propriété calculée `TemperatureF`.

## À retenir
- Une minimal API se lit de haut en bas comme une recette : on configure d'abord (`builder`, services), on construit (`Build`), puis on branche les middlewares et les routes avant `app.Run`.
- L'ordre des middlewares dans le pipeline compte : chaque requête les traverse dans l'ordre où ils sont déclarés.
- Un record est idéal pour transporter des données immuables ; une propriété calculée expose une valeur dérivée sans la dupliquer en mémoire.
- L'objet renvoyé par un endpoint est automatiquement sérialisé en JSON pour le client.

## Leçons liées
- Encapsulation & Propriétés
- Injection de dépendances
- Intro ASP.NET Core MVC
- ASP.NET Core MVC
