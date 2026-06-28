# Mini exo 1 : Anatomie MVC

## Objectif

Reconnaître à la lecture si un snippet est un Model, une View ou un Controller, et savoir ce que fait le runtime ASP.NET Core face à chacun.

## Énoncé

Pour chaque snippet, indique :

- M, V ou C
- ce qui se passe au runtime si une requête déclenche ce code

### Snippet 1

```csharp
public class Voiture
{
    public string Marque { get; set; }
    public string Modele { get; set; }
    public decimal TarifJournee { get; set; }
}
```

### Snippet 2

```csharp
public class VoituresController : Controller
{
    public IActionResult Index()
    {
        var voitures = new List<Voiture>
        {
            new Voiture { Marque = "Renault", Modele = "Clio" }
        };
        return View(voitures);
    }
}
```

### Snippet 3

```html
@model List<Voiture>

<h1>Catalogue</h1>
<ul>
@foreach (var v in Model)
{
    <li>@v.Marque @v.Modele</li>
}
</ul>
```

### Snippet 4

```csharp
[HttpPost]
public IActionResult Create(VoitureFormVm form)
{
    if (!ModelState.IsValid)
        return View(form);
    _service.Ajouter(form);
    return RedirectToAction("Index");
}
```

### Snippet 5

```csharp
public class VoitureCardVm
{
    [Required]
    public string Marque { get; set; }

    [Range(1, 9999)]
    public decimal TarifJournee { get; set; }
}
```

## Temps

5 minutes. Correction collective.
