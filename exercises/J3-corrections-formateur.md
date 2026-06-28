# Corrections J3 - Pour le formateur

Document à conserver privé. Toutes les corrections des 3 mini-exos et 3 exos principaux de la journée 3.

---

## Mini exo 1 - Composition ou héritage ?

| # | Paire | Choix | Justification | Forme C# |
|---|-------|-------|---------------|----------|
| 1 | Voiture / Moteur | composition | Une voiture a un moteur, pas est un moteur | `private Moteur _moteur;` |
| 2 | Smartphone / Caméra | composition | Le smartphone embarque une caméra | `private Camera _camera;` |
| 3 | Manager / Employé | héritage | Un manager est un employé avec des prérogatives en plus | `class Manager : Employe` |
| 4 | Chien / Animal | héritage | Un chien est un animal | `class Chien : Animal` |
| 5 | PlaylistMusicale / Chanson | composition | Une playlist contient des chansons | `private List<Chanson> _chansons;` |
| 6 | EmailService / Logger | composition | Le service utilise un logger, il n'en est pas un | `private ILogger _logger;` |
| 7 | CompteEpargne / CompteBancaire | héritage | Un compte épargne est un compte bancaire | `class CompteEpargne : CompteBancaire` |
| 8 | Restaurant / Table | composition | Le restaurant a des tables | `private List<Table> _tables;` |

### Notes pédagogiques

- Le piège à anticiper est sur la 6. Tentation de faire `class EmailService : BaseServiceWithLogger`. C'est l'anti-pattern qu'on combat dans l'exo principal 1.
- Sur la 3, débat possible : Manager pourrait aussi avoir des Employes en composition (sous sa supervision). Les deux lectures sont valides, l'important est de justifier.
- Sur la 7, certains étudiants vont vouloir composer pour pouvoir tester. Acceptable, mais l'héritage reste idiomatique ici car le compte épargne EST un compte bancaire avec contraintes en plus.

---

## Exercice principal 1 - Refactor en composition

```csharp
public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}

public class UserService
{
    private readonly ILogger _logger;

    public UserService(ILogger logger)
    {
        _logger = logger;
    }

    public void Inscrire(string email)
    {
        _logger.Log($"Inscription : {email}");
    }
}

public class OrderService
{
    private readonly ILogger _logger;

    public OrderService(ILogger logger)
    {
        _logger = logger;
    }

    public void Valider(int orderId)
    {
        _logger.Log($"Validation commande {orderId}");
    }
}

public class ReportService
{
    private readonly ILogger _logger;

    public ReportService(ILogger logger)
    {
        _logger = logger;
    }

    public void Generer()
    {
        _logger.Log("Génération du rapport");
    }
}

// Program.cs
var logger = new ConsoleLogger();
var users = new UserService(logger);
var orders = new OrderService(logger);
var reports = new ReportService(logger);

users.Inscrire("alice@test.com");
orders.Valider(42);
reports.Generer();
```

### Bonus

```csharp
public class FileLogger : ILogger
{
    private readonly string _path;

    public FileLogger(string path) { _path = path; }

    public void Log(string message)
    {
        File.AppendAllText(_path, $"[{DateTime.Now:HH:mm:ss}] {message}\n");
    }
}

// Program.cs
var consoleLogger = new ConsoleLogger();
var fileLogger = new FileLogger("logs.txt");

var users = new UserService(consoleLogger);
var orders = new OrderService(fileLogger);
var reports = new ReportService(consoleLogger);
```

### Pièges fréquents

- Oubli du `readonly` sur `_logger`. Acceptable au début, mais à mentionner comme bonne pratique.
- Tentation de garder `BaseService` avec un `ILogger` dedans. Possible mais on ajoute un niveau d'héritage inutile.
- Étudiant qui injecte `ConsoleLogger` au lieu de `ILogger` dans le constructeur : on dépend toujours d'une classe concrète, DIP violé.
- Oubli de tester avec FileLogger pour le bonus : c'est le moment de voir l'effet d'OCP en pratique.

---

## Mini exo 2 - Identifier la violation SOLID

| Snippet | Principe violé | Pourquoi |
|---------|----------------|----------|
| 1 | SRP | Utilisateur porte les données du domaine, persiste, envoie un email, génère un visuel. Quatre raisons de changer. |
| 2 | OCP | Ajouter un type de client oblige à modifier la méthode existante. Devrait être un Strategy avec une interface IReduction. |
| 3 | LSP | Manchot hérite d'Oiseau mais ne sait pas voler. Tout code qui prend un Oiseau et appelle Voler() casse silencieusement. |
| 4 | ISP | ITravailleur impose 4 méthodes. Un Developpeur n'a pas à dessiner ni écrire des specs. Segmenter en ICode, IDessine, etc. |
| 5 | DIP | RapportMensuel dépend en dur de MySqlDatabase. Devrait dépendre d'une abstraction IVentesRepository injectée. |

### Notes pédagogiques

