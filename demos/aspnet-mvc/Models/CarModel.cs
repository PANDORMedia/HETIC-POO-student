using aspnet.Models;

// Entité représentant un modèle de voiture (par exemple Clio, Serie 3).
// Un modèle se situe entre la marque (CarBrand) et la voiture concrète (Car).
public class CarModel

{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    // Clé étrangère vers la marque à laquelle ce modèle appartient.
    public int CarBrandId { get; set; }  // Foreign key to CarBrand

    // decimal est choisi pour un prix : plus précis que double pour des montants monétaires.
    public decimal DailyPrice { get; set; }

    public int NbSeats { get; set; }

    // Propriété typée par une énumération (voir Enums/Fuel.cs) : seules quelques valeurs sont permises.
    public Fuel Fuel { get; set; }
    // Côté "plusieurs-vers-un" : chaque modèle renvoie vers sa marque (propriété de navigation).
    public CarBrand CarBrand { get; set; } = null!; // Navigation property to CarBrand

    // Côté "un-vers-plusieurs" : un modèle peut avoir plusieurs voitures physiques.
    public List<Car> Cars { get; set; } = new ();
}
