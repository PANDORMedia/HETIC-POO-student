using System;
using System.Globalization;

namespace CarRental
{
    class Program
    {
        // Catalogue partagé de l'application : la liste de toutes les voitures en mémoire.
        // "static" car il appartient à la classe Program et est utilisé par ses méthodes statiques
        // (Main, AddCar, ListCars, RemoveCar) sans qu'on crée d'instance de Program.
        static List<Car> cars = new List<Car>();

        // Main est le POINT D'ENTREE du programme : c'est la première méthode exécutée au lancement.
        static void Main(string[] args)
        {
            // Au démarrage, on recharge le catalogue depuis le fichier CSV (persistance entre deux exécutions).
            // C'est l'outil CSVWriter qui gère la lecture : Program ne s'occupe pas du détail des fichiers.
            cars = CSVWriter.ReadFile();
            // Vehicule v = new Vehicule("Tesla", "Model 3", 200);

            // v.ShowDetails();

            // Vehicule v2 = new Vehicule("Audi", "A4", 160);
            // v2.ShowDetails();

            // Car c1 = new Car("Tesla", "Model 3", 200, 5, Fuel.ELECTRIC);

            // Motobike b1 = new Motobike("Yamaha", "R1", 200, 1000);

            // Van v1 = new Van("Ford", "Transit", 70, 9);

            // List<Vehicule> vehicules = new List<Vehicule>() { c1, b1, v1 };

            // foreach(var v in vehicules)
            // {
            //     v.ShowDetails();
            // }

            // Console.WriteLine($"Price for 5 days : {c1.GetPrice(5)}");
            // Console.WriteLine(c1.GetShortDescription());
            // Console.WriteLine(b1.GetShortDescription());

            Console.WriteLine("Car Rental v0.1");
            Console.WriteLine("----------------------");

            // Drapeau (booléen) qui maintient la boucle active tant qu'on ne veut pas quitter.
            bool run = true;

            // BOUCLE DE MENU : coeur d'un programme console interactif. Tant que run vaut true,
            // on réaffiche le menu, on lit le choix de l'utilisateur, puis on réagit en conséquence.
            while (run)
            {
                // Menu() affiche les options et renvoie un MenuAction (enum) selon la saisie.
                // Le switch aiguille vers le bon traitement. Travailler sur l'enum plutôt que sur des
                // nombres bruts rend chaque "case" auto-explicatif.
                switch (Menu())
                {
                    case MenuAction.AddCar:
                        // On construit une nouvelle voiture (saisie guidée), on l'ajoute au catalogue,
                        var car = AddCar();
                        cars.Add(car);
                        // on affiche sa fiche pour confirmation,
                        car.ShowDetails();
                        // puis on resauvegarde immédiatement le fichier pour ne rien perdre.
                        CSVWriter.WriteToFile(cars);
                        break;
                    case MenuAction.RemoveCar:
                        RemoveCar();
                        break;
                    case MenuAction.ListCars:
                        ListCars();
                        break;
                    case MenuAction.Exit:
                        // Environment.Exit(0) arrête le programme immédiatement (0 = fin sans erreur).
                        Environment.Exit(0);
                        break;
                    // Remarque : MenuAction.ShowMenu n'a pas de case, donc une saisie invalide ne
                    // déclenche rien et la boucle réaffiche simplement le menu au tour suivant.
                }
            }


        }

        // Affiche le menu, lit la saisie et la traduit en une valeur de l'enum MenuAction.
        public static MenuAction Menu()
        {
            Console.WriteLine("What do you want to do ?");
            Console.WriteLine("1. Add Car");
            Console.WriteLine("2. Remove Car");
            Console.WriteLine("3. List Cars");
            Console.WriteLine("4. Repeat Menu");
            Console.WriteLine("5. Exit");

            var response = 0;

            // int.TryParse tente de convertir le texte saisi en entier SANS lever d'exception :
            // si l'utilisateur tape autre chose qu'un nombre, response reste à 0 et le programme continue.
            int.TryParse(Console.ReadLine(), out response);

            // L'utilisateur voit des choix de 1 à 5, mais l'enum commence à 0 : on décale donc de 1
            // pour faire correspondre la saisie aux valeurs de MenuAction.
            response--;

            // Garde-fou : si le numéro dépasse la dernière action valide (Exit), on renvoie ShowMenu,
            // ce qui revient à "ne rien faire et réafficher le menu". On transforme ainsi une saisie
            // hors limites en comportement sûr plutôt qu'en erreur.
            if (response > (int)MenuAction.Exit)
            {
                return MenuAction.ShowMenu;
            }
            else
            {
                // Transtypage de l'entier vers l'enum : (MenuAction)2 devient MenuAction.ListCars.
                return (MenuAction)response;
            }

        }

