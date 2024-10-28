
namespace PetstoreAPITesting
{
    /// <summary>
    /// This stores a petData and a description for the
    /// the description of the testcase 
    /// </summary>
    public class PetTestData
    {
        private string casename;
        private PetData value;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="casename"> Description of the PetData within the test</param>
        /// <param name="value">The PetData that will be used in a test </param>
        public PetTestData(string casename, PetData value)
        {
            this.casename = casename;
            this.value = value;
        }

        /// <summary>
        /// Returns a copy of a description of a potential test case argument
        /// </summary>
        /// <returns>the description </returns>
        public string GetCasename() {
            return this.casename;
        }

        /// <summary>
        /// Gets a copy of the pet data that will be used within a test case
        /// </summary>
        /// <returns>the pet data for a test</returns>
        public PetData GetValue() { 
            return value;
        }
    }
}
