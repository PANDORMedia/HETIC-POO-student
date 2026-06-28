# Location de véhicules

Cette démo construit, en classe, le moteur d'une petite agence de location de véhicules : un catalogue de voitures géré depuis le terminal et persisté dans un fichier CSV. Elle sert de fil rouge pour relier la plupart des notions de POO du cours sur un même exemple concret.

## Concepts illustres
- Classe abstraite : `Vehicule` modélise ce qui est commun à tous les véhicules et ne peut pas être instanciée directement.
- Héritage : `Car`, `Van` et `Motobike` dérivent de `Vehicule` et réutilisent ses propriétés et ses méthodes.
- Polymorphisme : la méthode abstraite `ShowDetails` et la méthode virtuelle `GetShortDescription` ont une version propre à chaque classe fille.
- Encapsulation : champs privés exposés via des propriétés dont les setters valident les données (marque non vide, tarif positif).
- Interfaces de capacité : `IServiceable`, `IAssurable` et `IExportable` décrivent des contrats que seules certaines classes implémentent.
- Enums : `Fuel` pour le type de carburant, `MenuAction` pour piloter le menu sans nombres magiques.
- Séparation des responsabilités : `Tools/CSVWriter` isole la lecture et l'écriture sur disque, à l'écart du métier et du menu.
- Gestion des exceptions : la boucle de saisie attrape les erreurs des setters pour ne jamais planter.

## Lancer la demo
```bash
cd demos/location-voiture
dotnet run
```
Au lancement, le programme recharge le catalogue depuis `cars.csv` puis affiche un menu en boucle. L'étudiant peut ajouter une voiture (saisie guidée champ par champ, avec re-demande en cas d'erreur), en supprimer une, lister le catalogue, puis quitter. Il faut observer que chaque ajout ou suppression réécrit aussitôt le fichier CSV, et qu'une saisie invalide affiche un message sans interrompre le programme.

## Visite guidee du code
- `Program.cs` : point d'entrée et boucle de menu console. Lit la saisie, aiguille via un `switch` sur l'enum `MenuAction`, et orchestre l'ajout, la suppression et l'affichage des voitures.
- `Models/Vehicule.cs` : la classe abstraite de base. Porte l'encapsulation (propriétés validées), une méthode concrète `GetPrice`, une méthode abstraite `ShowDetails` et une méthode virtuelle `GetShortDescription`.
- `Models/Car.cs` : classe fille qui hérite de `Vehicule` et implémente les trois interfaces à la fois. Cas le plus complet de la démo (carburant, assurance, révision, export CSV).
- `Models/Van.cs` : classe fille sans interface, pour montrer que l'héritage et les capacités sont des choix indépendants.
- `Models/Motobike.cs` : classe fille qui redéfinit `GetShortDescription` en réutilisant `base` pour enrichir le comportement du parent.
- `Interfaces/IServiceable.cs` : contrat de la capacité "être révisable" (compteur de km, révision).
- `Interfaces/IAssurable.cs` : contrat de la capacité "être assurable" (catégorie et calcul de prime).
- `Interfaces/IExportable.cs` : contrat minimal "savoir s'exporter en CSV".
- `Enums/Fuel.cs` : liste fermée et nommée des types de carburant, stockée sous forme d'entier dans le CSV.
- `Enums/MenuAction.cs` : actions du menu, pour rendre la boucle lisible sans nombres magiques.
- `Tools/CSVWriter.cs` : outil statique de persistance qui lit et écrit `cars.csv`, séparé du reste du code.

## A retenir
- Une classe abstraite définit un socle commun mais délègue l'implémentation des détails aux classes filles.
- L'héritage transmet automatiquement tout le parent, alors qu'une interface est une capacité que chaque classe choisit d'ajouter.
- Centraliser la validation dans les propriétés permet au menu de se contenter d'attraper les exceptions, sans dupliquer les règles.
- Isoler la persistance dans une classe dédiée (`CSVWriter`) garde le métier et l'interface indépendants du format de stockage.

## Lecons liees
- Encapsulation et Propriétés
- Constructeurs et initialisation
- Héritage
- Polymorphisme (virtual / override)
- Classes abstraites
- Interfaces : contrats et découplage
- SOLID : SRP et OCP
- Exceptions orientées objet
