using APITest.Base;
using APITest.Library;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITest.Tests
{
    public class ApiTesting : HttpTestBase
    {
        public Settings settings = new Settings();
        public static string strTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Data", "TestData.csv");
        List<string> TestData = TestDataHelper.ReadInCSV("TestData.csv");


        [Test]
        public void GetCurrentStatus()
        {
            settings.RestClient = new RestClient(settings.BaseUrl);
            settings.Request = new RestRequest("current", Method.GET);
            settings.Request.AddQueryParameter(TestData[4], TestData[8]);
            settings.Request.AddQueryParameter(TestData[5], TestData[9]);
            settings.Request.AddQueryParameter(TestData[6], TestData[10]);
            settings.Response = settings.RestClient.Execute(settings.Request);
            var resPonse = settings.Response.DeserializeResponse();
            var reSponse = settings.Response.json();
            Assert.IsTrue(reSponse.IsValid(Schema("GetCurrentStatus")));
            Assert.IsTrue(reSponse.SelectToken("data[0].state_code").ToString().Equals("NY"));
        }

        [Test]
        public void GetForecast()
        {
            settings.RestClient = new RestClient(settings.BaseUrl);
            settings.Request = new RestRequest("forecast/daily", Method.GET);
            settings.Request.AddQueryParameter(TestData[7], TestData[11]);
            settings.Request.AddQueryParameter(TestData[6], TestData[10]);
            settings.Response = settings.RestClient.Execute(settings.Request);
            var resPonse = settings.Response.DeserializeResponse();
            var reSponse = settings.Response.json();
            Assert.IsTrue(reSponse.IsValid(Schema("2")));
            var recordCount = reSponse.SelectToken("data").Count();
            string[] Date = new string[recordCount];
            for(int i = 0; i < recordCount; i++)
            {
                Date[i] = DateTime.Parse(reSponse.SelectToken("data[" + i + "].datetime").ToString()).ToUniversalTime().ToString();
                Console.WriteLine(Date[i]);
            }
        }
        

        public JSchema Schema(string response)
        {
            JSchema jsonSchema;
            string jsonSchemaString =null;
            if (response.Equals("GetCurrentStatus"))
            {
                jsonSchemaString = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Data", "Schema1.txt"));
                jsonSchema = JSchema.Parse(jsonSchemaString);
                return jsonSchema;
            }
            else
            {
                jsonSchemaString = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory + @"\Data", "Schema2.txt"));
                jsonSchema = JSchema.Parse(jsonSchemaString);
                return jsonSchema;
            }
        }
    }
}
