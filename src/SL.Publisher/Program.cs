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

			/*
			// construct a scheduler factory
			ISchedulerFactory schedFact = new StdSchedulerFactory();

			// get a scheduler
			IScheduler sched = schedFact.GetScheduler();
			sched.Start();

			// construct job info
			JobDetail jobDetail = new JobDetail("SLPublisher", null, typeof(PublisherService));
			
			
			Trigger trigger = new SimpleTrigger("myTrigger",
								null,
								DateTime.UtcNow,
								null,
								SimpleTrigger.RepeatIndefinitely,
								TimeSpan.FromSeconds(20)) {StartTimeUtc = DateTime.UtcNow, Name = "SLPublish"}; 

			// start on the next even hour

        	sched.ScheduleJob(jobDetail, trigger); 
			sched.Start();
			 * */

			/*
			IJobDetail job = JobBuilder.Create<PublisherService>()
				.WithIdentity("job1", "group1")
				.Build();
			ITrigger trigger = TriggerBuilder.Create()
				.WithIdentity("trigger1", "group1")
				.StartAt(DateTime.UtcNow)
				.WithSimpleSchedule(x => x.WithIntervalInSeconds(30))
				.Build();

			ISchedulerFactory schedFact = new StdSchedulerFactory();

			// get a scheduler
			IScheduler sched = schedFact.GetScheduler();
        	sched.ScheduleJob(job, trigger);
			sched.Start();
			*/
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
