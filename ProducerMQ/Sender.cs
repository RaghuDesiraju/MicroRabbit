using System;
using RabbitMQ.Client;

namespace ProducerMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory=new ConnectionFactory(){HostName="localhost"};
            //open connection
            using(var connection=factory.CreateConnection())
            {
                //create fresh channel, session and model
                using(var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("BasicTest",false,false,false, null);
                    string message = "Getting started with .Net core Rabbit MQ";
                    var body = System.Text.Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish("", "BasicTest", null, body);
                    Console.WriteLine("Sent message " + message);                    
                }
                Console.WriteLine("Press any key to exit");
                Console.ReadLine();
            }
        }
    }
}
