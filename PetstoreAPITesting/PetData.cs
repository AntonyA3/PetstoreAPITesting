using System.Text.Json;


namespace PetstoreAPITesting
{
    public class PetData
    {
        new Dictionary<string, object> data;

        /// <summary>
        /// Initialised the pet with defaul valid data for 
        /// POST or put operations
        /// </summary>
        public PetData()
        {
            this.data = new Dictionary<string, object>();
            this.data.Add("id", 11l);
            this.data.Add("name", "Baz");
            Dictionary<string, object> category = new Dictionary<string, object>();
            category.Add("id", 0l);
            category.Add("name", "Dogs");
            this.data.Add("category", category);
            List<object> tags = new List<object>();
            Dictionary<string, object> tag = new Dictionary<string, object>();
            tag.Add("id", 0l);
            tag.Add("name", "string");
            tags.Add(tag);
            this.data.Add("tags", tags);
            this.data.Add("photoUrls", new List<object>());
            this.data.Add("status", "available");
        }

        /// <summary>
        /// This gets the status of the pet
        /// the function should only be used when verifying that
        /// the properties of a status is correct when the http response
        /// is a valid pet
        /// </summary>
        /// <returns>a string that is the pet status</returns>
        public string GetStatus()
        {
            return (string)this.data["status"];
        }


        /// <summary>
        /// This gets the category id of the pet
        /// the function should only be used when verifying that
        /// the properties of a category is correct when the http response
        /// is a valid pet
        /// </summary>
        /// <returns>A valid category Id</returns>

        public long GetCategoryId()
        {
            return (long)((Dictionary<string, object>)data["category"])["id"];
        }

        /// <summary>
        /// This gets the category name of the pet
        /// the function should only be used when verifying that
        /// the properties of a category is correct when the http response
        /// is a valid pet
        /// </summary>
        ///<returns>A valid category name</returns>
        public string GetCategoryName()
        {
            return (string)((Dictionary<string, object>)data["category"])["name"];
        }

        /// <summary>
        /// This gets the name of the pet
        /// the function should only be used when verifying that
        /// the properties of a pet name is correct when the http response
        /// is a valid pet
        /// </summary>
        /// <returns>A valid pet name</returns>
        public string GetName()
        {
            return (string)this.data["name"];
        }


        /// <summary>
        /// This gets the name of the pet
        /// the function should only be used when verifying that
        /// the properties of a pet id is correct when the http response
        /// is a valid pet
        /// </summary>
        /// <returns>The pet ID as a long integer</returns>
        public long GetID()
        {
            return (long)this.data["id"];
        }

        /// <summary>
        /// This sets the id to either a valid or an invalid ID
        /// </summary>
        /// <param name="id">a valid id as a long positive integer 
        /// or an invalid Id of another data type</param>
        /// <returns>An instance of the class PetData for chaining using builder pattern</returns>
        public PetData SetID(object id)
        {
            this.data["id"] = id;
            return this;
        }

        /// <summary>
        /// This returns a valid photoUrl that is used when
        /// asserting whether the photoUrl matches a specific value</summary>
        /// <param name="photoUrl"></param>
        /// <returns></returns>
        public PetData AddPhotoUrl(object photoUrl)
        {
            ((List<object>)this.data["photoUrls"]).Add(photoUrl);
            return this;
        }

        /// <summary>
        /// This returns a valid tag ID that is used when
        /// asserting whether the tagID matches a specific value
        /// </summary>
        /// <param name="index">the index to find the tag in the list of tags</param>
        /// <returns>the tag ID at the specified indes</returns>
        public long GetTagID(int index)
        {
            List<object> tags = (List<object>)this.data["tags"];
            Dictionary<string, object> tag = (Dictionary<string, object>)tags.ElementAt(index);
            long id = (long)tag["id"];
            return id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetTagName(int index)
        {
            List<object> tags = (List<object>)this.data["tags"];
            Dictionary<string, object> tag = (Dictionary<string, object>)tags.ElementAt(index);
            string id = (string)tag["name"];
            return id;
        }


        /// <summary>
        /// This gets the photurl at a particular index of the pet
        /// the function should only be used when verifying that
        /// the properties of a pets photoUrls are correct when the http response
        /// is a valid pet
        /// </summary>
        /// <returns>as string that is the photoURL at a particular index</returns>
        /// <param name="index">the index to get the photoUrl</param>
        /// <returns>A photourl string at a given index</returns>
        public string GetPhotoUrl(int index)
        {
            List<object> photoUrls = (List<object>)this.data["photoUrls"];
            return (string)photoUrls.ElementAt(index);
        }


        /// <summary>
        /// This sets the category id of a pet regardless of whether the
        /// category ID is valid
        /// </summary>
        /// <param name="categoryId">a valid positive integer or an invalid datatype as an ID</param>
        /// <returns>the current instance of PetData for chaining with builder pattern</returns>
        public PetData SetCategoryID(object categoryId)
        {
            ((Dictionary<string, object>)this.data["category"])["id"] = categoryId;
            return this;
        }

        /// <summary>
        /// This sets the category name of a pet regardless of whether
        /// it would be valid or invalid when sent as a http request
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>the current instance of PetData for chaining with builder pattern</returns>
        public PetData SetCategoryName(object categoryName)
        {
            ((Dictionary<string, object>)this.data["category"])["name"] = categoryName;
            return this;
        }


        /// <summary>
        /// This sets tha name of pet data regardless of whether the
        /// this can be used in an non failing request
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the current instance of PetData for chaining with builder pattern</returns>
        public PetData SetName(object name)
        {
            this.data["name"] = name;
            return this;
        }


        /// <summary>
        /// This sets the status of a pet regardless of whether the 
        /// status would be valid or invalid when making a http request
        /// </summary>
        /// <param name="status"></param>
        /// <returns>the current instance of PetData for chaining with builder pattern</returns>
        public PetData SetStatus(string status)
        {
            this.data["status"] = status; 
            return this;
        }


        /// <summary>
        /// Gets the Pet Data as a JsonString
        /// </summary>
        /// <returns>The json string representing this instande of a pet</returns>
        public string AsJSONString()
        {
            return JsonSerializer.Serialize(this.data);

        }

        /// <summary>
        /// Gets the PetData as a JSon element
        /// </summary>
        /// <returns>A Json element representing thsi instant of a pet</returns>
        public JsonElement AsJSON()
        {

            string jsonString = this.AsJSONString();
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }
    }
}
