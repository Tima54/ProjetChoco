using System;
using System.Collections.Generic;
using Core.Service;
using ListManager;
using Models;

namespace Core.ServicesAffichagesConsole
{
    /// <summary>
    /// Interface définissant les méthodes pour l'interaction avec la console.
    /// </summary>
    public interface IServiceConsole
    {
        /// <summary>
        /// Obtient un entier à partir de l'entrée utilisateur.
        /// </summary>
        /// <param name="content">Message à afficher à l'utilisateur.</param>
        /// <returns>La valeur entière saisie par l'utilisateur.</returns>
        int InputInt(string content);

        /// <summary>
        /// Obtient un nombre à virgule flottante à partir de l'entrée utilisateur.
        /// </summary>
        /// <param name="content">Message à afficher à l'utilisateur.</param>
        /// <returns>La valeur à virgule flottante saisie par l'utilisateur.</returns>
        float InputFloat(string content);

        /// <summary>
        /// Obtient une chaîne de caractères à partir de l'entrée utilisateur.
        /// </summary>
        /// <param name="content">Message à afficher à l'utilisateur.</param>
        /// <returns>La chaîne de caractères saisie par l'utilisateur.</returns>
        string InputString(string content);

        /// <summary>
        /// Obtient un mot de passe administrateur avec des conditions spécifiques.
        /// </summary>
        /// <returns>Le mot de passe administrateur saisi par l'utilisateur.</returns>
        string InputPasswordAdmin();

        /// <summary>
        /// Obtient une adresse à partir de l'entrée utilisateur avec validation.
        /// </summary>
        /// <returns>L'adresse saisie par l'utilisateur.</returns>
        string InputAdress();

        /// <summary>
        /// Obtient un numéro de téléphone à partir de l'entrée utilisateur avec validation.
        /// </summary>
        /// <returns>Le numéro de téléphone saisi par l'utilisateur.</returns>
        string InputPhoneNumber();

        /// <summary>
        /// Obtient une date au format spécifique (JJ/MM/AAAA) à partir de l'entrée utilisateur avec validation.
        /// </summary>
        /// <returns>La date saisie par l'utilisateur.</returns>
        DateTime InputDate();

        /// <summary>
        /// Affiche un menu utilisateur sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>Le menu utilisateur sous forme de chaîne de caractères.</returns>
        string UserMenu();

        /// <summary>
        /// Affiche la liste d'articles sous forme de chaîne de caractères.
        /// </summary>
        /// <param name="ArticleList">Liste d'articles à afficher.</param>
        /// <returns>La liste d'articles sous forme de chaîne de caractères.</returns>
        string PrintArticleList(BaseModelList<Article> ArticleList);

        /// <summary>
        /// Affiche un menu administrateur sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>Le menu administrateur sous forme de chaîne de caractères.</returns>
        string MenuAdmin();

        /// <summary>
        /// Obtient l'en-tête d'une facture sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>L'en-tête de la facture sous forme de chaîne de caractères.</returns>
        string EnteteFacture();

        /// <summary>
        /// Obtient une ligne de séparation dans une facture sous forme de chaîne de caractères.
        /// </summary>
        /// <returns>La ligne de séparation de la facture sous forme de chaîne de caractères.</returns>
        string FactureSeparation();

        /// <summary>
        /// Obtient les détails d'un utilisateur pour une facture sous forme de chaîne de caractères.
        /// </summary>
        /// <param name="User">L'utilisateur dont les détails doivent être affichés.</param>
        /// <returns>Les détails de l'utilisateur sous forme de chaîne de caractères.</returns>
        string FactureUserPart(User User);

        /// <summary>
        /// Permet de choisir un utilisateur parmi une liste.
        /// </summary>
        /// <param name="LU">La liste d'utilisateurs parmi lesquels choisir.</param>
        /// <returns>L'utilisateur sélectionné.</returns>
        User ChoiceUser(List<User> LU);

        /// <summary>
        /// Affiche la liste d'utilisateurs sous forme de chaîne de caractères.
        /// </summary>
        /// <param name="LU">La liste d'utilisateurs à afficher.</param>
        /// <returns>La liste d'utilisateurs sous forme de chaîne de caractères.</returns>
        string AfficherListUser(List<User> LU);

        /// <summary>
        /// Obtient les détails des articles achetés pour une facture sous forme de chaîne de caractères.
        /// </summary>
        /// <param name="ListItem">La liste d'articles achetés.</param>
        /// <returns>Les détails des articles achetés sous forme de chaîne de caractères.</returns>
        string FactureItemPart(List<ItemsPurchased> ListItem);

        /// <summary>
        /// Formate une date en une chaîne de caractères spécifique.
        /// </summary>
        /// <param name="Date">La date à formater.</param>
        /// <returns>La date formatée sous forme de chaîne de caractères.</returns>
        string FormatDate(DateTime Date);

        /// <summary>
        /// Obtient un nom de fichier basé sur le nom de l'utilisateur et la date.
        /// </summary>
        /// <param name="User">L'utilisateur pour lequel le fichier est généré.</param>
        /// <param name="Date">La date associée au fichier.</param>
        /// <returns>Le nom de fichier généré.</returns>
        string UserNameFile(User User, DateTime Date);
    }
}
