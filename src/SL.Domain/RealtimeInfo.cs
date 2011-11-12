using System.Collections.Generic;

namespace SLParser.Domain
{
    public class RealtimeInfo
    {
		public RealtimeInfo()
		{
			Buses = new List<Bus>();
		}

        public List<Bus> Buses { get; set; }
    }
}