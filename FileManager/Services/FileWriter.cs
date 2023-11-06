using FileManager.IServices;

namespace FileManager.Services
{
    public class FileWriter : IFileWriter
    {

        private static FileWriter instance = null;
        private static readonly object lockObject = new object();
        public static FileWriter Instance
        {
            get
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new FileWriter();
                    }
                    return instance;
                }
            }
        }

        private FileWriter() { }

        public bool WriteToFile(string FilePath, string Content)
        {
            try
            {
                // Écrit le contenu dans le fichier spécifié par le chemin FilePath.
                File.WriteAllText(FilePath, Content);
                return true; // L'écriture a réussi.
            }
            catch (IOException e)
            {
                return false; // Une erreur s'est produite lors de l'écriture dans le fichier.
            }
        }

        public bool FileExiste(string FilePath)
        {
            return File.Exists(FilePath); // Renvoie true si le fichier existe à l'emplacement spécifié, sinon false.
        }

        public bool CreateFile(string FilePath)
        {
            try
            {
                // Crée un nouveau fichier au chemin FilePath.
                File.WriteAllText(FilePath,"");

                return true; // La création du fichier a réussi.
            }
            catch (IOException e)
            {
                return false; // Une erreur s'est produite lors de la création du fichier.
            }
        }

        public bool WriteFactureUser(string FolderPath, string FileName, string Content)
        {
            try
            {
                // Vérifie si le dossier existe, sinon le crée
                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }

                // Chemin complet du fichier
                string FilePath = Path.Combine(FolderPath, FileName);

                // Écrit le contenu dans le fichier
                File.WriteAllText(FilePath, Content);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}