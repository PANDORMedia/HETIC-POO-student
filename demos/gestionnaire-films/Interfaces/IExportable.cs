// Interface : c'est un contrat. Elle liste des méthodes sans les implémenter.
// Toute classe qui écrit ": IExportable" promet de fournir ces méthodes.
// Avantage : on peut traiter de la même façon tout objet "exportable", peu importe sa classe réelle.
public interface IExportable
{
    // Les méthodes d'une interface n'ont pas de corps : seule la signature (nom + retour + paramètres) est imposée.
    // La classe qui signe le contrat devra écrire le code concret de ExportCSV et ExportJSON.
    string ExportCSV();
    string ExportJSON();

    // Méthode statique avec implémentation par défaut (possible depuis C# 8).
    // "Statique" signifie qu'elle appartient au type lui-même, pas à un objet précis.
    static string GetHeaderCSV() => throw new NotImplementedException();
}