- Le piège est de dire "ça viole les 5" sur le snippet 1. Faux : seul SRP est clairement violé. Garder un seul principe par violation.
- Sur le snippet 4, certains confondent avec SRP. Différence : SRP regarde la classe, ISP regarde l'interface.
- Sur le snippet 3, le réflexe est de dire "Manchot ne devrait pas hériter d'Oiseau". Vrai. La solution est de revoir la hiérarchie : `Oiseau` abstrait avec `OiseauVolant` et `OiseauTerrestre`.

---

## Exercice principal 2 - Refactor SOLID

```csharp
public class Facture
{
    public int Id { get; set; }
    public decimal Montant { get; set; }
    public string EmailClient { get; set; }
}

public interface IFactureRepository
{
    Facture LireParId(int id);
}

public interface IPdfGenerator
{
    string Generer(Facture facture);
}

public interface IEmailService
{
    void Envoyer(string destinataire, string sujet, string corps, string pieceJointe);
}

public class SqlFactureRepository : IFactureRepository
{
    private readonly string _connectionString;

    public SqlFactureRepository(string connStr) { _connectionString = connStr; }

    public Facture LireParId(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var cmd = new SqlCommand($"SELECT * FROM Factures WHERE Id={id}", connection);
        var reader = cmd.ExecuteReader();
        reader.Read();
        return new Facture
        {
            Id = id,
            Montant = (decimal)reader["Montant"],
            EmailClient = (string)reader["Email"]
        };
    }
}

public class FilePdfGenerator : IPdfGenerator
{
    public string Generer(Facture facture)
    {
        var path = $"factures/facture-{facture.Id}.pdf";
        File.WriteAllText(path, $"Facture {facture.Id} : {facture.Montant} EUR");
        return path;
    }
}

public class SmtpEmailService : IEmailService
{
    private readonly string _smtpHost;

    public SmtpEmailService(string host) { _smtpHost = host; }

    public void Envoyer(string destinataire, string sujet, string corps, string pieceJointe)
    {
        var smtp = new SmtpClient(_smtpHost);
        var mail = new MailMessage("noreply@entreprise.com", destinataire)
        {
            Subject = sujet,
            Body = corps
        };
        if (pieceJointe != null)
            mail.Attachments.Add(new Attachment(pieceJointe));
        smtp.Send(mail);
    }
}

public class FactureService
{
    private readonly IFactureRepository _repository;
    private readonly IPdfGenerator _pdfGenerator;
    private readonly IEmailService _emailService;

    public FactureService(
        IFactureRepository repository,
        IPdfGenerator pdfGenerator,
        IEmailService emailService)
    {
        _repository = repository;
        _pdfGenerator = pdfGenerator;
        _emailService = emailService;
    }

    public void TraiterFacture(int factureId)
    {
        var facture = _repository.LireParId(factureId);
        var pdfPath = _pdfGenerator.Generer(facture);
        _emailService.Envoyer(
            facture.EmailClient,
            $"Votre facture {factureId}",
            "Veuillez trouver votre facture en pièce jointe.",
            pdfPath);
    }
}

// Program.cs
var repo = new SqlFactureRepository("Server=...;Database=...");
var pdfGen = new FilePdfGenerator();
var email = new SmtpEmailService("smtp.entreprise.com");
var service = new FactureService(repo, pdfGen, email);

service.TraiterFacture(42);
```

### Bonus

```csharp
public class FakeEmailService : IEmailService
{
    public void Envoyer(string destinataire, string sujet, string corps, string pieceJointe)
    {
        Console.WriteLine($"[EMAIL FAKE] À : {destinataire}");
        Console.WriteLine($"  Sujet : {sujet}");
        Console.WriteLine($"  Pièce : {pieceJointe}");
    }
}

// Program.cs (test)
var service = new FactureService(repo, pdfGen, new FakeEmailService());
service.TraiterFacture(42);
```

### Pièges fréquents

- Étudiant qui garde du SQL inline dans FactureService. La motivation du repository est de cacher le SQL.
- Constructeur qui `new()` les implémentations au lieu de les recevoir : DIP toujours violé, on n'a pas avancé.
- Oubli des `readonly` sur les champs.
- Oubli du `using` sur SqlConnection (fuite de ressources).
- Confusion entre l'interface `IEmailService` et la classe `EmailService` : on demande des interfaces dans le constructeur, pas des classes.

---

## Mini exo 3 - Prédire le flux d'exception

### Cas A

Affiche : `1`, `2`, `3`, puis `4`. Aucune exception ne s'échappe.

Le throw est attrapé par le catch typé. Le finally s'exécute. Le code après le bloc try continue.

### Cas B

Affiche : `1`, `3`. L'exception `InvalidOperationException` s'échappe vers l'appelant.

Le catch n'attrape que `ArgumentException`, pas `InvalidOperationException`. L'exception passe à travers, mais le finally s'exécute quand même avant la propagation. Le `4` n'est jamais atteint.

