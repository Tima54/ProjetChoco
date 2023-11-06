using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IService
{
    /// <summary>
    /// Interface pour le service de gestion des articles.
    /// </summary>
    public interface IServiceArticle
    {
        /// <summary>
        /// Charge la liste d'articles à partir d'une source de données.
        /// </summary>
        /// <returns>True si le chargement a réussi, sinon false.</returns>
        bool LoadArticleList();

        /// <summary>
        /// Sauvegarde la liste d'articles dans une source de données.
        /// </summary>
        /// <returns>True si la sauvegarde a réussi, sinon false.</returns>
        bool SaveArticleList();

        /// <summary>
        /// Permet de créer un nouvel article et l'ajoute à la liste d'articles si possible.
        /// </summary>
        /// <returns>True si l'article a été ajouté, sinon false.</returns>
        bool formArticle();

        /// <summary>
        /// Recherche un article par sa référence.
        /// </summary>
        /// <param name="Reference">La référence de l'article à rechercher.</param>
        /// <returns>L'article trouvé ou null s'il n'existe pas.</returns>
        Article FindArticleByReference(string Reference);

        /// <summary>
        /// Convertit la liste d'articles en une représentation sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>La représentation de la liste d'articles sous forme de chaîne.</returns>
        string ArticleListToString();
    }

}
