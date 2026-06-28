namespace aspnet.Models;

// ViewModel dédié à la page d'erreur : un objet simple qui porte les données à afficher.
public class ErrorViewModel
{
    // Le "?" indique un type nullable : RequestId peut valoir null.
    public string? RequestId { get; set; }

    // Propriété calculée à corps d'expression (=>) : vraie seulement si un identifiant existe.
    // La vue s'en sert pour décider d'afficher ou non l'identifiant de requête.
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
