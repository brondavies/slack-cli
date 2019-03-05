using System.Configuration;

namespace slack
{
    partial class SlackCli
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
