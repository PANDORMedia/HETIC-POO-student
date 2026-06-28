# Corrections J2 - Pour le formateur

Document à conserver privé. Toutes les corrections des 4 exercices principaux et des 4 mini-exercices de la journée 2 (Héritage, Polymorphisme, Classes abstraites, Interfaces).

---

## Mini exo 1 - Is-a ou Has-a ?

| # | Paire | Relation | Code |
|---|-------|----------|------|
| 1 | Voiture / Moteur | has-a | `class Voiture { private Moteur _moteur; }` |
| 2 | Voiture / Véhicule | is-a | `class Voiture : Véhicule` |
| 3 | Bibliothèque / Livre | has-a | `class Bibliothèque { private List<Livre> _livres; }` |
| 4 | Chien / Animal | is-a | `class Chien : Animal` |
| 5 | Commande / LigneCommande | has-a | `class Commande { private List<LigneCommande> _lignes; }` |
| 6 | Étudiant / Personne | is-a | `class Étudiant : Personne` |
| 7 | Maison / Pièce | has-a | `class Maison { private List<Pièce> _pièces; }` |
| 8 | Carré / Forme | is-a | `class Carré : Forme` |

### Pièges fréquents

- "Voiture is-a Moteur" : la voiture n'est PAS un moteur, elle en possède un.
- "Maison is-a Pièce" : une maison n'est pas une pièce, elle contient plusieurs pièces.
- "Bibliothèque is-a Livre" : confusion classique entre contenant et contenu.

---

## Exercice principal 1 - Refactor en héritage

```csharp
public class MediaItem
{
    public string Titre { get; set; }
    public int Annee { get; set; }

    public MediaItem(string titre, int annee)
    {
        Titre = titre;
        Annee = annee;
    }

    public void Afficher()
    {
        Console.WriteLine($"{Titre} ({Annee})");
    }
}

public class Film : MediaItem
{
    public string Realisateur { get; set; }

    public Film(string titre, int annee, string realisateur)
        : base(titre, annee)
    {
        Realisateur = realisateur;
    }
}

public class Serie : MediaItem
{
    public int NbSaisons { get; set; }

    public Serie(string titre, int annee, int nbSaisons)
        : base(titre, annee)
    {
        NbSaisons = nbSaisons;
    }
}

public class Documentaire : MediaItem
{
    public string Sujet { get; set; }

    public Documentaire(string titre, int annee, string sujet)
        : base(titre, annee)
    {
        Sujet = sujet;
    }
}

// Program.cs
var f = new Film("Inception", 2010, "Nolan");
var s = new Serie("Breaking Bad", 2008, 5);
var d = new Documentaire("Cosmos", 1980, "Univers");

f.Afficher();
s.Afficher();
d.Afficher();
```

### Bonus

```csharp
public class Podcast : MediaItem
{
    public string Animateur { get; set; }

    public Podcast(string titre, int annee, string animateur)
        : base(titre, annee)
    {
        Animateur = animateur;
    }
}
// 10 lignes pour ajouter une nouvelle famille de médias.
```

### Pièges fréquents à corriger en plénière

- Oubli du `: base(...)` : erreur de compilation si MediaItem n'a pas de constructeur sans paramètre.
- L'étudiant qui recopie aussi Titre et Annee dans Film alors qu'ils sont hérités.
- Constructeur de Film qui ne passe pas tous les paramètres au parent.
- Étudiant qui met les propriétés Titre/Annee en private dans MediaItem : le Afficher() en plénière ne marche plus.

---

## Mini exo 2 - Prédire la sortie new vs override

| Cas | Code | Sortie | Pourquoi |
|-----|------|--------|----------|
| A | `Animal a1 = new Chien(); a1.Crier();` | `Bruit générique` | Chien utilise `new`, donc via le type déclaré Animal, c'est la méthode du parent. Chien.Crier() est cachée, pas remplacée. |
| B | `Chien c1 = new Chien(); c1.Crier();` | `Wouaf` | Via le type déclaré Chien, la méthode du Chien est trouvée directement. |
| C | `Animal a2 = new Chat(); a2.Crier();` | `Miaou` | Chat utilise `override`, dispatch polymorphique : c'est le type RÉEL qui décide. |
| D | `Chat c2 = new Chat(); c2.Crier();` | `Miaou` | Idem, type réel Chat. |

