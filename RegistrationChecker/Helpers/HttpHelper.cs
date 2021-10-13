using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using RegistrationChecker.Models;
using RegistrationChecker.Mapping;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.Linq;
using RegistrationChecker.Enums;

namespace RegistrationChecker.Helpers
{
    public static class HttpHelper
    {
        public static async Task<SearchResult> SendRequest(string registrationNumber)
        {
            // Add the API key for VES to all requests from the client
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-api-key", ConfigurationManager.AppSettings.Get("X_API_Key"));

            // Append the reg number to the query field in the url
            string requestUrl = string.Concat(ConfigurationManager.AppSettings.Get("API_PATH"), registrationNumber);

            try
            {
                // Log request
                LogHelper.LogMessage(LogMessageType.Info, $"{ConfigurationManager.AppSettings.Get("SEND_MESSAGE_INFO")}{registrationNumber}");

                // Send request
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                return await HandleResponse(response, registrationNumber);
            }
            catch (Exception ex)
            {
                // Return a generic message, but log the actual error for further inspection if needed
                SearchResult result = new SearchResult
                {
                    Success = false,
                    Message = $"{ConfigurationManager.AppSettings.Get("UNEXPECTED_ERROR")}"
                };

                // Log error and send back result
                LogHelper.LogMessage(LogMessageType.Error, $"{ConfigurationManager.AppSettings.Get("SEND_MESSAGE_ERROR")}{registrationNumber}: {ex.Message}");
                return result;
            }
        }        
        
        private static async Task<SearchResult> HandleResponse(HttpResponseMessage response, string registrationNumber)
        {
            SearchResult result = new SearchResult();

            if (response.IsSuccessStatusCode)
            {
                // Get data from the response and map it to our search result
                var data = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ApiResponse>>(data).First();
                result = Maps.MapApiResponseToSearchResult(model);

                // Log the response
                LogHelper.LogMessage(LogMessageType.Info, $"{ConfigurationManager.AppSettings.Get("RECEIVE_MESSAGE_INFO")}{registrationNumber}: {data}");
            }
            else
            {
                // Update result object
                result.Success = false;
                result.Message = $"{ConfigurationManager.AppSettings.Get("NO_TESTS")}{registrationNumber}";

                // Log response
                LogHelper.LogMessage(LogMessageType.Info, $"{ConfigurationManager.AppSettings.Get("RECEIVE_MESSAGE_ERROR")}{registrationNumber}");
            }

            return result;
        }
    }
}