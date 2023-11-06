using Models;

namespace Core
{
    /// <summary>
    /// Cette interface définit les opérations que la classe ServiceItemsPurchased doit implémenter.
    /// </summary>
    public interface IServiceItemsPurchased
    {
        /// <summary>
        /// Charge la liste d'articles achetés depuis un fichier de données.
        /// </summary>
        /// <returns>Vrai si le chargement a réussi, sinon faux.</returns>
        bool LoadItemsPurchasedList();

        /// <summary>
        /// Sauvegarde la liste d'articles achetés dans un fichier de données.
        /// </summary>
        /// <returns>Vrai si la sauvegarde a réussi, sinon faux.</returns>
        bool SaveItemsPurchasedList();

        /// <summary>
        /// Ajoute un article au panier de l'utilisateur.
        /// </summary>
        /// <param name="user">L'utilisateur à qui ajouter l'article.</param>
        /// <param name="indexItem">L'indice de l'article à ajouter.</param>
        /// <returns>Vrai si l'ajout a réussi, sinon faux.</returns>
        bool AddItemOnBasket(User user, int indexItem);

        /// <summary>
        /// Vérifie si un article existe déjà dans le panier.
        /// </summary>
        /// <param name="idChoco">L'identifiant de l'article à rechercher dans le panier.</param>
        /// <returns>L'article s'il existe dans le panier, sinon null.</returns>
        ItemsPurchased ItemExistOnBasket(Guid idChoco);

        /// <summary>
        /// Valide le panier de l'utilisateur et génère une facture.
        /// </summary>
        /// <param name="userConnected">L'utilisateur dont le panier doit être validé.</param>
        /// <returns>La facture générée.</returns>
        string ValidBasket(User userConnected);

        /// <summary>
        /// Calcule le prix total des articles achetés dans une liste donnée.
        /// </summary>
        /// <param name="itemsPurchaseds">La liste des articles achetés pour lesquels calculer le prix total.</param>
        /// <returns>Le prix total des articles achetés.</returns>
        float PriceItemsPurchasedList(List<ItemsPurchased> itemsPurchaseds);

        /// <summary>
        /// Génère une facture pour un utilisateur spécifié.
        /// </summary>
        /// <param name="user">L'utilisateur pour lequel générer la facture.</param>
        /// <returns>La facture générée.</returns>
        string FactureUser(User user);

        /// <summary>
        /// Génère une facture pour l'ensemble des articles achetés.
        /// </summary>
        /// <returns>La facture générée.</returns>
        string FactureTotal();

        /// <summary>
        /// Génère une facture pour un utilisateur spécifique en fonction d'une date donnée.
        /// </summary>
        /// <param name="dateTime">La date pour laquelle générer la facture.</param>
        /// <returns>La facture générée.</returns>
        string FactureByUser(User user);

        /// <summary>
        /// Génère une facture pour l'ensemble des articles achetés en fonction d'une date donnée.
        /// </summary>
        /// <param name="dateTime">La date pour laquelle générer la facture.</param>
        /// <returns>La facture générée.</returns>
        string FactureByDate(DateTime dateTime);
    }
}
