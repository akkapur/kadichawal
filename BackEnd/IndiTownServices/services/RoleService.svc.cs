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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "RoleService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select RoleService.svc or RoleService.svc.cs at the Solution Explorer and start debugging.
    public class RoleService : IRoleService
    {
        void IRoleService.CreateRole(string name)
        {
            //MembershipRole role = new MembershipRole() { RoleName = name };
            //IRoleCRUD<MembershipRole> crud = new RoleCRUD<MembershipRole>();
            //crud.Initialize();
            //crud.Create(role);
        }
    }
}
