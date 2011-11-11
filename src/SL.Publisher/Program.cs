using System;
using System.Collections.Generic;
using MassTransit;
using SLParser.Domain;
using Bus = MassTransit.Bus;

namespace SL.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
           Bus.Initialize(sbc =>
                              {
                                  sbc.ReceiveFrom("rabbitmq://localhost/test_queue");
                                  sbc.UseRabbitMq();
                      
                              });



            var info = new RealtimeInfo
                           {
                               Buses = new List<SLParser.Domain.Bus>
                                   {
                                       new SLParser.Domain.Bus {DepartTime = "17 min", LineNumber = "443"},
                                       new SLParser.Domain.Bus {DepartTime = "21:30", LineNumber = "71"}
                                   }
                           };

                //Bus.Instance.Publish(info);
                Bus.Instance.GetEndpoint(new Uri("rabbitmq://localhost/test_queue")).Send(info);
                    
            


        }


    }
}
