// Une énumération (enum) définit un ensemble fermé de valeurs nommées.
// Plutôt que de stocker un entier ou une chaîne libre pour le carburant, on contraint
// la valeur à l'une de ces constantes : le code est plus lisible et les erreurs de saisie évitées.
// Chaque nom est associé à un entier sous-jacent (Petrol vaut 0, Diesel vaut 1, etc.).
public enum Fuel
{
    Petrol = 0,
    Diesel = 1,
    Electric = 2,
    Hybrid = 3,
    HybridCell = 4,
    GPL = 5,
}
