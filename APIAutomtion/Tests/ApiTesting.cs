using APIAutomation.Base;
using APIAutomation.Library;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;

namespace APIAutomation.Tests
{
    public class ApiTesting : HttpTestBase
    {
        public Settings settings = new Settings();
        public static string strTestDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "TestData.csv");
        List<string> TestData = TestDataHelper.ReadInCSV("TestData.csv");


        [Test]
        [Retry(3)]
        public void GetCurrentStatus()
        {
            settings.RestClient = new RestClient(settings.BaseUrl);
            settings.Request = new RestRequest("current", Method.Get);
            settings.Request.AddQueryParameter(TestData[4], TestData[8]);
            settings.Request.AddQueryParameter(TestData[5], TestData[9]);
            settings.Request.AddQueryParameter(TestData[6], TestData[10]);
            settings.Response = settings.RestClient.Execute(settings.Request);
            var reSponse = settings.Response.json();
            Assert.That(reSponse.IsValid(Schema("GetCurrentStatus")), Is.True);
            Assert.That(reSponse.SelectToken("data[0].state_code").ToString().Equals("NY"), Is.True);
        }

        [Test]
        [Retry(3)]
        public void GetForecast()
        {
            settings.RestClient = new RestClient(settings.BaseUrl);
            settings.Request = new RestRequest("forecast/daily", Method.Get);
            settings.Request.AddQueryParameter(TestData[7], TestData[11]);
            settings.Request.AddQueryParameter(TestData[6], TestData[10]);
            settings.Response = settings.RestClient.Execute(settings.Request);
            var reSponse = settings.Response.json();
            Assert.That(reSponse.IsValid(Schema("2")), Is.True);
            var recordCount = reSponse.SelectToken("data").Count();
            string[] Date = new string[recordCount];
            for(int i = 0; i < recordCount; i++)
            {
                Date[i] = DateTime.Parse(reSponse.SelectToken("data[" + i + "].ob_time").ToString()).ToUniversalTime().ToString();
                Console.WriteLine(Date[i]);
            }
        }
        

        public static JSchema? Schema(string response)
        {
            //JSchema jsonSchema;
            var jsonSchemaString = response.Equals("GetCurrentStatus") ?
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Schema1.txt"))
                : File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Schema2.txt"));
            var jsonSchema = JSchema.Parse(jsonSchemaString);
            return jsonSchema;
        }
    }
}
