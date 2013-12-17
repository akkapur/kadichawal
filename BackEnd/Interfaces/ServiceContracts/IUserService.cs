using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ServiceContracts
{
    [ServiceContract]
    public interface IUserService
    {
        /// <summary>
        /// Creates a user. Returns the user if of the new user created.
        /// </summary>
        /// <param name="user">user to be created.</param>
        [OperationContract]
        string CreateUser(User user);

        /// <summary>
        /// Updates the user's profile.
        /// </summary>
        /// <param name="user"></param>
        [OperationContract]
        void UpdateUser(User user);

        /// <summary>
        /// Removes the user from the database.
        /// </summary>
        /// <param name="user">user to be deleted.</param>
        [OperationContract]
        void DeleteUser(User user);

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        [OperationContract]
        bool AuthenticateUser(string userName, string password);

        /// <summary>
        /// Sets the IsOnlineFlag for the user to false.
        /// </summary>
        /// <param name="username"></param>
        [OperationContract]
        void SignOutUser(string username);

        [OperationContract]
        User GetUser(string username);
    }
}
