# Exercice principal 4 : Interface IExportable

## Objectif

Définir un contrat IExportable indépendant de la hiérarchie MediaItem. L'implémenter sur Film et Série différemment, et écrire un service qui dépend uniquement de l'abstraction.

## Énoncé

### Étapes

1. Crée l'interface IExportable :

```csharp
public interface IExportable
{
    string Format { get; }
    string Nom { get; }
    string Exporter();
}
```

2. Implémente IExportable dans Film :
   - Format => "json"
   - Nom => Titre
   - Exporter() retourne un JSON style `{"titre":"Inception","annee":2010,"realisateur":"Nolan"}`

3. Implémente IExportable dans Serie :
   - Format => "xml"
   - Nom => Titre
   - Exporter() retourne un XML style `<serie><titre>BB</titre><annee>2008</annee><saisons>5</saisons></serie>`

4. Crée une classe statique Exporter avec une méthode :

```csharp
public static void Sauvegarder(IExportable item, string dossier)
```

Elle doit :
   - récupérer item.Exporter()
   - récupérer item.Format et item.Nom
   - écrire dans le fichier `{dossier}/{nom}.{format}` avec File.WriteAllText(...)

5. Dans Program.cs, crée un Film et une Série. Appelle Exporter.Sauvegarder() sur les deux.

6. Vérifie que les fichiers sont créés avec le bon contenu.

## Le moment magique

Observe que Exporter.Sauvegarder() ne mentionne ni Film, ni Série. Elle dépend uniquement du contrat IExportable. C'est exactement le découplage qu'on cherche.

## Bonus

- Ajoute IExportable à Documentaire avec un format CSV : "titre,annee,sujet\n{Titre},{Annee},{Sujet}"
- Écris une boucle qui exporte une List<IExportable> mélangeant Film, Série, Documentaire.

## Temps

15 minutes. Si tu finis en 10, attaque le bonus.
