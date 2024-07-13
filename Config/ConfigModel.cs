namespace Config
{
    public class ConfigModel
    {
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public bool QueueExclusive { get; set; }
        public bool QueueDurable { get; set; }
        public bool QueueAutoDelete { get; set; }
    }
}
