using System;
using System.Text;
using ZMQ;

namespace SL.SerialListener
{
	public class Listener
	{
		private readonly Writer _writer;
	    private Context _contex;
        private Socket _subscriberSocket;


		public Listener()
		{
			InitBus();
			_writer = new Writer("COM4");
            Listen();
		}

	    private void Listen()
        {
            while(true)
            {
                string info = _subscriberSocket.Recv(Encoding.Unicode);
                SendToArduino(info);
                Console.WriteLine(info);
            }
        }

		private void InitBus()
		{
		    _contex = new Context(1);
		    _subscriberSocket = _contex.Socket(SocketType.SUB);
            _subscriberSocket.Subscribe("",Encoding.Unicode);
            _subscriberSocket.Connect("tcp://127.0.0.1:5556");
		}

		
		private void SendToArduino(string info)
		{
			_writer.WriteToPort(info);
			Console.WriteLine("Writing this to COM4\n " + info);
		}
	}
}