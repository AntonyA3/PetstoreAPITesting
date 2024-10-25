using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Web;
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

        /// <summary>
        /// Makes a get request to find pets with a particular status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>[]> findByStatus(string status)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["status"] = status;
            using HttpResponseMessage response = await this.client.GetAsync(url + "/findByStatus?" + query.ToString());
            return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(await response.Content.ReadAsStringAsync());
        }


        /// <summary>
        /// Makes a get request ro find pets with particular tags
        /// </summary>
        /// <param name="tags"></param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>[]> findByTags(string tags)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["tags"] = tags;
            using HttpResponseMessage response = await this.client.GetAsync(url + "/findByTags?" + query.ToString());
            return JsonConvert.DeserializeObject<Dictionary<string, object>[]>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Makes a pos request to upload an image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>> uploadImage(int id, string filePath)
        {            
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));
                using (HttpResponseMessage response = await this.client.PostAsync(url + "/" + id + "/uploadImage", content))
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
                }
            }
        }

        /// <summary>
        /// Deletes a pet from the API with a delete request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>> deletePet(object id) {
            using HttpResponseMessage response = await this.client.DeleteAsync(url + "/" + id);
            
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Gets a pet from the API with a get request
        /// </summary>
        /// <param name="id">This should be an integer for a successful response</param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>> getPet(object id)
        {
            using HttpResponseMessage response = await this.client.GetAsync(url + "/" + id);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }


        /// <summary>
        /// Adds a pet by sending a POST request
        /// </summary>
        /// <param name="data"> </param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>> addPet(PetData data)
        {
            var content = new StringContent(data.AsJSON(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }


        /// <summary>
        /// Updates a pet by sending a PUT request
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        async public Task<Dictionary<string, object>> putPet(PetData data)
        {
            var content = new StringContent(data.AsJSON(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(await response.Content.ReadAsStringAsync());
        }
    }
}
