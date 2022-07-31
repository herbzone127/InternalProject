using IdeaForge.Core.Utilities;
using IdeaForge.Domain;

using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IdeaForge.Data
{
    /// <summary>
    /// A generic wrapper class to REST API calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class HTTPClientWrapper<T> where T : class
    {
        /// <summary>
        /// For getting the resources from a web api
        /// </summary>
        /// <param name="url">API Url</param>
        /// <returns>A Task with result object of type T</returns>
        public static async Task<T> Get(string url)
        {
            T result = null;
            using (var httpClient = new HttpClient())
            {
                if (!string.IsNullOrEmpty(Global.Token))
                {



                    
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer " + Global.Token);

                }
                var response =await httpClient.GetAsync(new Uri(url));

                response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = JsonConvert.DeserializeObject<T>(x.Result);
                });
            }

            return result;
        }

        

        /// <summary>
        /// For creating a new item over a web api using POST
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="postObject">The object to be created</param>
        /// <returns>A Task with created item</returns>
        public static async Task<string> PostRequest(string apiUrl, T postObject) 
        {
            string result = null;

            using (var client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(Global.Token))
                {




                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer " + Global.Token);

                }
                var serializeJson = JsonConvert.SerializeObject(postObject);
                HttpContent content = new StringContent(serializeJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = x.Result;

                });
            
            }

            return result;
        }

        /// <summary>
        /// For updating an existing item over a web api using PUT
        /// </summary>
        /// <param name="apiUrl">API Url</param>
        /// <param name="putObject">The object to be edited</param>
        public static async Task PutRequest(string apiUrl, T putObject)
        {
            using (var client = new HttpClient())
            {
                var serializeJson = JsonConvert.SerializeObject(putObject);
                HttpContent content = new StringContent(serializeJson);
                var response = await client.PutAsync(apiUrl, content).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
        }

        public static async Task<string> PostRequestToken(string apiUrl, T postObject,string token)
        {
            string result = null;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer "+token);
                var serializeJson = JsonConvert.SerializeObject(postObject);
                HttpContent content = new StringContent(serializeJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                        throw x.Exception;

                    result = x.Result;

                });

            }

            return result;
        }
    }
}
