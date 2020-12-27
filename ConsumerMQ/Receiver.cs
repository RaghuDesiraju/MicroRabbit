using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ConsumerMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory=new ConnectionFactory(){HostName="localhost"};
            //open connection
            using(var connection=factory.CreateConnection())
            {
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest",false,false,false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    //Console.WriteLine("Checking data received");
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();                      
                        var message = System.Text.Encoding.UTF8.GetString(body);
                        Console.WriteLine("Received message " + message);
                    };
                    channel.BasicConsume("BasicTest", true, consumer);
                }
                 Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}