### À retenir

- `new` masque, ne remplace pas. Comportement dépend du type DÉCLARÉ.
- `override` remplace. Comportement dépend du type RÉEL, toujours.
- En 99% des cas, tu veux `override`. `new` est une bombe à retardement.

### Question piège à poser en débrief

"Et si je passe c1 (typé Chien) à une méthode qui attend Animal, quelle Crier sera appelée ?"
Réponse : `Bruit générique`, parce que dans la méthode receveuse, c1 est vu comme Animal.

---

## Exercice principal 2 - Polymorphisme

```csharp
public class MediaItem
{
    public string Titre { get; set; }
    public int Annee { get; set; }

    public MediaItem(string titre, int annee)
    {
        Titre = titre;
        Annee = annee;
    }

    public virtual void Afficher()
    {
        Console.WriteLine($"{Titre} ({Annee})");
    }
}

public class Film : MediaItem
{
    public string Realisateur { get; set; }

    public Film(string titre, int annee, string realisateur)
        : base(titre, annee) { Realisateur = realisateur; }

    public override void Afficher()
    {
        Console.WriteLine($"🎬 {Titre} ({Annee}) - réalisé par {Realisateur}");
    }
}

public class Serie : MediaItem
{
    public int NbSaisons { get; set; }

    public Serie(string titre, int annee, int nbSaisons)
        : base(titre, annee) { NbSaisons = nbSaisons; }

    public override void Afficher()
    {
        Console.WriteLine($"📺 {Titre} ({Annee}) - {NbSaisons} saison(s)");
    }
}

public class Documentaire : MediaItem
{
    public string Sujet { get; set; }

    public Documentaire(string titre, int annee, string sujet)
        : base(titre, annee) { Sujet = sujet; }

    public override void Afficher()
    {
        Console.WriteLine($"📚 {Titre} ({Annee}) - sujet : {Sujet}");
    }
}

// Program.cs
var medias = new List<MediaItem>
{
    new Film("Inception", 2010, "Nolan"),
    new Serie("Breaking Bad", 2008, 5),
    new Documentaire("Cosmos", 1980, "Univers")
};

foreach (var m in medias)
{
    m.Afficher();
}
```

### Sortie attendue

```
🎬 Inception (2010) - réalisé par Nolan
📺 Breaking Bad (2008) - 5 saison(s)
📚 Cosmos (1980) - sujet : Univers
```

### Bonus avec base.Afficher()

```csharp
public override void Afficher()
{
    base.Afficher();              // Inception (2010)
    Console.WriteLine($"  réalisé par {Realisateur}");
}
```

### Pièges fréquents

- Oubli du `virtual` sur la méthode parent : `override` ne compile pas.
- Utilisation de `new` à la place d'`override` : comportement non polymorphique.
- L'étudiant qui type la liste List<Film> au lieu de List<MediaItem> et perd le polymorphisme.
- L'étudiant qui appelle Afficher() sur chaque élément individuellement, sans foreach : on rate la démo du dispatch dynamique sur collection.

---

## Mini exo 3 - abstract ou virtual ?

| # | Méthode | Choix | Justification |
|---|---------|-------|---------------|
| 1 | MediaItem.Afficher() | abstract | Chaque type de média s'affiche différemment, pas de "format par défaut" sensé. |
| 2 | MediaItem.GetUrl() | virtual | Le format "/media/{Id}" est un défaut raisonnable que la plupart peuvent garder. |
| 3 | Forme.CalculerAire() | abstract | Pas de formule générique. Chaque forme a sa propre formule (carré : c², cercle : πr², triangle : bh/2). |
| 4 | Notifier.EnvoyerNotification() | virtual | Le log console est un défaut acceptable. EmailNotifier, SmsNotifier overrideront. |
| 5 | Repository<T>.Save(T) | abstract | Le stockage est totalement spécifique à chaque techno. Aucun défaut sensé. |

