using FileManager.Services;
using ListManager;
using Log;
using System.Configuration;

namespace Core.Service
{
    public class ServiceArticle
    {

        private static ServiceArticle instance = null;
        private static readonly object lockObject = new object();

        public FileWriter FileWriter = FileWriter.Instance;
        public JsonToolkit JsonTool = new JsonToolkit();
        public ServiceConsole AC = new ServiceConsole();
        public CustomLog Logger = new CustomLog();

        public BaseModelList<Article> ArticleList = new BaseModelList<Article>();
        public static ServiceArticle Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ServiceArticle();
                    }
                    return instance;
                }
            }
        }

        private ServiceArticle() { }
        public bool LoadArticleList()
        {
            // Charge la liste d'articles en utilisant la méthode DeserializeArticles de JsonTool.
            return ArticleList.setlList(JsonTool.DeserializeArticles(ConfigurationManager.AppSettings["pathBDD"] + "articles.json"));
        }

        public bool SaveArticleList()
        {
            // Sérialise la liste d'articles en utilisant la méthode SerializeArticles de JsonTool,
            // puis écrit le résultat dans un fichier en utilisant FileWriter.
            return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathBDD"] + "articles.json", JsonTool.SerializeArticles(ArticleList.GetList()));
        }

        public bool formArticle()
        {
            // Crée un nouvel objet Article et initialise ses propriétés en demandant à l'utilisateur.
            Article Article = new Article();
            Article.Reference = AC.InputString("Veuillez entrer la reference de l'article");
            Article.Price = AC.InputFloat("Veuillez entrer le prix de l'article");

            // Vérifie si un article avec la même référence existe déjà dans la liste.
            if (FindArticleByReference(Article.Reference) == null)
            {
                // Ajoute l'article à la liste d'articles.
                ArticleList.Add(Article);
                // Sauvegarde la liste des articles
                Logger.WriteLog((SaveArticleList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles", true);
                return true; // L'ajout a réussi.
            }
            return false; // L'article existe déjà.
        }

        public Article FindArticleByReference(string Reference)
        {
            Article foundArticle = null;
            if (ArticleList.GetList() != null)
            {
                // Recherche l'article dans la liste par sa référence.
                foundArticle = ArticleList.GetList().Find(u => u.Reference == Reference)!;
            }
            return foundArticle;
        }

        public string ArticleListToString()
        {
            // Utilise la méthode PrintArticleList d'AC pour convertir la liste d'articles en une chaîne lisible.
            return AC.PrintArticleList(ArticleList);
        }


    }
}
