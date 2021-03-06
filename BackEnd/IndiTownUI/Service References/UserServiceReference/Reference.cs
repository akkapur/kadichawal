﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndiTownUI.UserServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="User", Namespace="http://schemas.datacontract.org/2004/07/Interfaces.DataContracts")]
    [System.SerializableAttribute()]
    public partial class User : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsOrganizationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordAnswerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordQuestionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string EmailAddress {
            get {
                return this.EmailAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailAddressField, value) != true)) {
                    this.EmailAddressField = value;
                    this.RaisePropertyChanged("EmailAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsOrganization {
            get {
                return this.IsOrganizationField;
            }
            set {
                if ((this.IsOrganizationField.Equals(value) != true)) {
                    this.IsOrganizationField = value;
                    this.RaisePropertyChanged("IsOrganization");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordAnswer {
            get {
                return this.PasswordAnswerField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordAnswerField, value) != true)) {
                    this.PasswordAnswerField = value;
                    this.RaisePropertyChanged("PasswordAnswer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string PasswordQuestion {
            get {
                return this.PasswordQuestionField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordQuestionField, value) != true)) {
                    this.PasswordQuestionField = value;
                    this.RaisePropertyChanged("PasswordQuestion");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserId {
            get {
                return this.UserIdField;
            }
            set {
                if ((object.ReferenceEquals(this.UserIdField, value) != true)) {
                    this.UserIdField = value;
                    this.RaisePropertyChanged("UserId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        string CreateUser(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/CreateUser", ReplyAction="http://tempuri.org/IUserService/CreateUserResponse")]
        System.Threading.Tasks.Task<string> CreateUserAsync(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUser", ReplyAction="http://tempuri.org/IUserService/UpdateUserResponse")]
        void UpdateUser(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/UpdateUser", ReplyAction="http://tempuri.org/IUserService/UpdateUserResponse")]
        System.Threading.Tasks.Task UpdateUserAsync(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        void DeleteUser(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/DeleteUser", ReplyAction="http://tempuri.org/IUserService/DeleteUserResponse")]
        System.Threading.Tasks.Task DeleteUserAsync(IndiTownUI.UserServiceReference.User user);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticateUser", ReplyAction="http://tempuri.org/IUserService/AuthenticateUserResponse")]
        bool AuthenticateUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/AuthenticateUser", ReplyAction="http://tempuri.org/IUserService/AuthenticateUserResponse")]
        System.Threading.Tasks.Task<bool> AuthenticateUserAsync(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SignOutUser", ReplyAction="http://tempuri.org/IUserService/SignOutUserResponse")]
        void SignOutUser(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/SignOutUser", ReplyAction="http://tempuri.org/IUserService/SignOutUserResponse")]
        System.Threading.Tasks.Task SignOutUserAsync(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUser", ReplyAction="http://tempuri.org/IUserService/GetUserResponse")]
        IndiTownUI.UserServiceReference.User GetUser(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUser", ReplyAction="http://tempuri.org/IUserService/GetUserResponse")]
        System.Threading.Tasks.Task<IndiTownUI.UserServiceReference.User> GetUserAsync(string username);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : IndiTownUI.UserServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<IndiTownUI.UserServiceReference.IUserService>, IndiTownUI.UserServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string CreateUser(IndiTownUI.UserServiceReference.User user) {
            return base.Channel.CreateUser(user);
        }
        
        public System.Threading.Tasks.Task<string> CreateUserAsync(IndiTownUI.UserServiceReference.User user) {
            return base.Channel.CreateUserAsync(user);
        }
        
        public void UpdateUser(IndiTownUI.UserServiceReference.User user) {
            base.Channel.UpdateUser(user);
        }
        
        public System.Threading.Tasks.Task UpdateUserAsync(IndiTownUI.UserServiceReference.User user) {
            return base.Channel.UpdateUserAsync(user);
        }
        
        public void DeleteUser(IndiTownUI.UserServiceReference.User user) {
            base.Channel.DeleteUser(user);
        }
        
        public System.Threading.Tasks.Task DeleteUserAsync(IndiTownUI.UserServiceReference.User user) {
            return base.Channel.DeleteUserAsync(user);
        }
        
        public bool AuthenticateUser(string userName, string password) {
            return base.Channel.AuthenticateUser(userName, password);
        }
        
        public System.Threading.Tasks.Task<bool> AuthenticateUserAsync(string userName, string password) {
            return base.Channel.AuthenticateUserAsync(userName, password);
        }
        
        public void SignOutUser(string username) {
            base.Channel.SignOutUser(username);
        }
        
        public System.Threading.Tasks.Task SignOutUserAsync(string username) {
            return base.Channel.SignOutUserAsync(username);
        }
        
        public IndiTownUI.UserServiceReference.User GetUser(string username) {
            return base.Channel.GetUser(username);
        }
        
        public System.Threading.Tasks.Task<IndiTownUI.UserServiceReference.User> GetUserAsync(string username) {
            return base.Channel.GetUserAsync(username);
        }
    }
}
