using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace PetstoreAPITesting
{
    internal class PetsEndpoint
    {
        public static Uri url = new Uri("https://petstore.swagger.io/v2/pet");
        HttpClient client;  
        public PetsEndpoint(HttpClient client) { 
            this.client = client;
        }


        private JsonElement getJsonElement(string response)
        {
            JsonElement element;
            using (JsonDocument document = JsonDocument.Parse(response))
            {
                element = document.RootElement.Clone();
            }
            return element;
        }


       
        async public Task<JsonElement> DeletePet(int id) {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = PetsEndpoint.url;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE"); request.Method = HttpMethod.Delete;

            var content = new StringContent("", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await this.client.DeleteAsync(url + "/" + id);
            return getJsonElement(await response.Content.ReadAsStringAsync());
        }

        async public Task<JsonElement> getPet(int id)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE");
            request.Method = HttpMethod.Get;
            using HttpResponseMessage response = await this.client.GetAsync(url + "/" + id);
            return getJsonElement(await response.Content.ReadAsStringAsync());

        }

        async public Task<JsonElement> addAnyPetOfId(int id)
        {
            return await addPetUncheckedId(id.ToString());
           
        }

        async public Task<JsonElement> addPetUncheckedId(string id){
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://petstore.swagger.io/v2/pet");
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE");
            var content = new StringContent(
            @"{
              ""id"":" + id + @",
              ""name"": ""Baz"",
              ""category"": {
                ""id"": 0,
                ""name"": ""Dogs""
              },
              ""photoUrls"": [
                ""string""
              ],
              ""tags"": [
                {
                  ""id"": 0,
                  ""name"": ""string""
                }
              ],
              ""status"": ""available""
            }"""
                , Encoding.UTF8, "application/json");

            using HttpResponseMessage response = await client.PutAsync("https://petstore.swagger.io/v2/pet", content);
            return getJsonElement(await response.Content.ReadAsStringAsync());
        }

        async public Task<JsonElement> putAnyPetOfId(int id)
        {
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://petstore.swagger.io/v2/pet");
            request.Method = HttpMethod.Put;
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, DELETE");
            var content = new StringContent(
            @"{
              ""id"":" + id + @",
              ""name"": ""Baz"",
              ""category"": {
                ""id"": 0,
                ""name"": ""Dogs""
              },
              ""photoUrls"": [
                ""string""
              ],
              ""tags"": [
                {
                  ""id"": 0,
                  ""name"": ""string""
                }
              ],
              ""status"": ""available""
            }"""
                , Encoding.UTF8, "application/json");


            using HttpResponseMessage response = await client.PutAsync("https://petstore.swagger.io/v2/pet", content);
            return getJsonElement(await response.Content.ReadAsStringAsync());


        }

        //async public Task<string> modifyPet()
        //{

        //}
    }
}
