using System;
using SLParser.Domain;
using Bus = MassTransit.Bus;
using MassTransit;
namespace SL.SerialListener
{
	public class Listener
	{
		private Writer _writer;

		public Listener()
		{
			InitBus();
			_writer = new Writer("COM4");
		}

		private void InitBus()
		{
			Bus.Initialize(sbc =>
			{
				sbc.UseRabbitMq();
				sbc.ReceiveFrom("rabbitmq://localhost/test_queue");
				sbc.Subscribe(subs => subs.Handler<RealtimeInfo>(SendToArduino));
			});

		}

		
		private void SendToArduino(RealtimeInfo info)
		{
			
		}


	}
}