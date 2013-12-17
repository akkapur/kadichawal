using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface IRoleService
    {
        [OperationContract]
        void CreateRole(string name);
    }
}
