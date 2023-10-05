using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace APIAutomation.Support
{
    public class APIHelper
    {

        private HttpClient _httpClient;
       // private dynamic? _Payload;

        public APIHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
           
        }



        public HttpClient AddHeaders( Dictionary<string, string> headers )
        {
            if(headers!=null)
            {
                foreach (string key in headers.Keys)
                {
                    _httpClient.DefaultRequestHeaders.Add(key, headers[key]);
                }

            }

            return _httpClient;
        }

       public async Task<HttpResponseMessage> PerformGetRequest(string requestURL) => await _httpClient.GetAsync(requestURL);
        public async Task<HttpResponseMessage> PerformDeleteRequest(string requestURL) => await _httpClient.DeleteAsync(requestURL);



        //public async Task<HttpResponseMessage> PerformGetRequest(string requestURL)
        //{
        //    Task<HttpResponseMessage> httpResponseMessage = _httpClient.GetAsync(requestURL);
        //    return await httpResponseMessage;
        //}





    }
}
