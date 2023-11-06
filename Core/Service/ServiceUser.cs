using FileManager.Services;
using ListManager;
using Log;
using Models;
using System.Configuration;

namespace Core.Service
{
    public class ServiceUser : IServiceUser
    {

        public FileWriter FileWriter = FileWriter.Instance;
        public JsonToolkit JsonTool = new JsonToolkit();
        public ServiceConsole AC = new ServiceConsole();
        public CustomLog Logger = new CustomLog();

        public User UserConnected;
        public BaseModelList<User> UserList = new BaseModelList<User>();

        public ServiceArticle ArticleService = ServiceArticle.Instance;
        public ServiceItemsPurchased ItemsPurchasedService = ServiceItemsPurchased.Instance;

        public ServiceUser()
        {
            // Charge la liste des utilisateurs depuis la base de données
            Logger.WriteLog((LoadUserList() ? "Réussi" : "Echec") + " : Chargement de la liste des utilisateurs depuis la BDD", false);
        }

        public bool LoadUserList()
        {
            return UserList.setlList(JsonTool.Deserialize(ConfigurationManager.AppSettings["pathBDD"] + "user.json"));
        }

        public bool SaveUserList()
        {
            return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathBDD"] + "user.json", JsonTool.Serialize(UserList.GetList()));
        }

        public bool FormUser()
        {
            // Crée un nouvel utilisateur et collecte des informations le concernant
            UserConnected = new User();
            UserConnected.LastName = AC.InputString("Veuillez entrer un nom");
            UserConnected.FirstName = AC.InputString("Veuillez entrer un prénom");
            UserConnected.PhoneNumber = AC.InputPhoneNumber();
            UserConnected.Adress = AC.InputAdress();

            // Vérifie si l'utilisateur existe déjà dans la liste
            if (!UserExist())
            {
                UserList.Add(UserConnected);
                // Sauvegarde la liste des utilisateurs
                Logger.WriteLog((SaveUserList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des utilisateurs", true);
                Console.WriteLine(AC.PrintArticleList(ArticleService.ArticleList));
                return false;
            }
            Console.WriteLine(AC.PrintArticleList(ArticleService.ArticleList));
            return true;
        }

        public bool UserExist()
        {
            User foundUser = UserList.GetList().Find(u => u.LastName.ToLower() == UserConnected.LastName.ToLower() && u.FirstName.ToLower() == UserConnected.FirstName.ToLower())!;
            if (foundUser != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UserMenu()
        {
            bool isFinish = false;
            while (!isFinish)
            {
                // Affiche une ligne de séparation
                Console.WriteLine(AC.FactureSeparation());

                // Affiche le menu utilisateur et obtient le choix de l'utilisateur sous forme de chaîne de caractères
                string choice = AC.InputString(AC.UserMenu());

                // Affiche une ligne de séparation
                Console.WriteLine(AC.FactureSeparation());

                int number;

                if (int.TryParse(choice, out number))
                {
                    // Si le choix est un nombre entier valide
                    if (number >= 0 && number < ArticleService.ArticleList.GetList().Count)
                    {
                        // Ajoute un article au panier de l'utilisateur et enregistre le résultat dans les journaux
                        Logger.WriteLog((ItemsPurchasedService.AddItemOnBasket(UserConnected, number) ? "Réussi" : "Echec") +
                            " : Ajout d'un article dans le panier (index : " + number + ")", false);
                    }
                    else
                    {
                        Console.WriteLine("Attention! Le nombre que vous avez entré ne correspond pas à un article dans la liste!");
                    }
                }
                else
                {
                    // Si le choix n'est pas un nombre entier, utilise une instruction `switch` pour gérer les choix de l'utilisateur
                    switch (choice.ToLower())
                    {
                        case "f":
                            // Crée une facture utilisateur et enregistre le résultat dans les journaux
                            Logger.WriteLog(FactureUser() ?
                                "Création de la facture réussie à l'emplacement : " + ConfigurationManager.AppSettings["pathFacturesClient"] + @"\" +
                                UserConnected.LastName + "_" + UserConnected.FirstName + @"\" + AC.UserNameFile(UserConnected, DateTime.Today) :
                                "Création de la facture non réussie", true);
                            break;
                        case "p":
                            // Affiche le montant du panier de l'utilisateur
                            Console.WriteLine("Montant de votre panier : " + ItemsPurchasedService.PriceItemsPurchasedList(ItemsPurchasedService.Basket) + "€");
                            break;
                        case "a":
                            // Affiche la liste des articles disponibles
                            Console.WriteLine(AC.PrintArticleList(ArticleService.ArticleList));
                            break;
                        default:
                            // Réinitialise le panier de l'utilisateur et quitte le menu
                            ItemsPurchasedService.Basket = new List<ItemsPurchased>();
                            isFinish = true;
                            break;
                    }
                }
            }
            return isFinish;
        }

        public bool FactureUser()
        {
            // Génère une facture pour les articles achetés par l'utilisateur
            string facture = ItemsPurchasedService.validBasket(UserConnected);
            Console.WriteLine(facture);

            // Enregistre la facture dans un fichier
            return FileWriter.WriteFactureUser(ConfigurationManager.AppSettings["pathFacturesClient"] + @"\" + UserConnected.LastName + "_" + UserConnected.FirstName, AC.UserNameFile(UserConnected, DateTime.Now), facture);
        }
    }
}
