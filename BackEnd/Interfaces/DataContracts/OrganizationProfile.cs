using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataContracts
{
    public class OrganizationProfile
    {
        public Organization Organization { get; set; }

        public Review[] Reviews { get; set; }

        //TODO : Add other details like pictures, media coverage, twitter posts, etc.
    }
}
