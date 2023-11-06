using Models;

namespace FileManager.IServices
{
    interface IJsonToolkit
    {
        /// <summary>
        /// Désérialise un fichier JSON en une liste d'objets de type User.
        /// </summary>
        /// <param name="filepath">Le chemin du fichier JSON à désérialiser.</param>
        /// <returns>La liste d'objets User résultant de la désérialisation.</returns>
        List<User> Deserialize(string filepath);

        /// <summary>
        /// Sérialise une liste d'objets de type User en une chaîne JSON.
        /// </summary>
        /// <param name="json">La liste d'objets User à sérialiser.</param>
        /// <returns>La chaîne JSON résultant de la sérialisation.</returns>
        string Serialize(List<User> json);

        /// <summary>
        /// Désérialise un fichier JSON en une liste d'objets de type Administrator.
        /// </summary>
        /// <param name="filepath">Le chemin du fichier JSON à désérialiser.</param>
        /// <returns>La liste d'objets Administrator résultant de la désérialisation.</returns>
        List<Administrator> DeserializeAdministrator(string filepath);

        /// <summary>
        /// Sérialise une liste d'objets de type Administrator en une chaîne JSON.
        /// </summary>
        /// <param name="json">La liste d'objets Administrator à sérialiser.</param>
        /// <returns>La chaîne JSON résultant de la sérialisation.</returns>
        string SerializeAdministrator(List<Administrator> json);

        /// <summary>
        /// Désérialise un fichier JSON en une liste d'objets de type Article.
        /// </summary>
        /// <param name="filepath">Le chemin du fichier JSON à désérialiser.</param>
        /// <returns>La liste d'objets Article résultant de la désérialisation.</returns>
        List<Article> DeserializeArticles(string filepath);

        /// <summary>
        /// Sérialise une liste d'objets de type Article en une chaîne JSON.
        /// </summary>
        /// <param name="json">La liste d'objets Article à sérialiser.</param>
        /// <returns>La chaîne JSON résultant de la sérialisation.</returns>
        string SerializeArticles(List<Article> json);

        /// <summary>
        /// Désérialise un fichier JSON en une liste d'objets de type ItemsPurchased.
        /// </summary>
        /// <param name="filepath">Le chemin du fichier JSON à désérialiser.</param>
        /// <returns>La liste d'objets ItemsPurchased résultant de la désérialisation.</returns>
        List<ItemsPurchased> DeserializeItemsPurchased(string filepath);

        /// <summary>
        /// Sérialise une liste d'objets de type ItemsPurchased en une chaîne JSON.
        /// </summary>
        /// <param name="json">La liste d'objets ItemsPurchased à sérialiser.</param>
        /// <returns>La chaîne JSON résultant de la sérialisation.</returns>
        string SerializeItemsPurchased(List<ItemsPurchased> json);

    }
}
