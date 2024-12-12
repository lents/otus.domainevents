using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Otus.Cqrs.RabbitMq
{
    public class RabbitService
    {
        private readonly ConnectionFactory _connectionFactory;
        private string _exchange;
        public RabbitService(IOptions<RabbitMqOptions> options)
        {
            var opt = options.Value!;
            _connectionFactory = new ConnectionFactory();
            _connectionFactory.HostName = opt.Server;
            _connectionFactory.UserName = opt.Login;
            _connectionFactory.Password = opt.Password;
            _connectionFactory.VirtualHost = "/";
            _exchange = opt.Exchange;
            IModel channel = CreateChannel();
            channel.ExchangeDeclare(opt.Exchange, ExchangeType.Direct);
            channel.QueueDeclare(opt.Queue, true, true, false);
            channel.QueueBind(opt.Queue, opt.Exchange, opt.RoutingKey);
        }

        private IModel CreateChannel()
        {
            var con = _connectionFactory.CreateConnection();
            var channel = con.CreateModel();
            return channel;
        }

        public async Task PublishAsync<T>(T value, string routingKey)
        {
            var channel = CreateChannel();
            IBasicProperties props = channel.CreateBasicProperties();
            props.ContentType = "text/plain";
            var b = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(value));
            channel.BasicPublish(_exchange, routingKey, props, b);
        }
    }
}