### Astuce pédagogique

Si tu hésites, essaie d'écrire le corps par défaut sur papier. Si tu n'en trouves pas qui ait du sens pour TOUS les enfants potentiels, c'est abstract.

---

## Exercice principal 3 - MediaItem abstract

### Étapes 1-2

```csharp
public abstract class MediaItem
{
    public string Titre { get; set; }
    public int Annee { get; set; }

    public MediaItem(string titre, int annee)
    {
        Titre = titre;
        Annee = annee;
    }

    public abstract void Afficher();
}
```

### Étape 3 - message du compilateur

```
Cannot create an instance of the abstract type or interface 'MediaItem'
```

### Étape 5 - message du compilateur sans override sur Podcast

```
'Podcast' does not implement inherited abstract member 'MediaItem.Afficher()'
```

### Étape 6 - Podcast complet

```csharp
public class Podcast : MediaItem
{
    public string Animateur { get; set; }

    public Podcast(string titre, int annee, string animateur)
        : base(titre, annee)
    {
        Animateur = animateur;
    }

    public override void Afficher()
    {
        Console.WriteLine($"🎙 {Titre} ({Annee}) - animé par {Animateur}");
    }
}
```

### Bonus

```csharp
public abstract class MediaItem
{
    // ... (déjà existant)

    public string GetResumeCourt() => $"{Titre} ({Annee})";
}

// Utilisation
var p = new Podcast("Underscore", 2021, "Micode");
Console.WriteLine(p.GetResumeCourt());
// Affiche : Underscore (2021)
```

### Points pédagogiques

