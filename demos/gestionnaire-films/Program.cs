using System;

namespace MyApp
{
    class Program
    {
        // Main est le POINT D'ENTRÉE du programme : c'est la première méthode exécutée au lancement.
        // On y assemble tous les objets pour montrer concrètement chaque concept POO.
        static void Main(string[] args)
        {
            // Création d'un objet (instanciation) avec "new" : le constructeur de Movie est appelé ici.
            Movie movie1 = new Movie("Batman Begins", 2005, "Christopher Nolan", "Action");

            // On utilise les propriétés et méthodes venant des interfaces (IStreamable, IRateable).
            movie1.Plateforme = "netflix";
            movie1.Rate(5);
            movie1.Comment = "C'était chouette";

            // Appel polymorphe : c'est la version Show() définie dans Movie qui s'exécute.
            movie1.Show();


            Movie movie2 = new Movie("Inception", 2010, "Christopher Nolan", "Science-Fiction");

            movie2.Show();

            // movie1 et movie2 sont deux objets DIFFÉRENTS en mémoire : la comparaison renvoie donc False.
            // (Par défaut, == sur des objets compare les références, pas le contenu.)
            Console.WriteLine($"{movie1 == movie2}");

            Movie movie3 = new Movie("The Dark Knight",  2008, "Chistopher Nolan", "Action");

            movie3.Show();

            Movie movie4 = new Movie("The Dark Knight Rises", 2012, "Christopher Nolan", "Action");

            movie4.Show();

            // Création d'une série : même démarche, mais TVShow a son propre constructeur (avec le nombre de saisons).
            TVShow tvs1 = new TVShow("The Big Bang Theory", 2007, 12);

            tvs1.Show();

            // Un utilisateur et ses films préférés : on remplit son tableau via AddMovie puis on affiche la liste.
            User u = new User("Sean", "sean@pandor.media");
            u.AddMovie(movie1);
            u.AddMovie(movie2);
            u.AddMovie(movie3);
            u.AddMovie(movie4);

            u.ShowList();


            // Appel des méthodes métier propres à Movie pour comparer deux films.
            Console.WriteLine($"movie2.IsMoreRecent(movie1) : {movie1.IsMoreRecent(movie2)}");

            Console.WriteLine($"movie2.IsSamePeriod(movie1) : {movie2.IsSamePeriod(movie1)}");
            // Création de deux documentaires : la troisième classe fille de MediaItem.
            Documentary d1 = new Documentary("Steve Jobs", 2012, "Biography", "La vie de Jobs");
            Documentary d2 = new Documentary("Les Pigeons à Paris", 2015, "Animal", "La vie des pigeons à Paris");


            Console.WriteLine("----------------");
            // DÉMONSTRATION CLÉ du polymorphisme : une seule liste contient des objets de TROIS types différents.
            // C'est possible car Movie, TVShow et Documentary héritent tous de MediaItem.
            List<MediaItem> mediaItems = new List<MediaItem>() { movie1, movie2, movie3, tvs1, d1, d2};

            foreach(var media in mediaItems) {

                // Le pattern "media is Movie movie" teste le type réel ET crée une variable typée (movie) si ça marche.
                // On ajoute juste un préfixe d'affichage selon le type, sans avoir touché aux méthodes Show().
                if(media is Movie movie)
                {
                    Console.Write("Film : ");
                    movie.Show();
                } else if( media is TVShow show)
                {
                    Console.Write("Série : ");
                    show.Show();
                } else if (media is Documentary doc)
                {
                    Console.Write("Documentaire : ");
                    doc.Show();
                }
                // En utilisant le mot clef "is".. préposez le "Show()" d'un "Film :", "Documentaire :" ou "Serie :
                // "Film : Batman - Tim Burton - 1989 - Action"

                // Sans modifier la méthode Show...
            }

           
            Console.WriteLine("----------------");

            // MediaItem mediaItem = new Movie("Batman", 1989, "Tim Burton", "Action");

            // mediaItem.Show();
            // Console.WriteLine(mediaItem.GetType());

            // On range tous les médias dans le catalogue polymorphe Collection.
            Collection c = new Collection();
            c.Add(movie1);
            c.Add(movie2);
            c.Add(tvs1);
            c.Add(d1);
            c.Add(d2);

            // GetStats compte chaque type présent dans la collection.
            c.GetStats();


            // Appel de la méthode GÉNÉRIQUE : FindByType<Movie>() ne renvoie que les films de la collection.
            // En remplaçant <Movie> par <TVShow> ou <Documentary>, on filtrerait l'autre type sans changer le code de la méthode.
            var foundItems = c.FindByType<Movie>();

            foreach(var item in foundItems)
            {
                item.Show();
                // Comme item est un Movie, il dispose des comportements des interfaces (streaming, note, export).
                if(item.IsAvailableOnStream())
                {
                    Console.WriteLine($"Available on {item.Plateforme} : {item.GetStreamUrl()}");
                }

                item.GetComment();

                // GetHeaderCSV est statique : on l'appelle sur la classe Movie, pas sur l'objet item.
                Console.Write(Movie.GetHeaderCSV());
                // Export du même film dans deux formats différents, grâce au contrat IExportable.
                Console.WriteLine(item.ExportCSV());
                Console.WriteLine(item.ExportJSON());
            }


        }
    }
}