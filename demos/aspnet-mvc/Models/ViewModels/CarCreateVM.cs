using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{

    // ViewModel pour la création d'une voiture : ne contient que la plaque et le modèle choisi.
    public class CarCreateVM
    {
        [Required(ErrorMessage = "La plaque est obligatoire")]
        public string PlateNumber { get; set; } = string.Empty;

        // [Range] impose une valeur entre 1 et int.MaxValue : un identifiant valide est >= 1,
        // donc 0 (aucun modèle sélectionné) est refusé comme invalide.
        [Range(1, int.MaxValue, ErrorMessage = "Le modèle est obligatoire")]
        public int CarModelId { get; set; }
    }
}
