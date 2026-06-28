
// "abstract" signifie que cette classe sert de MODÈLE commun mais ne peut pas être instanciée
// directement : on ne peut pas écrire "new Vehicule(...)". Un véhicule "en général" n'existe pas,
// on loue toujours une voiture, une moto ou un van. La classe abstraite regroupe ce que TOUS les
// véhicules partagent (marque, modèle, tarif) et oblige les classes filles à compléter le reste.
public abstract class Vehicule
{
    // Champ privé : le mot-clé "private" cache cette variable de l'extérieur de la classe.
    // C'est le coeur de l'ENCAPSULATION : on ne touche pas directement à _brand depuis dehors,
    // on passe par la propriété Brand ci-dessous, qui peut alors contrôler les valeurs.
    private string _brand = null!; // Le "null!" permet d'indiquer au compilateur et à IntelliSense qu'on l'affecte d'une manière qu'il ne peut pas comprendre

    // Propriété publique : c'est la "porte d'entrée" contrôlée vers le champ privé _brand.
    public string Brand
    {
        // Le get (accesseur en lecture) renvoie simplement la valeur stockée.
        get { return _brand; }
        // Le set (accesseur en écriture) reçoit la nouvelle valeur dans le mot-clé spécial "value".
        // Ici on en profite pour VALIDER : on refuse une marque vide. C'est tout l'intérêt de
        // l'encapsulation, garantir qu'un objet ne peut jamais se retrouver dans un état invalide.
        set
        {
            if (string.IsNullOrEmpty(value))
                // Lever une exception interrompt l'affectation et signale l'erreur à l'appelant.
                throw new ArgumentException("Brand must have value");
            _brand = value;
        }
    }

    // Même schéma que pour Brand : champ privé + propriété qui valide la saisie.
    private string _model = null!;
    public string Model
    {
        get { return _model; }
        set
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Model must have value");
            _model = value;
        }
    }

    // Le tarif journalier. On utilise le type decimal (et non double) car il est conçu pour
    // les montants d'argent : il évite les petites erreurs d'arrondi propres aux nombres à virgule flottante.
    private decimal _dailyPrice;
    public decimal DailyPrice
    {
        get { return _dailyPrice; }
        set
        {
            // Règle métier : un tarif nul ou négatif n'a pas de sens, on le refuse.
            if (value <= 0)
                throw new ArgumentException("Price must be a positif decimal");
            _dailyPrice = value;
        }
    }

    // Propriété auto-implémentée : quand aucune validation n'est nécessaire, C# permet d'écrire
    // "{ get; set; }" et génère automatiquement le champ privé derrière. Plus court, même principe.
    public bool IsAvailable { get; set; }


    // Le CONSTRUCTEUR : méthode spéciale appelée à la création d'un objet (avec "new").
    // Son rôle est de mettre l'objet dans un état cohérent dès le départ. Remarque qu'il affecte
    // les PROPRIÉTÉS (Brand, Model, DailyPrice) et non les champs privés : ainsi les validations
    // des setters s'appliquent même pendant la construction.
    public Vehicule(string brand, string model, decimal price)
    {
        Brand = brand;
        Model = model;
        DailyPrice = price;
    }

    // Méthode CONCRÈTE (avec un corps) héritée telle quelle par toutes les classes filles.
    // Le calcul du coût est identique pour tous les véhicules, donc inutile de le redéfinir ailleurs.
    public decimal GetPrice(int nbDays)
    {
        return DailyPrice * nbDays;
    }

    // Méthode ABSTRAITE : déclarée sans corps. Elle n'impose pas de code, seulement une obligation.
    // Chaque classe fille DOIT fournir sa propre version de ShowDetails (sinon le code ne compile pas).
    // C'est ainsi qu'on force chaque type de véhicule à savoir afficher sa fiche à sa manière.
    public abstract void ShowDetails();

    // Méthode VIRTUELLE : elle fournit un comportement par défaut ("Marque Modele"), mais le mot-clé
    // "virtual" autorise une classe fille à le REMPLACER (voir "override" dans Motobike).
    // Différence clef avec abstract : ici redéfinir est optionnel, la version de base existe déjà.
    public virtual string GetShortDescription()
    {
        return $"{Brand} {Model}";
    }

}
