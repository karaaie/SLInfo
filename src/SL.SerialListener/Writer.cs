using System;
using System.ComponentModel;
using System.IO.Ports;

namespace SL.SerialListener
{
	public class Writer
	{
		private SerialPort _arduinoPort;

		public Writer(string comPort)
		{
			ConfigurePort(comPort);
			_arduinoPort.Open();
		}

		public void WriteToPort(string msg)
		{
			if(!_arduinoPort.IsOpen) throw new Exception("Port is not open!");
			_arduinoPort.Write(msg);

		}

		private void ConfigurePort(string comPort)
		{
			IContainer components = new Container();
			_arduinoPort = new SerialPort(components);
			_arduinoPort.PortName = comPort;
			_arduinoPort.BaudRate = 9600;
		}
	}
}