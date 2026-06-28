// # Exercice 5 : Mini-défi : classe MovieCollection
// Crée une classe MovieCollection avec :

// Un champ privé films de type **List<Film>** (utilise System.Collections.Generic)
// Une méthode Add(Movie m) qui ajoute un film
// Une méthode All() qui affiche tous les films
// Une méthode FindByYear(int year) qui retourne tous les films de cette année
// Une méthode MostRecent() qui retourne le film le plus récent
// Teste avec 5+ films.

// MovieCollection est une version plus simple, SPÉCIALISÉE pour les films uniquement.
// Contrairement à Collection (qui accepte tout MediaItem), celle-ci ne contient que des Movie.
// Bon exemple pour comparer une collection générique à une collection dédiée à un seul type.
public class MovieCollection
{
    // Champ privé : la liste des films. Le type List<Movie> garantit qu'on ne peut y ranger QUE des films.
    private List<Movie> movies = new List<Movie>();

    // Ajoute simplement un film à la liste (ici sans contrôle de doublon).
    public void Add(Movie m)
    {
        movies.Add(m);
    }

    // Affiche tous les films. "var" laisse le compilateur deviner le type de movie (ici Movie).
    // L'appel movie.Show() reste polymorphe : c'est la version Show() de Movie qui s'exécute.
    public void All()
    {
        Console.WriteLine("All movies : ");
        foreach(var movie in movies)
        {
            Console.Write($"- ");
            movie.Show();
        }
    }

    // Renvoie tous les films d'une année donnée.
    // Where filtre la liste selon une condition (lambda), ToList transforme le résultat en nouvelle liste.
    public List<Movie> FindByYear(int year)
    {
        return movies.Where(m => m.ReleaseYear == year).ToList();
    }

    // Le type de retour Movie? (avec le point d'interrogation) signifie "un Movie OU null"
    // (au cas où la liste serait vide). OrderBy trie par année, FirstOrDefault prend le premier élément.
    public Movie? MostRecent()
    {
        return movies.OrderBy(x => x.ReleaseYear).FirstOrDefault();
    }

}