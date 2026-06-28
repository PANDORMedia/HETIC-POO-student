# Exercice principal 1 : Refactor en composition

## Objectif

Sortir une capacité technique d'une classe parente partagée pour la passer en composition.

## Énoncé

Tu pars du code suivant.

```csharp
public abstract class BaseService
{
    protected void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}

public class UserService : BaseService
{
    public void Inscrire(string email)
    {
        Log($"Inscription : {email}");
    }
}

public class OrderService : BaseService
{
    public void Valider(int orderId)
    {
        Log($"Validation commande {orderId}");
    }
}

public class ReportService : BaseService
{
    public void Generer()
    {
        Log("Génération du rapport");
    }
}
```

Trois services héritent d'une classe parente uniquement pour partager une capacité de log. `UserService` n'est pas un `BaseService` au sens métier. C'est un détail technique exposé en héritage.

## Étapes

1. Crée une interface `ILogger` avec une méthode `void Log(string message)`.
2. Crée une classe `ConsoleLogger` qui implémente `ILogger` avec le comportement actuel.
3. Supprime `BaseService`.
4. Chaque service reçoit un `ILogger` dans son constructeur, le stocke en champ privé, et l'utilise.
5. Dans `Program.cs`, instancie un `ConsoleLogger` et passe-le aux trois services.

## Bonus

Crée un `FileLogger` qui écrit dans `logs.txt`. Sans modifier les services, passe-le à `OrderService` uniquement. Les commandes loguent dans le fichier, les autres restent en console.
