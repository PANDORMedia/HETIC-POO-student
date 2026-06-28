# Exercice principal 2 : Refactor SOLID

## Objectif

Décomposer une classe qui viole SRP et DIP. Extraire les responsabilités, inverser les dépendances.

## Énoncé

Tu pars du code suivant.

```csharp
public class FactureService
{
    public void TraiterFacture(int factureId)
    {
        // Lecture en base
        var connection = new SqlConnection("Server=...;Database=...");
        connection.Open();
        var cmd = new SqlCommand($"SELECT * FROM Factures WHERE Id={factureId}", connection);
        var reader = cmd.ExecuteReader();
        reader.Read();
        var montant = (decimal)reader["Montant"];
        var emailClient = (string)reader["Email"];
        connection.Close();

        // Génération PDF
        var pdfPath = $"factures/facture-{factureId}.pdf";
        File.WriteAllText(pdfPath, $"Facture {factureId} : {montant} EUR");

        // Envoi email
        var smtp = new SmtpClient("smtp.entreprise.com");
        var mail = new MailMessage("noreply@entreprise.com", emailClient)
        {
            Subject = $"Votre facture {factureId}",
            Body = "Veuillez trouver votre facture en pièce jointe."
        };
        mail.Attachments.Add(new Attachment(pdfPath));
        smtp.Send(mail);
    }
}
```

Cette classe fait trois choses (lit en base, génère un PDF, envoie un email) et dépend de classes concrètes (SqlConnection, File, SmtpClient). Impossible à tester sans base ni SMTP.

## Étapes

1. Crée une classe `Facture` (modèle) avec `Id`, `Montant`, `EmailClient`.
2. Crée trois interfaces :
   - `IFactureRepository` avec `Facture LireParId(int id)`
   - `IPdfGenerator` avec `string Generer(Facture facture)` qui retourne le chemin du PDF
   - `IEmailService` avec `void Envoyer(string destinataire, string sujet, string corps, string pieceJointe)`
3. Crée les trois implémentations concrètes : `SqlFactureRepository`, `FilePdfGenerator`, `SmtpEmailService`. Chacune reçoit sa config (connection string, host SMTP) dans son constructeur.
4. Refactore `FactureService` pour qu'il reçoive les trois interfaces dans son constructeur. La méthode `TraiterFacture` orchestre les appels.
5. Dans `Program.cs`, instancie les trois implémentations et injecte-les dans `FactureService`.

## Bonus

Crée une implémentation alternative `FakeEmailService` qui se contente d'imprimer en console. Passe-la à `FactureService` pour tester sans envoyer d'email réel.

## Temps

15 minutes. Étapes 1 à 5 d'abord, bonus si tu as le temps.
