using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetstoreAPITesting
{
    internal class PetsEndpoint
    {
        public static Uri url = new Uri("https://petstore.swagger.io/v2/pet");
        HttpClient client;  
        public PetsEndpoint(HttpClient client) { 
            this.client = client;
        }

        async public Task<Dictionary<string, object>[]> findByStatus(string status)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["status"] = status;
            using HttpResponseMessage response = await this.client.GetAsync(url + "/findByStatus?" + query.ToString());
            return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(await response.Content.ReadAsStringAsync());
        }


        async public Task<Dictionary<string, object>> findByTags()
        {
            //TODO
            var content = new StringContent("", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await this.client.DeleteAsync(url + "/findByTags");

            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }


        async public Task<Dictionary<string, object>> uploadImage(int id)
        {
            //TODO
            using HttpResponseMessage response = await this.client.DeleteAsync(url + "/" + id + "/uploadImage");
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }


        async public Task<Dictionary<string, object>> deletePet(int id) {
            using HttpResponseMessage response = await this.client.DeleteAsync(url + "/" + id);
            
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }

        async public Task<Dictionary<string, object>> getPet(int id)
        {
            using HttpResponseMessage response = await this.client.GetAsync(url + "/" + id);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }

        async public Task<Dictionary<string, object>> addPet(PetData data)
        {
            var content = new StringContent(data.AsJSON(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }

        async public Task<Dictionary<string, object>> putPet(PetData data)
        {
            var content = new StringContent(data.AsJSON(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }
    }
}
