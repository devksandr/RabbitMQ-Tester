using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using Config;

Console.WriteLine("RabbitMQ messages reciever:");

var solutionPath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
string configPath = Path.Combine(solutionPath, ConfigBuilder.ConfigName);

if (File.Exists(configPath))
{
    var config = ConfigBuilder.Build(configPath);
    StartConsumeRabbitMQ(config);
}
else
{
    Console.WriteLine($"Config '{ConfigBuilder.ConfigName}' doesn't exist in the root of the projects");
}

void StartConsumeRabbitMQ(ConfigModel config)
{
    var factory = new ConnectionFactory { HostName = config.HostName };
    using var connection = factory.CreateConnection();
    using var channel = connection.CreateModel();

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += Consumer_Received;

    channel.QueueDeclare(
        queue: config.QueueName,
        exclusive: config.QueueExclusive,
        durable: config.QueueDurable,
        autoDelete: config.QueueAutoDelete,
        arguments: null);

    channel.BasicConsume(
        queue: config.QueueName,
        autoAck: true,
        consumer: consumer);

    Console.Read();
}

void Consumer_Received(object? sender, BasicDeliverEventArgs e)
{
    var body = e.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
}
