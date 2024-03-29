﻿using MaisSaude.Common;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace MaisSaude.Infra.RabbitMQ
{
    public class RabbitMQFactory
    {
        private IModel _channel;
        private readonly IConnection _iconnection;

        public RabbitMQFactory(IOptions<DadosBaseRabbitMQ> dadosBaseRabbitMQ)
        {
            var connectionFactory = new ConnectionFactory()
            {
                HostName = dadosBaseRabbitMQ.Value.HostName,
                UserName = dadosBaseRabbitMQ.Value.UserName,
                Password = dadosBaseRabbitMQ.Value.Password,
                Port = dadosBaseRabbitMQ.Value.Port,

            };

            _iconnection = connectionFactory.CreateConnection();
            _channel = _iconnection.CreateModel();

        }

        public IModel GetChannel()
        {
            if (!_channel.IsOpen)
            {
                _channel.Dispose();
                _channel = _iconnection.CreateModel();
            }
            return _channel;
        }


    }
}
