using Core.ServicesAffichagesConsole;
using ListManager;
using Models;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Service
{
    public class ServiceConsole : IServiceConsole
    {
        public ServiceConsole() { }

        public int InputInt(string content)
        {
            int Resultat = 0;
            bool isValid = false;

            // Affiche le contenu passé en paramètre
            Console.WriteLine(content);

            while (!isValid || Resultat < 0)
            {
                // Lire l'entrée de l'utilisateur
                string input = Console.ReadLine()!;

                // Tente de convertir l'entrée en un entier
                isValid = int.TryParse(input, out Resultat);

                // Si la conversion échoue ou que le résultat est négatif, affiche un message d'erreur
                if (!isValid || Resultat < 0)
                {
                    Console.WriteLine("Vous devez entrer un nombre entier positif !");
                }
            }

            return Resultat;
        }

        public float InputFloat(string content)
        {
            string Input = ""; float Resultat;

            // Affiche le contenu passé en paramètre
            Console.WriteLine(content);

            Input = Console.ReadLine()!;
            bool IsValid = float.TryParse(Input, out Resultat);

            while (!IsValid && Resultat >= 0)
            {
                // Si la conversion échoue ou que le résultat est négatif, affiche un message d'erreur
                Console.WriteLine("Vous devez entrer un nombre réel positif!");
                Input = Console.ReadLine()!;
            }

            return Resultat;
        }

        public string InputString(string content)
        {
            // Affiche le contenu passé en paramètre
            Console.WriteLine(content);

            // Lit et renvoie l'entrée utilisateur
            return Console.ReadLine()!;
        }

        public string InputPasswordAdmin()
        {
            bool MotDePasseValide = false; string Password = "";

            // Affiche un message demandant un mot de passe avec des conditions
            Console.WriteLine("Veuillez entrer un mot de passe (il doit contenir au moins 6 caractères alphanumériques et un caractère spécial)");

            while (!MotDePasseValide)
            {
                // Lit le mot de passe de l'utilisateur
                Password = Console.ReadLine()!;

                // Vérifie si le mot de passe respecte les conditions
                if (!Regex.IsMatch(Password, @"^(?=.*[A-Za-z0-9])(?=.*[^A-Za-z0-9]).{6,}$") || Password.Length < 6)
                {
                    // Affiche un message d'erreur en cas de mot de passe invalide
                    Console.WriteLine("Mot de passe incorrect (il doit contenir au moins 6 caractères alphanumériques et un caractère spécial), essayez un autre mot de passe :");
                }
                else
                {
                    // Si le mot de passe est valide, sort de la boucle
                    MotDePasseValide = true;
                }
            }

            return Password;
        }

        public string InputAdress()
        {
            bool AdressValid = false; string Adress = "";

            // Demande à l'utilisateur d'entrer une adresse
            Console.WriteLine("Veuillez entrer une adresse");

            while (!AdressValid)
            {
                // Lit l'adresse de l'utilisateur
                Adress = Console.ReadLine()!;

                // Vérifie si l'adresse est valide en utilisant une expression régulière
                if (!Regex.IsMatch(Adress, @"^[0-9]+\s+[A-Za-z0-9\s]+$"))
                {
                    // Affiche un message d'erreur en cas d'adresse invalide
                    Console.WriteLine("Adresse invalide! Veuillez entrer une adresse valide :");
                }
                else
                {
                    // Si l'adresse est valide, sort de la boucle
                    AdressValid = true;
                }
            }

            return Adress;
        }

        public string InputPhoneNumber()
        {
            bool PhoneNumberValid = false; string PhoneNumber = "";

            // Demande à l'utilisateur d'entrer un numéro de téléphone (10 chiffres)
            Console.WriteLine("Veuillez entrer un numéro de téléphone (10 chiffres)");

            while (!PhoneNumberValid)
            {
                // Lit le numéro de téléphone de l'utilisateur
                PhoneNumber = Console.ReadLine()!;

                // Vérifie si le numéro de téléphone est valide en utilisant une expression régulière
                if (!Regex.IsMatch(PhoneNumber, @"^\d{10}$"))
                {
                    // Affiche un message d'erreur en cas de numéro de téléphone invalide
                    Console.WriteLine("Numéro de téléphone invalide! Veuillez entrer un numéro de téléphone valide (10 chiffres) :");
                }
                else
                {
                    // Si le numéro de téléphone est valide, sort de la boucle
                    PhoneNumberValid = true;
                }
            }

            return PhoneNumber;
        }


        public DateTime InputDate()
        {
            DateTime Date = DateTime.Today;
            bool ValidInput = false;

            // Demande à l'utilisateur d'entrer une date au format JJ/MM/AAAA
            while (!ValidInput)
            {
                Console.Write("Veuillez entrer une date au format JJ/MM/AAAA : ");
                string Input = Console.ReadLine()!;

                // Tente de convertir l'entrée en une date avec le format spécifié
                if (DateTime.TryParseExact(Input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out Date))
                {
                    // Si la conversion réussit, la date est valide
                    ValidInput = true;
                }
                else
                {
                    // Affiche un message d'erreur en cas de format de date invalide
                    Console.WriteLine("Format de date invalide. Assurez-vous de respecter le format JJ/MM/AAAA.");
                }
            }

            return Date;
        }

        public string UserMenu()
        {
            // Retourne un menu utilisateur sous forme de chaîne de caractères
            return "Si vous voulez : \n Ajouter un article dans votre : Tapez le numéro de l'article voulu \n" +
                "Finaliser votre commande : Tapez F\n" +
                "Voir le prix de votre panier : Tapez P\n" +
                "Afficher la liste des articles : Tapez A\n" +
                "Quitter : Tapez Q ou autre \n";
        }

        public string PrintArticleList(BaseModelList<Article> ArticleList)
        {
            StringBuilder Sb = new StringBuilder();
            Sb.Append("Liste des articles :\n");
            int Index = 0;

            // Parcourt la liste d'articles et les ajoute au texte
            foreach (Article Article in ArticleList.GetList())
            {
                Sb.Append($"Index : {Index}, Référence : {Article.Reference}, Prix : {Article.Price}€\n");
                Index++;
            }

            return Sb.ToString();
        }

        public string MenuAdmin()
        {
            // Retourne un menu administrateur sous forme de chaîne de caractères
            return "Vous voulez : \n1 : Ajouter un article \n" +
                "2 : Créer une facture donnant la somme des articles vendus \n" +
                "3 : Créer une facture donnant la somme des articles vendus par acheteurs \n" +
                "4 : Créer une facture donnant la somme des articles vendus par date d'achat \n" +
                "5 ou plus : Quitter \n";
        }

        public string EnteteFacture()
        {
            // Retourne l'en-tête d'une facture sous forme de chaîne de caractères
            return "****************************************************************************************\n" +
                      "*****************************           FACTURE            *****************************\n" +
                      "****************************************************************************************\n";
        }

        public string FactureSeparation()
        {
            // Retourne une ligne de séparation dans une facture sous forme de chaîne de caractères
            return "----------------------------------------------------------------------------------------\n";
        }


        public string FactureUserPart(User User)
        {
            // Crée un objet StringBuilder pour construire la partie des informations du client dans une facture
            StringBuilder Sb = new StringBuilder();

            // Ajoute le nom du client
            Sb.Append("Nom du client : " + User.LastName + "\n");

            // Ajoute le prénom du client
            Sb.Append("Prénom du client : " + User.FirstName + "\n");

            // Ajoute l'adresse du client
            Sb.Append("Adresse du client : " + User.Adress + "\n");

            // Ajoute le numéro de téléphone du client
            Sb.Append("Téléphone du client : " + User.PhoneNumber + "\n");

            // Renvoie les informations du client sous forme de chaîne de caractères
            return Sb.ToString();
        }

        public User ChoiceUser(List<User> LU)
        {
            // Affiche la liste des utilisateurs en utilisant la méthode AfficherListUser
            Console.WriteLine(AfficherListUser(LU));

            int Index;
            Console.WriteLine("Tapez le numéro de l'utilisateur recherché");
            string Input = Console.ReadLine()!;
            bool IsValid = int.TryParse(Input, out Index);

            // Vérifie si l'index est valide
            while (!(IsValid && Index < LU.Count && Index >= 0))
            {
                Console.WriteLine("Le numéro est incorrect! Veuillez entrez à nouveau un numéro d'utilisateur :");
                Input = Console.ReadLine()!;
                IsValid = int.TryParse(Input, out Index);
            }

            // Renvoie l'utilisateur choisi
            return LU[Index];
        }

        public string AfficherListUser(List<User> LU)
        {
            // Crée un objet StringBuilder pour construire la liste des utilisateurs
            StringBuilder Sb = new StringBuilder();

            // Affiche le nom du premier utilisateur de la liste
            Sb.Append($"Liste des Utilisateurs ayant le nom {LU[0].LastName} :\n");

            int index = 0;

            // Parcourt la liste des utilisateurs et ajoute leurs informations à la chaîne de caractères
            foreach (User User in LU)
            {
                Sb.Append($"Index : {index}, Nom : {User.LastName}, Prénom : {User.FirstName}, Téléphone : {User.PhoneNumber}, Adresse : {User.Adress}\n");
                index++;
            }

            // Renvoie la liste des utilisateurs sous forme de chaîne de caractères
            return Sb.ToString();
        }


        public string FactureItemPart(List<ItemsPurchased> ListItem)
        {
            // Crée un objet StringBuilder pour construire la partie des articles achetés dans une facture
            StringBuilder Sb = new StringBuilder();

            // Obtient une instance du service d'articles
            ServiceArticle ArticleService = ServiceArticle.Instance;

            foreach (ItemsPurchased Item in ListItem)
            {
                // Récupère l'article acheté en utilisant son ID
                Article Art = ArticleService.ArticleList.GetById(Item.IDChocolat)!;

                // Vérifie si l'article existe
                if (Art != null)
                {
                    // Ajoute les informations de l'article à la chaîne de caractères de la facture
                    Sb.Append($"{Art.Reference.PadRight(20)}{Item.Quantity.ToString().PadRight(20)}{(Art.Price.ToString() + "€").PadRight(20)}{Item.DatePurchase.ToShortDateString()}\n");
                }
            }

            // Renvoie les détails des articles achetés sous forme de chaîne de caractères
            return Sb.ToString();
        }

        public string FormatDate(DateTime Date)
        {
            // Crée une chaîne de caractères formatée à partir de la date spécifiée
            return Date.Day + "_" + Date.Month + "_" + Date.Year + "_" + Date.Hour + "_" + Date.Minute;
        }

        public string UserNameFile(User User, DateTime Date)
        {
            // Crée un nom de fichier basé sur le nom de l'utilisateur et la date
            return User.LastName + "_" + User.FirstName + "_" + FormatDate(Date) + ".txt";
        }


    }


}
