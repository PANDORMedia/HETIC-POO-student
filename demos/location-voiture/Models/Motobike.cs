// Troisième classe fille de Vehicule. Elle ajoute la cylindrée et, surtout, montre comment
// REDÉFINIR une méthode virtuelle du parent (voir GetShortDescription plus bas).
public class Motobike : Vehicule
{
    // Donnée spécifique à la moto : la cylindrée du moteur, encapsulée comme d'habitude.
    private int _engineSize;
    public int EngineSize
    {
        get { return _engineSize; }
        set
        {
            if (value > 0)
                _engineSize = value;
            else
                _engineSize = 1;
        }
    }

    // Constructeur : délègue l'initialisation commune au parent puis enregistre la cylindrée.
    public Motobike(string brand, string model, int price, int engineSize) : base(brand, model, price)
    {
        EngineSize = engineSize;
    }

    // Implémentation obligatoire de la méthode abstraite ShowDetails, version moto.
    public override void ShowDetails()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Brand :\t\t {Brand}");
        Console.WriteLine($"Model :\t\t {Model}");
        Console.WriteLine($"Engine Size :\t\t {EngineSize}");
        Console.Write($"Price (/day):\t {DailyPrice} ");

        string availableStr = IsAvailable ? "(This bike is available)" : "(This bike isn't available)";
        Console.WriteLine(availableStr);

        Console.WriteLine("--------------------------");
    }

    // Ici on REDÉFINIT (override) la méthode virtuelle GetShortDescription du parent.
    // Astuce importante : "base.GetShortDescription()" rappelle la version d'origine ("Marque Modele")
    // pour la réutiliser, puis on l'enrichit avec la cylindrée. On étend le comportement parent
    // au lieu de tout réécrire. Car et Van, eux, gardent la version par défaut héritée.
    public override string GetShortDescription()
    {
        return base.GetShortDescription() + $" ({EngineSize}cc)";
    }

}
