// FileLogger est une autre implémentation du même contrat ILogger.
// Elle rend le même service (logger un message) mais d'une façon différente :
// elle écrit dans un fichier au lieu d'afficher à l'écran. C'est tout l'intérêt
// d'une interface : deux classes deviennent interchangeables car elles respectent
// le même contrat. Le code qui les utilise n'a pas besoin de savoir laquelle il manipule.
public class FileLogger : ILogger
{
    // Même signature que dans ILogger : on est obligé de la respecter, sinon le code
    // ne compile pas. C'est la garantie apportée par l'interface.
    public void Log(string message)
    {
        // StreamWriter écrit du texte dans un fichier. Le deuxième paramètre "true"
        // signifie "ajouter à la suite" (mode append) au lieu d'écraser le contenu.
        // Le bloc "using (...)" garantit que le fichier sera correctement fermé à la
        // fin, même en cas d'erreur : on parle de libération propre de la ressource.
        using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "log.txt"), true))
        {
            // On écrit la même ligne horodatée, mais dans le fichier cette fois.
            streamWriter.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}