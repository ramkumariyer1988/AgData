using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.IO;

namespace AgDataAPI
{
    public class AgDataAPITestCases:AgDataAPIBasePage
    {
        [SetUp]
        public void Setup()
        {
            //Can write any authentication code here or token generation
        }

        [Test(Description ="Get API"),Order(1)]
        public void GetAPI()
        {
                var restClient = SetUpRestClient(AgDataAPIConstants.uri);
                var restRequest = GetRequest("posts");
                var response = restClient.Execute(restRequest);
                ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.Success);           
        }

        [Test(Description ="Get API Negative Test Case"), Order(1)]
        public void GetAPINegative()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = GetRequest("postss");
            var response = restClient.Execute(restRequest);
            ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.NotFound);
        }

        [Test(Description ="Get API With Parameters"), Order(1)]
        public void GetAPIWithParameters()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = GetRequest("comments").AddUrlSegment("postId", 1);
            var response = restClient.Execute(restRequest);
            ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.Success);
        }

        [Test(Description ="Post Request without parameters"), Order(1)]
        public void PostAPI()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = PostRequest("posts");
            var response = restClient.Execute(restRequest);
            ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.Created);
        }

        [Test(Description ="Post API Negative Scenario"), Order(1)]
        public void PostAPINegative()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = PostRequest("postss");
            var response = restClient.Execute(restRequest);
            ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.NotFound);
        }

        [Test(Description ="Post API with Payload"), Order(1)]
        public void PostAPIWithPayload()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = PostRequest("posts");
            restRequest.RequestFormat = RestSharp.DataFormat.Json;

            //Edit Json and post the Json as Payload to the API

            string json = File.ReadAllText(GetProjectPath()+AgDataAPIConstants.postPayloadJsonFile);

            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(GetProjectPath() + AgDataAPIConstants.postPayloadJsonFile, output.ToString());

            restRequest.AddParameter("application/json", output, ParameterType.RequestBody);
            var response = restClient.Execute(restRequest);
            var content = response.Content.ToString();

            //Converting the response into json object and accessing the arrays and objects
            dynamic responseJsonObj = JsonConvert.DeserializeObject(content);
            var code = response.StatusCode;
            ValidateResponseCode(code.ToString(), AgDataAPIConstants.Created);
        }
        [Test(Description ="Put API with Valid Payload"), Order(1)]
        public void PutAPI()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = PutRequest("posts/1");
            restRequest.RequestFormat = RestSharp.DataFormat.Json;

            //Edit Json and post the Json as Payload to the API
            string json = File.ReadAllText(GetProjectPath()+AgDataAPIConstants.putPayloadJsonFile);
            dynamic jsonObj = JsonConvert.DeserializeObject(json);

            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(GetProjectPath() + AgDataAPIConstants.putPayloadJsonFile, output.ToString());

            restRequest.AddParameter("application/json", output, ParameterType.RequestBody);
            var response = restClient.Execute(restRequest);
            var content = response.Content.ToString();

            //Converting the response into json object and accessing the arrays and objects
            dynamic responseJsonObj = JsonConvert.DeserializeObject(content);
            var code = response.StatusCode;
            ValidateResponseCode(code.ToString(), AgDataAPIConstants.Success);
        }

        [Test(Description ="Delete API"), Order(1)]
        public void DeleteAPI()
        {
            var restClient = SetUpRestClient(AgDataAPIConstants.uri);
            var restRequest = DeleteRequest("posts/1");
            var response = restClient.Execute(restRequest);
            ValidateResponseCode(response.StatusCode.ToString(), AgDataAPIConstants.Success);
        }

        
    }
}