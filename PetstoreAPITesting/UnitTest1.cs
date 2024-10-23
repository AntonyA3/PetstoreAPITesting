using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.ComponentModel;

namespace PetstoreAPITesting
{
    public class Tests
    {

        HttpClient client;
        PetsEndpoint pets;


        //TODO: Move to JSON HELPER CLASS



        private int countProperties(JsonElement response)
        {
            int propertyCount = 0;
            foreach (JsonProperty property in response.EnumerateObject())
            {
                propertyCount++;

            }
            return propertyCount;

        }
        [SetUp]
        public void Setup()
        {
            client = new HttpClient();
            pets = new PetsEndpoint(client);
        }

        [Test]
        public async Task Add_Then_DeletePet()
        {
            //var response = await pets.DeletePet(11);
        }

        [Test]
        public async Task Cannot_Get_A_Pet_That_Does_Not_Exist()
        {
            await pets.addAnyPetOfId(267);
            await pets.DeletePet(267);
            var response = await pets.getPet(267);
            Assert.IsTrue(response.TryGetProperty("code", out JsonElement code));
            Assert.That(code.GetInt32(), Is.EqualTo(1));
            Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            Assert.That(type.GetString(), Is.EqualTo("error"));
            Assert.IsTrue(response.TryGetProperty("message", out JsonElement message));
            Assert.That(message.GetString(), Is.EqualTo("Pet not found"));
            Assert.That(countProperties(response), Is.EqualTo(3));

        }

        [Test]
        public async Task Cannot_Update_A_Pet_That_Doesnot_Exist ()
        {
            await pets.addAnyPetOfId(267);
            await pets.DeletePet(267);
            var response = await pets.putAnyPetOfId(267);
            Assert.Fail();
            //Assert.IsTrue(response.TryGetProperty("code", out JsonElement code));
            //Assert.That(code.GetInt32(), Is.EqualTo(400));
            //Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            //Assert.That(type.GetString(), Is.EqualTo("error"));
            //Assert.IsTrue(response.TryGetProperty("message", out JsonElement message));
            //Assert.That(message.GetString(), Is.EqualTo("Pet not found"));
            //Assert.That(countProperties(response), Is.EqualTo(3));
        }

        [Test]
        public async Task User_Can_Get_Pet_That_Exists()
        {
            var response = await pets.getPet(10);
            Assert.IsTrue(response.TryGetProperty("id", out JsonElement _));
            Assert.IsTrue(response.TryGetProperty("name", out JsonElement _));
            Assert.IsTrue(response.TryGetProperty("category", out JsonElement _));
            Assert.IsTrue(response.TryGetProperty("photoUrls", out JsonElement _));
            Assert.IsTrue(response.TryGetProperty("tags", out JsonElement _));
            Assert.IsTrue(response.TryGetProperty("status", out JsonElement _));
            Assert.IsFalse(response.TryGetProperty("age", out JsonElement _));
            Assert.IsFalse(response.TryGetProperty("STATUS", out JsonElement _));
            Assert.That(countProperties(response), Is.EqualTo(6));
        }


        [Test]
        public async Task User_Cannot_Delete_A_Pet_That_Doesnt_Exist()
        {

        }

        [Test]
        public async Task A_User_Cannot_Add_A_Pet_With_An_Invalid_Id()
        {
            var response = await pets.addPetUncheckedId("hello");
            Assert.IsTrue(response.TryGetProperty("code", out JsonElement code));
            Assert.That(code.GetInt32(), Is.EqualTo(400));
            Assert.IsTrue(response.TryGetProperty("type", out JsonElement type));
            Assert.That(type.GetString(), Is.EqualTo("unknown"));
            Assert.IsTrue(response.TryGetProperty("message", out JsonElement message));
            StringAssert.Contains("bad input", message.GetString());

        }


        [TearDown]
        public void Teardown()
        {
            client.Dispose();           
        }
    }
}

