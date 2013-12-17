using Interfaces.DataContracts;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface IPersonService
    {
        [OperationContract]
        void CreatePerson(Person person);

        [OperationContract]
        void UpdatePerson(Person person);

        [OperationContract]
        Person GetPerson(string userId);

        [OperationContract]
        PersonProfile GetPersonProfile(string userId);

        [OperationContract]
        void DeletePerson(Person person);
    }
}
