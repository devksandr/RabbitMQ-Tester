using Config;
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("RabbitMQ messages publisher:");

var solutionPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
string configPath = Path.Combine(solutionPath, ConfigBuilder.ConfigName);

if (File.Exists(configPath))
{
    var config = ConfigBuilder.Build(configPath);
    StartPublishRabbitMQ(config);
}
else
{
    Console.WriteLine($"Config '{ConfigBuilder.ConfigName}' doesn't exist in the root of the projects");
}

void StartPublishRabbitMQ(ConfigModel config)
{
    var factory = new ConnectionFactory { HostName = config.HostName };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    channel.QueueDeclare(
        queue: config.QueueName,
        exclusive: config.QueueExclusive,
        durable: config.QueueDurable,
        autoDelete: config.QueueAutoDelete,
        arguments: null);

    while (true)
    {
        Console.Write("> ");
        var message = Console.ReadLine();
        if (string.IsNullOrEmpty(message))
        {
            continue;
        }

        SendMessage(message, channel, config.QueueName);
    }
}

void SendMessage(string message, IModel channel, string queueName)
{
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "",
        routingKey: queueName,
        basicProperties: null,
        body: body);
}