using System;
using MassTransit;
using SLParser.Domain;
using Bus = MassTransit.Bus;

namespace SL.SerialListener
{
    class Program
    {
        static void Main(string[] args)
        {
            Bus.Initialize(sbc =>
            {
                sbc.UseRabbitMq();
                sbc.ReceiveFrom("rabbitmq://localhost/test_queue");
                sbc.Subscribe(subs => subs.Handler<RealtimeInfo>(info =>
                                                                     {
                                                                         foreach (var bus in info.Buses)
                                                                         {
                                                                             Console.WriteLine(String.Format("{0} leaves {1}",bus.LineNumber,bus.DepartTime));
                                                                         }
                                                                         Console.WriteLine("***");
                                                                     }
                                          ));                             
                });

            Console.WriteLine("waiting..");
        }
    }
}
