using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndiTownServices.services
{
    public class OrganizationService : IOrganizationService
    {
        public void CreateOrganization(Organization organization)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            orgCrud.Create(organization);
        }

        public void DeleteOrganization(Organization organization)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            orgCrud.Delete(organization.Id);
        }
    }
}
