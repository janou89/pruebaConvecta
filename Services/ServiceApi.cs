using pruebaConecta.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Microsoft.DotNet.MSIdentity.Shared;

namespace pruebaConecta.Services
{
    public class ServiceApi: IServicesApi
    {
        private static string _apiUrl;
        private static string _headers;
        private static string _apiInfoUrl;

        public ServiceApi()
        {
            _apiUrl = getApiUrl();
            _headers = getHeaders();
            _apiInfoUrl = getApiInfo();
        }

        public string getApiUrl()
        {
            var apiUrl = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return apiUrl.GetSection("ApiSettings:apiUrl").Value;
        }

        public string getApiInfo()
        {
            var apiUrl = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return apiUrl.GetSection("ApiSettings:apiInfo").Value;
        }

        public string getHeaders()
        {
            var headers = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            return headers.GetSection("ApiSettings:Ocp-Apim-Subscription-Key").Value;
        }

        public async Task<List<Property>> GetAllPropertiesByPage(int pageNumber)
        {
            List<Property> properties = new List<Property>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);

                    var content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(_apiUrl+ "?PageNumber="+pageNumber, content);
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ApiResult>(jsonResponse);                      
                        properties = result.items;

                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return properties;
        }

        public async Task<PropertyDetails> GetPropertyById(int idProperty)
        {
            PropertyDetails propertyObj = new PropertyDetails();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);
                    var response = await client.GetAsync(_apiUrl + $"/{idProperty}");
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        propertyObj = JsonConvert.DeserializeObject<PropertyDetails>(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return propertyObj;
        }


       

        public async Task<ApiResult> GetAllResult(PropertySearch propertySearch)
        {
                        
            ApiResult apiResult = new ApiResult();
            StringContent content;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);
                    if (propertySearch.idType == 0 && propertySearch.idBorough == 0)
                    {
                        content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
                    }
                    else
                    {
                        
                        var body = JsonConvert.SerializeObject(propertySearch);
                        Console.WriteLine("Entrando al else -->" + body);
                        JObject obj = JObject.Parse(body);
                        if (propertySearch.idType == 0)
                        {
                            obj.Remove("idType");
                        }
                        if (propertySearch.idBorough == 0)
                        {
                            obj.Remove("idBorough");
                        }
                        string newBody = obj.ToString();
                        content = new StringContent(newBody, System.Text.Encoding.UTF8, "application/json");
                    }
                    var response = await client.PostAsync(_apiUrl, content);
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        List<Property> properties = new List<Property>();
                        var result = JsonConvert.DeserializeObject<ApiResult>(jsonResponse);
                        properties = result.items;
                        Pagination pagination = new Pagination
                        {
                            totalRecords = result.totalRecords,
                            pageNumber = result.pageNumber,
                            pageSize = result.pageSize,
                            totalPages = result.totalPages
                        };
                       
                        apiResult.items = properties;
                        apiResult.pagination = pagination;

                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return apiResult;
        }

        public async Task<List<Region>> getAllRegions()
        {
            List<Region> region = new List<Region>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiInfoUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);

                    var response = await client.GetAsync(_apiInfoUrl + "/regions");
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        region = JsonConvert.DeserializeObject<List<Region>>(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return region;
        }

        public async Task<List<Comuna>> getAllComuna()
        {
            List<Comuna> comuna = new List<Comuna>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiInfoUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);

                    var response = await client.GetAsync(_apiInfoUrl + "/boroughs");
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        comuna = JsonConvert.DeserializeObject<List<Comuna>>(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return comuna;
        }

        public async Task<List<PropertyType>> getAllPropertyType()
        {
            List<PropertyType> propertyType = new List<PropertyType>();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_apiInfoUrl);
                    client.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue { NoCache = true };
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _headers);

                    var response = await client.GetAsync(_apiInfoUrl + "/property-types");
                    Console.WriteLine("Response Status Code: " + response.StatusCode);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        propertyType = JsonConvert.DeserializeObject<List<PropertyType>>(jsonResponse);
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud: " + response.ReasonPhrase);
                    }
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Excepción de solicitud HTTP: " + e.Message);
            }
            return propertyType;
        }
    }
}
