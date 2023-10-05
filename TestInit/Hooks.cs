using BoDi;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace APIAutomation.TestInit
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks

        private HttpClient? _httpClient;
        private IObjectContainer _objectContainer;
        private  ScenarioContext _scenarioContext;
        private ISpecFlowOutputHelper _specFlowOutputHelper;

        public Hooks( IObjectContainer objectContainer, ScenarioContext scenarioContext, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _objectContainer = objectContainer;
            _specFlowOutputHelper = specFlowOutputHelper;
            _scenarioContext = scenarioContext;
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)

        {


        }


        [BeforeScenario]
        public void BeforeScenario()
        {
            _httpClient = new HttpClient();
            _objectContainer.RegisterInstanceAs(_httpClient);
        }

       

        [AfterStep]
        public void AfterStep()
        {
            if (_scenarioContext.TestError != null)
            {

                string FailMsg = "<br><b>Message:</b>" + _scenarioContext.TestError.Message +
                                "<br><b> Inner Exception : </b>" + _scenarioContext.TestError.InnerException +
                                "<br><b> Stack Trace : </b>" + _scenarioContext.TestError.StackTrace;
                _specFlowOutputHelper.WriteLine(FailMsg);
            }
            
        }
    }
}