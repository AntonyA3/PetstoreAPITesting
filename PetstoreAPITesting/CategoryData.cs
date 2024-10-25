using Newtonsoft.Json;



namespace PetstoreAPITesting
{
    internal class CategoryData
    {
        private object id;
        private object name;
        public CategoryData()
        {
            this.id = 0;
            this.name = "Foo";
        }

        public CategoryData SetID(object id)
        {
            this.id = id;
            return this;
        }

        public CategoryData SetName(string name) {
            this.name = name;
            return this;
        }
       

        public string AsJSON()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
