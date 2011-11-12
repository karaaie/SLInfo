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
			new Listener();
		}
    }
}
