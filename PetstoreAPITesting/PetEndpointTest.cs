namespace PetstoreAPITesting
{
    /// <summary>
    /// This tests the Pets endpoint for the Petstore API
    /// Ideally these tests could be converted to the Gherkin format
    /// As a way to structure the tests in a better format.
    ///
    /// I designed the scenarios around a potential user making use of the API
    /// The scenarios make sure that the user receives the correct error responses
    /// when interacting with the API incorrectly an that the user receives an
    /// accurate response when interacting with thw API correctly
    /// </summary>
    [TestFixture]
    public class PetsEndpointTest 
    {
        private static PetTestData[] AUserPostsAPetWithTheSameID_cases =
        {
            new PetTestData("Default Pet",new PetData())
        };

        private static object[] GivenAPetWasAdded_whenIPostADifferentPetCases =
        {
            new object[]{
                new PetTestData("Default Valid Pet", new PetData()),
                new PetTestData("Other Pet", new PetData().SetID(21))
            }
        };

        private static object[] AUserUpdatesAPetThatAlreadyExists_cases =
        {
            new object[]{
                new PetTestData("Default Pet", new PetData()),
                new PetTestData("Default Pet with New name", new PetData().SetName("Baz"))
            }
        };

        private static object[] AUserPostsAnotherPetButWithInvalidProperties_cases =
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

        private static readonly PetTestData[] AUserPostsAPetWithCorrectDataProvided_cases =
        [
        new PetTestData(
            "Default Valid Pet",
            new PetData()
        ),
        new PetTestData(
            "Valid Category {id = 1, name = \"Good\"}",
            new PetData().SetCategoryID(1l).SetCategoryName("Good")
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

        private static readonly PetTestData[] AUserPostsAPetWithSomeInvalidDataProvided_cases = [
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
        
        private static readonly PetTestData[] AUserUpdatesAPetThatDoesNotExist_cases = [
            new PetTestData("Default Valid Pet",new PetData().SetID(212))
        ];
        
        private static readonly long[] AUserAttemptsToGetAPetThatDoesNotExist_cases = [
            212
        ];
        
        private static readonly long[] AUserAttemptsToDeleteAPetButProvidesIDOfNonExistentPet_cases = [
            212
        ];
        
        private static readonly PetTestData[] AUserUpdatesAPetWithSomeInvalidData_cases = [
            new PetTestData("A non existing and invalid pet",new PetData().SetID("Hello"))
        ];
        
        private static readonly object[] AUserAttemptsToGetAPetButProvidesAnInvalidID_cases = [
            "hello"
        ];
        
        private static readonly object[] GivenAPetWasDeleted_DELETE_invalid_case_Data = [
            "hello"
        ];
        
        private static readonly object[] AUserAttemptsToDeleteAPetButProvidesAnInvalidID_cases = [
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
        [TestCaseSource(nameof(AUserPostsAPetWithTheSameID_cases))]
        public async Task AUserPostsAPetWithTheSameID(PetTestData pet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Create_A_Pet(pet);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserPostsAnotherPetButWithInvalidProperties_cases))]
        public async Task AUserPostsAnotherPetButWithInvalidProperties(PetTestData pet, PetTestData invalidPet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Create_A_Pet(invalidPet);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserUpdatesAPetThatAlreadyExists_cases))]
        public async Task AUserUpdatesAPetThatAlreadyExists(PetTestData pet, PetTestData updated_pet)
        {
            await GivenAPetWasAdded(pet);
            var response = await pets.When_I_Update_A_pet(updated_pet);
            pets.Expect_A_Pet(updated_pet.GetValue(), response);
        }


        [Test]
        public async Task AUserGetsAPetThatAlreadyExists()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            await GivenAPetWasAdded(petdata);
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID());
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }


        [Test]
        public async Task AUserDeletesAPetThatAlreadyExists()
        {
            var petdata = new PetData();
            await GivenAPetWasAdded(new PetTestData("Default Valid Pet", petdata));
            var response = await pets.When_I_Delete_A_Pet(petdata.GetID());
            pets.ExpectASuccessMessage(response);
        }


        [Test]
        public async Task AUserUploadesAnImageToAPetThatAlreadyExists()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await GivenAPetWasAdded(valid_case);
            var response = await pets.uploadImage(valid_case.GetValue().GetID(), imageFolder + "/dog.jpg");
            pets.ExpectASuccessMessage(response);
        }


        [Test]
        public async Task AUserUploadsAnImageToAPetButProvidesAnInvalidImage()
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
        public async Task AUserFindsAPetBasesOffOneOrMoreValidStatus(string status)
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData().SetStatus(status));
            await GivenAPetWasAdded(valid_case);
            var response = await pets.WhenIFindPetsByStatus(status);
            pets.Expect_Non_Empty_List_of_Pets(response);
        }

        [Test]
        [TestCase("lol")]
        [TestCase(1)]
        public async Task AUserTriesToFindAPetBasedOnOneInvalidStatus(object status)
        {
            var response = await pets.findByStatus(status);
            pets.Expect_Empty_List_Of_Pets(response);
        }

        [Test]
        [TestCase("string")]
        public async Task AUserFindsPetsBasedOnAValidTag(string tags)
        {
            var response = await pets.findByTags(tags);
            pets.Expect_Non_Empty_List_of_Pets(response);
        }

        [Test]
        [TestCase("@@@")]
        [TestCase(1)]

        public async Task AUserTriesToFindPetsBasedOnAnInvalidTag(object tag)
        {
            var response = await pets.findByTags(tag);
            pets.Expect_Empty_List_Of_Pets(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserPostsAPetWithCorrectDataProvided_cases))]
        public async Task AUserPostsAPetWithCorrectDataProvided(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Create_A_Pet(data);

            pets.Expect_A_Pet(data.GetValue(), response);
        }

        [Test]
        [TestCaseSource(nameof(AUserPostsAPetWithSomeInvalidDataProvided_cases))]
        public async Task AUserPostsAPetWithSomeInvalidDataProvided(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Create_A_Pet(data);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserUpdatesAPetThatDoesNotExist_cases))]
        public async Task AUserUpdatesAPetThatDoesNotExist(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(), data.GetValue().GetID());
            var response = await pets.When_I_Update_A_pet(new PetTestData("A non existing pet", data.GetValue()));
            pets.Expect_invalid_pet_error_message(response);
        }

        [TestCaseSource(nameof(AUserUpdatesAPetWithSomeInvalidData_cases))]
        [Test]
        public async Task AUserUpdatesAPetWithSomeInvalidData(PetTestData data)
        {
            await GivenAPetWasDeleted(data.GetCasename(),  data.GetValue().GetID());
            var response = await pets.When_I_Update_A_pet(data);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserAttemptsToGetAPetThatDoesNotExist_cases))]
        public async Task AUserAttemptsToGetAPetThatDoesNotExist(long id)
        {
            await GivenAPetWasDeleted("pet with id" + id, id);
            var response = await pets.When_I_Get_A_Pet(id);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserAttemptsToGetAPetButProvidesAnInvalidID_cases))]
        public async Task AUserAttemptsToGetAPetButProvidesAnInvalidID(object id)
        {
            await GivenAPetWasDeleted("pet with id" + id, id);
            var response = await pets.When_I_Get_A_Pet(id);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserAttemptsToDeleteAPetButProvidesIDOfNonExistentPet_cases))]
        public async Task AUserAttemptsToDeleteAPetButProvidesIDOfNonExistentPet(long id)
        {
            await GivenAPetWasDeleted("pet with id " + id, id);
            var response = await pets.When_I_Delete_A_Pet(id);
            pets.Expect_invalid_pet_error_message(response);
        }

        [Test]
        [TestCaseSource(nameof(AUserAttemptsToDeleteAPetButProvidesAnInvalidID_cases))]
        public async Task AUserAttemptsToDeleteAPetButProvidesAnInvalidID(object id)
        {
            await GivenAPetWasDeleted("pet with invalid id" + id, id);
            var response = await pets.When_I_Delete_A_Pet(id);
            pets.Expect_invalid_pet_unknown_message(response);
        }


        [Test]
        public async Task AUserUpdatesAPetThatAlreadyExists()
        {
            PetTestData petdata = new PetTestData("Existing Pet", new PetData());
            await GivenAPetWasUpdated(petdata);
            var response = await pets.When_I_Create_A_Pet(petdata);
            pets.Expect_invalid_pet_unknown_message(response);
        }

        [Test]
        public async Task AUserGetsAPetThatWasUpdated()
        {
            PetTestData petdata = new PetTestData("Default pet", new PetData());
            await GivenAPetWasUpdated(petdata);
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID);
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }

        [Test]
        public async Task AUserGetsThePetAfterHavingUploadedAnImage()
        {
            var petdata = new PetTestData("Default Valid Pet", new PetData());
            await GivenAnImageWasUploadedToAPet();
            var response = await pets.When_I_Get_A_Pet(petdata.GetValue().GetID());
            pets.Expect_A_Pet(petdata.GetValue(), response);
        }

        [Test]
        public async Task AUserUploadsAnotherPetImageThePetAfterHavingUploadedAnImage()
        {
            PetTestData valid_case = new PetTestData("Pet exists", new PetData());
            await GivenAnImageWasUploadedToAPet();
            var response = await pets.uploadImage(valid_case.GetValue().GetID(), PetsEndpointTest.imageFolder + "/dog.jpg");
            pets.ExpectASuccessMessage(response);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            client.Dispose();
        }
    }   
}

