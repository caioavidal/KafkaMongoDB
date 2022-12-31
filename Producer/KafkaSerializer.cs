using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    public class KafkaSerializer<T> : ISerializer<T>
    {
        byte[] ISerializer<T>.Serialize(T data, SerializationContext context)
        {
            var str = System.Text.Json.JsonSerializer.Serialize(data); //you can also use Newtonsoft here
            var bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }
    }
}
