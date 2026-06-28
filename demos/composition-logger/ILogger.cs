// ILogger est une interface : un contrat qui déclare ce qu'une classe doit
// savoir faire, sans dire comment le faire. Ici le contrat est minimal :
// "tout logger doit savoir enregistrer un message".
// Programmer "par interface" permet au reste du code de dépendre de cette
// abstraction (ILogger) plutôt que d'une classe concrète précise. On peut alors
// brancher n'importe quelle implémentation (console, fichier, base de données)
// sans rien modifier ailleurs : c'est ce qui rend le code souple et testable.
public interface ILogger
{
    // Méthode du contrat : toute classe qui implémente ILogger doit fournir une
    // méthode Log(string). Une interface ne contient pas de corps : elle déclare
    // seulement la signature (le nom, les paramètres, le type de retour).
    void Log(string message);
}