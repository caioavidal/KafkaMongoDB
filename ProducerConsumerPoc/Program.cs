// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;
using Newtonsoft.Json;
using Persistence;
using Producer;

Console.WriteLine("##### Producer #####");

Console.WriteLine("Sending messages...");

var producer = new KafkaProducer();
while (true)
{
    producer.Send();
    Thread.Sleep(10);
}