namespace CarRental
{
    class Program
    {
        // Main est le point d'entrée du programme : la première méthode exécutée.
        static void Main()
        {

            // C'est ici, et seulement ici, que l'on choisit l'implémentation concrète
            // du logger. On crée un ConsoleLogger et on l'injecte dans UserService via
            // son constructeur. UserService, lui, ne connait que l'interface ILogger :
            // il ignore totalement qu'il s'agit d'un ConsoleLogger.
            // Pour logguer dans un fichier, il suffirait d'écrire new FileLogger() à la
            // place, sans toucher une seule ligne du code des services. Cet endroit où
            // l'on assemble les objets concrets s'appelle le point de composition.
            UserService userService = new UserService(new ConsoleLogger());

            // On appelle la méthode métier. En coulisse, elle utilise le logger injecté :
            // la ligne s'affichera donc dans la console.
            userService.Inscrire("sean@pandor.media");
        }
    }
}