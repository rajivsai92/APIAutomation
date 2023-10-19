using System.Net.Cache;
using APIAutomation.Configuration;
using APIAutomation.Support;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace APIAutomation.Tests.StepDefinitions
{
    [Binding]
    public sealed class CommonStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly FeatureContext _featureContext;

        private ReadConfig _readConfig;
        private APIHelper _apiHelper;
        private
        public string ApiURL { get; set; }

        public CommonStepDefinitions(ScenarioContext scenarioContext, 
            FeatureContext featureContext,
            ReadConfig readConfig,
            APIHelper aPIHelper)
        {
            this._scenarioContext = scenarioContext;
            this._featureContext = featureContext;
            this._readConfig = readConfig;
            this._apiHelper = aPIHelper;

        }

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        [Given(@"I Set API Call with baseurl and endpoint as (.*)")]
        public void GivenISetAPICallWithBaseurlAndEndpointAs(string endpoint)
        {

            ApiURL = _readConfig.GetConfigData()["baseURL_" + (_readConfig.GetConfigData()["Environment"])] + (endpoint.TrimStart().TrimEnd());
            _scenarioContext.Add("ApiURL", ApiURL);

        }

        [When(@"I set (.*) as path parameter to base url")]
        public void WhenISetVariableAsPathParameterToBaseUrl(string pathVariable)
        {
            ApiURL = ApiURL + "?" + "expand=" + pathVariable;
            _scenarioContext.Remove("ApiURL");
            _scenarioContext.Add("ApiURL", ApiURL);



        }

        [Then(@"I Set Additional  Request headers")]
        public void ThenISetAdditionalRequestHeaders(Table requestHeader)
        {
            if(requestHeader.RowCount !=0)
            {

                _apiHelper.AddHeaders(_apiHelper.ConvertTabletoDictionary(requestHeader));

            }


        }
        [Then(@"I Set Default Request headers")]
        public void ThenISetDefaultRequestHeaders(Table requestHeader)
        {
            


        }




        [Then(@"I perform GET request")]
        public void ThenIPerformGETRequest()
        {
            _scenarioContext.Add("httpResponse", _apiHelper.PerformGetRequest(_scenarioContext["ApiURL"].ToString()));
        }

        [Then(@"Validate the response status as (.*)")]
        public void ThenValidateTheResponseStatusAs(int statusCode)
        {
            Assert.AreEqual(statusCode, _apiHelper.GetResponseCode(_scenarioContext["httpResponse"] as Task<HttpResponseMessage>));


        }

        


    }

}