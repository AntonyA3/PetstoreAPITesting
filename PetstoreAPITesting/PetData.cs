using Newtonsoft.Json;


namespace PetstoreAPITesting
{
    public class PetData
    {
        private Dictionary<string, object> data;

        public PetData()
        {
            this.data = new Dictionary<string, object>();
            this.data.Add("id", 11);
            this.data.Add("name", "Baz");
            Dictionary<string, object> category = new Dictionary<string, object>();
            category.Add("id", 0);
            category.Add("name", "Dogs");
            this.data.Add("category", category);
            List<object> tags = new List<object>();
            Dictionary<string, object> tag = new Dictionary<string, object>();
            tag.Add("id", 0);
            tag.Add("name", "string");
            tags.Add(tag);
            this.data.Add("tags", tags);
            this.data.Add("photoUrls", new object[0]);
            this.data.Add("status", "available");
        }

        public PetData SetValidID(int id)
        {
            this.data["id"] = id;
            return this;
        }

        public PetData SetTags(string tags) {
            string[] tagarray = tags.Split(',');
            var newtaglist = new List<object>();
            int identifier = 0;
            foreach (var tag in tagarray)
            {
                newtaglist.Add(new{
                     id = identifier,
                     name = tag
                });
                identifier++;
            }
            this.data["tags"] = newtaglist;
            return this;
        }

        public object GetName()
        {
            return this.data["name"];
        }

        public object GetCategory()
        {
            return this.data["category"];
        }


        public int GetID()
        {
            return (int)this.data["id"];
        }



        public PetData SetID(object id)
        {
            this.data["id"] = id;
            return this;
        }

        public PetData SetPhotoUrls(object photoUrl)
        {
            this.data["photoUrls"] = photoUrl;
            return this;
        }

        public object GetStatus()
        {
            return this.data["status"];
        }

        public object GetPhotoUrls()
        {
            return this.data["photoUrls"];
        }

        public object GetTags()
        {
            return this.data["tags"];
        }

        public PetData SetCategory(object category)
        {
            this.data["category"] = category;
            return this;
        }

        public PetData SetName(object name) {
            this.data["name"] = name;
            return this;
        }

        public PetData SetStatus(string status) { 
            this.data["status"] = status; 
            return this; 
        }

        public string AsJSON()
        {
            return JsonConvert.SerializeObject(this.data);

        }

    }
}
