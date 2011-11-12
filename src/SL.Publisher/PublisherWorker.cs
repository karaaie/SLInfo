using System;
using System.Text;
using MassTransit;
using MassTransit.BusConfigurators;
using SLParser.Domain;
using ZMQ;
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
			/*Bus.Initialize(sbc =>
			               	{
			               		ServiceBusConfiguratorExtensions.ReceiveFrom(sbc, "rabbitmq://localhost/test_queue");
			               		RabbitMqServiceBusExtensions.UseRabbitMq<ServiceBusConfigurator>(sbc);

			               	});*/


			//  Prepare our context and publisher
			using (Context context = new Context(1))
			{
				using (Socket publisher = context.Socket(SocketType.PUB))
				{
					publisher.Bind("tcp://*:5556");

					//  Initialize random number generator
					Random rand = new Random(System.DateTime.Now.Millisecond);
					while (true)
					{
						//  Get values that will fool the boss
						int zipcode, temperature, relHumidity;
						zipcode = rand.Next(0, 100000);
						temperature = rand.Next(-80, 135);
						relHumidity = rand.Next(10, 60);

						//  Send message to all subscribers
						string update = zipcode.ToString() + " " + temperature.ToString() +
							" " + relHumidity.ToString();
						publisher.Send(update, Encoding.Unicode);
					}
				}
			}

		}

		public void Publish(RealtimeInfo info)
		{
			/*

			Bus.Instance.GetEndpoint(new Uri("rabbitmq://localhost/test_queue")).Send(info);
			 * */

			Console.WriteLine("sending bus information");
		}
	}
}