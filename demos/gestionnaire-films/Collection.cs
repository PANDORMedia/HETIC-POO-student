// Catalogue polymorphe,
// Créez une classe Collection qui :

// Contient une List<MediaItem>
// A une méthode Add(MediaItem media) qui empêche les doublons de titre
// A une méthode GetStats() retournant le nombre de films, séries et documentaires
// Bonus : 
// A une méthode FindByType<T>() qui retourne tous les médias d'un type donné (a quoi correspond le <T> ?)

// Collection est un catalogue polymorphe : elle stocke des MediaItem.
// Grâce à l'héritage, la même liste peut contenir des Movie, des tvshow et des Documentary,
// puisque tous sont des MediaItem. C'est l'intérêt du polymorphisme pour ranger des objets de types variés.
public class Collection
{
    // Champ privé : la liste réelle des médias, protégée de l'extérieur (encapsulation).
    // List<MediaItem> est déjà un exemple de générique fourni par .NET : List<T> sait contenir n'importe quel type T.
    private List<MediaItem> _items = new List<MediaItem>();
    // Propriété en lecture seule : on expose la liste sans permettre de la remplacer par une autre.
    public List<MediaItem> Items
    {
        get { return _items; }
    }

    // Ajoute un média en empêchant les doublons de titre.
    // FirstOrDefault parcourt la liste et renvoie le premier élément au même titre, ou null si aucun.
    // (X => x.Title == item.Title) est une expression lambda : une mini-fonction de test appliquée à chaque élément.
    public void Add(MediaItem item)
    {
        if(Items.FirstOrDefault(x => x.Title == item.Title) == null)
            Items.Add(item);
        else
            throw new Exception("Item already present in collection");
    }

    // Compte combien d'éléments appartiennent à chaque type concret.
    // "X is Movie" teste le type réel de l'objet à l'exécution : encore du polymorphisme, on interroge la vraie nature de chaque média.
    public void GetStats()
    {
        int nbMovies = Items.Count(x => x is Movie);
        int nbTVShows = Items.Count(x => x is TVShow);
        int nbDocs = Items.Count(x => x is Documentary);

        Console.WriteLine($"Collection has {nbMovies} movies, {nbTVShows} shows and {nbDocs} docs.");
    }

    // Méthode générique : le <T> est un type "joker" choisi par l'appelant (ex : FindByType<Movie>()).
    // La contrainte "where T : MediaItem" limite T aux classes qui héritent de MediaItem.
    // OfType<T>() ne garde que les éléments du type demandé. Avantage : une seule méthode marche
    // pour Movie, tvshow ou Documentary, sans dupliquer le code pour chaque type.
    public List<T> FindByType<T>() where T: MediaItem
    {
        return Items.OfType<T>().ToList();
    }


}