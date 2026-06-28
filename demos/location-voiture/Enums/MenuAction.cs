// Cet enum représente les actions possibles du menu console.
// Concept clef : on remplace des "nombres magiques" (0, 1, 2...) par des noms parlants.
// Dans Program.cs, le switch teste MenuAction.AddCar plutôt que "case 0", ce qui rend la
// boucle de menu beaucoup plus lisible et moins sujette aux erreurs.
public enum MenuAction
{
    // Les valeurs partent de 0 car elles correspondent aux choix du menu une fois décalés :
    // l'utilisateur tape 1 à l'écran, le code soustrait 1, et retombe sur ces entiers.
    AddCar = 0,
    RemoveCar = 1,
    ListCars = 2,
    ShowMenu = 3,
    Exit = 4,
}
