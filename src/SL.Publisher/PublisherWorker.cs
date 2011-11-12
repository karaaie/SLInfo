using System;
using MassTransit;
using MassTransit.BusConfigurators;
using SLParser.Domain;
using Bus = MassTransit.Bus;

namespace SL.Publisher
{
	public class PublisherWorker
	{
		public PublisherWorker()
		{
			InitBus();
		}

		private static void InitBus()
		{
			Bus.Initialize(sbc =>
			               	{
			               		ServiceBusConfiguratorExtensions.ReceiveFrom(sbc, "rabbitmq://localhost/test_queue");
			               		RabbitMqServiceBusExtensions.UseRabbitMq<ServiceBusConfigurator>(sbc);

			               	});
		}

		public void Publish(RealtimeInfo info)
		{
			Bus.Instance.GetEndpoint(new Uri("rabbitmq://localhost/test_queue")).Send(info);
			Console.WriteLine("sending " + info.Buses[0].LineNumber);
		}
	}
}