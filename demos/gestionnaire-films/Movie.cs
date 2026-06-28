// La classe Movie illustre plusieurs concepts d'un coup :
// - héritage : "Movie : MediaItem" signifie que Movie hérite de MediaItem.
//   Movie récupère donc gratuitement Title, ReleaseYear et l'obligation de définir Show().
// - Interfaces : après MediaItem, les noms IStreamable, IRateable, IExportable sont des contrats
//   que Movie s'engage à respecter. Une classe n'hérite que d'une seule classe mère,
//   mais peut signer autant d'interfaces qu'elle veut (ici trois).
public class Movie : MediaItem, IStreamable, IRateable, IExportable
{
    // Champs privés : l'encapsulation protège le réalisateur et le genre du film.
    private string _director;
    private string _genre;
    // Propriété Director : encore une validation dans le set pour refuser un réalisateur vide.
    public string Director
    {
        get { return _director; }
        set {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid Director");
            _director = value;
        }
    }
    // Propriété Genre : même logique de contrôle à l'écriture.
    public string Genre {
        get { return _genre; }
        set {
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid Genre");
            _genre = value;
        }
    }

    // Propriété auto-implémentée : écriture courte "{ get; set; }" quand aucune validation n'est nécessaire.
    // Le compilateur crée tout seul le champ privé caché derrière. Plateforme satisfait l'interface IStreamable.
    public string Plateforme { get; set; }
    // Propriété Rating exigée par l'interface IRateable. Ici le set ne lève pas d'exception :
    // il corrige la valeur pour la garder entre 0 et 5 (une autre façon d'encapsuler une règle métier).
    private int _rating;
    public int Rating
    {
        get { return _rating; }
        set {
            if(value < 0) value = 0;
            if(value > 5) value = 5;
             _rating = value;
        }
    }

    // Commentaire libre de l'utilisateur (exigé par IRateable).
    public string Comment { get; set; }

    // Constructeur paramétré du film.
    // ": Base(title, releaseYear)" appelle d'abord le constructeur de la classe mère MediaItem,
    // qui se charge de valider et stocker le titre et l'année. Ensuite seulement on remplit
    // les propriétés propres au film (Director, Genre). C'est la chaîne de construction de l'héritage.
    public Movie(string title, int releaseYear, string director,  string genre) : base(title, releaseYear)
    {
        Director = director;
        Genre = genre;
    }

    // "Override" redéfinit la méthode abstraite Show() héritée de MediaItem.
    // C'est le polymorphisme concret : un Movie sait s'afficher à sa manière (titre, réalisateur, année, genre).
    public override void Show()
    {
        Console.WriteLine($"{Title} - {Director} - {ReleaseYear} - {Genre}");
    }

    // Méthode métier spécifique au film : "this" désigne le film courant, "m" l'autre film comparé.
    // Retourne vrai si le film courant est sorti après l'autre.
    public bool IsMoreRecent(Movie m)
    {
        return this.ReleaseYear > m.ReleaseYear;
    }

    // Retourne vrai si les deux films sont sortis à moins de 5 ans d'écart (même période).
    public bool IsSamePeriod(Movie m)
    {
        return Math.Abs(this.ReleaseYear - m.ReleaseYear) <= 5;
    }

    // Implémentation du contrat IStreamable : disponible en streaming si une plateforme a été renseignée.
    public bool IsAvailableOnStream()
    {
        return !string.IsNullOrEmpty(Plateforme);
    }

    // Suite du contrat IStreamable : construit l'URL de lecture à partir de la plateforme et du titre.
    // Si aucune plateforme n'est définie, on refuse de fabriquer un lien invalide en levant une exception.
    public string GetStreamUrl()
    {
        if(IsAvailableOnStream())
        {
            return $"https://{Plateforme}.com/play/{Title.Replace(' ', '-')}";
        } else
        {
            throw new Exception("No Platform Set");
        }
    }

    // Implémentation du contrat IRateable : noter le film. La validation 0 à 5 est faite par le set de Rating.
    public void Rate(int rating)
    {
        Rating = rating;
    }

    // Implémentation du contrat IRateable : afficher le commentaire de l'utilisateur.
    public void GetComment()
    {
        Console.WriteLine(Comment);
    }

    // Implémentation du contrat IExportable : produit une ligne au format CSV (valeurs séparées par des virgules).
    public string ExportCSV()
    {
        return $"{Title},{ReleaseYear},{Director},{Genre}";
    }

    // Implémentation du contrat IExportable : produit le même film au format JSON.
    // Le @$"..." combine chaîne interpolée ($) et chaîne verbatim (@) ; les doubles accolades {{ }} affichent une seule accolade.
    public string ExportJSON()
    {
        return @$"{{
            ""title"" : ""{Title}"",
            ""releaseDate"" : {ReleaseYear},
            ""director"" : ""{Director}"",
            ""genre"" : ""{Genre}""
        }}";
    }


    // Méthode statique : elle appartient à la classe Movie, pas à un film précis.
    // On l'appelle donc avec Movie.GetHeaderCSV() et non avec un objet. Elle fournit la ligne d'en-tête du CSV.
    public static string GetHeaderCSV()
    {
        return "title,releaseDate,director,genre\n";
    }

}
