using System;
using System.Linq;
using System.Text;
using SLParser.Domain;
using ZMQ;

namespace SL.Publisher
{
	public class PublisherWorker
	{
	    private Context _context;
	    private Socket _publishSocket;

		public PublisherWorker()
		{
			InitBus();
		}

		private void InitBus(){

            _context = new Context(1);
		    _publishSocket = _context.Socket(SocketType.PUB);
		    _publishSocket.Bind("tcp://127.0.0.1:5556");
		}

		public void Publish(RealtimeInfo info)
		{
            _publishSocket.Send(ConvertRealtimeInfoToString(info), Encoding.Unicode);
			Console.WriteLine("sending bus information");
		}

        private static string ConvertRealtimeInfoToString(RealtimeInfo info)
        {
            string output = "";
            foreach (var bus in info.Buses)
            {
                if (bus.LineNumber.Count() == 2)
                {
                    output += String.Format("{0}  {2} {1}X", bus.LineNumber, bus.DepartTime, bus.Destination);
                }
                else
                {
                    output += String.Format("{0} {2} {1}X", bus.LineNumber, bus.DepartTime, bus.Destination);
                }
            }

            return output;
        }

	}
}