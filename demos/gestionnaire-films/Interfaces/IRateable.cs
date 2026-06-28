// INTERFACE "notable" : contrat pour tout média auquel on peut attribuer une note et un commentaire.
// Une classe peut signer PLUSIEURS interfaces à la fois (voir Movie qui en implémente trois).
public interface IRateable
{
    // Une interface peut aussi exiger des PROPRIÉTÉS (ici une note et un commentaire),
    // pas seulement des méthodes. La classe devra fournir le get et le set.
    int Rating {get; set;}
    string Comment {get; set;}
    // Comportements attendus : noter le média et afficher son commentaire.
    void Rate(int rating);
    void GetComment();
}