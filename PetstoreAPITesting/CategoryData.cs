using Newtonsoft.Json;



namespace PetstoreAPITesting
{

    public class CategoryData
    {
        private object id;
        private object name;

        /// <summary>
        /// Initialises a category with default valid properties
        /// </summary>
        public CategoryData()
        {
            this.id = 0;
            this.name = "Foo";
        }

        /// <summary>
        /// Set a category id which will be an long datatype in valid REST Petstore
        /// API requests
        /// </summary>
        /// <param name="id">The id assigned to the category</param>
        /// <returns>the current instance of CategoryData for chaining with builder pattern</returns>
        public CategoryData SetID(object id)
        {
            this.id = id;
            return this;
        }

        /// <summary>
        /// Ses the name of the category which would ideally be a string
        /// </summary>
        /// <param name="name"></param>
        /// <returns>the current instance of CategoryData for chaining with builder pattern</returns>
        public CategoryData SetName(string name) {
            this.name = name;
            return this;
        }


        /// <summary>
        /// Gets the ID and will fail if the ID object cannot be casted to a long
        /// no tests that will post use an invalid id as category data will use this function
        /// </summary>
        /// <returns>the id assigned to the category</returns>
        public long GetID()
        {
            return (long)this.id;
        }

        /// <summary>
        /// Gets the name of the category and will fail if the object that stores the
        /// category name is not a string, 
        /// No tests that verify if the name of a category is invalid would need to use this
        /// getter function
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return (string)this.name;
        }

        /// <summary>
        /// Serialises the category data as json
        /// </summary>
        /// <returns> a json string</returns>
        public string AsJSONString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
