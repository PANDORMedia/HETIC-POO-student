# Mini exo 3 : Prédire le flux d'exception

## Objectif

Maîtriser l'ordre d'exécution dans un try / catch / finally, et comprendre l'enchaînement entre catch typés et propagation.

## Énoncé

Pour chaque cas, prédis :

- ce qui s'affiche dans la console
- si une exception s'échappe de la méthode, et son type

### Cas A

```csharp
static void CasA()
{
    try
    {
        Console.WriteLine("1");
        throw new InvalidOperationException("oops");
    }
    catch (InvalidOperationException)
    {
        Console.WriteLine("2");
    }
    finally
    {
        Console.WriteLine("3");
    }
    Console.WriteLine("4");
}
```

### Cas B

```csharp
static void CasB()
{
    try
    {
        Console.WriteLine("1");
        throw new InvalidOperationException("oops");
    }
    catch (ArgumentException)
    {
        Console.WriteLine("2");
    }
    finally
    {
        Console.WriteLine("3");
    }
    Console.WriteLine("4");
}
```

### Cas C

```csharp
static int CasC()
{
    try
    {
        Console.WriteLine("1");
        return 10;
    }
    finally
    {
        Console.WriteLine("2");
    }
}
```

### Cas D

```csharp
static void CasD()
{
    try
    {
        try
        {
            throw new InvalidOperationException("inner");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("A");
            throw new ArgumentException("wrapped", ex);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"B: {ex.GetType().Name}");
        Console.WriteLine($"Inner: {ex.InnerException?.GetType().Name}");
    }
}
```

## Temps

5 minutes. Correction collective.
