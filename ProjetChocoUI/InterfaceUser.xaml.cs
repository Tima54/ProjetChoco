using Core.Service;
using Log;
using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;

namespace ProjetChocoUI
{
    /// <summary>
    /// Logique d'interaction pour InterfaceUser.xaml
    /// </summary>
    public partial class InterfaceUser : Window
    {

        public ServiceUser ServiceUser;
        public CustomLog Logger = new CustomLog();
        public ServiceArticle serviceArticle = ServiceArticle.Instance;
        public ServiceItemsPurchased ItemsPurchasedService = ServiceItemsPurchased.Instance;
        public InterfaceUser()
        {
            InitializeComponent();
            ServiceUser = new ServiceUser();
            serviceArticle.LoadArticleList();
            ItemsPurchasedService.LoadItemsPurchasedList();
            CanvasUserConnect.Visibility = Visibility.Visible;
            CanvasMenuUser.Visibility = Visibility.Collapsed;
        }

        private void ValidFormUser(object sender, RoutedEventArgs e)
        {
            // Crée un nouvel utilisateur et collecte des informations le concernant
            ServiceUser.UserConnected = new User();
            ServiceUser.UserConnected.LastName = InputNom.Text;
            ServiceUser.UserConnected.FirstName = InputPrenom.Text;
            ServiceUser.UserConnected.PhoneNumber = InputTel.Text;
            ServiceUser.UserConnected.Adress = InputAdresse.Text;

            // Vérifie si l'utilisateur existe déjà dans la liste
            if (!ServiceUser.UserExist())
            {
                ServiceUser.UserList.Add(ServiceUser.UserConnected);
                // Sauvegarde la liste des utilisateurs
                Logger.WriteLog((ServiceUser.SaveUserList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des utilisateurs", false);
            }
            CanvasUserConnect.Visibility = Visibility.Collapsed;
            CanvasMenuUser.Visibility = Visibility.Visible;

            // Initialisation de la liste d'articles
            ObservableCollection<ArticleView> ListeArticles = new ObservableCollection<ArticleView>();
            int Index = 0;
            foreach (Article article in serviceArticle.ArticleList.GetList())
            {
                ListeArticles.Add(new ArticleView(Index, article));
                Index++;
            }
            // Liaison de la ListView à la liste d'articles
            ListArticle.ItemsSource = ListeArticles;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void InputLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InputPassword_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ListArticle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AjouterArticlePanier(object sender, RoutedEventArgs e)
        {
            // Demande à l'utilisateur la quantité d'articles à ajouter.
            int Quantity = int.Parse(InputQuantity.Text);

            // Vérifie si l'article existe déjà dans le panier.
            if (ItemsPurchasedService.ItemExistOnBasket(serviceArticle.ArticleList.GetByIndex(int.Parse(InputIndex.Text)).Id) != null)
            {
                // Si l'article est déjà dans le panier, augmente simplement la quantité.
                ItemsPurchasedService.ItemExistOnBasket(serviceArticle.ArticleList.GetByIndex(int.Parse(InputIndex.Text)).Id).Quantity += Quantity;
            }
            else
            {
                // Si l'article n'est pas dans le panier, crée un nouvel article et l'ajoute au panier.
                ItemsPurchased Items = new ItemsPurchased();
                Items.IDAcheteur = ServiceUser.UserConnected.Id;
                Items.IDChocolat = ItemsPurchasedService.ArticleService.ArticleList.GetByIndex(int.Parse(InputIndex.Text)).Id;
                Items.Quantity = Quantity;
                Items.DatePurchase = DateTime.Today;
                ItemsPurchasedService.Basket.Add(Items);
            }
            RefreshBasketView();
            RefreshMontant();
        }


        public void RefreshBasketView()
        {
            ObservableCollection<ItemBasketView> BasketList = new ObservableCollection<ItemBasketView>();
            foreach (ItemsPurchased Items in ItemsPurchasedService.Basket)
            {
                BasketList.Add(new ItemBasketView(Items.Quantity, serviceArticle.ArticleList.GetById(Items.IDChocolat)));
            }
            // Liaison de la ListView à la liste d'articles
            BasketView.ItemsSource = BasketList;
        }

        public void RefreshMontant()
        {
            Montant.Text = @"Montant total de votre panier: " + ItemsPurchasedService.PriceItemsPurchasedList(ItemsPurchasedService.Basket) + "€";
        }

        private void ValidBasket(object sender, RoutedEventArgs e)
        {
            ItemsPurchasedService.ItemsPurchasedeList = ItemsPurchasedService.ItemsPurchasedeList.Concat(ItemsPurchasedService.Basket).ToList();
            // Sauvegarde la liste des articles achetés
            Logger.WriteLog((ItemsPurchasedService.SaveItemsPurchasedList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles achetés", true);
            string facture = ItemsPurchasedService.FactureUser(ServiceUser.UserConnected);
            ItemsPurchasedService.Basket = new List<ItemsPurchased>();
            // Afficher la fenêtre pop-up avec le texte
            MessageBox.Show(facture, "Ma facture", MessageBoxButton.OK);
            RefreshBasketView();
            RefreshMontant();
        }
    }
}
