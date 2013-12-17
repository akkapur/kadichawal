using Database.DAC.CRUD;
using Database.DAC.Membership;
using Interfaces.DataContracts;
using Interfaces.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Web.Configuration;
using System.Web.Security;

namespace IndiTownServices.services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {
        public string CreateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("User cannot be null");        

            MembershipCreateStatus status = new MembershipCreateStatus();
            MongoMembershipProvider userCRUD = new MongoMembershipProvider();
            userCRUD.Initialize(String.Empty, GetConfigValueCollection());
            userCRUD.CreateUser(user, out status);
            if (status == MembershipCreateStatus.Success)
                return user.UserId;
            else
                throw new Exception();
        }

        public void UpdateUser(User user)
        {
            MongoMembershipProvider userCRUD = new MongoMembershipProvider();
            userCRUD.Initialize(String.Empty, GetConfigValueCollection());
            userCRUD.UpdateUser(user);
        }

        public void DeleteUser(User user)
        {
            MongoMembershipProvider userCRUD = new MongoMembershipProvider();
            userCRUD.Initialize(String.Empty, GetConfigValueCollection());
            userCRUD.DeleteUser(user.UserName, true);
        }

        public bool AuthenticateUser(string userName, string password)
        {
            MongoMembershipProvider userCRUD = new MongoMembershipProvider();
            userCRUD.Initialize(String.Empty, GetConfigValueCollection());
            return userCRUD.ValidateUser(userName, password);
        }

        public void SignOutUser(string username)
        {
            MongoMembershipProvider userCRUD = new MongoMembershipProvider();
            userCRUD.Initialize(String.Empty, GetConfigValueCollection());
            userCRUD.SignOutUser(username);
        }

        public User GetUser(string username)
        {
            UserCRUD<User> userCRUD = new UserCRUD<User>();
            userCRUD.Initialize();
            return userCRUD.Read(x => x.UserName == username).Single<User>();
        }

        private NameValueCollection GetConfigValueCollection()
        {
            NameValueCollection collection = new NameValueCollection();
            var section = ConfigurationManager.GetSection("system.web/machineKey") as MachineKeySection;
            if (section != null)
            {
                collection.Add("ValidationKey", section.ValidationKey);
                collection.Add("DecryptionKey", section.DecryptionKey);
            }
            return collection;
        }
    }
}
