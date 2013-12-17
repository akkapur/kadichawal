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
    public class UserService : IUserService
    {
        public void CreateUser(User user)
        {
            IUserCRUD<User> userCRUD = new UserCRUD<User>();
            userCRUD.Initialize();
            userCRUD.Create(user);
        }

        public void DeleteUser(User user)
        {
            IUserCRUD<User> userCRUD = new UserCRUD<User>();
            userCRUD.Initialize();
            userCRUD.Delete(user.Id);
        }
    }
}
