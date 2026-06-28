# Programmation Orientée Objet - C# (HETIC)

Dépôt étudiant du cours **Programmation Orientée Objet en C#**. Tu y trouves
tout le matériel utilisé en classe : les leçons en ligne, les démos de code
écrites en direct, et les exercices avec leurs corrigés.

> Le site de leçons : **https://pandormedia.github.io/HETIC-POO-student/**

---

## Ce qu'il y a dans ce dépôt

| Dossier | Contenu |
| --- | --- |
| [`docs/`](docs/) | Le site de leçons publié via GitHub Pages : 60 leçons (slides, lectures, quiz), réparties en 4 modules. |
| [`demos/`](demos/) | Les 6 projets C# montrés en classe, prêts à cloner, compiler et lancer. Chaque projet a son propre README. |
| [`exercises/`](exercises/) | Les énoncés des exercices (mini, main, projets fil rouge) et les corrigés. |

---

## Le parcours en 4 modules

1. **Environnement .NET, classes et encapsulation** : ton premier projet console, les classes, les objets, les constructeurs, les propriétés.
2. **Héritage, polymorphisme et interfaces** : réutiliser et spécialiser du code, les classes abstraites, les contrats d'interface.
3. **Composition, SOLID et exceptions** : composer plutôt qu'hériter, les cinq principes SOLID, la gestion d'erreurs orientée objet.
4. **ASP.NET Core MVC** : la POO appliquée à une vraie application web, avec injection de dépendances et accès aux données.

Commence par le site de leçons, puis ouvre la démo correspondante dans
[`demos/`](demos/) pour voir le concept tourner pour de vrai.

---

## Démarrer

Tu as besoin du **SDK .NET 8** ([téléchargement officiel](https://dotnet.microsoft.com/download/dotnet/8.0)).
Vérifie ton installation :

```bash
dotnet --version   # doit afficher 8.x
```

Cloner le dépôt et lancer une première démo :

```bash
git clone https://github.com/PANDORMedia/HETIC-POO-student.git
cd HETIC-POO-student/demos/gestionnaire-films
dotnet run
```

Chaque dossier de démo contient un `README.md` qui explique le concept
illustré, le rôle de chaque fichier, et la commande exacte pour le lancer.

---

## Les démos en un coup d'oeil

| Démo | Concept principal | Type |
| --- | --- | --- |
| [`gestionnaire-films`](demos/gestionnaire-films) | Héritage, polymorphisme, classes abstraites, interfaces, génériques | Console |
| [`composition-logger`](demos/composition-logger) | Composition et programmation par interface | Console |
| [`location-voiture`](demos/location-voiture) | Héritage, interfaces, enums, export CSV, SOLID | Console |
| [`aspnet-mvc`](demos/aspnet-mvc) | MVC, injection de dépendances, pattern repository, EF Core + SQLite | Web |
| [`minimal-api`](demos/minimal-api) | API minimale ASP.NET Core | Web |
| [`minimal-api-2`](demos/minimal-api-2) | API minimale, deuxième itération | Web |

---

Cours dispensé à HETIC. Questions : sean@pandor.media
