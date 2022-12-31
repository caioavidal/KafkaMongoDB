using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producer
{
    public class KafkaDeserializer<T> : IDeserializer<T>
    {
        public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context) => System.Text.Json.JsonSerializer.Deserialize<T>(data); //you can also use Newtonsoft here

    }
}
