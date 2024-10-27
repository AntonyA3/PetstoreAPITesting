using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;


namespace PetstoreAPITesting
{
    public class PetData
    {
        new Dictionary<string, object> data;
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

        public PetData SetValidID(long id)
        {
            this.data["id"] = id;
                //JsonDocument.Parse(id.ToString()).RootElement.Clone();
            return this;
        }



        public string GetStatus()
        {

            return (string)this.data["status"];
            //.GetString();

        }

        public long GetCategoryId()
        {
            return (long)((Dictionary<string, object>)data["category"])["id"];
                //).GetInt64();
        }

        public string GetCategoryName()
        {
            return (string)((Dictionary<string, object>)data["category"])["name"];

            //return this.data["category"].GetProperty("name").GetString();
        }

        public string GetName()
        {
            return (string)this.data["name"];//.GetString();

        }

        public object GetID()
        {
            return this.data["id"];//.GetInt64();
        }

        public PetData SetID(object id)
        {
            this.data["id"] = id;
            return this;
        }

        public PetData AddPhotoUrl(object photoUrl)
        {
            ((List<object>)this.data["photoUrls"]).Add(photoUrl); //JsonDocument.Parse(photoUrl.ToString()).RootElement.Clone();
            return this;
        }

        public long GetTagID(int index)
        {
            List<object> tags = (List<object>)this.data["tags"];
            Dictionary<string, object> tag = (Dictionary<string, object>)tags.ElementAt(index);
            long id = (long)tag["id"];
            return id;
        }

        public string GetTagName(int index)
        {
            List<object> tags = (List<object>)this.data["tags"];
            Dictionary<string, object> tag = (Dictionary<string, object>)tags.ElementAt(index);
            string id = (string)tag["name"];
            return id;
        }

        public string GetPhotoUrl(int index)
        {
            List<object> photoUrls = (List<object>)this.data["photoUrls"];
            return (string)photoUrls.ElementAt(index);
        }



        public PetData SetCategoryID(object categoryId)
        {
            ((Dictionary<string, object>)this.data["category"])["id"] = categoryId; //JsonDocument.Parse(category.ToString()).RootElement.Clone();
            return this;
        }

        public PetData SetCategoryName(object categoryName)
        {
            ((Dictionary<string, object>)this.data["category"])["name"] = categoryName; //JsonDocument.Parse(category.ToString()).RootElement.Clone();
            return this;
        }

        public PetData SetName(object name)
        {
            this.data["name"] = name; //JsonDocument.Parse(name.ToString()).RootElement.Clone();
            return this;
        }

        public PetData SetStatus(string status)
        {
            this.data["status"] = status; // JsonDocument.Parse(status.ToString()).RootElement.Clone();
            return this;
        }


        public string AsJSONString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this.data);

        }


        public JsonElement AsJSON()
        {
    
            string jsonString = System.Text.Json.JsonSerializer.Serialize(this.data);
            using (JsonDocument document = JsonDocument.Parse(jsonString))
            {
                JsonElement root = document.RootElement;
                return root.Clone();
            }
        }
    }
}
