using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace BrincandoComREDIS.Dados
{
    public class REDIS_DB
    {
        public ConnectionMultiplexer redis { get; set; }
        public ISubscriber sub { get; set; }

        public void Connect(string channel)
        {
            redis = ConnectionMultiplexer.Connect(channel);
            IDatabase db = redis.GetDatabase();
        }
        public string Subscriber(string channel)
        {

            string redisValues = string.Empty;
            sub = redis.GetSubscriber();
            sub.Subscribe(channel, (ch, msg) =>
            {
                redisValues = msg.ToString();
            });


            return redisValues;
        }

        public long Publisher(string channel, string message)
        {
            var sub = redis.GetSubscriber();
            long retorno = sub.Publish(channel, message);
            return retorno;
        }

        public string HSet()
        {
            return string.Empty;
        }
    }
}
