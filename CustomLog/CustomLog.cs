using System.Configuration;

namespace Log
{
    public class CustomLog : ICustomLog
    {
        public CustomLog() { }

        public void WriteLog(string message, bool messageConsole)
        {
            // Construction du chemin du fichier journal en utilisant des informations de configuration
            string path = string.Format(ConfigurationManager.AppSettings["pathLogs"] + DateTime.Today.ToString("dd-MM-yyyy ss") + "{0}", ".txt");

            // Formatage du message avec un horodatage actuel et ajout d'un caractère de nouvelle ligne
            message = "[" + DateTime.Now.ToString() + "] " + message + "\n";

            // Ajout du message au fichier journal spécifié par le chemin
            File.AppendAllText(path, message);

            // Affichage du message dans la console si messageConsole est vrai (true)
            if (messageConsole)
            {
                Console.WriteLine(message);
            }
        }

    }
}