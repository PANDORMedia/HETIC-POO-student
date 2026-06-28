// Une interface est un contrat : elle liste ce qu'une classe doit savoir faire,
// sans dire comment le faire (aucun corps de méthode ici, juste les signatures).
// "Serviceable" = révisable. Toute classe qui déclare implémenter IServiceable
// s'engage à fournir ces trois membres. Concept POO : séparer la capacité (être révisable)
// du type concret. On pourra ainsi manipuler "quelque chose de révisable" sans savoir
// si c'est une voiture, un van, etc.
public interface IServiceable
{
    // Une propriété peut figurer dans un contrat. La classe devra l'exposer en lecture et écriture.
    int KmSinceLastService {get; set;}

    // Méthode à implémenter : effectuer la révision (remettra le compteur à zéro côté classe).
    void PerformService();

    // Méthode à implémenter : ajouter des kilomètres parcourus depuis la dernière révision.
    void AddKilometers(int kms);
}
