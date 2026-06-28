// Contrat minimal : "je sais me transformer en une ligne CSV".
// Une interface peut ne contenir qu'un seul membre. Ici elle isole la responsabilité
// "savoir s'exporter". L'outil csvwriter pourra dépendre de cette capacité et rester
// indépendant des détails internes de chaque classe exportée.
public interface IExportable
{
    // Retourne la représentation texte de l'objet au format CSV (une seule ligne).
    string ExportAsCSV();
}