        // Construit une voiture en interrogeant l'utilisateur champ par champ.
        public static Car AddCar()
        {
            // On part d'une voiture "brouillon" avec des valeurs provisoires valides.
            // Les vraies valeurs seront ensuite affectées via les propriétés, ce qui déclenche
            // leur validation. Le constructeur exige des valeurs non vides, d'où ces "temp" et 1.
            Car temp = new Car("temp", "temp", 1, 1, Fuel.DIESEL);

            Console.WriteLine("----- ADD CAR -----");

            // PATRON DE SAISIE ROBUSTE, répété pour chaque champ ci-dessous :
            // "while (true)" boucle indéfiniment, et on ne sort (break) qu'après une saisie valide.
            // Si le setter de la propriété lève une ArgumentException (valeur refusée), le catch
            // affiche le message et la boucle redemande la saisie sans planter le programme.
            // C'est l'intérêt d'avoir centralisé la validation dans les propriétés du modèle :
            // le menu n'a qu'à attraper l'exception, il ne revérifie pas les règles lui-même.
            while (true)
            {
                try
                {
                    Console.Write("Brand : ");
                    temp.Brand = Console.ReadLine();
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Model : ");
                    temp.Model = Console.ReadLine();
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (true)
            {
                try
                {
                    Console.Write("Number of seats : ");
                    int nbSeats = -1;

                    // Ici la saisie peut ne pas être un nombre du tout : on le détecte avec TryParse
                    // et on lève nous-même une ArgumentException pour rejouer la boucle proprement.
                    if (!int.TryParse(Console.ReadLine(), out nbSeats))
                        throw new ArgumentException("Nb seats must be >= 1");

                    temp.NbSeats = nbSeats;
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            while (true)
            {
                try
                {
                    int tempFuel = -1;

                    // On affiche les carburants numérotés pour que l'utilisateur en choisisse un.
                    Console.WriteLine("Fuel ? ");
                    Console.WriteLine("1. Diesel");
                    Console.WriteLine("2. Petrol");
                    Console.WriteLine("3. Electric");
                    Console.WriteLine("4. Hydrogen");
                    Console.WriteLine("5. Hydrogen Cell");
                    Console.WriteLine("6. Hybrid");
                    Console.WriteLine("7. GPL");

                    if (!int.TryParse(Console.ReadLine(), out tempFuel))
                        throw new ArgumentException("Value must be between 1 and 7");

                    // Même décalage que pour le menu : l'utilisateur tape 1 à 7, l'enum va de 0 à 6.
                    tempFuel--;

                    // On vérifie que le nombre tombe bien dans la plage valide de l'enum Fuel,
                    // bornée par sa première (DIESEL) et sa dernière (GPL) valeur. Si oui, on convertit
                    // l'entier en valeur d'enum ; sinon on rejette la saisie.
                    if (tempFuel >= (int)Fuel.DIESEL && tempFuel <= (int)Fuel.GPL)
                    {
                        temp.Fuel = (Fuel)tempFuel;
                    }
                    else
                        throw new ArgumentException("Value must be between 1 and 7");
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            while (true)
            {
                try
                {
                    Console.Write("Daily Price : ");
                    decimal dailyPrice = -1;

                    // On lit le tarif en InvariantCulture pour rester cohérent avec le format du CSV
                    // (point décimal). Si la conversion échoue, on relance la saisie via l'exception.
                    if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out dailyPrice))
                        throw new ArgumentException("Price must be a positive number");

                    // L'affectation passe par la propriété DailyPrice, qui refusera un tarif <= 0.
                    temp.DailyPrice = dailyPrice;
                    break;
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            // A ce stade, tous les champs ont été saisis et validés : on retourne la voiture prête.
            return temp;

        }

        // Affiche le catalogue. Les deux paramètres ont une VALEUR PAR DÉFAUT (= false) : on peut
        // appeler ListCars() sans argument, ou ListCars(true, true) pour une liste courte numérotée.
        // Cela évite d'écrire plusieurs méthodes différentes pour des affichages proches.
        public static void ListCars(bool shortList = false, bool numbered = false)
        {
            Console.WriteLine("---- ALL CARS ----");
            int index = 0;
            // On parcourt chaque voiture du catalogue.
            foreach (var car in cars)
            {
                index++;
                if (shortList)
                {
                    // Mode court : juste marque et modèle, avec un numéro si numbered est vrai.
                    string prep = numbered ? $"{index}. " : "";
                    Console.WriteLine($"{prep}{car.Brand} {car.Model}");
                }
                else
                    // Mode détaillé : on délègue à la fiche complète de la voiture (polymorphisme).
                    car.ShowDetails();
            }
        }

        // Supprime une voiture choisie par son numéro dans la liste.
        public static void RemoveCar()
        {
            Console.WriteLine("---- REMOVE CAR ---- ");
            Console.WriteLine("Which car do you want to delete ?");
            // On réutilise ListCars en mode court et numéroté pour montrer les choix possibles.
            ListCars(true, true);

            while (true)
            {
                try
                {
                    Console.Write("> ");
                    int removal = -1;

                    if (!int.TryParse(Console.ReadLine(), out removal))
                        throw new Exception("Please enter a numerical value");

                    // L'affichage est numéroté à partir de 1, mais une liste est indexée à partir de 0.
                    removal--;
                    // RemoveAt enlève l'élément à cet index. Si l'index est hors de la liste,
                    // elle lève une ArgumentOutOfRangeException, gérée juste en dessous.
                    cars.RemoveAt(removal);

                    Console.WriteLine("Car Removed");
                    ListCars(true, true);

                    break;

                }
                // Plusieurs catch permettent de réagir différemment selon le TYPE d'erreur.
                // Le plus précis (index hors limites) doit être placé AVANT le plus général (Exception),
                // car C# teste les catch dans l'ordre et s'arrête au premier qui correspond.
                catch(ArgumentOutOfRangeException e)
                {
                    Console.WriteLine("Please chose a valid value");
                }
                catch (Exception e)
                {
                    // Filet de sécurité : attrape toute autre erreur imprévue (ex. saisie non numérique).
                    Console.WriteLine(e);
                }
            }
        }
    }
}
