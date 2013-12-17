using Database.DAC.CRUD;
using Interfaces.DataContracts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Security;

namespace Database.DAC.Membership
{
    public class MongoMembershipProvider : MembershipProvider
    {
        //private int newPasswordLength = 8;
        //private string eventSource = "MongoMembershipProvider";
        //private string eventLog = "Application";
        //private string exceptionMessage = "An exception occurred. Please check the Event Log.";
        private MachineKeySection machineKey;
        private bool pWriteExceptionsToEventLog;


        private string pApplicationName;
        private bool pEnablePasswordReset;
        private bool pEnablePasswordRetrieval;
        private bool pRequiresQuestionAndAnswer;
        private bool pRequiresUniqueEmail;
        private int pMaxInvalidPasswordAttempts;
        private int pPasswordAttemptWindow;
        private MembershipPasswordFormat pPasswordFormat;

        public override string ApplicationName
        {
            get { return pApplicationName; }
            set { pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return pEnablePasswordReset; }
        }


        public override bool EnablePasswordRetrieval
        {
            get { return pEnablePasswordRetrieval; }
        }


        public override bool RequiresQuestionAndAnswer
        {
            get { return pRequiresQuestionAndAnswer; }
        }


        public override bool RequiresUniqueEmail
        {
            get { return pRequiresUniqueEmail; }
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { return pMaxInvalidPasswordAttempts; }
        }


        public override int PasswordAttemptWindow
        {
            get { return pPasswordAttemptWindow; }
        }


        public override MembershipPasswordFormat PasswordFormat
        {
            get { return pPasswordFormat; }
        }

