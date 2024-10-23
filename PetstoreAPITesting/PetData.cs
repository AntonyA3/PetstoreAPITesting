using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PetstoreAPITesting
{
    internal class PetData
    {
        public Dictionary<string, object> data;



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
            this.data.Add("status", "available");
        }

        public PetData SetValidID(int id)
        {
            this.data["id"] = id;
            return this;
        }

        public PetData SetID(object id)
        {
            this.data["id"] = id;
            return this;
        }

        public string AsJSON()
        {
            return JsonConvert.SerializeObject(this.data);

        }

    }
}
