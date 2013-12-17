using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.DataContracts
{
    public class OrganizationUser
    {
        public User User { get; set; }

        public Organization Organization { get; set; }
    }
}
