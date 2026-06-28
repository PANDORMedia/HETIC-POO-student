# Gestionnaire de films

Cette démo, écrite en direct pendant le module sur l'héritage, le polymorphisme et les interfaces, modélise un petit catalogue de médias (films, séries et documentaires). Elle sert de fil conducteur pour voir tous les piliers de la POO travailler ensemble dans un même programme console.

## Concepts illustrés
- Classe abstraite : `MediaItem` pose le modèle commun (titre, année) et impose une méthode `Show()` sans pouvoir être instanciée directement.
- Héritage : `Movie`, `TVShow` et `Documentary` héritent de `MediaItem` et réutilisent ce qui est commun en ajoutant leurs propres données.
- Polymorphisme : chaque classe fille redéfinit `Show()` avec `override`, et le test `media is Movie` interroge le type réel d'un objet à l'exécution.
- Encapsulation : les champs sont privés et exposés via des propriétés dont le `set` valide la donnée (titre non vide, année réaliste, note ramenée entre 0 et 5).
- Interfaces : `IExportable`, `IRateable` et `IStreamable` sont des contrats que `Movie` signe en plus de son héritage (une classe peut implémenter plusieurs interfaces).
- Génériques : `Collection.FindByType<T>()` filtre le catalogue par type avec une seule méthode réutilisable, encadrée par la contrainte `where T : MediaItem`.

## Lancer la demo
```bash
cd demos/gestionnaire-films
dotnet run
```
Au lancement, le programme crée plusieurs films, une série et des documentaires, puis les affiche un par un via `Show()`. L'étudiant doit observer dans le terminal que chaque type s'affiche dans son propre format, que la boucle préfixe chaque ligne par `Film :`, `Série :` ou `Documentaire :`, et enfin les statistiques du catalogue suivies de l'export d'un film en CSV et en JSON.

## Visite guidée du code
- `MediaItem.cs` : la classe abstraite de base. Elle porte l'encapsulation (propriétés validées) et déclare la méthode abstraite `Show()` qui rend le polymorphisme possible.
- `Movie.cs` : la classe la plus riche. Elle hérite de `MediaItem` ET implémente trois interfaces (streaming, notation, export) : l'exemple central pour montrer héritage et interfaces réunis.
- `TVShow.cs` : une série, deuxième classe fille. Elle ajoute le nombre de saisons et fournit sa propre version de `Show()`.
- `Documentary.cs` : un documentaire, troisième classe fille. Elle illustre en plus une propriété calculée en lecture seule (`IsBiography`).
- `Collection.cs` : le catalogue polymorphe. Une `List<MediaItem>` qui mélange les trois types, avec la méthode générique `FindByType<T>()`.
- `MovieCollection.cs` : une collection spécialisée pour les films seuls, à comparer avec la collection générique précédente.
- `User.cs` : un utilisateur et ses films préférés rangés dans un tableau de taille fixe. Montre volontairement des champs publics, en contraste avec l'encapsulation de `MediaItem`.
- `Interfaces/IExportable.cs` : le contrat d'export (CSV et JSON) avec une méthode statique par défaut.
- `Interfaces/IRateable.cs` : le contrat de notation (note et commentaire).
- `Interfaces/IStreamble.cs` : le contrat de diffusion en streaming (plateforme et lien de lecture).
- `Program.cs` : le point d'entrée. Il assemble tous les objets et fait tourner chaque concept à la suite.

## A retenir
- Une classe abstraite définit un socle commun et oblige ses filles à compléter les méthodes manquantes ; on ne peut pas l'instancier seule.
- Le polymorphisme permet de manipuler des objets de types variés à travers leur type parent (`List<MediaItem>`) tout en exécutant le bon comportement pour chacun.
- L'héritage relie une classe à UNE seule classe mère, alors qu'on peut implémenter PLUSIEURS interfaces : ce sont deux outils complémentaires.
- L'encapsulation protège les données : la validation vit dans le `set` des propriétés, donc un objet ne peut pas exister dans un état invalide.
- Une méthode générique (`FindByType<T>()`) évite de dupliquer le même code pour chaque type.

## Lecons liees
- Classes abstraites
- Héritage
- Polymorphisme (virtual / override)
- Interfaces : contrats et découplage
