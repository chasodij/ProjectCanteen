using ProjectCanteen.BLL.Services.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace ProjectCanteen.BLL.Services.Implementations
{
    public class MessageService : IMessageService
    {
        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "admin",
                Password = "admin",
                VirtualHost = "/"
            };

            using (var connection = factory.CreateConnection())

            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("orders", durable: false, exclusive: false);

                var jsonString = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonString);

                channel.BasicPublish("", "orders", body: body);
            }
        }
    }
}
