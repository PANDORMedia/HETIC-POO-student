using System.ComponentModel.DataAnnotations;

namespace aspnet.Models.ViewModels
{
    // ViewModel (VM) = objet de transport entre la vue (le formulaire) et le contrôleur.
    // On sépare volontairement ce ViewModel de l'entité CarBrand : le formulaire n'expose
    // que les champs que l'utilisateur doit saisir, jamais toute l'entité (sécurité, clarté).
    public class CarBrandCreateVM
    {
        // L'attribut [Required] est une règle de validation déclarative : c'est lui que
        // ModelState.IsValid vérifie côté contrôleur. Le message s'affiche si le champ est vide.
        [Required(ErrorMessage = "Le nom de la marque est obligatoire")]
        public string Name { get; set; } = string.Empty;
    }
}
