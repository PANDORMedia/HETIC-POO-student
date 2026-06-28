# Location de véhicules

Une agence de location veut un outil pour gérer son catalogue de véhicules. Tu en construis le moteur, puis l'interface en ligne de commande. Le catalogue est persisté dans un fichier CSV sur disque.

## Partie 1

Crée une classe Vehicule avec :
- Marque, Modele : string
- TarifJournee : decimal
- Disponible : bool. Vrai à l'instanciation.

Les champs sont privés. L'accès passe par des propriétés. Le setter de Marque rejette une chaîne vide. Le setter de TarifJournee rejette zéro ou négatif. Lance ArgumentException avec un message clair.

Le constructeur prend marque, modele, tarif.

Méthode AfficherFiche qui imprime une fiche lisible sur trois lignes.

Dans Program.cs, instancie deux véhicules, modifie leurs propriétés, déclenche volontairement une validation.

## Partie 2

Vehicule devient abstract. AfficherFiche aussi.

Trois classes filles :
- Voiture ajoute NbPlaces, TypeCarburant
- Moto ajoute Cylindree
- Camionnette ajoute VolumeCoffreM3

Chaque fille a son constructeur qui appelle base. Chaque fille implémente AfficherFiche à sa manière.

`new Vehicule(...)` ne doit plus compiler.

## Partie 3

Dans Program.cs, déclare une List<Vehicule> contenant une instance de chaque type. Parcours-la avec foreach et appelle AfficherFiche.

Ajoute CalculerCout sur Vehicule. Méthode concrète qui prend un nombre de jours et retourne TarifJournee multiplié par ce nombre. La règle est la même pour tous, pas besoin de la rendre virtuelle.

Ajoute DescriptionCourte en virtual sur Vehicule. Renvoie "Marque Modele". Override dans Moto pour intercaler la cylindrée.

## Partie 4

Deux interfaces :

```
interface IAssurable
{
    decimal CalculerPrimeAssurance();
    string CategorieAssurance { get; }
}

interface IRevisable
{
    void EffectuerRevision();
    int KmDepuisDerniereRevision { get; }
    void AjouterKilometres(int km);
}
```

Voiture et Moto implémentent IAssurable. Prime de la voiture : TarifJournee * 30, catégorie "VL". Prime de la moto : TarifJournee * 40, catégorie "2RM".

Les trois véhicules implémentent IRevisable. EffectuerRevision remet le compteur à zéro. AjouterKilometres incrémente.

Dans Program.cs, écris deux méthodes statiques : AfficherKilometrage qui prend un IRevisable, AfficherPrime qui prend un IAssurable. Parcours ta liste de véhicules et appelle la méthode adaptée à ce que chacun implémente. La Camionnette ne passera pas à AfficherPrime, c'est attendu.

## Partie 5

L'agent travaille depuis un terminal. Boucle un menu : lister, ajouter une voiture, supprimer une voiture, quitter.

Ajouter demande marque, modèle, tarif, nombre de places, type de carburant via Console.ReadLine. Construit la Voiture et l'ajoute au catalogue.

Supprimer affiche le catalogue numéroté, lit un index, retire l'entrée.

Le catalogue est persisté dans voitures.csv à la racine du projet. Une ligne par voiture, champs séparés par des points-virgules. Au démarrage, charge le fichier s'il existe. À chaque ajout ou suppression, réécris-le.

Pour les décimaux, utilise CultureInfo.InvariantCulture en lecture et en écriture.

Si une valeur saisie est invalide, ton setter lance une exception. Attrape-la dans le menu, affiche le message, repars sur le menu sans planter.

## Partie 6 : Bonus

Classe Reservation :
- Client : string
- Vehicule : référence
- DateDebut, DateFin : DateTime
- Confirmer marque le véhicule indisponible. Si le véhicule l'était déjà, lance VehiculeIndisponibleException avec un message parlant.
- CalculerMontantTotal utilise la durée et le tarif.

VehiculeIndisponibleException hérite de Exception et accepte un message au constructeur.

```
interface ITarifCalculator
{
    decimal Calculer(Vehicule v, int nbJours);
}
```

Deux implémentations :
- TarifStandard renvoie v.CalculerCout(nbJours).
- TarifWeekend applique 1.5x si la période inclut un samedi ou un dimanche.

Le constructeur de Reservation accepte un ITarifCalculator. CalculerMontantTotal délègue à la stratégie injectée. Reservation ne connaît aucune classe concrète de calcul.

Dans Program.cs, crée deux réservations avec les deux stratégies. Affiche les montants. Confirme deux fois la même réservation pour voir ton exception remonter.
