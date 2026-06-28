# API minimale ASP.NET Core (itération 2)

Deuxième version de l'API minimale écrite en classe : on part du squelette de la première itération et on ajoute des endpoints regroupés, un identifiant lu dans l'URL et des réponses HTTP choisies explicitement. Le but est de montrer comment une API répond différemment selon que la ressource demandée existe ou non.

## Concepts illustrés
- Groupes d'endpoints avec `MapGroup` pour factoriser le préfixe d'URL commun (`/todos`).
- Paramètre de route `{id}` lié automatiquement au paramètre `int id` de la méthode (model binding).
- Réponses HTTP explicites avec `Results.Ok` (200) et `Results.NotFound` (404).
- Le record `Todo` comme modèle de données immuable (constructeur primaire, propriétés en lecture seule, valeurs par défaut).
- Sérialisation JSON générée à la compilation via `AppJsonSerializerContext` (source-generated JSON, compatible AOT).

## Lancer la démo
```bash
cd demos/minimal-api-2
dotnet run
```
Au lancement, `dotnet run` affiche une URL locale (par exemple `http://localhost:5209`). Ouvre `/todos/` pour recevoir la liste complète des tâches en JSON, puis `/todos/1` pour récupérer une seule tâche. L'étudiant doit observer la différence entre `/todos/1` (qui existe et renvoie 200 avec un objet) et `/todos/99` (qui n'existe pas et renvoie un statut 404 sans corps).

## Visite guidée du code
- `Program.cs` : tout le programme tient dans ce fichier. Il construit l'application, déclare le groupe d'endpoints `/todos`, expose deux routes GET (liste complète et recherche par identifiant), définit le record `Todo` (le modèle) et le contexte de sérialisation JSON. C'est lui qui porte les concepts de routage, de groupe d'endpoints et de codes de réponse HTTP.

## À retenir
- `MapGroup` regroupe plusieurs routes sous un même préfixe d'URL : on écrit le chemin commun une seule fois.
- Un paramètre de route `{id}` est converti et injecté automatiquement dans la méthode : pas besoin de lire l'URL à la main.
- Une API ne renvoie pas qu'un contenu, elle renvoie aussi un code de statut : 200 pour un succès, 404 quand la ressource est introuvable.
- Le record `Todo` montre qu'un objet peut servir de simple porteur de données immuables, sans logique compliquée.

## Leçons liées
- Module 4 : ASP.NET Core MVC, OOP appliquée, leçons "Intro ASP.NET Core MVC" et "ASP.NET Core MVC".
- Module 4 : leçon "Models & Controllers OOP" (le record `Todo` joue le rôle de modèle).
