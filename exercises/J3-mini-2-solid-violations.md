# Mini exo 2 : Identifier la violation SOLID

## Objectif

Repérer en lecture quel principe SOLID est violé dans un snippet, et justifier en une phrase.

## Énoncé

Pour chaque snippet, indique le principe violé (S, O, L, I ou D) et pourquoi.

### Snippet 1

```csharp
public class Utilisateur
{
    public string Nom { get; set; }
    public string Email { get; set; }

    public void SauverEnBase() { /* ... */ }
    public void EnvoyerEmailBienvenue() { /* ... */ }
    public string GenererCarteVisite() { /* ... */ }
}
```

### Snippet 2

```csharp
public class CalculReduction
{
    public decimal Calculer(string typeClient, decimal montant)
    {
        if (typeClient == "VIP") return montant * 0.8m;
        else if (typeClient == "Pro") return montant * 0.9m;
        else if (typeClient == "Standard") return montant;
        else if (typeClient == "Etudiant") return montant * 0.85m;
        else return montant;
    }
}
```

### Snippet 3

```csharp
public class Oiseau
{
    public virtual void Voler() { Console.WriteLine("Je vole"); }
}

public class Manchot : Oiseau
{
    public override void Voler()
    {
        throw new NotSupportedException("Un manchot ne vole pas");
    }
}
```

### Snippet 4

```csharp
public interface ITravailleur
{
    void Coder();
    void DessinerMaquette();
    void EcrireSpec();
    void TenirReunion();
}

public class Developpeur : ITravailleur
{
    public void Coder() { /* ... */ }
    public void DessinerMaquette() { throw new NotImplementedException(); }
    public void EcrireSpec() { throw new NotImplementedException(); }
    public void TenirReunion() { throw new NotImplementedException(); }
}
```

### Snippet 5

```csharp
public class RapportMensuel
{
    public void Generer()
    {
        var donnees = new MySqlDatabase().LireVentes();
        // ... formatage
    }
}
```
