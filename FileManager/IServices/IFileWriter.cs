namespace FileManager.IServices
{
    public interface IFileWriter
    {
        /// <summary>
        /// Écrit un contenu dans un fichier.
        /// </summary>
        /// <param name="FilePath">Le chemin du fichier où écrire le contenu.</param>
        /// <param name="Content">Le contenu à écrire dans le fichier.</param>
        /// <returns>True si l'écriture a réussi, sinon false.</returns>
        bool WriteToFile(string FilePath, string Content);

        /// <summary>
        /// Vérifie si un fichier existe.
        /// </summary>
        /// <param name="FilePath">Le chemin du fichier à vérifier.</param>
        /// <returns>True si le fichier existe, sinon false.</returns>
        bool FileExiste(string FilePath);

        /// <summary>
        /// Crée un nouveau fichier.
        /// </summary>
        /// <param name="FilePath">Le chemin du fichier à créer.</param>
        /// <returns>True si la création du fichier a réussi, sinon false.</returns>
        bool CreateFile(string FilePath);

        /// <summary>
        /// Écrit le contenu de la facture utilisateur dans un fichier spécifié situé dans un dossier donné.
        /// </summary>
        /// <param name="FolderPath">Le chemin du dossier où le fichier de la facture doit être enregistré.</param>
        /// <param name="FileName">Le nom du fichier de la facture à créer ou écraser.</param>
        /// <param name="Content">Le contenu de la facture à écrire dans le fichier.</param>
        /// <returns>Retourne `true` si l'écriture du fichier s'est déroulée avec succès, sinon `false`.</returns>
        bool WriteFactureUser(string FolderPath, string FileName, string Content);
    }
}
