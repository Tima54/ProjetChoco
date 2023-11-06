using FileManager.Services;
using Log;
using Models;
using System.Configuration;
using System.Text;

namespace Core.Service
{
    public class ServiceItemsPurchased
    {

        private static ServiceItemsPurchased instance = null;
        private static readonly object lockObject = new object();

        public FileWriter FileWriter = FileWriter.Instance;
        public JsonToolkit JsonTool = new JsonToolkit();
        public ServiceConsole AC = new ServiceConsole();
        public CustomLog Logger = new CustomLog();

        public List<ItemsPurchased> ItemsPurchasedeList = new List<ItemsPurchased>();
        public List<ItemsPurchased> Basket = new List<ItemsPurchased>();
        public ServiceArticle ArticleService = ServiceArticle.Instance;

        public static ServiceItemsPurchased Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ServiceItemsPurchased();
                    }
                    return instance;
                }
            }
        }

        private ServiceItemsPurchased() { }

        public bool LoadItemsPurchasedList()
        {
            // Désérialise les articles achetés depuis le fichier spécifié.
            ItemsPurchasedeList = JsonTool.DeserializeItemsPurchased(ConfigurationManager.AppSettings["pathBDD"] + "articles_achetes.json");
            if (ItemsPurchasedeList == null)
            {
                // Si la liste est vide, crée une nouvelle liste.
                ItemsPurchasedeList = new List<ItemsPurchased>();
                return false; // Indique le chargement échoué.
            }
            return true; // Indique le chargement réussi.
        }

        public bool SaveItemsPurchasedList()
        {
            return FileWriter.WriteToFile(ConfigurationManager.AppSettings["pathBDD"] + "articles_achetes.json", JsonTool.SerializeItemsPurchased(ItemsPurchasedeList));
        }

        public bool AddItemOnBasket(User User, int IndexItem)
        {
            // Demande à l'utilisateur la quantité d'articles à ajouter.
            int Quantity = AC.InputInt("Combien en voulez vous?");

            // Vérifie si l'article existe déjà dans le panier.
            if (ItemExistOnBasket(ArticleService.ArticleList.GetByIndex(IndexItem).Id) != null)
            {
                // Si l'article est déjà dans le panier, augmente simplement la quantité.
                ItemExistOnBasket(ArticleService.ArticleList.GetByIndex(IndexItem).Id).Quantity += Quantity;
                return false; // Indique que l'article a été ajouté au panier existant.
            }
            else
            {
                // Si l'article n'est pas dans le panier, crée un nouvel article et l'ajoute au panier.
                ItemsPurchased Items = new ItemsPurchased();
                Items.IDAcheteur = User.Id;
                Items.IDChocolat = ArticleService.ArticleList.GetByIndex(IndexItem).Id;
                Items.Quantity = Quantity;
                Items.DatePurchase = DateTime.Today;
                Basket.Add(Items);
                return true; // Indique que l'article a été ajouté au panier.
            }
        }

        public ItemsPurchased ItemExistOnBasket(Guid IdChoco)
        {
            return Basket.Find(u => u.IDChocolat == IdChoco)!;
        }

        public string validBasket(User UserConnected)
        {
            ItemsPurchasedeList = ItemsPurchasedeList.Concat(Basket).ToList();
            // Sauvegarde la liste des articles achetés
            Logger.WriteLog((SaveItemsPurchasedList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles achetés", true);
            string facture = FactureUser(UserConnected);
            Basket = new List<ItemsPurchased>();
            return facture;
        }

        public float PriceItemsPurchasedList(List<ItemsPurchased> ItemsPurchaseds)
        {
            float TotalPrice = 0;
            foreach (ItemsPurchased Items in ItemsPurchaseds)
            {
                TotalPrice += Items.Quantity * ArticleService.ArticleList.GetById(Items.IDChocolat).Price;
            }
            return TotalPrice;
        }

        public string FactureUser(User User)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AC.EnteteFacture());
            sb.Append(AC.FactureSeparation());
            sb.Append(AC.FactureUserPart(User));
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Référence".PadRight(20)}{"Quantité".PadRight(20)}Prix\n");
            sb.Append(AC.FactureSeparation());
            foreach (ItemsPurchased Item in Basket)
            {
                Article Art = ArticleService.ArticleList.GetById(Item.IDChocolat)!;
                if (Art != null)
                {
                    sb.Append($"{Art.Reference.PadRight(20)}{Item.Quantity.ToString().PadRight(20)}{Art.Price}\n");
                }
            }
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Montant Total :".PadRight(20)}{PriceItemsPurchasedList(Basket)}\n");
            return sb.ToString();
        }


        public string FactureTotal()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AC.EnteteFacture());
            sb.Append($"{"Référence".PadRight(20)}{"Quantité".PadRight(20)}{"Prix".PadRight(20)}Date d'achat\n");
            sb.Append(AC.FactureSeparation());
            sb.Append(AC.FactureItemPart(ItemsPurchasedeList));
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Montant Total :".PadRight(20)}{PriceItemsPurchasedList(ItemsPurchasedeList)}€\n");
            return sb.ToString();
        }

        public string FactureByUser(User User)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AC.EnteteFacture());
            sb.Append("Informations sur l'utilisateur :\n");
            sb.Append(AC.FactureUserPart(User));
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Référence".PadRight(20)}{"Quantité".PadRight(20)}{"Prix".PadRight(20)}Date d'achat\n");
            sb.Append(AC.FactureSeparation());
            List<ItemsPurchased> LTmp = ItemsPurchasedeList.FindAll(u => u.IDAcheteur == User.Id);
            sb.Append(AC.FactureItemPart(LTmp));
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Montant Total :".PadRight(20)}{PriceItemsPurchasedList(LTmp)}€\n");
            return sb.ToString();
        }

        public string FactureByDate(DateTime dateTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(AC.EnteteFacture());
            sb.Append($"{"Référence".PadRight(20)}{"Quantité".PadRight(20)}{"Prix".PadRight(20)}Date d'achat\n");
            sb.Append(AC.FactureSeparation());
            List<ItemsPurchased> LTmp = ItemsPurchasedeList.FindAll(u => u.DatePurchase.ToShortDateString() == dateTime.ToShortDateString());
            sb.Append(AC.FactureItemPart(LTmp));
            sb.Append(AC.FactureSeparation());
            sb.Append($"{"Montant Total :".PadRight(20)}{PriceItemsPurchasedList(LTmp)}€\n");
            return sb.ToString();
        }
    }
}
