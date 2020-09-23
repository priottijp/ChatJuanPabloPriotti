using ChatJuanPabloPriotti.Core.Hubs;
using ChatJuanPabloPriotti.Hubs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace ChatJuanPabloPriotti.Receiver
{
    class Receiver
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "jobs_queue",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: true,
                                     arguments: null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                Console.WriteLine(" [*] Waiting for messages.");
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Consumer.Consumer c = new Consumer.Consumer();
                    c.Consume(message);
                    Console.WriteLine(" [x] Received {0}", message);
                    int dots = message.Split('.').Length - 1;
                    Thread.Sleep(dots * 1000);
                    Console.WriteLine(" [x] Done");
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                };
                channel.BasicConsume(queue: "jobs_queue",
                                     autoAck: false,
                                     consumer: consumer);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
