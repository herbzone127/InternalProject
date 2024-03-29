﻿using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading;
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
                if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                {



                    var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                    if (user?.token != null)
                    {

                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer " + user.token);

                    }

                }
                //if (!string.IsNullOrEmpty(Global.Token))
                //{




                //   

                //}
                //else
                //{

                //}
                var response = await httpClient.GetAsync(new Uri(url));

                //response.EnsureSuccessStatusCode();
                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                    {
                        if (!Barrel.Current.IsExpired(url))
                        {
                            result= Barrel.Current.Get<T>(url); 
                        }
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        if (x.Result.ToLowerInvariant().Contains("status"))
                        {
                            result = JsonConvert.DeserializeObject<T>(x.Result);
                            Barrel.Current.Add<T>(url, result, TimeSpan.FromHours(1));
                        }

                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //throw new Exception( x.Exception;
                    }


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
                if (!Barrel.Current.IsExpired(UrlHelper.pilotOTPURl))
                {



                    var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                    if (user != null)
                    {

                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer " + user.token);

                    }

                }
                var serializeJson = JsonConvert.SerializeObject(postObject);
                HttpContent content = new StringContent(serializeJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);

                //response.EnsureSuccessStatusCode();

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                    {
                        if (!Barrel.Current.IsExpired(apiUrl))
                        {
                            result = Barrel.Current.Get<string>(apiUrl);
                        }
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        result = x.Result;
                        Barrel.Current.Add<string>(apiUrl, result, TimeSpan.FromHours(1));
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //throw x.Exception;
                    }


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
                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer "+token);
                var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                if (user != null)
                {

                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Authorization", $"Bearer " + user.token);

                }
                var serializeJson = JsonConvert.SerializeObject(postObject);
                HttpContent content = new StringContent(serializeJson, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(apiUrl, content).ConfigureAwait(false);

              

                await response.Content.ReadAsStringAsync().ContinueWith((Task<string> x) =>
                {
                    if (x.IsFaulted)
                    {
                        if (x.Result != null)
                            throw new Exception(x.Result);
                        if(x.Exception != null)
                        throw new Exception(x.Exception.Message, x.Exception);
                    }
                        
                    if (response.IsSuccessStatusCode)
                    {

                        result = x.Result;
                    }
                    if(response.StatusCode== System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.InternalServerError)
                    {
                        if (x.Result != null)

                            throw new Exception($"{x.Result},Token:{user.token}");
                        if(x.Exception != null)
                        throw new Exception(x.Exception?.Message, x.Exception);
                    }
                

                });

            }

            return result;
        }
    }
}
