using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;


namespace APIAutomation.Library
{
    public static class CustomLibraries
    {
        public static Dictionary<string, string> DeserializeResponse(this RestResponse restResponse)
        {
            if (string.IsNullOrWhiteSpace(restResponse.Content))
                return [];
            var JSONObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(restResponse.Content);
            return JSONObj ?? [];
        }

        public static JObject? json(this RestResponse restResponse)
        {
            var obs = JObject.Parse(restResponse.Content);
            return obs;
        }

        public static List<Dictionary<string, string>> DeserializeResponseToList(this RestResponse restResponse)
        {
            if (string.IsNullOrWhiteSpace(restResponse.Content))
                return [];
            var list = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(restResponse.Content);
            return list ?? [];
        }

        public static async Task<RestResponse<T>> ExecuteAsyncRequest<T>(this RestClient client, RestRequest request) where T : class, new()
        {
            var restResponse = await client.ExecuteAsync<T>(request);
            if (restResponse.ErrorException != null)
            {
                const string message = "Error retrieving response.";
                throw new ApplicationException(message, restResponse.ErrorException);
            }

            return restResponse;
        }

        public static object? DeserializeResponseFlexible(this RestResponse restResponse)
        {
            if (string.IsNullOrWhiteSpace(restResponse.Content))
                return null;

            var token = JToken.Parse(restResponse.Content);
            if (token.Type == JTokenType.Array)
                return token.ToObject<List<Dictionary<string, string>>>();
            if (token.Type == JTokenType.Object)
                return token.ToObject<Dictionary<string, string>>();
            return null;
        }
    }
}
