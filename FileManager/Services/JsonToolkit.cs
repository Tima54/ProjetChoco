using FileManager.IServices;
using Models;
using Newtonsoft.Json;

namespace FileManager.Services
{
    public class JsonToolkit : IJsonToolkit
    {
        public List<User> Deserialize(string filepath)
        {
            try
            {
                // Lit le contenu du fichier JSON.
                string json = File.ReadAllText(filepath);
                // Désérialise le contenu JSON en une liste d'objets User.
                return JsonConvert.DeserializeObject<List<User>>(json)!;
            }
            catch(Exception e){
                return new List<User>();
            }
        }

        public string Serialize(List<User> json)
        {
            // Sérialise une liste d'objets User en une chaîne JSON.
            return JsonConvert.SerializeObject(json);
        }

        public List<Administrator> DeserializeAdministrator(string filepath)
        {
            try
            {
                // Lit le contenu du fichier JSON.
                string json = File.ReadAllText(filepath);
                // Désérialise le contenu JSON en une liste d'objets Administrator.
                return JsonConvert.DeserializeObject<List<Administrator>>(json)!;
            }
            catch (Exception e)
            {
                return new List<Administrator>();
            }
        }

        public string SerializeAdministrator(List<Administrator> json)
        {
            // Sérialise une liste d'objets Administrator en une chaîne JSON.
            return JsonConvert.SerializeObject(json);
        }

        public List<Article> DeserializeArticles(string filepath)
        {
            try
            {
                // Lit le contenu du fichier JSON.
                string json = File.ReadAllText(filepath);
                // Désérialise le contenu JSON en une liste d'objets Article.
                return JsonConvert.DeserializeObject<List<Article>>(json)!;
            }
            catch (Exception e)
            {
                return new List<Article>();
            }
        }

        public string SerializeArticles(List<Article> json)
        {
            // Sérialise une liste d'objets Article en une chaîne JSON.
            return JsonConvert.SerializeObject(json);
        }

        public List<ItemsPurchased> DeserializeItemsPurchased(string filepath)
        {
            try
            {
                // Lit le contenu du fichier JSON.
                string json = File.ReadAllText(filepath);
                // Désérialise le contenu JSON en une liste d'objets ItemsPurchased.
                return JsonConvert.DeserializeObject<List<ItemsPurchased>>(json)!;
            }
            catch (Exception e)
            {
                return new List<ItemsPurchased>();
            }
        }

        public string SerializeItemsPurchased(List<ItemsPurchased> json)
        {
            // Sérialise une liste d'objets ItemsPurchased en une chaîne JSON.
            return JsonConvert.SerializeObject(json);
        }

    }
}