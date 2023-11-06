using Core.IService;
using FileManager.Services;
using ListManager;
using Log;
using Models;
using System.Configuration;

namespace Core.Service
{
    public class ServiceAdmin : IServiceAdmin
    {
        public string FilePathAdmin = ConfigurationManager.AppSettings["pathBDD"] + "admin.json";

        public FileWriter FileWriter = FileWriter.Instance;
        public JsonToolkit JsonTool = new JsonToolkit();
        public ServiceConsole AC = new ServiceConsole();
        public CustomLog Logger = new CustomLog();

        public Administrator AdminConnected;
        public BaseModelList<Administrator> AdminList = new BaseModelList<Administrator>();
        public ServiceArticle ArticleService = ServiceArticle.Instance;
        public ServiceItemsPurchased ItemsPurchasedService = ServiceItemsPurchased.Instance;
        public ServiceUser ServiceUser;

        public ServiceAdmin()
        {
            // Charge la liste des administrateurs depuis la BDD et écrit un message de log.
            Logger.WriteLog((LoadAdminList() ? "Réussi" : "Echec") + " : Chargement de la liste des administrateurs depuis la BDD", false);
        }

        public bool LoadAdminList()
        {
            // Charge la liste des administrateurs depuis un fichier de données.
            return AdminList.setlList(JsonTool.DeserializeAdministrator(FilePathAdmin));
        }

        public bool SaveAdminList()
        {
            // Sauvegarde la liste des administrateurs dans un fichier de données.
            return FileWriter.WriteToFile(FilePathAdmin, JsonTool.SerializeAdministrator(AdminList.GetList()));
        }

        public bool AdminMenu()
        {
            // Affiche un menu d'administration et effectue des actions en fonction du choix de l'administrateur.
            bool IsFinish = false;
            while (!IsFinish)
            {
                switch (AC.InputInt(AC.MenuAdmin()))
                {
                    case 1:
                        // Crée un nouvel article et écrit un message de log.
                        Logger.WriteLog((ArticleService.formArticle() ? "Création de l'article réussi" : "L'article existe déjà dans la BDD"), false);
                        break;
                    case 2:
                        // Génère une facture totale et écrit un message de log.
                        Logger.WriteLog((FactureTotal() ? "Création de la facture total réussi" : "Création de la facture total non possible"), false);
                        break;
                    case 3:
                        // Génère une facture par utilisateur et écrit un message de log.
                        Logger.WriteLog((FactureByUser() ? "Création de la facture par utilisateur réussi" : "Création de la facture par utilisateur non possible"), false);
                        break;
                    case 4:
                        // Génère une facture par date et écrit un message de log.
                        Logger.WriteLog((FactureByDate() ? "Création de la facture par date réussi" : "Création de la facture par date non possible"), false);
                        break;
                    default:
                        IsFinish = true;
                        break;
                }
            }
            return IsFinish;
        }

        public bool FormAdmin()
        {
            // Crée un nouvel administrateur.
            AdminConnected = new Administrator();
            AdminConnected.Login = AC.InputString("Veuillez entrer un login");
            AdminConnected.Password = AC.InputPasswordAdmin();
            if (AdminFindByPasswordAndLogin(AdminConnected.Password, AdminConnected.Login) != null)
            {
                // Si l'administrateur existe déjà, utilise l'administrateur existant.
                AdminConnected = AdminFindByPasswordAndLogin(AdminConnected.Password, AdminConnected.Login);
                // Sauvegarde la liste des administrateurs
                Logger.WriteLog((SaveAdminList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des administrateurs", true);
                return true;
            }
            else
            {
                // Sinon, ajoute le nouvel administrateur à la liste.
                AdminList.Add(AdminConnected);
                return false;
            }
        }

        public Administrator AdminFindByPasswordAndLogin(string Password, string Login)
        {
            // Recherche un administrateur par mot de passe et nom d'utilisateur.
            return AdminList.GetList().Find(u => u.Password == Password && u.Login == Login)!;
        }

        public bool FactureByDate()
        {
            // Génère une facture par date.
            DateTime Date = AC.InputDate();
            if (DateExist(Date))
            {
                string facture = ItemsPurchasedService.FactureByDate(Date);
                Console.WriteLine(facture);
                return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathFactures"] + "Facuture_By_Date_" + AC.FormatDate(DateTime.Now) + ".txt", facture);
            }
            Console.WriteLine("La date que vous avez entrée n'existe pas dans la liste des articles achetés");
            return false;
        }

        public bool FactureByUser()
        {
            // Génère une facture par utilisateur.
            List<User> Users = UserExistByName(AC.InputString("Veuillez entrer le nom de l'utilisateur recherché"));
            User User = ChoiceUser(Users);
            if (User != null)
            {
                string facture = ItemsPurchasedService.FactureByUser(User);
                Console.WriteLine(facture);
                return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathFactures"] + "Facuture_By_User_" + AC.FormatDate(DateTime.Now) + ".txt", facture);
            }
            Console.WriteLine("L'utilisateur que vous avez entré n'existe pas dans la liste des articles achetés");
            return false;
        }

        public bool FactureTotal()
        {
            // Génère une facture totale.
            string facture = ItemsPurchasedService.FactureTotal();
            Console.WriteLine(facture);
            return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathFactures"] + "Facuture_Total_" + AC.FormatDate(DateTime.Now) + ".txt", facture);
        }

        public bool DateExist(DateTime Date)
        {
            // Vérifie si une date existe dans la liste des articles achetés.
            if (ItemsPurchasedService.ItemsPurchasedeList.Find(u => u.DatePurchase.ToShortDateString() == Date.ToShortDateString()) != null)
            {
                return true;
            }
            return false;
        }

        public List<User> UserExistByName(string LastName)
        {
            // Recherche des utilisateurs par nom.
            List<User> ListUser = ServiceUser.UserList.GetList().FindAll(u => u.LastName.ToLower() == LastName.ToLower());
            return ListUser;
        }

        public User ChoiceUser(List<User> LU)
        {
            // Sélectionne un utilisateur à partir d'une liste.
            if (LU.Count > 1)
            {
                return AC.ChoiceUser(LU);
            }
            else if (LU.Count == 1)
            {
                return LU[0];
            }
            else
            {
                return null;
            }
        }

    }
}
