using System;
using System.Collections.Generic;
using System.Net;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using SLParser.Domain;

namespace SL.Web
{
    public class WebParser
    {
        private string _url;
        private string _searchPath;

        public WebParser(string url, string path)
        {
            if(String.IsNullOrEmpty(path)) throw new ArgumentException("path can't be null or empty");
            if(String.IsNullOrEmpty(url)) throw new ArgumentException("url can't be null or empty");

            _url = url;
            _searchPath = path;
        }

        public void GetWebPage()
        {
            var htmlDocument = new HtmlDocument();
            try
            {
                var request = WebRequest.Create(_url);
                var response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                htmlDocument.Load(responseStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            ParseWebPage(htmlDocument.DocumentNode);
        }

        public IList<Bus> ParseWebPage(HtmlNode document)
        {
            var buses = document.QuerySelectorAll(_searchPath);

            return ConvertHtmlNodeToBuses(buses);
        }

        public static IList<Bus> ConvertHtmlNodeToBuses(IEnumerable<HtmlNode> buses)
        {
            if(buses==null) throw new ArgumentNullException("buses");

            var tempList = new List<HtmlNode>(buses);
            var myBuses = new List<Bus>();

            for(int i=0;i<tempList.Count;i += 3)
            {
                var tmpBus = new Bus
                                    {
                                        LineNumber = HtmlNodeToString(tempList[i]),
                                        Destination = HtmlNodeToString(tempList[i + 1]),
                                        DepartTime = HtmlNodeToString(tempList[i + 2])
                                    };

                myBuses.Add(tmpBus);
            }
            return myBuses;
        }

        public static string HtmlNodeToString(HtmlNode node)
        {
            if(node == null) throw new ArgumentNullException("node");
            return node.InnerText.ToString().Trim();
        }

    }
}
