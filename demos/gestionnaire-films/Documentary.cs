// Troisième classe fille de MediaItem : un documentaire, avec un type et un thème en plus.
// On voit ici que plusieurs classes peuvent hériter de la même classe mère, chacune avec ses ajouts.
public class Documentary : MediaItem
{
    // Propriété Type : ici le set n'a aucune validation, il stocke directement la valeur (exemple minimal d'encapsulation).
    private string _type;
    public string Type
    {
        get { return _type; }
        set { _type = value; }
    }

    // Propriété Theme : même principe que Type.
    private string _theme;
    public string Theme
    {
        get { return _theme; }
        set { _theme = value; }
    }

    // Propriété calculée en lecture seule (pas de set) : sa valeur est déduite à la volée d'un autre champ.
    // Elle renvoie vrai si le documentaire est une biographie, sans stocker de donnée supplémentaire.
    public bool IsBiography
    {
        get
        {
            return _type == "Biography";
        }
    }

    // Constructeur : délègue titre et année à la classe mère, puis renseigne le type et le thème.
    public Documentary(string title, int releaseYear, string type, string theme) : base(title, releaseYear)
    {
        Type = type;
        Theme = theme;
    }

    // Polymorphisme : version de Show() propre au documentaire (ordre et champs différents des autres médias).
    public override void Show()
    {
        Console.WriteLine($"{Title} - {Theme} - {ReleaseYear} - {Type}");
    }
}