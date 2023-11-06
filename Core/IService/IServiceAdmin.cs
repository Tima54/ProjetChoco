using Models;
using System;
using System.Collections.Generic;

namespace Core.IService
{
    /// <summary>
    /// Interface pour les services administrateurs.
    /// </summary>
    public interface IServiceAdmin
    {
        /// <summary>
        /// Charge la liste des administrateurs depuis un fichier de données.
        /// </summary>
        /// <returns>True si le chargement réussit, sinon False.</returns>
        bool LoadAdminList();

        /// <summary>
        /// Sauvegarde la liste des administrateurs dans un fichier de données.
        /// </summary>
        /// <returns>True si la sauvegarde réussit, sinon False.</returns>
        bool SaveAdminList();

        /// <summary>
        /// Affiche un menu d'administration et effectue des actions en fonction du choix de l'administrateur.
        /// </summary>
        /// <returns>True si l'administration est terminée, sinon False.</returns>
        bool AdminMenu();

        /// <summary>
        /// Crée un nouvel administrateur.
        /// </summary>
        /// <returns>True si la création réussit, sinon False.</returns>
        bool FormAdmin();

        /// <summary>
        /// Recherche un administrateur par mot de passe et nom d'utilisateur.
        /// </summary>
        /// <param name="Password">Le mot de passe de l'administrateur.</param>
        /// <param name="Login">Le nom d'utilisateur de l'administrateur.</param>
        /// <returns>L'administrateur correspondant ou null s'il n'existe pas.</returns>
        Administrator AdminFindByPasswordAndLogin(string Password, string Login);

        /// <summary>
        /// Génère une facture pour un utilisateur en fonction d'une date spécifiée.
        /// </summary>
        /// <returns>True si la génération de facture réussit, sinon False.</returns>
        bool FactureByDate();

        /// <summary>
        /// Génère une facture pour un utilisateur en fonction de son nom.
        /// </summary>
        /// <returns>True si la génération de facture réussit, sinon False.</returns>
        bool FactureByUser();

        /// <summary>
        /// Génère une facture totale.
        /// </summary>
        /// <returns>True si la génération de facture réussit, sinon False.</returns>
        bool FactureTotal();

        /// <summary>
        /// Vérifie si une date spécifiée existe dans la liste des articles achetés.
        /// </summary>
        /// <param name="Date">La date à vérifier.</param>
        /// <returns>True si la date existe, sinon False.</returns>
        bool DateExist(DateTime Date);

        /// <summary>
        /// Recherche des utilisateurs par nom.
        /// </summary>
        /// <param name="LastName">Le nom à rechercher.</param>
        /// <returns>Une liste d'utilisateurs correspondant au nom.</returns>
        List<User> UserExistByName(string LastName);

        /// <summary>
        /// Sélectionne un utilisateur à partir d'une liste.
        /// </summary>
        /// <param name="users">La liste d'utilisateurs parmi laquelle choisir.</param>
        /// <returns>L'utilisateur sélectionné ou null s'il n'y en a pas.</returns>
        User ChoiceUser(List<User> users);
    }
}
