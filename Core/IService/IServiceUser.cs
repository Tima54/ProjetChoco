using System;

namespace Core
{
    /// <summary>
    /// Interface définissant les méthodes pour gérer les opérations liées aux utilisateurs.
    /// </summary>
    public interface IServiceUser
    {
        /// <summary>
        /// Charge la liste des utilisateurs depuis une source de données.
        /// </summary>
        /// <returns>True si le chargement a réussi, sinon False.</returns>
        bool LoadUserList();

        /// <summary>
        /// Sauvegarde la liste des utilisateurs dans une source de données.
        /// </summary>
        /// <returns>True si la sauvegarde a réussi, sinon False.</returns>
        bool SaveUserList();

        /// <summary>
        /// Crée un nouvel utilisateur ou modifie un utilisateur existant.
        /// </summary>
        /// <returns>True si l'opération a réussi, sinon False.</returns>
        bool FormUser();

        /// <summary>
        /// Vérifie si un utilisateur existe déjà dans la liste.
        /// </summary>
        /// <returns>True si l'utilisateur existe, sinon False.</returns>
        bool UserExist();

        /// <summary>
        /// Gère le menu de l'utilisateur pour la gestion du panier et de la facture.
        /// </summary>
        /// <returns>True si l'utilisateur a terminé, sinon False.</returns>
        bool UserMenu();

        /// <summary>
        /// Génère et affiche la facture de l'utilisateur.
        /// </summary>
        /// <returns>True si la création de la facture a réussi, sinon False.</returns>
        bool FactureUser();
    }
}
