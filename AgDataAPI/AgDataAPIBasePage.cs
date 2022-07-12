using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgDataAPI
{
    public class AgDataAPIBasePage
    {
        public RestClient SetUpRestClient(string baseurl, string username = null, string password = null)
        {
            RestClient restClient = new RestClient(baseurl);
            restClient.Authenticator = new HttpBasicAuthenticator(username, password);
            return restClient;
        }
        public RestRequest PostRequest(string endpoint, string acceptToken = null)
        {

            RestRequest restRequest = new RestRequest(endpoint, Method.POST);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", acceptToken);
            return restRequest;
        }
        public RestRequest PutRequest(string endpoint, string acceptToken = null)
        {

            RestRequest restRequest = new RestRequest(endpoint, Method.PUT);
            restRequest.RequestFormat = DataFormat.Json;
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddHeader("Accept", acceptToken);
            return restRequest;
        }


        public RestRequest AddParameter(RestRequest restRequest, Dictionary<string, object> parameter)
        {
            foreach (KeyValuePair<string, object> entry in parameter)
            {
                restRequest.AddParameter(entry.Key, entry.Value);
            }
            return restRequest;
        }


        public RestRequest GetRequest(string endpoint, Dictionary<string, object> parameter, string acceptToken = null)
        {
            RestRequest getRequest = new RestRequest(endpoint, Method.GET);
            getRequest = AddParameter(getRequest, parameter);
            getRequest.RequestFormat = DataFormat.Json;
            getRequest.AddHeader("Content-Type", "application/json");
            getRequest.AddHeader("Accept", acceptToken);
            return getRequest;
        }

        public RestRequest GetRequest(string endpoint, string acceptToken = null)
        {
            RestRequest getRequest = new RestRequest(endpoint, Method.GET);
            getRequest.RequestFormat = DataFormat.Json;
            getRequest.AddHeader("Content-Type", "application/json");
            getRequest.AddHeader("Accept", acceptToken);
            return getRequest;
        }

        public RestRequest DeleteRequest(string endpoint, string acceptToken = null)
        {
            RestRequest deleteRequest = new RestRequest(endpoint, Method.DELETE);
            deleteRequest.RequestFormat = DataFormat.Json;
            deleteRequest.AddHeader("Content-Type", "application/json");
            deleteRequest.AddHeader("Accept", acceptToken);
            return deleteRequest;
        }

        public void ValidateResponseCode(string statusCode, string expStatus)
        {
            if (statusCode != expStatus)
            {
                Assert.Fail("Expected Response was : " + expStatus + " But Actual Response is : " + statusCode);
            }

        }

        public static string GetProjectPath()
        {
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));
            return new Uri(actualPath).LocalPath;
        }
    }
}
