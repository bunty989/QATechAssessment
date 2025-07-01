using RestSharp;

namespace APIAutomation.Base
{
    public class Settings
    {
        public Uri BaseUrl = new Uri(ConfigHelper.ConfigSetting("RestSharpConfig:", "Url"));
        public RestResponse Response;
        public RestRequest Request;
        public RestClient RestClient = new RestClient();
    }
}
