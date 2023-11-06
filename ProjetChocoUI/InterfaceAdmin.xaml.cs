using Core.Service;
using Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjetChocoUI
{
    /// <summary>
    /// Logique d'interaction pour InterfaceAdmin.xaml
    /// </summary>
    public partial class InterfaceAdmin : Window
    {

        public Administrator AdminConnected;
        public ServiceAdmin AdminService;
        public ServiceArticle ArticleService = ServiceArticle.Instance;
        public CustomLog Logger = new CustomLog();

        public InterfaceAdmin()
        {
            InitializeComponent();
            AdminService = new ServiceAdmin();
            ArticleService.LoadArticleList();
            CanvasArticleForm.Visibility = Visibility.Collapsed;
            CanvasMenuAdmin.Visibility = Visibility.Collapsed;
            CanvasAdminConnect.Visibility = Visibility.Visible;
        }

        private void ValidForm(object sender, RoutedEventArgs e)
        {
            AdminService.AdminConnected = new Administrator();
            AdminService.AdminConnected.Login = InputLogin.Text;
            AdminService.AdminConnected.Password = InputPassword.Text;

            if (AdminService.AdminFindByPasswordAndLogin(AdminService.AdminConnected.Password, AdminService.AdminConnected.Login) != null)
            {
                // Si l'administrateur existe déjà, utilise l'administrateur existant.
                AdminService.AdminConnected = AdminService.AdminFindByPasswordAndLogin(AdminService.AdminConnected.Password, AdminService.AdminConnected.Login);
                // Sauvegarde la liste des administrateurs
                AdminService.Logger.WriteLog((AdminService.SaveAdminList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des administrateurs", false);
            }
            else
            {
                // Sinon, ajoute le nouvel administrateur à la liste.
                AdminService.AdminList.Add(AdminConnected);
            }
            CanvasAdminConnect.Visibility = Visibility.Collapsed;
            CanvasMenuAdmin.Visibility = Visibility.Visible;

        }

        private void ButtonFactureTotal(object sender, RoutedEventArgs e)
        {
            AdminService.FactureTotal();
        }

        private void ButtonArticleForm(object sender, RoutedEventArgs e)
        {
            CanvasArticleForm.Visibility = Visibility.Visible;
            CanvasMenuAdmin.Visibility = Visibility.Collapsed;
        }

        private void InputLogin_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void InputPassword_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ValidArticle(object sender, RoutedEventArgs e)
        {
            // Crée un nouvel objet Article et initialise ses propriétés en demandant à l'utilisateur.
            Article Article = new Article();
            Article.Reference = InputRef.Text;
            Article.Price = int.Parse(InputPrice.Text);

            // Vérifie si un article avec la même référence existe déjà dans la liste.
            if (ArticleService.FindArticleByReference(Article.Reference) == null)
            {
                // Ajoute l'article à la liste d'articles.
                ArticleService.ArticleList.Add(Article);
                // Sauvegarde la liste des articles
                Logger.WriteLog((ArticleService.SaveArticleList() ? "Réussi" : "Echec") + " : Sauvegarde de la liste des articles", false);
            }
            CanvasArticleForm.Visibility = Visibility.Collapsed;
            CanvasMenuAdmin.Visibility = Visibility.Visible;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
