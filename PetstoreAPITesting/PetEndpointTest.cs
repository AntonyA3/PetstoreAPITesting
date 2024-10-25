
using Newtonsoft.Json;
using PetstoreAPITesting;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetstoreAPITesting
{
    public class TestPet
    {
        static string imageFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/images";
        static string longPetName = new string('A', 1000);
        HttpClient client;
        PetsEndpoint pets;
        private static readonly PetTestData[] ValidPets =
        [
            new PetTestData(
                "Default Valid Pet",
                new PetData()
            ),
            new PetTestData(
                "Valid Category {id = 1, name = \"Good\"}",
                new PetData().SetCategory(new CategoryData().SetID(1).SetName("Good"))
            ),
            new PetTestData(
                "Category Name",
                new PetData().SetCategory(new CategoryData().SetID(1).SetName("Good"))
            ),
            new PetTestData(
                "Category Name with whitespace",
                new PetData().SetCategory(new CategoryData().SetID(0).SetName("The Best"))
            ),
            new PetTestData(
                "Category name with hyphen",
                new PetData().SetCategory(new CategoryData().SetID(0).SetName("The-Best"))
            ),
            new PetTestData(
                "Standard Name",
                new PetData().SetCategory(new CategoryData().SetID(1).SetName("Doge"))
            ),
            new PetTestData(
                "Name with hyphen",
                new PetData().SetCategory(new CategoryData().SetID(0).SetName("N-euon"))
            ),
            new PetTestData(
                "Name with space",
                new PetData().SetCategory(new CategoryData().SetID(0).SetName("High Sierra"))
            ),
            new PetTestData(
                "Status is pending",
                new PetData().SetStatus("pending")
            ),
            new PetTestData(
                "Status is active",
                new PetData().SetStatus("active")
            ),
            new PetTestData(
                "Valid Photo URL",
                new PetData().SetPhotoUrls(new string[]{"bazinga"})
            )

        ];
        private static readonly PetTestData[] InvalidPets = [
            new PetTestData(
                "Pet With A string of a number as an ID",
                new PetData().SetID("212")
            ),
            new PetTestData(
                "Pet With A floating point number as an ID",
                new PetData().SetID(21.2)
            ),
            new PetTestData(
                "Pet With A word as an ID",
                new PetData().SetID("Neuon")
            ),
            new PetTestData(
                "Pet With an empty string as an ID",
                new PetData().SetID("")
            ),
            new PetTestData(
               "String as Category ID",
                new PetData().SetCategory(new CategoryData().SetID("Hello").SetName("Good"))
            ),
            new PetTestData(
               "Symbols in Category Name",
               new PetData().SetCategory(new CategoryData().SetID(1).SetName("@@@"))
            ),
            new PetTestData(
               "Negative category Id",
               new PetData().SetCategory(new CategoryData().SetID(-1).SetName("Good"))
            ),
            new PetTestData(
                "Name is html tag",
                new PetData().SetName("<h1>Hello, \" + user +\"</h1>")
            ),
            new PetTestData(
                "Pet With A word as an ID",
                new PetData().SetName("Neuon")
            ),
            new PetTestData(
                "Pet With an empty string as an ID",
                new PetData().SetName("")
            ),
            new PetTestData(
               "A name with a symbol",
                new PetData().SetName("\"ca$e\"")
            ),
            new PetTestData(
               "A name with a number",
               new PetData().SetName("D0ge")
            ),
            new PetTestData(
               "A name that is too long",
               new PetData().SetName(new string('A', 1000))
            ),
            new PetTestData(
               "An empty string",
               new PetData().SetName("")
            ),
             new PetTestData(
                "Status is bazinga",
                new PetData().SetStatus("pending")
            ),
            new PetTestData(
                "Status is bar",
                new PetData().SetStatus("active")
            ),
            new PetTestData(
                "an integer as Photo URL",
                new PetData().SetPhotoUrls(new object[]{1})
            )
        ];


        private void Expect_A_Pet(PetData pet, Dictionary<string, object> response)
        {
            TestContext.WriteLine("Expect pet as response");
            Assert.IsTrue(response.ContainsKey("id"));
            Assert.AreEqual(response["id"], pet.GetID());
            Assert.IsTrue(response.ContainsKey("name"));
            Assert.AreEqual(response["name"], pet.GetName());
            Assert.IsTrue(response.ContainsKey("category"));
            Assert.AreEqual(response["category"], pet.GetCategory());
            Assert.IsTrue(response.ContainsKey("photoUrls"));
            Assert.AreEqual(response["photoUrls"], pet.GetPhotoUrls());
            Assert.IsTrue(response.ContainsKey("tags"));
            Assert.AreEqual(response["tags"], pet.GetTags());
            Assert.IsTrue(response.ContainsKey("status"));
            Assert.AreEqual(response["status"], pet.GetStatus());
            Assert.IsFalse(response.ContainsKey("age"));
            Assert.IsFalse(response.ContainsKey("STATUS"));
            Assert.That(response.Count, Is.EqualTo(6));
        }

        private void Expect_invalid_pet_unknown_message(Dictionary<string, object> response)
        {
            TestContext.WriteLine("Expect pet unknown message");

            Assert.IsTrue(response.ContainsKey("type"));
            Assert.That(response["type"], Is.EqualTo("unknown"));
        }

        private void Expect_invalid_pet_error_message(Dictionary<string, object> response)
        {
            TestContext.WriteLine("Expect invalid pet error message");
            Assert.IsTrue(response.ContainsKey("type"));
            Assert.That(response["type"], Is.EqualTo("error"));
        }


        private void Expect_Empty_List_Of_Pets(Dictionary<string, object>[] response)
        {
            TestContext.WriteLine("Expect Empty List Of Pets");
            Assert.Equals(response.Count(), 0);

        }

        private void Expect_Non_Empty_List_of_Pets(Dictionary<string, object>[] response)
        {
            TestContext.WriteLine("Expect None Empty List Of Pets");
            Assert.AreNotEqual(response.Count(), 0);

        }
        
        private async Task<Dictionary<string, object>> When_I_Create_A_Pet(PetTestData petdata)
        {
            TestContext.WriteLine("When I POST pet described as: " + petdata.Casename);
            return await pets.addPet(petdata.Value);
        }

        private async Task<Dictionary<string, object>> When_I_Update_A_pet(PetTestData petdata)
        {
            TestContext.WriteLine("When I PUT pet described as: " + petdata.Casename);
            return await pets.putPet(petdata.Value);
        }

        private async Task<Dictionary<string, object>>  When_I_Get_A_Pet(object id)
        {
            TestContext.WriteLine("When I GET a pet with the id: " + id);
            return await pets.getPet(id);
        }

        private async Task<Dictionary<string, object>> When_I_Delete_A_Pet(object id)
        {
            TestContext.WriteLine("When I DELETE a pet with the id: " + id);
            return await pets.getPet(id);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            client = new HttpClient();
            pets = new PetsEndpoint(client);
        }

        private async Task Given_That_A_Pet_Does_Not_Exist(object id)
        {
            TestContext.WriteLine("Given a that pet with id: " + id + " does not exist");
            await pets.deletePet(id);   
        }

        private async Task And_a_pet_does_not_exist(object id)
        {
            TestContext.WriteLine("And a pet with id: " + id + " does not exist");
            await pets.deletePet(id);
        }


        private async Task Given_that_a_pet_does_exist(PetTestData data)
        {
            TestContext.WriteLine("Given a that pet with id: " + data.Value.GetID() + "described as: " + data.Casename);
            await pets.addPet(data.Value);
        }

        [Test]
        [Category("TestPOSTRequest")]
        [TestCaseSource(nameof(ValidPets))]
        public async Task POST_valid_case(PetTestData data)
        {
            await Given_That_A_Pet_Does_Not_Exist(data.Value.GetID());
            var response = await When_I_Create_A_Pet(data);
            Expect_A_Pet(data.Value, response);
        }

        [Test]
        [TestCaseSource(nameof(InvalidPets))]
        [Category("TestPOSTRequest")]
        public async Task POST_invalid_case(PetTestData data)
        {
            var response = await When_I_Create_A_Pet(data);
            Expect_invalid_pet_error_message(response);
        }

        private static readonly PetTestData[] PUT_non_existant_case_Data =
        [
            new PetTestData("Default Valid Pet",new PetData().SetID(212))
        ];

        [Test]
        [TestCaseSource("PUT_non_existant_case_Data")]
        public async Task PUT_non_existant_case(PetTestData data)
        {
            await Given_That_A_Pet_Does_Not_Exist(data.Value.GetID());
            var response = await When_I_Update_A_pet(new PetTestData("A non existing pet" , data.Value));
            Expect_invalid_pet_error_message(response);
        }


        private static readonly PetTestData[] PUT_invalid_case_Data =
        [
            new PetTestData("A non existing and invalid pet",new PetData().SetID("Hello"))
        ];
        [TestCaseSource("PUT_invalid_case_Data")]
        [Test]
        public async Task PUT_invalid_case(PetTestData data)
        {
            var response = await When_I_Update_A_pet(data);
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCase(212)]

        public async Task GET_non_existent_case(int id)
        {
            await Given_That_A_Pet_Does_Not_Exist(id);
            var response = await When_I_Get_A_Pet(id);
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCase("hello")]

        public async Task GET_invalid_case(object id)
        {
            var response = await When_I_Get_A_Pet(id);
            Expect_invalid_pet_unknown_message(response);
        }


        [Test]
        [TestCase(212)]
        public async Task DELETE_non_existent_case(int id)
        {
            await Given_That_A_Pet_Does_Not_Exist(id);
            var response =await  When_I_Delete_A_Pet(id);
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCase("hello")]

        public async Task DELETE_invalid_case(object id)
        {
            var response = await When_I_Delete_A_Pet(id);
            Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task POST_POST_duplicate_case()
        {
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", new PetData()));
            var response = await When_I_Create_A_Pet(new PetTestData("Same Pet", new PetData()));
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        public async Task POST_POST_diffrent_pets_case()
        {
            PetTestData data = new PetTestData("Default Valid Pet", new PetData());
            await Given_that_a_pet_does_exist(data);
            var response = await When_I_Create_A_Pet(new PetTestData("Different Pet", new PetData().SetValidID(21)));
            Expect_A_Pet(data.Value, response);
        }

        [Test]
        public async Task POST_POST_invalid_case()
        {
            PetTestData data = new PetTestData("Default Valid Pet", new PetData());
            await Given_that_a_pet_does_exist(data);
            PetTestData data2 = new PetTestData("Different Pet", new PetData().SetValidID(21));
            var response = await When_I_Create_A_Pet(data2);
            Expect_A_Pet(data2.Value, response);
        }

        [Test]
        public async Task POST_PUT_existing_case()
        {
            PetTestData data = new PetTestData("Default Valid Pet", new PetData());
            await Given_that_a_pet_does_exist(data);
            var response = await When_I_Update_A_pet(new PetTestData("Same Pet ID", new PetData().SetName("Hello")));
            Expect_A_Pet(data.Value, response);
        }

        [Test]
        public async Task POST_PUT_non_existing_case()
        {
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", new PetData()));
            var response = await When_I_Update_A_pet(new PetTestData("Same Pet ID", new PetData().SetName("@@@")));
            Expect_invalid_pet_error_message(response);
        }


        [Test]
        public async Task POST_PUT_invalid_case()
        {
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", new PetData()));
            var response = await When_I_Update_A_pet(new PetTestData("Same Pet ID", new PetData().SetName("@@@")));
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        public async Task POST_GET_existing_case()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            await Given_that_a_pet_does_exist(petdata);
            var response = await When_I_Get_A_Pet(petdata.Value.GetID());
            Expect_A_Pet(petdata.Value, response);
        }

        [Test]
        public async Task POST_GET_non_existing_case()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            const int non_id = 10;
            await Given_that_a_pet_does_exist(petdata);
            await And_a_pet_does_not_exist(non_id);
            var response = await When_I_Get_A_Pet(non_id);
            Expect_A_Pet(petdata.Value, response);
        }

        [Test]
        public async Task POST_GET_invalid_case()
        {
            var response = await When_I_Get_A_Pet(new PetTestData("Same Pet ID", new PetData().SetID("Hello")));
            Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task POST_DELETE_existing_case()
        {
            var petdata = new PetData();
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", petdata));
            var response = await When_I_Delete_A_Pet(new PetTestData("Same Pet ID", petdata));
            Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task POST_DELETE_non_existing_case()
        {
            var petdata = new PetData();
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", petdata));
            var response = await When_I_Delete_A_Pet(new PetTestData("Same Pet ID", petdata));
            Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task POST_DELETE_invalid_case()
        {
            var petdata = new PetData();
            await Given_that_a_pet_does_exist(new PetTestData("Default Valid Pet", petdata));
            var response = await When_I_Delete_A_Pet(new PetTestData("Same Pet ID", petdata));
            Expect_invalid_pet_unknown_message(response);
        }


        [Test]
        public async Task POST_PUT_POST_invalid_case() { 
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
             
        }
        [Test]
        public async Task POST_PUT_PUT_valid_case() {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }
        [Test]
        public async Task POST_PUT_GET_invalid_case() {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }
        [Test]
        public async Task POST_PUT_DELETE_invalid_case() {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }


        [Test]
        public async Task POST_GET_POST_invalid_case()
        {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);

        }

        [Test]
        public async Task POST_GET_PUT_valid_case()
        {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }

        [Test]
        public async Task POST_GET_GET_invalid_case()
        {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }

        [Test]
        public async Task POST_GET_DELETE_invalid_case()
        {
            PetTestData petdata = new PetTestData("lol", new PetData());
            await Given_that_a_pet_does_exist(petdata);
        }


        [Test]
        public async Task UPLOAD_valid_case()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await Given_that_a_pet_does_exist(valid_case);
            var response = await pets.uploadImage(valid_case.Value.GetID(), "hello");

        }


        [Test]
        public async Task UPLOAD_invalid_case()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await Given_that_a_pet_does_exist(valid_case);
            var response = await pets.uploadImage(valid_case.Value.GetID(), "hello");
            Expect_invalid_pet_error_message(response);
        }

        [Test]
        public async Task UPLOADIMAGE_GET_valid_case()
        {
            PetTestData valid_case = new PetTestData("Default Pet", new PetData());
            PetTestData result_case = new PetTestData("Default With image/dog.jpg", 
                    new PetData().SetPhotoUrls(new string[]{ "image/dog.jpg" })
            );
            await Given_that_a_pet_does_exist(valid_case);
            //AND I Upload an Image
            await pets.uploadImage(valid_case.Value.GetID(), "image/dog.jpg");
            var response = await pets.getPet(valid_case.Value.GetID());
            Expect_A_Pet(result_case.Value, response);
        }



        [Test]
        public async Task FINDBYSTATUS_valid_case()
        {
            var response = await pets.findByStatus("asa");
            Expect_Non_Empty_List_of_Pets(response);
        }


        [Test]
        public async Task FINDBYSTATUS_invalid_case()
        {
            var response = await pets.findByStatus("asa");
            Expect_Empty_List_Of_Pets(response);
        }


        [Test]
        public async Task FINDBYTAG_valid_case()
        {
            var response = await pets.findByStatus("asa");
            Expect_Non_Empty_List_of_Pets(response);

        }

        [Test]
        public async Task FINDBYTAG_invalid_case()
        {
            var response = await pets.findByStatus("asa");
            Expect_Empty_List_Of_Pets(response);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            client.Dispose(); 
        }
    }
}

