using System.Globalization;
using System.Threading.Tasks.Dataflow;

// Csvwriter illustre la séparation des responsabilités : la persistance sur disque (lire et écrire
// le fichier) est regroupée ici, à l'écart des classes métier (Vehicule, Car...) et du menu (Program).
// Chaque partie du code a ainsi un seul rôle. Si demain on change de format de stockage, on ne
// modifie que cette classe. Toutes ses méthodes sont statiques car csvwriter est un simple outil :
// il n'a pas d'état propre à mémoriser, on l'utilise via csvwriter.ReadFile() / csvwriter.WriteToFile().
public class CSVWriter
{
    // Sauvegarde la liste des voitures dans le fichier. Retourne true si tout s'est bien passé.
    public static bool WriteToFile(List<Car> cars)
    {
        // Try/catch : écrire sur le disque peut échouer (fichier verrouillé, droits manquants...).
        // On entoure donc l'opération pour ne pas faire planter tout le programme en cas de problème.
        try
        {
            // "Using" garantit que le fichier sera correctement fermé et vidé (flush) à la fin
            // du bloc, même si une erreur survient. C'est essentiel pour ne pas laisser le fichier ouvert.
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Environment.CurrentDirectory, "cars.csv")))
            {
                // On écrit d'abord la ligne d'entête (les noms de colonnes), via la méthode statique de Car.
                outputFile.WriteLine(Car.GetCSVHeader());
                // Puis une ligne par voiture. C'est chaque Car qui sait s'exporter (ExportAsCSV),
                // l'outil ne connaît pas les détails internes : il s'appuie sur la capacité IExportable.
                foreach (var car in cars)
                {
                    outputFile.WriteLine(car.ExportAsCSV());
                }
            }
        }
        catch (Exception e)
        {
            // En cas d'échec, on affiche l'erreur et on signale l'échec par false plutôt que de crasher.
            Console.WriteLine(e);
            return false;
        }
        return true;
    }

    // Recharge le catalogue depuis le fichier au démarrage : opération inverse de WriteToFile.
    public static List<Car> ReadFile()
    {
        // On construit une liste vide qu'on va remplir au fil de la lecture.
        List<Car> tempList = new List<Car>();

        // Même principe que pour l'écriture : "using" ferme proprement le flux de lecture à la fin.
        using(StreamReader file = new StreamReader(Path.Combine(Environment.CurrentDirectory, "cars.csv")))
        {
            // Première ligne lue mais ignorée : c'est l'entête (Brand,Model,...), pas une donnée.
            file.ReadLine();

            // On lit ligne par ligne jusqu'à la fin du fichier.
            while(!file.EndOfStream)
            {
                string line = file.ReadLine();
                // Split(',') découpe la ligne CSV en un tableau de valeurs, une par colonne.
                string[] values = line.Split(',');
                // On reconstruit un objet Car à partir du texte : c'est la désérialisation.
                // Chaque colonne (texte) doit être reconvertie vers le bon type avec Parse :
                Car temp = new Car(
                    values[0], //Brand
                    values[1], //Model
                    // InvariantCulture en lecture aussi, pour interpréter le point comme séparateur décimal.
                    decimal.Parse(values[2], NumberStyles.Number, CultureInfo.InvariantCulture), //Price
                    int.Parse(values[3]), // NbSeats
                    // L'entier stocké est reconverti vers l'enum Fuel par un transtypage "(Fuel)".
                    (Fuel) int.Parse(values[4])); // Fuel

                tempList.Add(temp);
            }
        }

        return tempList;
    }
}
