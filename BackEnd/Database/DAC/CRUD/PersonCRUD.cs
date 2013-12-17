
using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.DAC.CRUD
{
    public interface IPersonCRUD<T> : IBaseCRUD<T>, ICRUDInitializer where T : Person, new()
    {
    }

    public class PersonCRUD<T> : EntityCRUD<T>, IPersonCRUD<T> where T : Person, new()
    {
        public PersonCRUD()
            : base(typeof(Person))
        {
        }
    }
}
