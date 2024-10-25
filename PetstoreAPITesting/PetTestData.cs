using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetstoreAPITesting
{
    public class PetTestData
    {
        private string casename;
        private PetData value;
        public PetTestData(string casename, PetData value)
        {
            this.casename = casename;
            this.value = value;
        }

        public string Casename { get; set; }
        public PetData Value { get; set; }
    }
}
