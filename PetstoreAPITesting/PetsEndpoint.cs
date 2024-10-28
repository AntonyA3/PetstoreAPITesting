using System.Text;
using System.Text.Json;

namespace PetstoreAPITesting
{
    /// <summary>
    /// These are some of the helper functions assertions and events
    /// that will occur as part of Creating Reading Updating and Deleting data 
    /// within the petstore API for the pets endpout
    /// </summary>
    public class PetsEndpoint
    {

        public async Task<JsonElement> When_I_Create_A_Pet(PetTestData petdata)
        {
            TestContext.WriteLine("When I POST pet described as: " + petdata.GetCasename());
            return await this.postPet(petdata.GetValue().AsJSON());
        }

        public void Expect_invalid_pet_unknown_message(JsonElement response)
        {
            TestContext.WriteLine("Expect pet unknown message");

            Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            Assert.That(type.GetString(), Is.EqualTo("unknown"));
        }


        public void Expect_A_Pet(PetData pet, JsonElement response)
        {
            TestContext.WriteLine("Expect pet as response");
            Assert.IsTrue(response.TryGetProperty("id", out JsonElement id));
            Assert.IsTrue(response.TryGetProperty("name", out JsonElement name));
            Assert.IsTrue(response.TryGetProperty("status", out JsonElement status));
            Assert.IsTrue(response.TryGetProperty("category", out JsonElement category));
            Assert.IsFalse(response.TryGetProperty("age", out JsonElement _));
            Assert.IsFalse(response.TryGetProperty("STATUS", out JsonElement _));
            
            Assert.IsTrue(response.TryGetProperty("photoUrls", out JsonElement photoUrls));
            Assert.IsTrue(response.TryGetProperty("tags", out JsonElement tags));
            Assert.That(response.EnumerateObject().Count(), Is.EqualTo(6));
            


            Assert.That(id.GetInt64(), Is.EqualTo(pet.GetID()));
            Assert.That(name.GetString(), Is.EqualTo(pet.GetName()));
            Assert.That(status.GetString(), Is.EqualTo(pet.GetStatus()));
            
            Assert.That(category.GetProperty("id").GetInt64(), Is.EqualTo(pet.GetCategoryId()));
            Assert.That(category.GetProperty("name").GetString(), Is.EqualTo(pet.GetCategoryName()));

            for(int i = 0; i < tags.EnumerateArray().Count(); i++)
            {
                Assert.IsTrue(tags.EnumerateArray().ElementAt(i).TryGetProperty("id", out JsonElement tagId));
                Assert.IsTrue(tags.EnumerateArray().ElementAt(i).TryGetProperty("name", out JsonElement tagName));

                Assert.That(tagId.GetInt64(), Is.EqualTo(pet.GetTagID(0)));
                Assert.That(tagName.GetString(), Is.EqualTo(pet.GetTagName(0)));

            }

            for (int i = 0; i < photoUrls.EnumerateArray().Count(); i++)
            {
                Assert.That(photoUrls.EnumerateArray().ElementAt(i).GetString(), Is.EqualTo(pet.GetPhotoUrl(0)));
            }
        }

        public void Expect_invalid_pet_error_message(JsonElement response)
        {
            TestContext.WriteLine("Expect invalid pet error message");
            Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            Assert.That(type.GetString(), Is.EqualTo("error"));
        }


        public void Expect_Empty_List_Of_Pets(JsonElement response)
        {
            TestContext.WriteLine("Expect Empty List Of Pets");
            Assert.That(response.GetArrayLength(), Is.EqualTo(0));

        }

        public void Expect_Non_Empty_List_of_Pets(JsonElement response)
        {
            TestContext.WriteLine("Expect None Empty List Of Pets");
            Assert.That(response.GetArrayLength(), Is.Not.EqualTo(0));
        }

    

        public void ExpectASuccessMessage(JsonElement response)
        {
            TestContext.WriteLine("Expect a success message");
            Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            Assert.That(type.GetString(), Is.EqualTo("unknown"));
        }

        public async Task<JsonElement> WhenIFindPetsByStatus(object status){
            TestContext.WriteLine("When I find pets by status: " + status);
            return await this.findByStatus(status);
        }


