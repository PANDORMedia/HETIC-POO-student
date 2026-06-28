# Exercice principal 2 : Polymorphisme avec virtual / override

## Objectif

Rendre Afficher() polymorphique sur ta hiérarchie MediaItem et observer le dispatch dynamique en parcourant une List<MediaItem>.

## Énoncé

Tu pars du code de l'exo 1.

### Étapes

1. Modifie MediaItem.Afficher() pour ajouter le mot-clé virtual.

2. Override Afficher() dans Film pour afficher :

```
🎬 {Titre} ({Annee}) - réalisé par {Realisateur}
```

3. Override Afficher() dans Serie pour afficher :

```
📺 {Titre} ({Annee}) - {NbSaisons} saison(s)
```

4. Override Afficher() dans Documentaire pour afficher :

```
📚 {Titre} ({Annee}) - sujet : {Sujet}
```

5. Dans Program.cs, crée une List<MediaItem> contenant un Film, une Série, un Documentaire :

```csharp
var medias = new List<MediaItem>
{
    new Film("Inception", 2010, "Nolan"),
    new Serie("Breaking Bad", 2008, 5),
    new Documentaire("Cosmos", 1980, "Univers")
};
```

6. Itère sur la liste avec un foreach et appelle Afficher() sur chaque élément. Observe que chaque élément utilise sa propre version, alors que la variable est typée MediaItem.

## Bonus

Dans Film.Afficher(), appelle base.Afficher() en premier pour réutiliser la logique parent, puis ajoute la ligne "réalisé par".

## Temps

15 minutes. Si tu finis en 10, attaque le bonus.
