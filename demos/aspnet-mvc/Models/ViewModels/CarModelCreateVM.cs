using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    // ViewModel pour la création d'un modèle : reprend les champs saisissables dans le formulaire.
    public class CarModelCreateVM
    {
        [Required(ErrorMessage = "Le nom du modèle est obligatoire")]
        public string Name { get; set; } = string.Empty;

        // On exige un identifiant de marque valide (>= 1) : oblige à sélectionner une marque.
        [Range(1, int.MaxValue, ErrorMessage = "La marque est obligatoire")]
        public int CarBrandId { get; set; }

        public decimal DailyPrice { get; set; }

        public int NbSeats { get; set; }

        // Le type énuméré Fuel restreint les choix de carburant aux valeurs définies.
        public Fuel Fuel { get; set; }

    }
}
