using System;
using RabbitMQ.Client;
using System.Text;

namespace ChatJuanPabloPriotti.Producer
{
    public static class Producer
    {
        public static bool PostMessage(string message)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "jobs_queue", durable: true, exclusive: false, autoDelete: true, arguments: null);
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "", routingKey: "jobs_queue", basicProperties: null, body: body);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
