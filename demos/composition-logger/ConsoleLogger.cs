// ConsoleLogger est une implémentation concrète du contrat ILogger.
// Le ": ILogger" signifie "cette classe respecte le contrat ILogger" : elle
// s'engage donc à fournir une vraie méthode Log. C'est du polymorphisme : partout
// où le code attend un ILogger, on pourra fournir un ConsoleLogger à la place.
public class ConsoleLogger : ILogger
{
    // Implémentation concrète de la méthode déclarée dans l'interface.
    // Pour cette classe, "logger" veut dire : afficher le message dans la console.
    public void Log(string message)
    {
        // On affiche l'heure (HH:mm:ss) suivie du message. Le $"..." est une chaine
        // interpolée : C# remplace {DateTime.Now:HH:mm:ss} et {message} par leurs
        // valeurs réelles au moment de l'exécution.
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
    }
}