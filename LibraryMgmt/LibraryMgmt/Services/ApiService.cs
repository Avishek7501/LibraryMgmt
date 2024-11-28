﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LibraryMgmt.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7253/api/")
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Generic GET method
        /// </summary>
        public async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                // Log the requested endpoint
                Console.WriteLine($"[ApiService] GET Request to: {endpoint}");

                // Perform the GET request
                var response = await _httpClient.GetAsync(endpoint);

                // Log response details
                Console.WriteLine($"[ApiService] Response: {(int)response.StatusCode} {response.ReasonPhrase}");

                // If the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    Console.WriteLine($"[ApiService] Response Body: {json}");

                    // Deserialize the response to the specified type
                    var result = JsonConvert.DeserializeObject<T>(json);

                    // Check for null deserialization result
                    if (result == null)
                    {
                        throw new Exception("Failed to deserialize response to the expected type.");
                    }

                    return result;
                }

                // If the response is unsuccessful, log the content and throw an exception
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[ApiService] Error Response Body: {errorContent}");

                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}. Details: {errorContent}");
            }
            catch (Exception ex)
            {
                // Log and rethrow the exception with added context
                Console.WriteLine($"[ApiService] Exception: {ex.Message}");
                throw new Exception($"Failed to GET from {endpoint}. Exception: {ex.Message}");
            }
        }

        /// <summary>
        /// Generic POST method
        /// </summary>
        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(responseJson);
                }

                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to POST to {endpoint}: {ex.Message}");
            }
        }

        /// <summary>
        /// Generic PUT method
        /// </summary>
        public async Task<bool> PutAsync(string endpoint, object data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to PUT to {endpoint}: {ex.Message}");
            }
        }

        /// <summary>
        /// Generic DELETE method
        /// </summary>
        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                throw new Exception($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to DELETE from {endpoint}: {ex.Message}");
            }
        }
    }
}
