namespace Core
{
    public interface ICoreTerminal
    {
        /// <summary>
        /// Vérifie l'existence des fichiers de données et les crée s'ils n'existent pas.
        /// </summary>
        /// <returns>Retourne true si les fichiers existent ou ont été créés avec succès, sinon false.</returns>
        bool FilesExistsAndCreate();

        /// <summary>
        /// Charge les données de base de données initiales.
        /// </summary>
        /// <returns>Retourne true si les données ont été chargées avec succès, sinon false.</returns>
        bool LoadBDD();

        /// <summary>
        /// Sauvegarde toutes les données de base de données.
        /// </summary>
        /// <returns>Retourne true si les données ont été sauvegardées avec succès, sinon false.</returns>
        bool SaveAll();
    }
}