// Interface "diffusable en streaming" : contrat pour les médias disponibles sur une plateforme.
// Remarque : le nom de l'interface (IStreamable) peut différer du nom du fichier (IStreamble.cs),
// le compilateur se base sur le nom du type, pas sur celui du fichier.
public interface IStreamable
{
    // Propriété imposée : la plateforme de diffusion (ex : "netflix").
    string Plateforme {get; set;}
    // Comportements imposés : savoir si le média est disponible et construire son lien de lecture.
    bool IsAvailableOnStream();
    string GetStreamUrl();
}