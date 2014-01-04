using Database.DAC.CRUD;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace IndiTownServices.services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OrganizationService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OrganizationService.svc or OrganizationService.svc.cs at the Solution Explorer and start debugging.
    public class OrganizationService : IOrganizationService
    {
        public string CreateOrganization(Organization organization)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            orgCrud.Create(organization);
            return organization.OrganizationId;
        }

        public void DeleteOrganization(Organization organization)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            //orgCrud.Delete(organization.Id);
        }

        public IEnumerable<Organization> GetOrganizationByType(BusinessType businessType = BusinessType.Unknown)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            return businessType == BusinessType.Unknown ? orgCrud.ReadAll() : orgCrud.Read(x => x.BusinessType == businessType);
        }

        public OrganizationProfile GetOrgranizationProfile(string organizationId)
        {
            IOrganizationCRUD<Organization> orgCrud = new OrganizationCRUD<Organization>();
            orgCrud.Initialize();
            Organization organization = orgCrud.Read(organizationId);

            IReviewCRUD<Review> reviewCrud = new ReviewCRUD<Review>();
            reviewCrud.Initialize();

            IEnumerable<Review> reviews = reviewCrud.Read(x => x.OrganizationId == organizationId);

            OrganizationProfile organizationProfile = new OrganizationProfile
            {
                Organization = organization,
                Reviews = reviews.ToArray()
            };
            return organizationProfile;
        }
    }
}
