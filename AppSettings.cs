using System.IO;
using System.Web.Script.Serialization;

namespace FileSearch
{
    public class AppSettings<T> where T : new()
    {
        private const string DEFAULT_FILENAME = "settings.json";

        public void Save(string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(this));
        }

        public static void Save(T settings, string fileName = DEFAULT_FILENAME)
        {
            File.WriteAllText(fileName, (new JavaScriptSerializer()).Serialize(settings));
        }

        public static T Load(string fileName = DEFAULT_FILENAME)
        {
            T settings = new T();

            if (File.Exists(fileName))
                settings = (new JavaScriptSerializer()).Deserialize<T>(File.ReadAllText(fileName));

            return settings;
        }
    }
}