### Cas C

Affiche : `1`, `2`. Retourne `10`.

Le `finally` s'exécute APRÈS l'évaluation de la valeur de retour mais AVANT que le contrôle quitte la méthode. Donc "2" s'affiche après "1", puis 10 est retourné.

### Cas D

Affiche :

```
A
B: ArgumentException
Inner: InvalidOperationException
```

Le catch inner attrape `InvalidOperationException`, affiche `A`, puis throw un `ArgumentException` qui wrappe l'original via `innerException`. Le catch outer attrape `ArgumentException`, affiche `B` avec son type, puis `Inner` qui révèle le type original.

### Notes pédagogiques

- Le cas C surprend toujours. Insister sur le fait que finally se positionne entre l'évaluation du return et le retour effectif.
- Le cas B met en évidence pourquoi le catch typé est important : on évite d'attraper ce qu'on ne sait pas gérer.
- Le cas D est l'usage canonique d'`innerException` : on enrichit le contexte sans perdre l'information d'origine. Insister que `throw ex` aurait perdu la stack, contrairement à `throw new Ex("wrapped", ex)`.

---

## Exercice principal 3 - Exception métier

```csharp
public class SoldeInsuffisantException : Exception
{
    public decimal SoldeActuel { get; }
    public decimal MontantDemande { get; }
    public decimal MontantManquant => MontantDemande - SoldeActuel;

    public SoldeInsuffisantException(decimal solde, decimal demande)
        : base($"Solde {solde:C} insuffisant pour retrait de {demande:C}. Manque {demande - solde:C}.")
    {
        SoldeActuel = solde;
        MontantDemande = demande;
    }
}

public class Compte
{
    public string Numero { get; }
    public string Titulaire { get; }
    public decimal Solde { get; private set; }

    public Compte(string numero, string titulaire, decimal soldeInitial)
    {
        if (soldeInitial < 0)
            throw new ArgumentOutOfRangeException(
                nameof(soldeInitial),
                "Le solde initial ne peut pas être négatif.");
        Numero = numero;
        Titulaire = titulaire;
        Solde = soldeInitial;
    }

    public void Crediter(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(montant),
                "Le montant doit être strictement positif.");
        Solde += montant;
    }

    public void Retirer(decimal montant)
    {
        if (montant <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(montant),
                "Le montant doit être strictement positif.");
        if (montant > Solde)
            throw new SoldeInsuffisantException(Solde, montant);
        Solde -= montant;
    }
}

// Program.cs
var compte = new Compte("FR-001", "Alice", 100);
compte.Crediter(50);
compte.Retirer(30);
Console.WriteLine($"Solde : {compte.Solde:C}");

try
{
    compte.Retirer(500);
}
catch (SoldeInsuffisantException ex)
{
    Console.WriteLine($"Retrait refusé. Il manque {ex.MontantManquant:C}.");
}
```

### Sortie attendue

```
Solde : 120,00 €
Retrait refusé. Il manque 380,00 €.
```

### Bonus avec rollback

```csharp
public void Virer(decimal montant, Compte destinataire)
{
    Retirer(montant);
    try
    {
        destinataire.Crediter(montant);
    }
    catch
    {
        Crediter(montant);  // rollback du débit
        throw;
    }
}
```

`Retirer` peut throw `SoldeInsuffisantException` : dans ce cas rien n'a été modifié, on laisse remonter. Si `Crediter` du destinataire échoue, on annule le débit du compte source avant de propager.

### Pièges fréquents

- Oubli d'initialiser `SoldeActuel` et `MontantDemande` dans le constructeur de l'exception : les propriétés restent à 0.
- Message d'exception sans interpolation ou sans format monétaire (`:C`).
- Setter `Solde` public au lieu de `private set` : casse l'encapsulation.
- `Crediter` qui accepte 0 : ce n'est pas un crédit, on refuse.
- Étudiant qui `catch (Exception)` au lieu de `catch (SoldeInsuffisantException)` : on perd la spécificité, on attrape aussi les `ArgumentOutOfRangeException`.
- Bonus sans rollback : on perd l'argent en cas d'erreur. Faire la démo en plénière avec un destinataire à null pour provoquer le bug.

---

## Récap pédagogique J3

| Bloc | Mini | Principal | Concepts clés |
|------|------|-----------|---------------|
| Composition | composition vs héritage 2.0 | refactor BaseService vers ILogger | has-a, capacité injectée, ILogger |
| SOLID | identifier la violation | refactor FactureService | SRP, DIP, interfaces, injection |
| Exceptions OO | prédire le flux | SoldeInsuffisantException | exception métier, contexte, try / catch / finally |

### Bibliographie

- Clean Architecture, Robert C. Martin : SOLID en profondeur
- Working Effectively with Legacy Code, Michael Feathers : refactor en composition
- Framework Design Guidelines, Cwalina et Abrams : conventions exceptions .NET
- Effective C#, Bill Wagner : items sur exceptions et dependency injection
