﻿using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface IOrganizationService
    {
        /// <summary>
        /// Creates an organization record.
        /// </summary>
        /// <param name="organization">Organization to be created.</param>
        [OperationContract]
        string CreateOrganization(Organization organization);

        /// <summary>
        /// Removes an organization record.
        /// </summary>
        /// <param name="organization">Organization to be deleted.</param>
        [OperationContract]
        void DeleteOrganization(Organization organization);
    }
}