using System;
using System.Threading;
using Quartz;
using Quartz.Impl;
using SL.Web;

namespace SL.Publisher
{
    class Program
    {

        static void Main(string[] args)
        {
           	var service = new PublisherService();
			while(true)
			{
				service.Execute(null);
				Thread.Sleep(20000);
			}

        }

    }

	public class PublisherService : IJob
	{
		private const string Url =
	"http://realtid.sl.se/Default.aspx?epslanguage=SV&WbSgnMdl=4030-SmFybGFiZXJnIChOYWNrYSk%3d-_----";

		private const string Path = ".noBorder>tr>td";

		private PublisherWorker _publisherWorker;
		private WebParser _parser;


		public PublisherService()
		{
			_publisherWorker = new PublisherWorker();
			_parser = new WebParser(Url,Path);
		}

		public void Execute(IJobExecutionContext context)
		{
			var info = _parser.GetWebPage();
			_publisherWorker.Publish(info);
			Console.WriteLine("I finished a job now");
		}
	}
}
