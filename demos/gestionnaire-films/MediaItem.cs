// Classe ABSTRAITE : elle sert de modèle commun à tous les médias (films, séries, documentaires).
// Le mot-clé "abstract" interdit de créer directement un MediaItem (new MediaItem(...) est impossible).
// On ne peut instancier que ses classes filles. C'est la base de l'HÉRITAGE : tout ce qui est commun
// à un film, une série ou un documentaire est écrit une seule fois ici, puis réutilisé par les enfants.
public abstract class MediaItem
{
    // ENCAPSULATION : le champ est privé (_title). Personne ne peut le modifier directement depuis l'extérieur.
    // Le souligné au début du nom (_title) est une convention pour signaler "champ privé interne".
    private string _title;
    // La PROPRIÉTÉ Title est la porte d'entrée publique vers le champ privé _title.
    // get : permet de lire la valeur. set : permet de l'écrire, mais en passant par une VALIDATION.
    public string Title {
        get
        {
            return _title;
        }

       set
        {
            // Règle métier : un titre vide ou ne contenant que des espaces est refusé.
            // "value" est le mot-clé qui représente la valeur reçue lors de l'affectation (ex : item.Title = "...").
            // En levant une exception, on garantit qu'un MediaItem ne peut JAMAIS exister avec un titre invalide.
            if(string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Invalid Title");
            _title = value;
        }
    }
    // Même principe d'encapsulation pour l'année de sortie : champ privé + propriété publique avec contrôle.
    private int _releaseYear;
    public int ReleaseYear
    {
        get
        {
            return _releaseYear;
        }

        set
        {
            // Validation : 1888 est l'année du plus vieux film connu, et on tolère jusqu'à 5 ans dans le futur
            // (pour les sorties annoncées). Toute autre valeur est considérée comme une erreur.
            if(value < 1888 || value > DateTime.Now.Year + 5)
                throw new ArgumentException("Invalid Date");

            _releaseYear = value;
        }
    }


    // Méthode ABSTRAITE : déclarée ici mais SANS corps (pas de { ... }).
    // Elle oblige chaque classe fille à fournir sa propre version de Show().
    // C'est le moteur du POLYMORPHISME : on sait que tout MediaItem sait s'afficher,
    // mais chaque type (Movie, TVShow, Documentary) décidera COMMENT il s'affiche.
    public abstract void Show();

    // CONSTRUCTEUR de la classe de base : il reçoit le titre et l'année communs à tous les médias.
    // En affectant via les propriétés Title et ReleaseYear (et non les champs _title/_releaseYear),
    // on déclenche automatiquement les validations définies plus haut dès la création de l'objet.
    public MediaItem(string title, int releaseYear)
    {
        Title = title;
        ReleaseYear = releaseYear;
    }
}