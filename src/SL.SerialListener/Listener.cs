using System;
using System.Linq;
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
			string outputString = info.Buses.Aggregate("", (current, bus) => current + String.Format("{0}    {1}X", bus.LineNumber, bus.DepartTime));
			_writer.WriteToPort(outputString);

			Console.WriteLine("Writing this to COM4\n " + outputString);
		}
	}
}