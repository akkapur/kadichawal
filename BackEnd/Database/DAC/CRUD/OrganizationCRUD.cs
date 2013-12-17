using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.CRUD
{
    public interface IOrganizationCRUD<T> : IBaseCRUD<T>, ICRUDInitializer where T : Organization, new()
    {
    }

    public class OrganizationCRUD<T> : EntityCRUD<T>, IOrganizationCRUD<T> where T : Organization, new()
    {
        public OrganizationCRUD()
            : base(typeof(Organization))
        {
        }
    }

}
