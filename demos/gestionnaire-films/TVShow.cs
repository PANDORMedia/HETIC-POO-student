// TVShow hérite aussi de MediaItem : une série est un média, avec en plus un nombre de saisons.
// Elle réutilise Title et ReleaseYear et ajoute sa propre donnée spécifique (NbSeasons).
public class TVShow : MediaItem
{
    // Champ privé + propriété : encapsulation du nombre de saisons avec validation (doit être strictement positif).
    private int _nbSeasons;
    public int NbSeasons {
        get { return _nbSeasons; }
        set
        {
            if(value > 0)
                _nbSeasons = value;
            else
                throw new ArgumentException("Seasons must be postive value");
        }
    }

    // POLYMORPHISME : TVShow fournit sa propre version de Show(), différente de celle de Movie.
    // Un même appel media.Show() affichera un format adapté au type réel de l'objet.
    public override void Show()
    {
        Console.WriteLine($"{Title} - {ReleaseYear} - {NbSeasons}");
    }

    // Constructeur : il délègue le titre et l'année à la classe mère via base(...), puis fixe le nombre de saisons.
    public TVShow(string title, int releaseYear, int nbSeasons) : base(title, releaseYear)
    {
        NbSeasons = nbSeasons;
    }
}