# Projet fil rouge : la flotte de location sur le web

Tu reprends ton domaine de l'exercice console et tu construis une application web ASP.NET Core MVC autour. Tu réutilises tes classes sans les modifier. Tu ajoutes les couches Repository, Service, Controllers, Views.

## Phase 1 : Setup

Crée le projet :

```bash
dotnet new mvc -n LocationWeb
cd LocationWeb
```

Crée un dossier `Domain/` dans le projet et copie-y tes classes de l'exercice console :

- `Vehicule` (abstract)
- `Voiture`, `Moto`, `Camionnette`
- `IAssurable`, `IRevisable`
- `Reservation`, `ITarifCalculator`, `TarifStandard`, `TarifWeekend`
- `VehiculeIndisponibleException`

Vérifie que le projet compile et que `dotnet run` lance le site avec la page d'accueil par défaut.

## Phase 2 : Lecture

Tu affiches la flotte sans pouvoir la modifier.

1. Dans `Controllers/`, crée `VoituresController`. Pour cette phase, il porte une `List<Voiture>` statique en dur avec 3 ou 4 voitures.

2. Action `Index()` : retourne la liste. Vue Razor `Views/Voitures/Index.cshtml` qui affiche la liste avec un lien vers le détail de chaque voiture.

3. Action `Detail(int id)` : retourne une voiture précise ou `NotFound()`. Vue Razor `Views/Voitures/Detail.cshtml` qui affiche la fiche.

4. Ajoute un lien vers `/Voitures` dans le `_Layout.cshtml` (menu en haut de page).

## Phase 3 : Écriture

Tu ajoutes la création, modification et suppression de voitures via des formulaires.

1. Crée un `VoitureFormVm` dans `Models/` avec les champs nécessaires à la saisie, annotés avec DataAnnotations :

```csharp
public class VoitureFormVm
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Marque requise")]
    [StringLength(50)]
    public string Marque { get; set; }

    [Required]
    [StringLength(50)]
    public string Modele { get; set; }

    [Range(1, 9999)]
    public decimal TarifJournee { get; set; }

    [Range(1, 9)]
    public int NbPlaces { get; set; }

    [Required]
    public string TypeCarburant { get; set; }
}
```

2. Action `Create()` GET : retourne la vue avec un form vide. Action `Create(VoitureFormVm form)` POST : valide via `ModelState.IsValid`, sinon re-rend la vue avec les erreurs. Si ok, ajoute la voiture, redirige vers `Index`.

3. Action `Edit(int id)` GET : pré-remplit le form avec la voiture existante. Action `Edit(int id, VoitureFormVm form)` POST : valide, applique les changements, redirige.

4. Action `Delete(int id)` GET : affiche une page de confirmation. Action `Delete(int id, IFormCollection _)` POST : supprime, redirige vers `Index`.

5. Crée les vues correspondantes : `Create.cshtml`, `Edit.cshtml`, `Delete.cshtml`. Utilise `asp-for`, `asp-validation-for`, `asp-action`.

## Phase 4 : DI et persistance JSON

Tu sors la liste du controller et tu la mets derrière une interface.

1. Crée `Services/IVoitureRepository.cs` :

```csharp
public interface IVoitureRepository
{
    IEnumerable<Voiture> Lister();
    Voiture LirePar(int id);
    void Ajouter(Voiture voiture);
    void Mettre(int id, Voiture voiture);
    void Supprimer(int id);
}
```

2. Crée `Services/VoitureRepositoryJson.cs` qui implémente l'interface, lit et écrit dans `App_Data/voitures.json`. Au démarrage, charge le fichier si présent, sinon part d'une liste vide. À chaque ajout, modification ou suppression, réécrit le fichier.

   Utilise `System.Text.Json.JsonSerializer`. Le `Vehicule` étant abstract, sérialise uniquement la classe concrète (Voiture) à cette étape.

3. Crée `Services/IVoitureService.cs` et `VoitureService.cs`. Le service reçoit un `IVoitureRepository` dans son constructeur et expose les mêmes opérations, plus la logique métier propre à l'app (mapping ViewModel vers domaine, par exemple).

4. Dans `Program.cs`, enregistre les services :

```csharp
builder.Services.AddSingleton<IVoitureRepository, VoitureRepositoryJson>();
builder.Services.AddScoped<IVoitureService, VoitureService>();
```

5. `VoituresController` reçoit `IVoitureService` dans son constructeur et l'utilise. Plus aucune liste statique dans le controller.

6. Vérifie qu'un redémarrage du serveur conserve les voitures ajoutées (la persistance JSON fait son travail).

## Phase 5 : Réservation

Tu permets à un utilisateur de réserver une voiture sur une période, avec calcul du tarif via une stratégie injectée.

1. Crée un `ReservationFormVm` :

```csharp
public class ReservationFormVm
{
    [Required]
    public int VoitureId { get; set; }

    [Required, StringLength(100)]
    public string Client { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime DateDebut { get; set; }

    [Required, DataType(DataType.Date)]
    public DateTime DateFin { get; set; }
}
```

2. Crée `ReservationsController` avec deux actions :

   - `Reserver(int voitureId)` GET : affiche le form pré-rempli avec la voiture.
   - `Reserver(ReservationFormVm form)` POST : valide, construit une `Reservation`, calcule le montant total via un `ITarifCalculator` injecté, affiche une page de confirmation.

3. Enregistre `ITarifCalculator` dans `Program.cs`. Choisis `TarifStandard` par défaut. Le choix de la stratégie passe par la DI uniquement.

4. Si la voiture est indisponible quand on confirme, le `VehiculeIndisponibleException` remonte. Catche-le dans l'action et renvoie une page d'erreur lisible.

## Bonus

- Ajoute un système de pagination simple sur l'index (10 voitures par page).
- Ajoute une recherche par marque et modèle (formulaire en haut de l'index).
- Étends la persistance aux Moto et Camionnette en gérant le polymorphisme JSON (champ `Type` dans le JSON, désérialisation custom).
- Ajoute une seconde implémentation `TarifWeekend` et propose le choix de la stratégie dans le form de réservation.
- Stylise avec Bootstrap (déjà inclus dans le template MVC par défaut).

## Critères d'évaluation

### Fonctionnel

- L'application tourne sur localhost
- Les phases 1 à 4 sont atteintes au minimum
- CRUD complet sur Voiture fonctionne via formulaires
- La persistance JSON survit au redémarrage du serveur

### Architecture

- Le dossier `Domain/` est réutilisé tel quel, sans annotation web ni dépendance ASP.NET
- `IVoitureRepository` et `IVoitureService` sont injectés via leurs interfaces
- Aucune logique métier ni accès direct au stockage dans les controllers
- Validation côté serveur via DataAnnotations et `ModelState.IsValid`
- Au moins un ViewModel distinct des classes du domaine

### Code

- Pas d'exception générique lancée ou attrapée
- Pas de catch silencieux
- Champs privés, propriétés publiques, encapsulation respectée
