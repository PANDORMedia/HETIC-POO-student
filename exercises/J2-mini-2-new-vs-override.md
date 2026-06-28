# Mini exo 2 : Prédire la sortie - new vs override

## Objectif

Voir en pratique pourquoi new est un piège et pourquoi override est la bonne forme. Différencier le dispatch par type déclaré et le dispatch par type réel.

## Énoncé

Lis le code ci-dessous et prédis la sortie console pour chaque cas. Note ta réponse avant de tester.

```csharp
class Animal
{
    public virtual void Crier() => Console.WriteLine("Bruit générique");
}

class Chien : Animal
{
    public new void Crier() => Console.WriteLine("Wouaf");
}

class Chat : Animal
{
    public override void Crier() => Console.WriteLine("Miaou");
}

// Cas A
Animal a1 = new Chien();
a1.Crier();

// Cas B
Chien c1 = new Chien();
c1.Crier();

// Cas C
Animal a2 = new Chat();
a2.Crier();

// Cas D
Chat c2 = new Chat();
c2.Crier();
```

Pour chaque cas (A, B, C, D), écris :

- la sortie console exacte
- une phrase d'explication

## Temps

5 minutes. On corrige collectivement après.
