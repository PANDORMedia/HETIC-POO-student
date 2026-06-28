// Entité représentant une marque (par exemple Renault, BMW).
public class CarBrand
{
    // Clé primaire de la marque.
    public int Id { get; set; }
    // Nom de la marque.
    public string Name { get; set; }


    // Relation un-vers-plusieurs : une marque possède plusieurs modèles.
    // "= new ()" initialise la liste vide pour éviter une référence nulle (NullReferenceException).
    public List<CarModel> CarModels { get; set; } = new ();
}
