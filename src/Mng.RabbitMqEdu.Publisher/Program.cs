using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory
{
    HostName = "localhost",
};

using var connection = factory.CreateConnection();
using var chanel = connection.CreateModel();

chanel.QueueDeclare("hello", exclusive: false);

const string message = "Hello World";
var messageBytes = Encoding.UTF8.GetBytes(message);

chanel.BasicPublish("", "hello", null, messageBytes);
Console.ReadLine();