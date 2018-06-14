using System.Configuration;

namespace slack
{
    partial class Program
    {
        private static string Config(string name)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(name);
            }
            catch
            {
                return null;
            }
        }
    }
}
