using Newtonsoft.Json;
using PetstoreAPITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

class OtherProgram
{

    static async Task Main(string[] args)
    {
        var client = new HttpClient();
        var pets = new PetsEndpoint(client);
        var response = await pets.findByStatus("");
        Console.WriteLine(response.Count());
        
        Console.WriteLine(JsonConvert.SerializeObject(response));

        //var query = HttpUtility.ParseQueryString(string.Empty);
        //query["foo"] = "bar";
        //query["bar"] = "bazinga";
        //string queryString = query.ToString();
        //Console.WriteLine(queryString);

        //PetData data = new PetData().SetValidID(267);
        //var deleteResponse = await pets.deletePet(267);

        //var response = await pets.putPet(data);
        //Console.WriteLine(JsonConvert.SerializeObject(deleteResponse));
        //Console.WriteLine(" ");
        //Console.WriteLine(JsonConvert.SerializeObject(response));
        //Console.WriteLine(" ");

        //PetData data = new PetData().SetID(230);
        //var response = await pets.addPet(data);
        //var response2 = await pets.deletePet(230);
        //var response3 = await pets.deletePet(230);

        //Console.WriteLine(JsonConvert.SerializeObject(response));
        //Console.WriteLine(" ");
        //Console.WriteLine(JsonConvert.SerializeObject(response2));
        //Console.WriteLine(" ");
        //Console.WriteLine(JsonConvert.SerializeObject(response3));

        //PetData data = new PetData().SetValidID(11);
        //var response1 = await pets.addPet(data);
        //var response2 = await pets.deletePet(11);
        ////assert successful adding
        //Console.WriteLine(response1.ToString());

        ////assert successful deletion
        //Console.WriteLine(response2.ToString());
    }
}