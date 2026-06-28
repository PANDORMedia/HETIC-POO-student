// BaseService regroupe le point commun de tous les services : chacun a besoin
// d'un logger pour tracer ce qu'il fait.
// Point clé de la démo : le service NE CRÉE PAS son logger lui-même (aucun
// "new ConsoleLogger()" ici). Il le REÇOIT de l'extérieur. C'est l'INVERSION DE
// DÉPENDANCE : la classe dépend de l'abstraction ILogger, et c'est l'appelant
// (ailleurs dans le programme) qui décide quelle implémentation concrète fournir.
// Pourquoi recevoir au lieu de créer ? Parce que créer son propre logger
// figerait le choix dans le béton (toujours la console, par exemple). En le
// recevant, le service reste réutilisable, configurable et facile à tester.
public class BaseService
{
    // Champ privé : seul l'intérieur de la classe peut y accéder directement.
    // Son type est ILogger (l'interface), surtout pas ConsoleLogger ou FileLogger :
    // le service ignore donc volontairement QUELLE sorte de logger il manipule.
    private ILogger _logger;

    // Propriété protected : accessible par cette classe ET ses sous-classes
    // (UserService, OrderService, ReportService), mais pas par le reste du code.
    // C'est de l'ENCAPSULATION : on expose un accès contrôlé au champ privé _logger.
    protected ILogger Logger
    {
        get
        {
            return _logger;
        }
        set { _logger = value;}
    }

    // Constructeur : c'est ICI que la dépendance entre dans l'objet. On parle
    // d'INJECTION PAR CONSTRUCTEUR (le logger est "injecté" à la création).
    // Avantage : on peut brancher n'importe quel ILogger (console, fichier, ou un
    // faux logger pour les tests) sans jamais modifier le code de BaseService.
    public BaseService(ILogger logger)
    {
        _logger = logger;
    }
}
// UserService HÉRITE de BaseService (le ": BaseService"). Il récupère donc le
// champ et la propriété Logger sans avoir à les réécrire (réutilisation par
// héritage). À noter la relation avec le logger : le service "a-un" logger
// (COMPOSITION), il n'"est-pas" un logger. La composition relie deux objets qui
// collaborent, sans imposer la lourde relation "est-un" de l'héritage.
public class UserService : BaseService
{
    // Le constructeur reçoit un ILogger et le transmet à la classe de base via
    // ": base(logger)". La dépendance remonte ainsi jusqu'à BaseService qui la stocke.
    public UserService(ILogger logger) : base(logger)
    {
    }

    // Méthode métier propre à UserService. Elle utilise Logger sans savoir s'il
    // écrit dans la console ou dans un fichier : elle parle au contrat (ILogger),
    // pas à une implémentation. C'est exactement ça, programmer par interface.
    public void Inscrire(string email)
    {
        Logger.Log($"Inscription : {email}");
    }
}

// OrderService suit exactement le même schéma : il reçoit son logger et l'utilise.
// Trois services différents partagent le même mécanisme grâce à BaseService, ce qui
// évite de dupliquer la gestion du logger dans chacun.
public class OrderService : BaseService
{
    public OrderService(ILogger logger) : base(logger)
    {
    }

    // Méthode métier : valider une commande. Le log sert à tracer l'action.
    public void Valider(int orderId)
    {
        Logger.Log($"Validation commande {orderId}");
    }
}

// ReportService : troisième exemple. Il montre que ce modèle se duplique sans
// effort pour autant de services qu'on veut, chacun restant indépendant du type
// concret de logger réellement utilisé.
public class ReportService : BaseService
{
    public ReportService(ILogger logger) : base(logger)
    {
    }

    // Méthode métier : générer un rapport. Le texte passé à Log est une chaine de
    // caractères applicative (du code), pas un commentaire : on n'y touche donc pas,
    // même s'il contient un accent.
    public void Generer()
    {
        Logger.Log("Génération du rapport");
    }
}