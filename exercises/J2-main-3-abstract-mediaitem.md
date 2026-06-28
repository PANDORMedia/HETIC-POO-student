# Exercice principal 3 : MediaItem en classe abstraite

## Objectif

Empêcher l'instanciation directe de MediaItem et forcer chaque type de média à fournir son propre comportement Afficher().

## Énoncé

Tu pars du code de l'exo 2 (polymorphisme).

### Étapes

1. Transforme MediaItem en classe abstraite : ajoute `abstract` devant `class`.

2. Transforme Afficher() en méthode abstraite : retire le corps `{ ... }`, mets un `;` à la fin :

```csharp
public abstract void Afficher();
```

3. Vérifie que `new MediaItem(...)` ne compile plus. Lis le message d'erreur du compilateur.

4. Vérifie que Film, Serie, Documentaire compilent toujours (ils ont déjà l'override).

5. Crée une nouvelle classe Podcast : MediaItem avec une propriété Animateur (string) et SANS implémenter Afficher(). Que dit le compilateur ?

6. Corrige Podcast en ajoutant l'override de Afficher() :

```
🎙 {Titre} ({Annee}) - animé par {Animateur}
```

7. Ajoute un Podcast à ta liste List<MediaItem> dans Program.cs. Affiche tout.

## Bonus

Ajoute une méthode CONCRÈTE non-abstract sur MediaItem :

```csharp
public string GetResumeCourt() => $"{Titre} ({Annee})";
```

Vérifie qu'elle est disponible sur Film, Série, Documentaire, Podcast sans aucun override. Une classe abstraite peut mélanger méthodes concrètes ET abstraites.

## Temps

15 minutes. Étapes 1 à 7 d'abord, bonus si tu as le temps.
