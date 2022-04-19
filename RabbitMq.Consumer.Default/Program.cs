using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMq.Consumer.Default
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

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, e) => {
                        var body = e.Body;
                        var mes = Encoding.UTF8.GetString(body.ToArray());
                        System.Console.WriteLine("Recieved message {0}", mes);
                    };

                    channel.BasicConsume("", autoAck: true, consumer: consumer);

                    System.Console.WriteLine("Subscribed to the queue 'dev-gueue'... ");
                    Console.ReadKey();       
                }
            } 
            
        }
    }
}
