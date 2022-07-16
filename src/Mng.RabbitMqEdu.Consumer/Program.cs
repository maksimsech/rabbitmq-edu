using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory
{
    HostName = "localhost",
};

using var connection = factory.CreateConnection();
using var chanel = connection.CreateModel();

chanel.QueueDeclare("hello", exclusive: false);

var consumer = new EventingBasicConsumer(chanel);

consumer.Received += (_, eventArgs) =>
{
    var body = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
    Console.WriteLine(body);
};

chanel.BasicConsume("hello", true, consumer);

Console.ReadLine();