        private int pMinRequiredNonAlphanumericCharacters;

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return pMinRequiredNonAlphanumericCharacters; }
        }

        private int pMinRequiredPasswordLength;

        public override int MinRequiredPasswordLength
        {
            get { return pMinRequiredPasswordLength; }
        }

        private string pPasswordStrengthRegularExpression;

        public override string PasswordStrengthRegularExpression
        {
            get { return pPasswordStrengthRegularExpression; }
        }

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "MongoMembershipProvider";

            // Initialize the abstract base class.
            base.Initialize(name, config);

            pApplicationName = GetConfigValue(config["applicationName"],
                                            System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
            pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
            pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
            pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
            pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
            pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
            pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
            pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
            pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
            pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

            machineKey = new MachineKeySection();
            machineKey.ValidationKey = GetConfigValue(config["ValidationKey"], "BC96ACF785D616E466E3977F9C4F622886CB3B1E");
            machineKey.DecryptionKey = GetConfigValue(config["DecryptionKey"], "F50C9A39346153D09B94CDCBED0358CC1110CF4EFDBAABBC3F5735EC02C3C43E");

            string temp_format = config["passwordFormat"];
            if (temp_format == null)
            {
                temp_format = "Hashed";
            }

            switch (temp_format)
            {
                case "Hashed":
                    pPasswordFormat = MembershipPasswordFormat.Hashed;
                    break;
                case "Encrypted":
                    pPasswordFormat = MembershipPasswordFormat.Encrypted;
                    break;
                case "Clear":
                    pPasswordFormat = MembershipPasswordFormat.Clear;
                    break;
                default:
                    throw new ProviderException("Password format not supported.");
            }

        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved,
             object providerUserKey,
             out MembershipCreateStatus status)
        {
            User user = new User();
            user.Password = password;
            user.EmailAddress = email;
            user.PasswordQuestion = passwordQuestion;
            user.PasswordAnswer = passwordAnswer;

            return CreateUser(user, out status);
        }

        public MembershipUser CreateUser(User user, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;

            ValidatePasswordEventArgs args =
              new ValidatePasswordEventArgs(user.UserName, user.Password, true);

            OnValidatingPassword(args);

            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }

            if (RequiresUniqueEmail && GetUserNameByEmail(user.EmailAddress) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }

            MembershipUser u = GetUser(user.UserName, false);

            if (u == null)
            {
                DateTime createDate = DateTime.Now;

                user.CreationDate = createDate;
                user.IsApproved = true;
                user.Password = EncodePassword(user.Password);
                user.PasswordQuestion = user.PasswordQuestion;
                user.PasswordAnswer = String.IsNullOrWhiteSpace(user.PasswordAnswer) ? String.Empty : EncodePassword(user.PasswordAnswer);
                user.FailedPasswordAttemptWindowStart = createDate;
                user.LastPasswordChangedDate = createDate;
                user.LastActivityDate = createDate;
                user.FailedPasswordAttemptCount = 0;
                user.FailedPasswordAttemptWindowStart = createDate;
                user.FailedPasswordAnswerAttemptCount = 0;
                user.FailedPasswordAnswerAttemptWindowStart = createDate;

                try
                {
                    IUserCRUD<User> userCRUD = new UserCRUD<User>();
                    userCRUD.Initialize();
                    userCRUD.Create(user);
                }
                catch (Exception)
                {
                    status = MembershipCreateStatus.ProviderError;
                }

                return GetUser(user.UserName, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }

            return null;
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            MembershipUser memberShipUser = null;

            try
            {
                IUserCRUD<User> userCRUD = new UserCRUD<User>();
                userCRUD.Initialize();
                User user = userCRUD.Read(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();

                if (user != null)
                {
                    memberShipUser = new MembershipUser(System.Web.Security.Membership.Provider.Name, user.UserName, user.UserId, user.EmailAddress, user.PasswordQuestion,
                        String.Empty, user.IsApproved, user.IsLockedOut, user.CreationDate, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate, 
                        user.LastLockedOutDate);
                }
            }
            catch(Exception)
            {
                throw;
            }

            return memberShipUser;
        }

        public User GetUser(string username)
        {
            try
            {
                IUserCRUD<User> userCRUD = new UserCRUD<User>();
                userCRUD.Initialize();
                User user = userCRUD.Read(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string GetUserNameByEmail(string emailAddress)
        {
            IUserCRUD<User> userCRUD = new UserCRUD<User>();
            userCRUD.Initialize();
            User user = userCRUD.Read(x => x.EmailAddress.ToLower() == emailAddress.ToLower()).FirstOrDefault<User>();
            if (user != null)
                return user.EmailAddress;

            return String.Empty;
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
                return defaultValue;

            return configValue;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                IUserCRUD<User> userCRUD = new UserCRUD<User>();
                userCRUD.Initialize();
                userCRUD.Delete(x => x.UserName.ToLower() == username.ToLower());
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            try
            {
                IUserCRUD<User> userCRUD = new UserCRUD<User>();
                userCRUD.Initialize();
                userCRUD.Update(user);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public void SignOutUser(string username)
        {
            IUserCRUD<User> userCRUD = new UserCRUD<User>();
            userCRUD.Initialize();
            User user = userCRUD.Read(x => x.UserName.ToLower() == username.ToLower()).FirstOrDefault();
            if (user != null)
            {
                user.IsOnLine = false;
                UpdateUser(user);
            }
        }

        public override bool ValidateUser(string username, string password)
        {
            try
            {
                IUserCRUD<User> userCRUD = new UserCRUD<User>();
                userCRUD.Initialize();
                User user = userCRUD.Read(x => x.UserName.ToLower() == username.ToLower() && x.IsLockedOut == false).FirstOrDefault<User>();
                if (user != null)
                {
                    if (CheckPassword(password, user.Password))
                    {
                        if (user.IsApproved)
                        {
                            user.LastLoginDate = DateTime.Now;
                            user.IsOnLine = true;
                            userCRUD.Update(user);
                            return true;
                        }
                        return false;
                    }
                    else //failed login attempt
                    {
                        DateTime windowStart = user.FailedPasswordAttemptWindowStart;
                        DateTime windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);
                        int failureCount = user.FailedPasswordAttemptCount;
                        if (failureCount == 0 || DateTime.Now > windowEnd) //first failure or outside the window.
                        {
                            user.FailedPasswordAttemptCount += 1;
                            user.FailedPasswordAttemptWindowStart = DateTime.Now;
                        }
                        else
                        {
                            if (failureCount++ >= MaxInvalidPasswordAttempts) //attempts exceed or equals to max invalid attempts allowed
                            {
                                user.IsLockedOut = true;
                                user.LastLockedOutDate = DateTime.Now;
                            }
                            else //in the window of invalid attempts.
                            {
                                user.FailedPasswordAttemptCount = failureCount;
                            }
                        }
                        userCRUD.Update(user);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        //
        // CheckPassword
        //   Compares password values based on the MembershipPasswordFormat.
        //

        private bool CheckPassword(string password, string dbpassword)
        {
            string pass1 = password;
            string pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
                default:
                    break;
            }

            if (pass1 == pass2)
            {
                return true;
            }

            return false;
        }

        private string EncodePassword(string password)
        {
            string encodedPassword = password;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    encodedPassword =
                      Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    HMACSHA1 hash = new HMACSHA1();
                    hash.Key = HexToByte(machineKey.ValidationKey);
                    encodedPassword =
                      Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                    break;
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return encodedPassword;
        }

        private string UnEncodePassword(string encodedPassword)
        {
            string password = encodedPassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Clear:
                    break;
                case MembershipPasswordFormat.Encrypted:
                    password =
                      Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                    break;
                case MembershipPasswordFormat.Hashed:
                    throw new ProviderException("Cannot unencode a hashed password.");
                default:
                    throw new ProviderException("Unsupported password format.");
            }

            return password;
        }

        //
        // HexToByte
        //   Converts a hexadecimal string to a byte array. Used to convert encryption
        // key values from the configuration.
        //

        private byte[] HexToByte(string hexString)
        {
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}
