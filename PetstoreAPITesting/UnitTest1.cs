using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.ComponentModel;
using Newtonsoft.Json;

namespace PetstoreAPITesting
{
    public class Tests
    {

        HttpClient client;
        PetsEndpoint pets;


        private void assertMessage(Dictionary<string,object> response, object code, object type, object message)
        {
            Assert.IsTrue(response.ContainsKey("code"));
            Assert.That(response["code"], Is.EqualTo(code));
            Assert.IsTrue(response.ContainsKey("type"));
            Assert.That(response["type"], Is.EqualTo(type));
            Assert.IsTrue(response.ContainsKey("message"));
            Assert.That(response["message"], Is.EqualTo(message));
            Assert.That(response.Count, Is.EqualTo(3));
        }

        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            pets = new PetsEndpoint(client);
        }

        [Test]
        public async Task AddPet_Then_DeletePet()
        {
            PetData data = new PetData().SetValidID(11);
            var postResponse = await pets.addPet(data);
           
            var deleteResponse = await pets.deletePet(11);
            assertMessage(deleteResponse, 200, "unknown", "11");
        }

        [Test]
        public async Task Cannot_Get_A_Pet_That_Does_Not_Exist()
        {
            PetData data = new PetData().SetValidID(267);
            await pets.deletePet(267);
            var response = await pets.getPet(267);
            assertMessage(response, 1, "error", "Pet not found");
        }

        [Test]
        public async Task Cannot_Update_A_Pet_That_Doesnot_Exist ()
        {
            PetData data = new PetData().SetValidID(267);
            var deleteResponse = await pets.deletePet(267);
            assertMessage(deleteResponse, 400, "error", "Pet not found");
            var putResponse = await pets.putPet(data);
        }

        [Test]
        public async Task User_Can_Get_Pet_That_Exists()
        {
            var response = await pets.getPet(10);
            Assert.IsTrue(response.ContainsKey("id"));
            Assert.IsTrue(response.ContainsKey("name"));
            Assert.IsTrue(response.ContainsKey("category"));
            Assert.IsTrue(response.ContainsKey("photoUrls"));
            Assert.IsTrue(response.ContainsKey("tags"));
            Assert.IsTrue(response.ContainsKey("status"));
            Assert.IsFalse(response.ContainsKey("age"));
            Assert.IsFalse(response.ContainsKey("STATUS"));
            Assert.That(response.Count, Is.EqualTo(6));
        }


        [Test]
        public async Task User_Cannot_Delete_A_Pet_That_Doesnt_Exist()
        {

            PetData data = new PetData().SetID(20);
            var response = await pets.addPet(data);
            var response2 = await pets.deletePet(1);
            assertMessage(response2, 200, "unknown", "230");
            var response3 = await pets.deletePet(1);
            Assert.IsNull(response3);

        }

        [Test]
        public async Task User_will_find_no_items_when_finding_unvaialable_status()
        {
            var response = await pets.findByStatus("Hello@");
            Assert.That(0, Is.EqualTo(response.Count()));
            Console.WriteLine(JsonConvert.SerializeObject(response));
        }

        [Test]
        public async Task User_will_find_items_when_finding_avialable_status()
        {
            var response = await pets.findByStatus("pending");
            Assert.That(response.Count(), Is.GreaterThan(0));
        }

        [Test]
        public async Task A_User_Cannot_Add_A_Pet_With_An_Invalid_Id()
        {
            PetData data = new PetData().SetID("hello");
            var response = await pets.addPet(data);
            assertMessage(response, 500, "unknown", "something bad happened");
        }


        [TearDown]
        public void Teardown()
        {
            client.Dispose();           
        }
    }
}

