using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataContracts
{
    public class SearchResult
    {
        public Organization Organization { get; set; }

        public Review[] Reviews { get; set; }
    }
}
