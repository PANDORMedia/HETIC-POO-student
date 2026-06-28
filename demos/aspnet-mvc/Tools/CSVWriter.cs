using System.Globalization;
using System.Threading.Tasks.Dataflow;
using aspnet.Exportables;
using aspnet.Models;

// Classe utilitaire (helper) qui regroupe la lecture et l'écriture de fichiers CSV.
// Toutes ses méthodes sont STATIQUES : on appelle CSVWriter.WriteToFile(...) sans créer d'objet,
// car ce service ne porte aucun état propre. C'est une boîte à outils, pas une entité du domaine.
public class CSVWriter
{
    // Écrit la liste des voitures dans le fichier cars.csv. Renvoie false en cas d'échec.
    public static bool WriteToFile(List<ExportableCar> cars)
    {
        Console.WriteLine("Writing to file...");
        try
        {
            // "using" garantit que le fichier est correctement fermé et libéré à la fin du bloc,
            // même si une exception survient (gestion déterministe des ressources).
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "cars.csv")))
            {
                // On écrit d'abord la ligne d'en-tête, obtenue via la méthode statique de la classe.
                outputFile.WriteLine(ExportableCar.GetCSVHeader());
                foreach (var car in cars)
                {
                    // Chaque voiture produit sa propre ligne CSV (méthode imposée par IExportable).
                    outputFile.WriteLine(car.ExportAsCSV());
                }
            }
        }
        catch (Exception e)
        {
            // Gestion d'erreur : on capture l'exception, on l'affiche et on signale l'échec.
            Console.WriteLine(e);
            return false;
        }
        return true;
    }

    // Lit le fichier cars.csv et reconstruit une liste d'objets ExportableCar.
    public static List<ExportableCar> ReadFile()
    {
        Console.WriteLine("Reading from file...");
        List<ExportableCar> tempList = new List<ExportableCar>();

        using (StreamReader file = new StreamReader(Path.Combine(Environment.CurrentDirectory, "cars.csv")))
        {
            // On saute la première ligne, qui contient l'en-tête et non des données.
            file.ReadLine();

            // On parcourt le fichier ligne par ligne jusqu'à la fin du flux.
            while (!file.EndOfStream)
            {
                string line = file.ReadLine();
                // Split découpe la ligne en colonnes à chaque virgule.
                string[] values = line.Split(',');
                // int.Parse convertit le texte lu en entier (conversion / parsing).
                ExportableCar temp = new ExportableCar()
                {
                    Id = int.Parse(values[0]),
                    // Brand = values[1], //Brand
                    // Model = values[2], //Model
                    // NbSeats = int.Parse(values[3])
                };

                tempList.Add(temp);
            }
        }

        return tempList;
    }
}
