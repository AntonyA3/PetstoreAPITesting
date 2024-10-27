using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PetstoreAPITesting
{
    [TestFixture]
    public class PetsEndpointTest 
    {
        private static PetTestData[] GivenAPetWasAdded_whenIPostADuplicatePet_cases =
        {
            new PetTestData("Default Pet",new PetData())
        };

        private static object[] GivenAPetWasAdded_whenIPostADifferentPetCases =
        {
            new object[]{
                new PetTestData("Default Valid Pet", new PetData()),
                new PetTestData("Other Pet", new PetData().SetValidID(21))
            }
        };

        private static object[] GivenAPetWasAdded_WhenIUpdateAnExistingPetCases =
        {
            new object[]{
                new PetTestData("Default Pet", new PetData()),
                new PetTestData("Default Pet with New name", new PetData().SetName("Baz"))
            }
        };

        private static object[] GivenAPetWasAdded_whenIPostAnInvalidPet_cases =
        {
            new object[]{
                new PetTestData("Default Valid Pet", new PetData()),
                new PetTestData("Other Pet", new PetData().SetID("dc"))
            }
        };

        private static object[] GivenAPetWasAdded_WhenIUpdateANonExistingPetCases =
{
            new object[]{
                new PetTestData("Default Pet", new PetData()),
                new PetTestData("Default Pet with New name", new PetData().SetName("Baz"))
            }
        };

        private static object[] GivenAPetWasAdded_WhenIUpdateAnInvalidPetCases =
        {
            new object[]{
                new PetTestData("Default Valid Pet", new PetData()),
                new PetTestData("Same Pet ID Invalid Name", new PetData().SetName("@@@"))
            }
        };

        private static readonly PetTestData[] GivenAPetWasDeleted_WhenIPostAValidPet_cases =
        [
        new PetTestData(
            "Default Valid Pet",
            new PetData()
        ),
        new PetTestData(
            "Valid Category {id = 1, name = \"Good\"}",
            new PetData().SetCategoryID(1l).SetCategoryName("Good")
            //(new CategoryData().SetID(1).SetName("Good"))
        ),
        new PetTestData(
            "Category Name",
            new PetData().SetCategoryID(1l).SetCategoryName("Good")
        ),
        new PetTestData(
            "Category Name with whitespace",
            new PetData().SetCategoryID(0l).SetCategoryName("The Best")
        ),
        new PetTestData(
            "Category name with hyphen",
            new PetData().SetCategoryID(0l).SetCategoryName("The-Best")
        ),
        new PetTestData(
            "Standard Name",
            new PetData().SetCategoryID(1l).SetCategoryName("Doge")
        ),
        new PetTestData(
            "Name with hyphen",
            new PetData().SetCategoryID(0l).SetCategoryName("N-euon")
        ),
        new PetTestData(
            "Name with space",
            new PetData().SetCategoryID(0l).SetCategoryName("High Sierra")
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
            new PetData().AddPhotoUrl("bazinga")
        )

        ];

        private static readonly PetTestData[] GivenAPetWasDeleted_WhenIPostAnInvalidPet_cases = [
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
            new PetData().SetCategoryID("Hello").SetCategoryName("Good")
        ),
        new PetTestData(
            "Symbols in Category Name",
            new PetData().SetCategoryID(1l).SetCategoryName("@@@")
        ),
        new PetTestData(
            "Negative category Id",
            new PetData().SetCategoryID(-1l).SetCategoryName("Good")
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
            new PetData().AddPhotoUrl(1)
        )
];
        
        private static readonly PetTestData[] GivenAPetWasDeleted_WhenIPutANonExistentPet_cases = [
            new PetTestData("Default Valid Pet",new PetData().SetID(212))
        ];
        
        private static readonly long[] GivenAPetWasDeleted_WhenIGetANonExistentPet_cases = [
            212
        ];
        
        private static readonly long[] GivenAPetWasDeleted_WhenIDeleteANonExistentPet_cases = [
            212
        ];
        
        private static readonly PetTestData[] GivenAPetWasDeleted_WhenIPutAnInvalidPet_cases = [
            new PetTestData("A non existing and invalid pet",new PetData().SetID("Hello"))
        ];
        
        private static readonly object[] GivenAPetWasDeleted_WhenIGetAnInvalidPet_cases = [
            "hello"
        ];
        
        private static readonly object[] GivenAPetWasDeleted_DELETE_invalid_case_Data = [
            "hello"
        ];
        
        private static readonly object[] GivenAPetWasDeleted_WhenIDeleteAnInvalidPet_cases = [
            "hello"
        ];


        static string imageFolder = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/images";
       
        public HttpClient client;
        public PetsEndpoint pets;


        [OneTimeSetUp]
        public void Setup()
        {
            this.client = new HttpClient();
            this.pets = new PetsEndpoint(this.client);
        }

        private async Task GivenAPetWasAdded(PetTestData data)
        {
            TestContext.WriteLine("Given a that pet with id:  described as: " + data.GetCasename());
            await pets.postPet(data.GetValue().AsJSON());
        }

        private async Task GivenAPetWasDeleted(string description, object id)
        {

            TestContext.WriteLine("Given a that pet describes as: " + description);
            await pets.deletePet(id);
        }

        private async Task GivenAnImageWasUploadedToAPet()
        {
            PetData pet = new PetData();
            await pets.postPet(pet.AsJSON());
            await pets.uploadImage(pet.GetID(), PetsEndpointTest.imageFolder + "/dog.jpg");
        }

        private async Task GivenAPetWasUpdated(PetTestData data)
        {
            TestContext.WriteLine("Given a that pet with described as: " + data.GetCasename() + " was updated");
            await pets.postPet(new PetData().AsJSON());
            await pets.putPet(data.GetValue().AsJSON());
        }

        private async Task AndAPetWasDeleted(PetTestData data)
        {
            TestContext.WriteLine("And a pet was deleted with id: " + data.GetValue().GetID() + "described as: " + data.GetCasename());
            await pets.deletePet(data.GetValue());
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasAdded_whenIPostADuplicatePet_cases))]
        public async Task GivenAPetWasAdded_WhenIPostADuplicatePet(PetTestData pet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Create_A_Pet(pet);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasAdded_whenIPostAnInvalidPet_cases))]
        public async Task GivenAPetWasAdded_WhenIPostAnInvalidPet(PetTestData pet, PetTestData invalidPet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Create_A_Pet(invalidPet);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasAdded_WhenIUpdateAnExistingPetCases))]
        public async Task GivenAPetWasAdded_WhenIUpdateAnExistingPet(PetTestData pet, PetTestData updated_pet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Update_A_pet(updated_pet);
            pets.Expect_A_Pet(updated_pet.GetValue(), response);
        }


        [Test]
        public async Task GivenAPetWasAdded_WhenIGetAnExistingPet()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            await GivenAPetWasAdded(petdata);
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID());
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }


        [Test]
        public async Task GivenAPetWasAdded_WhenIDeleteAnExistingPet()
        {
            var petdata = new PetData();
            await GivenAPetWasAdded(new PetTestData("Default Valid Pet", petdata));
            var response = await pets.When_I_Delete_A_Pet(petdata.GetID());
            pets.ExpectASuccessMessage(response);
        }


        [Test]
        public async Task GivenAPetWasAdded_WhenIUploadAValidPetImage()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await GivenAPetWasAdded(valid_case);
            var response = await pets.uploadImage(valid_case.GetValue().GetID(), imageFolder + "/dog.jpg");
            pets.ExpectASuccessMessage(response);
        }


        [Test]
        public async Task GivenAPetWasAdded_WhenIUploadAnInvalidPetImage()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await GivenAPetWasAdded(valid_case);
            var response = await pets.uploadImage(valid_case.GetValue().GetID(), imageFolder + "/dog.txt");
            pets.Expect_invalid_pet_unknown_message(response);
        }


        [Test]
        [TestCase("available")]
        [TestCase("pending")]
        [TestCase("sold")]
        [TestCase("sold,pending")]

        public async Task GivenAPetWasAdded_WhenIFindPetsByStatus(string status)
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData().SetStatus(status));
            await GivenAPetWasAdded(valid_case);
            var response = await pets.WhenIFindPetsByStatus(status);
            pets.Expect_Non_Empty_List_of_Pets(response);
        }

        [Test]
        [TestCase("lol")]
        [TestCase(1)]
        public async Task GivenAPetWasAdded_WhenIFindPetsByAnInvalidStatus(object status)
        {
            var response = await pets.findByStatus(status);
            pets.Expect_Empty_List_Of_Pets(response);
        }

        [Test]
        [TestCase("string")]
        public async Task GivenAPetWasAdded_WhenIFindPetsByAValidTag(string tags)
        {
            var response = await pets.findByTags(tags);
            pets.Expect_Non_Empty_List_of_Pets(response);
        }

        [Test]
        [TestCase("@@@")]
        [TestCase(1)]

        public async Task GivenAPetWasAdded_WhenIFindPetsByAnInvalidTag(object tag)
        {
            var response = await pets.findByTags(tag);
            pets.Expect_Empty_List_Of_Pets(response);
        }

        [Test]
        [Category("TestPOSTRequest")]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIPostAValidPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIPostAValidPet(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Create_A_Pet(data);

            pets.Expect_A_Pet(data.GetValue(), response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIPostAnInvalidPet_cases))]
        [Category("TestPOSTRequest")]
        public async Task GivenAPetWasDeleted_WhenIPostAnInvalidPet(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Create_A_Pet(data);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIPutANonExistentPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIPutANonExistentPet(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Update_A_pet(new PetTestData("A non existing pet", data.GetValue()));
            pets.Expect_invalid_pet_error_message(response);
        }

        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIPutAnInvalidPet_cases))]
        [Test]
        public async Task GivenAPetWasDeleted_WhenIPutAnInvalidPet(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(),  data.GetValue().GetID());
            var response = await pets.When_I_Update_A_pet(data);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIGetANonExistentPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIGetANonExistentPet(long id)
        {
            await GivenAPetWasDeleted("pet with id" + id, id);
            var response = await pets.When_I_Get_A_Pet(id);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIGetAnInvalidPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIGetAnInvalidPet(object id)
        {
            await GivenAPetWasDeleted("pet with id" + id, id);
            var response = await pets.When_I_Get_A_Pet(id);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIDeleteANonExistentPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIDeleteANonExistentPet(long id)
        {
            await GivenAPetWasDeleted("pet with id " + id, id);
            var response = await pets.When_I_Delete_A_Pet(id);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(GivenAPetWasDeleted_WhenIDeleteAnInvalidPet_cases))]
        public async Task GivenAPetWasDeleted_WhenIDeleteAnInvalidPet(object id)
        {
            await GivenAPetWasDeleted("pet with invalid id" + id, id);
            var response = await pets.When_I_Delete_A_Pet(id);
            pets.Expect_invalid_pet_unknown_message(response);
        }


        [Test]
        public async Task GivenAPetWasUpdated_WhenAPetAlreadyExists()
        {
            PetTestData petdata = new PetTestData("Existing Pet", new PetData());
            await GivenAPetWasUpdated(petdata);
            var response = await pets.When_I_Create_A_Pet(petdata);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task GivenAPetWasUpdated_WhenIGetAPet()
        {
            PetTestData petdata = new PetTestData("Default pet", new PetData());
            await GivenAPetWasUpdated(petdata);
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID);
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }

        [Test]
        public async Task GivenAnImageWasUploadedToAPet_WhenIGetAnExistingPet()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            await GivenAnImageWasUploadedToAPet();
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID());
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }

        [Test]
        public async Task GivenAnImageWasUploadedToAPet_WhenIUploadAnotherPetImage()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await GivenAnImageWasUploadedToAPet();
            var response = await pets.uploadImage(valid_case.GetValue().GetID(), PetsEndpointTest.imageFolder + "/dog.jpg");
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            client.Dispose();
        }
    }   
}