- Une classe abstraite peut avoir des méthodes concrètes ET abstraites.
- Le constructeur reste public (les sous-classes l'appellent via base), mais on ne peut pas `new MediaItem(...)` directement.
- L'erreur du compilateur à l'étape 5 est exactement ce qu'on veut : on force la déclaration explicite du comportement.

---

## Mini exo 4 - Interface ou classe abstraite ?

| # | Cas | Choix | Pourquoi |
|---|-----|-------|----------|
| 1 | Film, Date, Score comparables | interface (IComparable<T>) | Capacité transverse. Ces 3 types n'ont rien d'autre en commun. |
| 2 | Factoriser Titre/Annee pour Film, Série, Doc | classe abstraite (MediaItem) | Vraie famille avec code commun. |
| 3 | Type "libéré proprement" | interface (IDisposable) | Capacité transverse. Fichier, connexion DB, socket : aucune famille commune. |
| 4 | Forme avec Couleur/Position partagés + CalculerAire forcé | classe abstraite | Code partagé ET comportement forcé. Sous-classes vraiment des Forme. |
| 5 | Type sérialisable en JSON | interface (IJsonSerializable) | Capacité transverse. N'importe quoi de n'importe quelle famille peut l'être. |
| 6 | Animal avec Manger() commun et Crier() défini par espèce | classe abstraite | Code partagé + comportement abstrait. Vraie famille. |

### Mnémo

Classe abstraite = ADN. Interface = compétence.

Une seule famille (ADN). Plein de compétences possibles.

---

## Exercice principal 4 - IExportable

```csharp
public interface IExportable
{
    string Format { get; }
    string Nom { get; }
    string Exporter();
}

public class Film : MediaItem, IExportable
{
    public string Realisateur { get; set; }

    public Film(string titre, int annee, string realisateur)
        : base(titre, annee) { Realisateur = realisateur; }

    public override void Afficher() { /* ... */ }

    public string Format => "json";
    public string Nom => Titre;
    public string Exporter()
    {
        return $"{{\"titre\":\"{Titre}\",\"annee\":{Annee},\"realisateur\":\"{Realisateur}\"}}";
    }
}

public class Serie : MediaItem, IExportable
{
    public int NbSaisons { get; set; }

    public Serie(string titre, int annee, int nbSaisons)
        : base(titre, annee) { NbSaisons = nbSaisons; }

    public override void Afficher() { /* ... */ }

    public string Format => "xml";
    public string Nom => Titre;
    public string Exporter()
    {
        return $"<serie><titre>{Titre}</titre><annee>{Annee}</annee><saisons>{NbSaisons}</saisons></serie>";
    }
}

public static class Exporter
{
    public static void Sauvegarder(IExportable item, string dossier)
    {
        Directory.CreateDirectory(dossier);
        var chemin = Path.Combine(dossier, $"{item.Nom}.{item.Format}");
        File.WriteAllText(chemin, item.Exporter());
        Console.WriteLine($"Exporté : {chemin}");
    }
}

// Program.cs
var f = new Film("Inception", 2010, "Nolan");
var s = new Serie("BreakingBad", 2008, 5);

Exporter.Sauvegarder(f, "export");
Exporter.Sauvegarder(s, "export");
```

### Sortie attendue

```
Exporté : export/Inception.json
Exporté : export/BreakingBad.xml
```

Fichiers créés :

- export/Inception.json : `{"titre":"Inception","annee":2010,"realisateur":"Nolan"}`
- export/BreakingBad.xml : `<serie><titre>BreakingBad</titre>...</serie>`

### Points pédagogiques clés

- Exporter.Sauvegarder() ne mentionne ni Film ni Serie. Elle dépend uniquement de IExportable. C'est LE découplage qu'on cherche.
- Ajouter Documentaire qui implémente IExportable ne nécessite AUCUNE modification de Exporter.Sauvegarder() : c'est l'OCP (Open/Closed Principle) qu'on verra en J3.
- Les espaces dans les titres peuvent poser problème dans les noms de fichiers : c'est volontairement laissé comme piège.
- Une vraie implémentation utiliserait System.Text.Json.JsonSerializer ou System.Xml.Serialization. Ici on écrit à la main pour bien voir le contrat.

### Bonus CSV

```csharp
public class Documentaire : MediaItem, IExportable
{
    public string Sujet { get; set; }

    public Documentaire(string titre, int annee, string sujet)
        : base(titre, annee) { Sujet = sujet; }

    public override void Afficher() { /* ... */ }

    public string Format => "csv";
    public string Nom => Titre;
    public string Exporter()
    {
        return $"titre,annee,sujet\n{Titre},{Annee},{Sujet}";
    }
}
```

### Boucle finale

```csharp
var exportables = new List<IExportable>
{
    new Film("Inception", 2010, "Nolan"),
    new Serie("BreakingBad", 2008, 5),
    new Documentaire("Cosmos", 1980, "Univers")
};

foreach (var item in exportables)
{
    Exporter.Sauvegarder(item, "export");
}
```

Note : ici on utilise List<IExportable>, pas List<MediaItem>. C'est volontaire : on veut juste des "exportables", peu importe la famille.

---

## Récap pédagogique J2

| Bloc | Mini | Principal | Concepts clés |
|------|------|-----------|---------------|
| Héritage | is-a vs has-a | MediaItem parent | `: base()`, membres hérités, protected |
| Polymorphisme | new vs override | List<MediaItem> + foreach | virtual, override, dispatch dynamique |
| Classes abstraites | abstract vs virtual | MediaItem abstract + Podcast | abstract class, abstract method |
| Interfaces | interface vs abstract class | IExportable + Exporter.Sauvegarder | contrat, multi-implémentation, découplage |

### Bibliographie pour aller plus loin

- Microsoft Learn : "Inheritance in C#"
- Effective C#, Bill Wagner : items 21 à 28 (héritage et polymorphisme)
- Clean Code, Robert C. Martin : chapitre 10 (classes)
- "Favor composition over inheritance" : Joshua Bloch, Effective Java item 18 (équivalent C#)
