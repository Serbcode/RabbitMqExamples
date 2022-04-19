using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace RabbitMq.Publisher.Default
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            do {
                int TimeToSleep = new Random().Next(1000, 3000);                
                Thread.Sleep(TimeToSleep);

                var factory = new ConnectionFactory() { HostName = "localhost" };

                using (var connection = factory.CreateConnection())                
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("dev-queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                    
                    string message = $"Mesasge from Publisher N {counter}";
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "dev-queue", basicProperties: null, 
                    body: body);
                    Console.WriteLine($"Message was sent into Default Exchange N {counter++}");
                }
                            
            } while (true);


        }
    }
}
