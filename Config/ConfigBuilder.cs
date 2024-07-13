using System.Configuration;

namespace Config
{
    public static class ConfigBuilder
    {
        public static string ConfigName { get; } = "app.config";

        public static ConfigModel Build(string configPath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap { ExeConfigFilename = configPath };
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(
                fileMap, 
                ConfigurationUserLevel.None);
            var appSettings = config.AppSettings;

            var configModel = new ConfigModel
            {
                HostName = appSettings.Settings["HostName"].Value,
                QueueName = appSettings.Settings["QueueName"].Value,
                QueueExclusive = Convert.ToBoolean(appSettings.Settings["QueueExclusive"].Value),
                QueueDurable = Convert.ToBoolean(appSettings.Settings["QueueDurable"].Value),
                QueueAutoDelete = Convert.ToBoolean(appSettings.Settings["QueueAutoDelete"].Value)
            };

            return configModel;
        }
    }
}
