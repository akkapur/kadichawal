
using Interfaces.DataContracts;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.CRUD
{
    public interface IUserCRUD<T> : IBaseCRUD<T>, ICRUDInitializer where T : User, new()
    {
    }

    public class UserCRUD<T> : EntityCRUD<T>, IUserCRUD<T> where T : User, new()
    {
        public UserCRUD()
            : base(typeof(User))
        {
        }
    }
}
