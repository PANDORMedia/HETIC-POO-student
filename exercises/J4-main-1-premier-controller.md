# Exercice principal 1 : Premier controller

## Objectif

Valider le cycle MVC bout en bout sur un premier projet ASP.NET Core MVC. Pas encore de DI, pas encore de persistance. Juste Controllers + Views.

## Énoncé

### Étapes

1. Crée le projet :

```bash
dotnet new mvc -n LocationWeb
cd LocationWeb
dotnet run
```

Ouvre `https://localhost:5001`. Tu vois la page d'accueil par défaut.

2. Dans `Models/`, crée `Voiture.cs` :

```csharp
public class Voiture
{
    public int Id { get; set; }
    public string Marque { get; set; }
    public string Modele { get; set; }
    public decimal TarifJournee { get; set; }
    public int NbPlaces { get; set; }
    public string TypeCarburant { get; set; }
}
```

3. Dans `Controllers/`, crée `VoituresController.cs` avec une liste statique en mémoire et deux actions :

   - `Index()` retourne la liste complète
   - `Detail(int id)` retourne une voiture précise, ou `NotFound()` si l'id n'existe pas

4. Dans `Views/Voitures/`, crée `Index.cshtml` qui affiche la liste avec un lien vers le détail de chaque voiture.

5. Dans `Views/Voitures/`, crée `Detail.cshtml` qui affiche la fiche complète d'une voiture, avec un lien retour vers la liste.

6. Ajoute un lien vers `/Voitures` depuis la page d'accueil (modifie `Views/Home/Index.cshtml`).

7. Lance, navigue, vérifie que tout marche.

## Bonus

Ajoute une action `RechercherParMarque(string marque)` accessible via `/Voitures/RechercherParMarque?marque=Renault`. Elle filtre la liste et utilise la même view `Index.cshtml`.

## Temps

20 minutes. Pas de DI ni de persistance à cette étape : on valide le cycle MVC.
