using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using NUnit.Framework;
using SL.Web;

namespace SL.Tests.WebTests
{
    public class WebParserTests
    {

        [Test]    
        public void WebParser_ctor_url_null()
        {

            Assert.Throws<ArgumentException>(
                delegate
                    {

                        new WebParser(null, "");
                    }
                );
        }

        [Test]
        public void WebParser_ctor_url_empty()
        {
            Assert.Throws<ArgumentException>(
                delegate
                {

                    new WebParser(null, "");
                }
                );
        }

        [Test]
        public void WebParser_ctor_path_null()
        {
            Assert.Throws<ArgumentException>(
                delegate
                    {
                        new WebParser("", null);
                    });
        }

        [Test]
        public void WebParser_ctor_path_empty()
        {
            Assert.Throws<ArgumentException>(
                delegate
                    {
                        new WebParser("", null);
                    });
        }

        [Test]
        public void WebParser_GetWebPage()
        {
            
            var webparser =
                new WebParser(
                    "http://realtid.sl.se/Default.aspx?epslanguage=SV&WbSgnMdl=4030-SmFybGFiZXJnIChOYWNrYSk%3d-_----",
                    ".noBorder>tr>td");

            Assert.IsNotNull(webparser);

        }

        [Test]
        public void WebParser_GetWebPage_incorrect_adresss()
        {
            Assert.DoesNotThrow(
                    () =>new WebParser("non-existing-adress", "non-existing-path")
                );
        }

        [Test]
        public void WebParser_HtmlNodeToString_non_null_parameter()
        {
            var htmlNode = HtmlNode.CreateNode("<b>hej</b>");
            var answer = WebParser.HtmlNodeToString(htmlNode);

            Assert.AreEqual("hej",answer);

        }

        [Test]
        public void WebParser_HtmlNodeToString_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(() => WebParser.HtmlNodeToString(null));
        }

        [Test]
        public void WebParser_ConvertHtmlNodeToBuses_correct_parameter()
        {
            var firstNode = HtmlNode.CreateNode("<tr>A</tr>");
            var secondNode = HtmlNode.CreateNode("<tr>B</tr>");
            var thirdNode = HtmlNode.CreateNode("<tr>C</tr>");

            var nodeList = new List<HtmlNode>();
            nodeList.Add(firstNode);
            nodeList.Add(secondNode);
            nodeList.Add(thirdNode);

            var busList = WebParser.ConvertHtmlNodeToBuses(nodeList);

            Assert.AreEqual(1,busList.Buses.Count);

            var bus = busList.Buses[0];

            Assert.AreEqual("A",bus.LineNumber);
            Assert.AreEqual("B",bus.Destination);
            Assert.AreEqual("C",bus.DepartTime);
        }

        [Test]
        public void WebParser_ConvertHtmlNodeToBuses_null_parameter()
        {
            Assert.Throws<ArgumentNullException>(
                () => WebParser.ConvertHtmlNodeToBuses(null)
                );
        }
    }
}