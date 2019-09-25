using RestSharp;
using System;
using System.Configuration;

namespace APITest.Base
{
    public class Settings
    {
        public Uri BaseUrl = new Uri(ConfigurationManager.AppSettings["baseUrl"]);
        public IRestResponse Response;
        public IRestRequest Request;
        public RestClient RestClient = new RestClient();
    }
}
