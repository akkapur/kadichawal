﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IndiTownUI.ReviewServiceReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Review", Namespace="http://schemas.datacontract.org/2004/07/Interfaces.DataContracts")]
    [System.SerializableAttribute()]
    public partial class Review : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime CreationDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsApprovedField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrganizationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReviewIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReviewTextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReviewerIdField;
        
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
        public System.DateTime CreationDate {
            get {
                return this.CreationDateField;
            }
            set {
                if ((this.CreationDateField.Equals(value) != true)) {
                    this.CreationDateField = value;
                    this.RaisePropertyChanged("CreationDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsApproved {
            get {
                return this.IsApprovedField;
            }
            set {
                if ((this.IsApprovedField.Equals(value) != true)) {
                    this.IsApprovedField = value;
                    this.RaisePropertyChanged("IsApproved");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrganizationId {
            get {
                return this.OrganizationIdField;
            }
            set {
                if ((object.ReferenceEquals(this.OrganizationIdField, value) != true)) {
                    this.OrganizationIdField = value;
                    this.RaisePropertyChanged("OrganizationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReviewId {
            get {
                return this.ReviewIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ReviewIdField, value) != true)) {
                    this.ReviewIdField = value;
                    this.RaisePropertyChanged("ReviewId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReviewText {
            get {
                return this.ReviewTextField;
            }
            set {
                if ((object.ReferenceEquals(this.ReviewTextField, value) != true)) {
                    this.ReviewTextField = value;
                    this.RaisePropertyChanged("ReviewText");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReviewerId {
            get {
                return this.ReviewerIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ReviewerIdField, value) != true)) {
                    this.ReviewerIdField = value;
                    this.RaisePropertyChanged("ReviewerId");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReviewServiceReference.IReviewService")]
    public interface IReviewService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/CreateReview", ReplyAction="http://tempuri.org/IReviewService/CreateReviewResponse")]
        string CreateReview(IndiTownUI.ReviewServiceReference.Review review);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/CreateReview", ReplyAction="http://tempuri.org/IReviewService/CreateReviewResponse")]
        System.Threading.Tasks.Task<string> CreateReviewAsync(IndiTownUI.ReviewServiceReference.Review review);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetReview", ReplyAction="http://tempuri.org/IReviewService/GetReviewResponse")]
        IndiTownUI.ReviewServiceReference.Review GetReview(string reviewId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetReview", ReplyAction="http://tempuri.org/IReviewService/GetReviewResponse")]
        System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review> GetReviewAsync(string reviewId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetUserReviews", ReplyAction="http://tempuri.org/IReviewService/GetUserReviewsResponse")]
        IndiTownUI.ReviewServiceReference.Review[] GetUserReviews(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetUserReviews", ReplyAction="http://tempuri.org/IReviewService/GetUserReviewsResponse")]
        System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review[]> GetUserReviewsAsync(string userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetBusinessReview", ReplyAction="http://tempuri.org/IReviewService/GetBusinessReviewResponse")]
        IndiTownUI.ReviewServiceReference.Review[] GetBusinessReview(string organizationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IReviewService/GetBusinessReview", ReplyAction="http://tempuri.org/IReviewService/GetBusinessReviewResponse")]
        System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review[]> GetBusinessReviewAsync(string organizationId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IReviewServiceChannel : IndiTownUI.ReviewServiceReference.IReviewService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReviewServiceClient : System.ServiceModel.ClientBase<IndiTownUI.ReviewServiceReference.IReviewService>, IndiTownUI.ReviewServiceReference.IReviewService {
        
        public ReviewServiceClient() {
        }
        
        public ReviewServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReviewServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReviewServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReviewServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string CreateReview(IndiTownUI.ReviewServiceReference.Review review) {
            return base.Channel.CreateReview(review);
        }
        
        public System.Threading.Tasks.Task<string> CreateReviewAsync(IndiTownUI.ReviewServiceReference.Review review) {
            return base.Channel.CreateReviewAsync(review);
        }
        
        public IndiTownUI.ReviewServiceReference.Review GetReview(string reviewId) {
            return base.Channel.GetReview(reviewId);
        }
        
        public System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review> GetReviewAsync(string reviewId) {
            return base.Channel.GetReviewAsync(reviewId);
        }
        
        public IndiTownUI.ReviewServiceReference.Review[] GetUserReviews(string userId) {
            return base.Channel.GetUserReviews(userId);
        }
        
        public System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review[]> GetUserReviewsAsync(string userId) {
            return base.Channel.GetUserReviewsAsync(userId);
        }
        
        public IndiTownUI.ReviewServiceReference.Review[] GetBusinessReview(string organizationId) {
            return base.Channel.GetBusinessReview(organizationId);
        }
        
        public System.Threading.Tasks.Task<IndiTownUI.ReviewServiceReference.Review[]> GetBusinessReviewAsync(string organizationId) {
            return base.Channel.GetBusinessReviewAsync(organizationId);
        }
    }
}
