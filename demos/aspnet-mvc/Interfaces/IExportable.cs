// Petite interface à une seule méthode : elle décrit une CAPACITÉ ("être exportable en CSV").
// N'importe quelle classe peut implémenter cette capacité, indépendamment de sa hiérarchie.
// C'est un exemple de programmation orientée comportement (proche du principe de ségrégation
// des interfaces : des contrats petits et ciblés).
public interface IExportable
{
    // Toute classe exportable devra savoir produire sa représentation sous forme de ligne CSV.
    string ExportAsCSV();
}
