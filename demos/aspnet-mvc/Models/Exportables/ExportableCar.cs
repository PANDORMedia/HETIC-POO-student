using aspnet.Models;

namespace aspnet.Exportables
{
    // Exemple d'héritage combiné à un contrat : ExportableCar hérite de Car (classe de base)
    // et implémente l'interface IExportable. Elle est donc "une Car" qui possède en plus
    // la capacité d'être exportée en CSV. En C# on n'hérite que d'une seule classe, mais on
    // peut implémenter plusieurs interfaces.
    public class ExportableCar : Car, IExportable
    {
        // Constructeur sans paramètre. ": Base()" appelle explicitement le constructeur de Car.
        public ExportableCar() : base()
        {

        }
        // Constructeur de copie : construit une ExportableCar à partir d'une Car existante.
        // On recopie ici les données utiles depuis l'objet source c.
        public ExportableCar(Car c)
        {
            this.Id = c.Id;
            // This.Brand = c.Brand;
            // this.Model = c.Model;
            // this.NbSeats = c.NbSeats;

        }
        // Implémentation concrète imposée par l'interface IExportable.
        // L'interpolation $"{...}" construit la ligne CSV à partir des propriétés de l'objet.
        public string ExportAsCSV()
        {
        return $"{Id}";//,{Brand},{Model},{NbSeats}";
        }

        // Méthode statique : elle appartient à la classe, pas à une instance précise.
        // On l'appelle via ExportableCar.Getcsvheader() pour obtenir la ligne d'en-tête du fichier.
        public static string GetCSVHeader()
        {
            return "Id,Brand,Model,NbSeats";
        }
    }
}
