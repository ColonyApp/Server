﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ColonyServerWebApplication.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/", ConfigurationName="ServiceReference1.WebService1Soap")]
    public interface WebService1Soap {
        
        // CODEGEN: Generating message contract since element name HelloWorldResult from namespace http://colonywebappdb.azurewebsites.net/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/HelloWorld", ReplyAction="*")]
        ColonyServerWebApplication.ServiceReference1.HelloWorldResponse HelloWorld(ColonyServerWebApplication.ServiceReference1.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.HelloWorldResponse> HelloWorldAsync(ColonyServerWebApplication.ServiceReference1.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateUserId", ReplyAction="*")]
        System.Guid CreateUserId();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateUserId", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Guid> CreateUserIdAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateGroupId", ReplyAction="*")]
        System.Guid CreateGroupId();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateGroupId", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Guid> CreateGroupIdAsync();
        
        // CODEGEN: Generating message contract since element name nickName from namespace http://colonywebappdb.azurewebsites.net/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateUser", ReplyAction="*")]
        ColonyServerWebApplication.ServiceReference1.CreateUserResponse CreateUser(ColonyServerWebApplication.ServiceReference1.CreateUserRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/CreateUser", ReplyAction="*")]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.CreateUserResponse> CreateUserAsync(ColonyServerWebApplication.ServiceReference1.CreateUserRequest request);
        
        // CODEGEN: Generating message contract since element name nickName from namespace http://colonywebappdb.azurewebsites.net/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsUser", ReplyAction="*")]
        ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse IsExistsUser(ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsUser", ReplyAction="*")]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse> IsExistsUserAsync(ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest request);
        
        // CODEGEN: Generating message contract since element name groupName from namespace http://colonywebappdb.azurewebsites.net/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsGroup", ReplyAction="*")]
        ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse IsExistsGroup(ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsGroup", ReplyAction="*")]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse> IsExistsGroupAsync(ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsUserGroupChain", ReplyAction="*")]
        bool IsExistsUserGroupChain(System.Guid userId, System.Guid groupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/IsExistsUserGroupChain", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> IsExistsUserGroupChainAsync(System.Guid userId, System.Guid groupId);
        
        // CODEGEN: Generating message contract since element name oldNickname from namespace http://colonywebappdb.azurewebsites.net/ is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/ModifyNickName", ReplyAction="*")]
        ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse ModifyNickName(ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://colonywebappdb.azurewebsites.net/ModifyNickName", ReplyAction="*")]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse> ModifyNickNameAsync(ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.HelloWorldRequestBody Body;
        
        public HelloWorldRequest() {
        }
        
        public HelloWorldRequest(ColonyServerWebApplication.ServiceReference1.HelloWorldRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody {
        
        public HelloWorldRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.HelloWorldResponseBody Body;
        
        public HelloWorldResponse() {
        }
        
        public HelloWorldResponse(ColonyServerWebApplication.ServiceReference1.HelloWorldResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class HelloWorldResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody() {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult) {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateUser", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.CreateUserRequestBody Body;
        
        public CreateUserRequest() {
        }
        
        public CreateUserRequest(ColonyServerWebApplication.ServiceReference1.CreateUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class CreateUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string nickName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string mailAddress;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string groupName;
        
        public CreateUserRequestBody() {
        }
        
        public CreateUserRequestBody(string nickName, string mailAddress, string groupName) {
            this.nickName = nickName;
            this.mailAddress = mailAddress;
            this.groupName = groupName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CreateUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="CreateUserResponse", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.CreateUserResponseBody Body;
        
        public CreateUserResponse() {
        }
        
        public CreateUserResponse(ColonyServerWebApplication.ServiceReference1.CreateUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class CreateUserResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool CreateUserResult;
        
        public CreateUserResponseBody() {
        }
        
        public CreateUserResponseBody(bool CreateUserResult) {
            this.CreateUserResult = CreateUserResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsExistsUserRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsExistsUser", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.IsExistsUserRequestBody Body;
        
        public IsExistsUserRequest() {
        }
        
        public IsExistsUserRequest(ColonyServerWebApplication.ServiceReference1.IsExistsUserRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class IsExistsUserRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string nickName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string mailAddress;
        
        public IsExistsUserRequestBody() {
        }
        
        public IsExistsUserRequestBody(string nickName, string mailAddress) {
            this.nickName = nickName;
            this.mailAddress = mailAddress;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsExistsUserResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsExistsUserResponse", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.IsExistsUserResponseBody Body;
        
        public IsExistsUserResponse() {
        }
        
        public IsExistsUserResponse(ColonyServerWebApplication.ServiceReference1.IsExistsUserResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class IsExistsUserResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool IsExistsUserResult;
        
        public IsExistsUserResponseBody() {
        }
        
        public IsExistsUserResponseBody(bool IsExistsUserResult) {
            this.IsExistsUserResult = IsExistsUserResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsExistsGroupRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsExistsGroup", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequestBody Body;
        
        public IsExistsGroupRequest() {
        }
        
        public IsExistsGroupRequest(ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class IsExistsGroupRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string groupName;
        
        public IsExistsGroupRequestBody() {
        }
        
        public IsExistsGroupRequestBody(string groupName) {
            this.groupName = groupName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class IsExistsGroupResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="IsExistsGroupResponse", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponseBody Body;
        
        public IsExistsGroupResponse() {
        }
        
        public IsExistsGroupResponse(ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class IsExistsGroupResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool IsExistsGroupResult;
        
        public IsExistsGroupResponseBody() {
        }
        
        public IsExistsGroupResponseBody(bool IsExistsGroupResult) {
            this.IsExistsGroupResult = IsExistsGroupResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ModifyNickNameRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ModifyNickName", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequestBody Body;
        
        public ModifyNickNameRequest() {
        }
        
        public ModifyNickNameRequest(ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class ModifyNickNameRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public System.Guid userId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string oldNickname;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string newNickname;
        
        public ModifyNickNameRequestBody() {
        }
        
        public ModifyNickNameRequestBody(System.Guid userId, string oldNickname, string newNickname) {
            this.userId = userId;
            this.oldNickname = oldNickname;
            this.newNickname = newNickname;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ModifyNickNameResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ModifyNickNameResponse", Namespace="http://colonywebappdb.azurewebsites.net/", Order=0)]
        public ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponseBody Body;
        
        public ModifyNickNameResponse() {
        }
        
        public ModifyNickNameResponse(ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://colonywebappdb.azurewebsites.net/")]
    public partial class ModifyNickNameResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public bool ModifyNickNameResult;
        
        public ModifyNickNameResponseBody() {
        }
        
        public ModifyNickNameResponseBody(bool ModifyNickNameResult) {
            this.ModifyNickNameResult = ModifyNickNameResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface WebService1SoapChannel : ColonyServerWebApplication.ServiceReference1.WebService1Soap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebService1SoapClient : System.ServiceModel.ClientBase<ColonyServerWebApplication.ServiceReference1.WebService1Soap>, ColonyServerWebApplication.ServiceReference1.WebService1Soap {
        
        public WebService1SoapClient() {
        }
        
        public WebService1SoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebService1SoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ColonyServerWebApplication.ServiceReference1.HelloWorldResponse ColonyServerWebApplication.ServiceReference1.WebService1Soap.HelloWorld(ColonyServerWebApplication.ServiceReference1.HelloWorldRequest request) {
            return base.Channel.HelloWorld(request);
        }
        
        public string HelloWorld() {
            ColonyServerWebApplication.ServiceReference1.HelloWorldRequest inValue = new ColonyServerWebApplication.ServiceReference1.HelloWorldRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.HelloWorldRequestBody();
            ColonyServerWebApplication.ServiceReference1.HelloWorldResponse retVal = ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).HelloWorld(inValue);
            return retVal.Body.HelloWorldResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.HelloWorldResponse> ColonyServerWebApplication.ServiceReference1.WebService1Soap.HelloWorldAsync(ColonyServerWebApplication.ServiceReference1.HelloWorldRequest request) {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.HelloWorldResponse> HelloWorldAsync() {
            ColonyServerWebApplication.ServiceReference1.HelloWorldRequest inValue = new ColonyServerWebApplication.ServiceReference1.HelloWorldRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.HelloWorldRequestBody();
            return ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).HelloWorldAsync(inValue);
        }
        
        public System.Guid CreateUserId() {
            return base.Channel.CreateUserId();
        }
        
        public System.Threading.Tasks.Task<System.Guid> CreateUserIdAsync() {
            return base.Channel.CreateUserIdAsync();
        }
        
        public System.Guid CreateGroupId() {
            return base.Channel.CreateGroupId();
        }
        
        public System.Threading.Tasks.Task<System.Guid> CreateGroupIdAsync() {
            return base.Channel.CreateGroupIdAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ColonyServerWebApplication.ServiceReference1.CreateUserResponse ColonyServerWebApplication.ServiceReference1.WebService1Soap.CreateUser(ColonyServerWebApplication.ServiceReference1.CreateUserRequest request) {
            return base.Channel.CreateUser(request);
        }
        
        public bool CreateUser(string nickName, string mailAddress, string groupName) {
            ColonyServerWebApplication.ServiceReference1.CreateUserRequest inValue = new ColonyServerWebApplication.ServiceReference1.CreateUserRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.CreateUserRequestBody();
            inValue.Body.nickName = nickName;
            inValue.Body.mailAddress = mailAddress;
            inValue.Body.groupName = groupName;
            ColonyServerWebApplication.ServiceReference1.CreateUserResponse retVal = ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).CreateUser(inValue);
            return retVal.Body.CreateUserResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.CreateUserResponse> ColonyServerWebApplication.ServiceReference1.WebService1Soap.CreateUserAsync(ColonyServerWebApplication.ServiceReference1.CreateUserRequest request) {
            return base.Channel.CreateUserAsync(request);
        }
        
        public System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.CreateUserResponse> CreateUserAsync(string nickName, string mailAddress, string groupName) {
            ColonyServerWebApplication.ServiceReference1.CreateUserRequest inValue = new ColonyServerWebApplication.ServiceReference1.CreateUserRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.CreateUserRequestBody();
            inValue.Body.nickName = nickName;
            inValue.Body.mailAddress = mailAddress;
            inValue.Body.groupName = groupName;
            return ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).CreateUserAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse ColonyServerWebApplication.ServiceReference1.WebService1Soap.IsExistsUser(ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest request) {
            return base.Channel.IsExistsUser(request);
        }
        
        public bool IsExistsUser(string nickName, string mailAddress) {
            ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest inValue = new ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.IsExistsUserRequestBody();
            inValue.Body.nickName = nickName;
            inValue.Body.mailAddress = mailAddress;
            ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse retVal = ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).IsExistsUser(inValue);
            return retVal.Body.IsExistsUserResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse> ColonyServerWebApplication.ServiceReference1.WebService1Soap.IsExistsUserAsync(ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest request) {
            return base.Channel.IsExistsUserAsync(request);
        }
        
        public System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsUserResponse> IsExistsUserAsync(string nickName, string mailAddress) {
            ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest inValue = new ColonyServerWebApplication.ServiceReference1.IsExistsUserRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.IsExistsUserRequestBody();
            inValue.Body.nickName = nickName;
            inValue.Body.mailAddress = mailAddress;
            return ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).IsExistsUserAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse ColonyServerWebApplication.ServiceReference1.WebService1Soap.IsExistsGroup(ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest request) {
            return base.Channel.IsExistsGroup(request);
        }
        
        public bool IsExistsGroup(string groupName) {
            ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest inValue = new ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequestBody();
            inValue.Body.groupName = groupName;
            ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse retVal = ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).IsExistsGroup(inValue);
            return retVal.Body.IsExistsGroupResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse> ColonyServerWebApplication.ServiceReference1.WebService1Soap.IsExistsGroupAsync(ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest request) {
            return base.Channel.IsExistsGroupAsync(request);
        }
        
        public System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.IsExistsGroupResponse> IsExistsGroupAsync(string groupName) {
            ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest inValue = new ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.IsExistsGroupRequestBody();
            inValue.Body.groupName = groupName;
            return ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).IsExistsGroupAsync(inValue);
        }
        
        public bool IsExistsUserGroupChain(System.Guid userId, System.Guid groupId) {
            return base.Channel.IsExistsUserGroupChain(userId, groupId);
        }
        
        public System.Threading.Tasks.Task<bool> IsExistsUserGroupChainAsync(System.Guid userId, System.Guid groupId) {
            return base.Channel.IsExistsUserGroupChainAsync(userId, groupId);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse ColonyServerWebApplication.ServiceReference1.WebService1Soap.ModifyNickName(ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest request) {
            return base.Channel.ModifyNickName(request);
        }
        
        public bool ModifyNickName(System.Guid userId, string oldNickname, string newNickname) {
            ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest inValue = new ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequestBody();
            inValue.Body.userId = userId;
            inValue.Body.oldNickname = oldNickname;
            inValue.Body.newNickname = newNickname;
            ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse retVal = ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).ModifyNickName(inValue);
            return retVal.Body.ModifyNickNameResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse> ColonyServerWebApplication.ServiceReference1.WebService1Soap.ModifyNickNameAsync(ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest request) {
            return base.Channel.ModifyNickNameAsync(request);
        }
        
        public System.Threading.Tasks.Task<ColonyServerWebApplication.ServiceReference1.ModifyNickNameResponse> ModifyNickNameAsync(System.Guid userId, string oldNickname, string newNickname) {
            ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest inValue = new ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequest();
            inValue.Body = new ColonyServerWebApplication.ServiceReference1.ModifyNickNameRequestBody();
            inValue.Body.userId = userId;
            inValue.Body.oldNickname = oldNickname;
            inValue.Body.newNickname = newNickname;
            return ((ColonyServerWebApplication.ServiceReference1.WebService1Soap)(this)).ModifyNickNameAsync(inValue);
        }
    }
}
