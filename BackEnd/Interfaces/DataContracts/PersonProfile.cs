using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataContracts
{
    public class PersonProfile
    {
        public Person Person { get; set; }

        public IEnumerable<Review> Reviews { get; set; }
    }
}
