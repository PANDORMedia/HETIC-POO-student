using System.Globalization;

// La classe Car illustre deux mécanismes à la fois :
// 1) l'héritage : "Car : Vehicule" signifie qu'une Car est un Vehicule et récupère gratuitement
//    Brand, Model, DailyPrice, GetPrice, etc. Sans les réécrire.
// 2) L'implémentation d'interfaces : les noms qui suivent (IAssurable, IServiceable, IExportable)
//    sont des contrats que Car s'engage à respecter. C# autorise une seule classe parente mais
//    plusieurs interfaces, ce qui permet de cumuler des capacités.
public class Car : Vehicule, IAssurable, IServiceable, IExportable
{
    // Champ spécifique à la voiture (le van et la moto ne l'ont pas) : encapsulé par une propriété.
    private int nbSeats;
    public int NbSeats
    {
        get { return nbSeats; }
        set
        {
            // Stratégie de validation différente du tarif : plutôt que de lever une exception,
            // on "corrige" une saisie incorrecte en repliant vers 1 place minimum.
            if (value > 0)
                nbSeats = value;
            else
                nbSeats = 1;
        }
    }

    // Propriété de type enum : le carburant ne peut prendre que les valeurs définies dans Fuel.
    public Fuel Fuel { get; set; }

    // Cette propriété satisfait le contrat IAssurable (qui exige InsuranceCategory).
    public string InsuranceCategory { get; set; } = null!;

    // Cette propriété satisfait le contrat IServiceable (qui exige KmSinceLastService).
    public int KmSinceLastService { get; set; }

    // Constructeur de Car. ": Base(brand, model, price)" appelle d'abord le constructeur de la
    // classe parente Vehicule pour initialiser la partie héritée, avant de régler les champs propres
    // à la voiture. C'est la façon d'éviter de redupliquer la logique d'initialisation du parent.
    public Car(string brand, string model, decimal price, int seats, Fuel fuel) : base(brand, model, price)
    {
        NbSeats = seats;
        Fuel = fuel;
    }
    // "Override" fournit l'implémentation obligatoire de la méthode abstraite ShowDetails du parent.
    // Concept de polymorphisme : quand on appelle car.ShowDetails() sur un Vehicule, c'est cette
    // version (spécifique à la voiture, avec sièges, carburant et assurance) qui s'exécute.
    public override void ShowDetails()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Brand :\t\t {Brand}");
        Console.WriteLine($"Model :\t\t {Model}");
        Console.WriteLine($"Seats :\t\t {NbSeats}");
        Console.WriteLine($"Fuel :\t\t {Fuel}");
        Console.Write($"Price (/day):\t {DailyPrice} ");

        // Opérateur ternaire : "condition ? ValeurSiVrai : valeurSiFaux", une écriture compacte du if/else.
        string availableStr = IsAvailable ? "(This car is available)" : "(This car isn't available)";
        Console.WriteLine(availableStr);

        Console.WriteLine($"Insurance :\t {GetInsurancePrice()} EUR");
        Console.WriteLine($"Last Service :\t {KmSinceLastService}km ago");
        Console.WriteLine("--------------------------");
    }

    // Implémentation imposée par le contrat IAssurable. Car décide ici de sa formule de prime.
    public decimal GetInsurancePrice()
    {
        return DailyPrice * 30;
    }

    // Implémentation imposée par IServiceable : faire la révision remet le compteur de km à zéro.
    public void PerformService()
    {
        KmSinceLastService = 0;
    }

    // Implémentation imposée par IServiceable : cumule les kilomètres parcourus.
    public void AddKilometers(int kms)
    {
        KmSinceLastService += kms;
    }

    // Implémentation imposée par IExportable : la voiture sait se sérialiser en une ligne CSV.
    // CultureInfo.InvariantCulture force le point comme séparateur décimal (et non la virgule),
    // sinon le decimal entrerait en conflit avec la virgule qui sépare déjà les colonnes du CSV.
    // On exporte le carburant via "(int) Fuel" : on stocke le numéro de l'enum, pas son nom.
    public string ExportAsCSV()
    {
        return $"{Brand},{Model},{DailyPrice.ToString(CultureInfo.InvariantCulture)},{NbSeats},{(int) Fuel}";
    }

    // Méthode statique : elle appartient à la classe Car elle-même, pas à une voiture en particulier.
    // On l'appelle donc via Car.Getcsvheader() sans avoir d'instance. Logique car l'entête du fichier
    // est la même pour toutes les voitures, ce n'est pas une donnée propre à un objet.
    public static string GetCSVHeader()
    {
        return "Brand,Model,DailyPrice,NbSeats,Fuel";
    }
}
