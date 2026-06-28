// Un enum (énumération) définit un type dont les valeurs possibles sont une liste fixe et nommée.
// Ici, plutôt que de stocker le carburant sous forme de texte libre ("diesel", "Diesel", "diesl"...),
// on impose un ensemble fermé de choix valides. Le compilateur empêche d'écrire une valeur hors liste.
// Avantage pédagogique : un enum donne un nom lisible (Fuel.ELECTRIC) à une donnée qui, en mémoire,
// n'est qu'un simple entier.
public enum Fuel {
    // Chaque membre reçoit une valeur entière explicite. C'est utile pour deux raisons :
    // 1) la persistance : dans le fichier CSV on enregistre l'entier (ex. 2) Et non le mot ELECTRIC ;
    // 2) la stabilité : fixer les nombres à la main évite qu'ils changent si on réordonne la liste.
    DIESEL = 0,
    PETROL = 1,
    ELECTRIC = 2,
    HYDROGEN = 3,
    HYDROGENCELL = 4,
    HYBRID = 5,
    GPL = 6
}
