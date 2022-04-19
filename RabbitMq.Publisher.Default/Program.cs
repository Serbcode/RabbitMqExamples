using System;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMq.Publisher.Default
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("dev-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    
                    string message = "Mesasge from Publisher";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "dev-queue", basicProperties: null, 
                    body: body);
                    Console.WriteLine("Message was sent into Default Exchange");
                }
            }            
        }
    }
}
