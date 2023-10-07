using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using APIAutomation.APISupport;
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

        public async Task<HttpResponseMessage> PerformPostRequest(string requestURL, dynamic payLoad)
        {

            Task<HttpResponseMessage> postResponse;
            if(payLoad!=null)

            {
                var jsonString = HandleContent.SerializeJsonString(payLoad);
                HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/vnd.api+json");
                postResponse = _httpClient.PostAsync(requestURL, httpContent);
                     
            }
            else
            {
                postResponse = _httpClient.PostAsync(requestURL, null);

            }
            return await postResponse;
            
        }


        public Task<string> ParamsToStringAsync(Dictionary<string,string> urlparams)
        {

            using (HttpContent content = new FormUrlEncodedContent(urlparams))
                return content.ReadAsStringAsync();

        }



        public int GetResponseCode(Task<HttpResponseMessage> httpResponse)
        {
            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            return (int)httpResponseMessage.StatusCode;
        }


        public T GetResponseData<T>(Task<HttpResponseMessage> httpResponse)
        {


            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            HttpContent httpContent = httpResponseMessage.Content;
            Task<string> responseData = httpContent.ReadAsStringAsync();
            return HandleContent.GetJsonContent<T>(responseData.Result);

        }



        public string GetResponseDataWithoutDTO(Task<HttpResponseMessage> httpResponse)
        {


            HttpResponseMessage httpResponseMessage = httpResponse.Result;
            HttpContent httpContent = httpResponseMessage.Content;
            Task<string> responseData = httpContent.ReadAsStringAsync();
            return responseData.Result.ToString();

        }

        public Dictionary<string,string> ConvertTabletoDictionary(Table table)
        {
            Dictionary<string, string> queryParams = new Dictionary<string, string>();
            foreach (var item in table.Rows)
            {
                queryParams.Add(item[0], item[1]);

            }
            return queryParams;

        }


        





        //public async Task<HttpResponseMessage> PerformGetRequest(string requestURL)
        //{
        //    Task<HttpResponseMessage> httpResponseMessage = _httpClient.GetAsync(requestURL);
        //    return await httpResponseMessage;
        //}





    }
}
