// # Créer la classe Utilisateur
// Crée un fichier Utilisateur.cs avec :

// Champs :
// - nom (string)
// - email (string)
// - Un champ filmsPreferes de type Film[] (tableau de 10 max)
// - Un champ nombreFilms (int) pour suivre le nombre de films ajoutés
// - Une méthode AjouterFilm(Film f) qui ajoute un film au tableau (si pas plein)
// - Une méthode AfficherListe() qui affiche tous les films préférés
// - Crée un utilisateur, ajoute les 3 films de l'exercice 1 et affiche sa liste.

// Classe User : représente un utilisateur et ses films préférés.
// Attention pédagogique : ici les champs sont publics (pas d'encapsulation), c'est volontairement
// plus simple que MediaItem. On voit ainsi le contraste avec les propriétés validées vues ailleurs.
public class User
{
    public string name;
    public string email;

    // Tableau de taille fixe : exactement 10 cases réservées en mémoire dès le départ.
    // À comparer avec List<Movie> (plus bas en commentaire) qui, elle, grandit toute seule.
    public Movie[] favoriteMovies = new Movie[10];
    //Public List<Movie> movies = new List<Movie>();

    // Compteur du nombre de films réellement ajoutés (le tableau a 10 cases mais elles ne sont pas toutes remplies).
    public int numberOfMovies = 0;

    // Ajoute un film au tableau, mais seulement s'il reste de la place (maximum 10).
    public void AddMovie(Movie m)
    {
        // Si nombre de films inferieur à 10
        if(numberOfMovies < 10)
        {
            // J'ajoute le film
            favoriteMovies[numberOfMovies] = m;
            numberOfMovies++;
        }
    }

    // Affiche la liste des films préférés. On boucle de 0 jusqu'au nombre réel de films
    // (et non jusqu'à 10) pour ne pas afficher les cases encore vides du tableau.
    // FavoriteMovies[i].Show() rappelle le polymorphisme : chaque film s'affiche via sa propre méthode Show().
    public void ShowList()
    {
        Console.WriteLine($"Films préférés de {name} :");
        for(int i = 0; i < numberOfMovies; i++)
        {
            Console.Write($"{i + 1}. ");
            favoriteMovies[i].Show();
        }
    }
    // Constructeur : "this.name" désigne le champ de l'objet, "name" le paramètre reçu.
    // Le mot-clé this lève l'ambiguïté quand les deux portent le même nom.
    public User(string name, string email)
    {
        this.name = name;
        this.email = email;
    }
}