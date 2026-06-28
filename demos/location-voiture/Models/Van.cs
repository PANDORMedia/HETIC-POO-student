// Van hérite de Vehicule mais, contrairement à Car, n'implémente aucune interface.
// C'est volontaire : dans cette démo un van n'est ni assurable ni révisable. Cela montre que
// les capacités (interfaces) se choisissent classe par classe, alors que l'héritage transmet
// systématiquement tout ce que possède le parent.
public class Van : Vehicule
{
    // Donnée propre au van : le volume de chargement. Même schéma d'encapsulation que les autres.
    private int _volume;
    public int Volume
    {
        get { return _volume; }
        set
        {
            if (value > 0)
                _volume = value;
            else
                _volume = 1;
        }
    }

    // Le constructeur transmet marque, modèle et tarif au parent via "base(...)", puis fixe le volume.
    public Van(string brand, string model, int price, int volume) : base(brand, model, price)
    {
        Volume = volume;
    }

    // Obligation issue de la classe abstraite : Van fournit sa propre fiche, centrée sur le volume.
    public override void ShowDetails()
    {
        Console.WriteLine("--------------------------");
        Console.WriteLine($"Brand :\t\t {Brand}");
        Console.WriteLine($"Model :\t\t {Model}");
        Console.WriteLine($"Storage Volume :\t {Volume}");
        Console.Write($"Price (/day):\t {DailyPrice} ");

        string availableStr = IsAvailable ? "(This van is available)" : "(This van isn't available)";
        Console.WriteLine(availableStr);

        Console.WriteLine("--------------------------");
    }

}