        public async Task And_a_pet_does_not_exist(object id)
        {
            TestContext.WriteLine("And a pet with id: " + id + " does not exist");
            await this.deletePet(id);
        }


        public async Task<JsonElement> When_I_Delete_A_Pet(object id)
        {
            TestContext.WriteLine("When I DELETE a pet with the id: " + id);
            return await this.deletePet(id);
        }

        public async Task<JsonElement> When_I_Update_A_pet(PetTestData petdata)
        {
            TestContext.WriteLine("When I PUT pet described as: " + petdata.GetCasename());
            return await this.putPet(petdata.GetValue().AsJSON());
        }

        public async Task<JsonElement> When_I_Get_A_Pet(object id)
        {
            TestContext.WriteLine("When I GET a pet with the id: " + id);
            return await this.getPet(id);
        }

        public static Uri url = new Uri("https://petstore.swagger.io/v2/pet");
        HttpClient client;  
        public PetsEndpoint(HttpClient client) { 
            this.client = client;
        }
        /// <summary>
        /// Makes a get request to find pets with a particular status
        /// </summary>
        /// <param name="status">the query string arguments for the status</param>
        /// <returns>the http response as a Json Element</returns>
        async public Task<JsonElement> findByStatus(object status)
        {       
            using HttpResponseMessage response = await this.client.GetAsync(url + "/findByStatus?status=" + status);
            using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }


        /// <summary>
        /// Makes a get request ro find pets with particular tags
        /// </summary>
        /// <param name="tags">The query string to used to find tags</param>
        /// <returns>the http response as a Json element</returns>
        async public Task<JsonElement> findByTags(object tags)
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await this.client.GetAsync(url + "/findByTags?tags=" + tags);
            using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }

        /// <summary>
        /// Makes a POST request to upload an image
        /// </summary>
        /// <param name="id">The id of the pet to upload the image to the pet</param>
        /// <param name="filePath">The file path of the image to be uploaded</param>
        /// <returns>A http response as a Json Element</returns>
        async public Task<JsonElement> uploadImage(object id, string filePath)
        {            
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StreamContent(File.OpenRead(filePath)), "file", Path.GetFileName(filePath));
                using (HttpResponseMessage response = await this.client.PostAsync(url + "/" + id + "/uploadImage", content))
                {
                    using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
                    {
                        JsonElement root = document.RootElement;
                        return root.Clone();
                    }
                }
            }
        }

        /// <summary>
        /// Deletes a pet from the API with a delete request
        /// </summary>
        /// <param name="id">The Id of the pet to delete</param>
        /// <returns>A http response as a Json Element</returns>
        async public Task<JsonElement> deletePet(object id) {
            using (HttpResponseMessage response = await this.client.DeleteAsync(url + "/" + id))
            {
                string res = await response.Content.ReadAsStringAsync();
                if (res.Count() == 0) {
                    res = "{}";
                }
                using (JsonDocument document = JsonDocument.Parse(res))
                {
                    JsonElement root = document.RootElement;
                    return root.Clone();
                }
                
            }
        }

        /// <summary>
        /// Gets a pet from the API with a get request
        /// </summary>
        /// <param name="id">This should be an integer for a successful response</param>
        /// <returns>The Http Response as a Json Element</returns>
        async public Task<JsonElement> getPet(object id)
        {
            using HttpResponseMessage response = await this.client.GetAsync(url + "/" + id);
            using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }


        /// <summary>
        /// Adds a pet by sending a POST request
        /// </summary>
        /// <param name="data">The pet data in json format that will be the request content</param>
        /// <returns>The Http response as a Json Element</returns>
        async public Task<JsonElement> postPet(JsonElement data)
        {
            var content = new StringContent(data.GetRawText(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }


        /// <summary>
        /// Updates a pet by sending a PUT request
        /// </summary>
        /// <param name="data">The pet data in json format that will be the request content</param>
        /// <returns>The Http response as a Json Element</returns>
        async public Task<JsonElement> putPet(JsonElement data)
        {
            var content = new StringContent(data.GetRawText(), Encoding.UTF8, "application/json");
            using HttpResponseMessage response = await client.PostAsync("https://petstore.swagger.io/v2/pet", content);
            using (JsonDocument document = JsonDocument.Parse(await response.Content.ReadAsStringAsync()))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }
    }
}
