using System.Reflection.Metadata.Ecma335;
using Models;
using ListManager;
using System.Collections.Generic;
using Log;
using System.Configuration;
using FileManager.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Service
{
    public class CoreTerminal : ICoreTerminal
    {
        public FileWriter FileWriter = FileWriter.Instance;
        public JsonToolkit JsonTool = new JsonToolkit();
        public ServiceConsole AC = new ServiceConsole();

        public ServiceAdmin ServiceAdmin;
        public ServiceUser ServiceUser;
        public ServiceArticle ArticleService = ServiceArticle.Instance;
        public ServiceItemsPurchased ItemsPurchasedService = ServiceItemsPurchased.Instance;

        public CustomLog Logger = new CustomLog();

        public CoreTerminal()
        {
            // Vérifie l'existence des fichiers de données et les crée si nécessaire
            FilesExistsAndCreate();
            // Charge les données de base de données initiales
            LoadBDD();
            // Efface la console
            Console.Clear();
            bool IsFinsish = false;
            while (!IsFinsish)
            {
                // Demande à l'utilisateur de choisir s'il veut se connecter en tant qu'administrateur, utilisateur, ou quitter
                switch (AC.InputInt("Connexion en tant que : \n 1 : Administrateur \n 2 : Utilisateur \n 3 : Quitter"))
                {
                    case 1:
                        // Tentative de connexion ou d'inscription de l'administrateur
                        Logger.WriteLog(ServiceAdmin!.FormAdmin() ? "Connexion de l'administrateur" : "Inscription de l'administrateur", true);
                        // Associe le service utilisateur à l'administrateur
                        ServiceAdmin.ServiceUser = ServiceUser!;
                        // Affiche le menu de l'administrateur
                        ServiceAdmin!.AdminMenu();
                        // Enregistre la déconnexion de l'administrateur
                        Logger.WriteLog("Déconnexion de l'administrateur", true);
                        break;
                    case 2:
                        // Tentative de connexion ou d'inscription de l'utilisateur
                        Logger.WriteLog(ServiceUser!.FormUser() ? "Connexion de l'utilisateur" : "Inscription de l'utilisateur", true);
                        // Affiche le menu de l'utilisateur
                        ServiceUser!.UserMenu();
                        // Enregistre la déconnexion de l'utilisateur
                        Logger.WriteLog("Déconnexion de l'utilisateur", true);
                        break;
                    default:
                        // L'utilisateur a choisi de quitter
                        IsFinsish = true;
                        break;
                }
            }
            // Sauvegarde toutes les données de base de données
            SaveAll();
        }

        public bool FilesExistsAndCreate()
        {
            // Vérifie si le fichier de sauvegarde des administrateurs n'existe pas, et le crée le cas échéant
            if (!FileWriter.FileExiste(ConfigurationManager.AppSettings["pathBDD"] + "admin.json"))
            {
                Logger.WriteLog((FileWriter.CreateFile(ConfigurationManager.AppSettings["pathBDD"] + "admin.json") ? "Réussi" : "Echec") + " : Création du fichier de sauvegarde de la liste des administrateurs", true);
            }

            // Vérifie si le fichier de sauvegarde des utilisateurs n'existe pas, et le crée le cas échéant
            if (!FileWriter.FileExiste(ConfigurationManager.AppSettings["pathBDD"] + "user.json"))
            {
                Logger.WriteLog((FileWriter.CreateFile(ConfigurationManager.AppSettings["pathBDD"] + "user.json") ? "Réussi" : "Echec") + " : Création du fichier de sauvegarde de la liste des utilisateurs", true);
            }

            // Vérifie si le fichier de sauvegarde des articles n'existe pas, et le crée le cas échéant
            if (!FileWriter.FileExiste(ConfigurationManager.AppSettings["pathBDD"] + "articles.json"))
            {
                Logger.WriteLog((FileWriter.CreateFile(ConfigurationManager.AppSettings["pathBDD"] + "articles.json") ? "Réussi" : "Echec") + " : Création du fichier de sauvegarde de la liste des articles", true);
            }

            // Vérifie si le fichier de sauvegarde des articles achetés n'existe pas, et le crée le cas échéant
            if (!FileWriter.FileExiste(ConfigurationManager.AppSettings["pathBDD"] + "articles_achetes.json"))
            {
                Logger.WriteLog((FileWriter.CreateFile(ConfigurationManager.AppSettings["pathBDD"] + "articles_achetes.json") ? "Réussi" : "Echec") + " : Création du fichier de sauvegarde de la liste des articles achetés", true);
            }

            return true;
        }

        public bool LoadBDD()
        {
            // Initialise le service administrateur
            ServiceAdmin = new ServiceAdmin();
            // Initialise le service utilisateur
            ServiceUser = new ServiceUser();
            // Charge la liste des articles depuis la base de données
            ArticleService.LoadArticleList();
            // Charge la liste des articles achetés depuis la base de données
            ItemsPurchasedService.LoadItemsPurchasedList();
            return true;
        }

        public bool SaveAll()
        {
            // Sauvegarde la liste des administrateurs
            Logger.WriteLog((ServiceAdmin.SaveAdminList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des administrateurs", true);
            // Sauvegarde la liste des utilisateurs
            Logger.WriteLog((ServiceUser.SaveUserList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des utilisateurs", true);
            // Sauvegarde la liste des articles
            Logger.WriteLog((ArticleService.SaveArticleList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles", true);
            // Sauvegarde la liste des articles achetés
            Logger.WriteLog((ItemsPurchasedService.SaveItemsPurchasedList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles achetés", true);
            return true;
        }
    }
}