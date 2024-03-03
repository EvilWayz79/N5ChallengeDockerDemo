using Confluent.Kafka;
using Microsoft.Identity.Client;
using System.Formats.Tar;

namespace N5ChallengeDockerDemo.Services
{
    public class ProducerService
    {
        private readonly IConfiguration _configuration;
        private readonly IProducer<Null, string> _producer;

        /// <summary>
        /// kafka service
        /// </summary>
        /// <param name="configuration"></param>
        public ProducerService(IConfiguration configuration)
        {
            try
            {
                _configuration = configuration;
                var producerconfig = new ProducerConfig
                {
                    BootstrapServers = _configuration["kafka:BootstrapServers"]
                };

                _producer = new ProducerBuilder<Null, string>(producerconfig).Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public async Task ProduceAsync(string topic, string message)
        {
            var kafkamessage = new Message<Null, string> { Value = message, };
            await _producer.ProduceAsync(topic,kafkamessage);
        }
    }
}
