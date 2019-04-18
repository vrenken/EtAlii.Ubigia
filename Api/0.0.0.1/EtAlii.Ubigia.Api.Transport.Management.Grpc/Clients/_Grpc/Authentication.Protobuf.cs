// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Authentication.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol {

  /// <summary>Holder for reflection information generated from Authentication.proto</summary>
  public static partial class AuthenticationReflection {

    #region Descriptor
    /// <summary>File descriptor for Authentication.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AuthenticationReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRBdXRoZW50aWNhdGlvbi5wcm90bxI4RXRBbGlpLlViaWdpYS5BcGkuVHJh",
            "bnNwb3J0Lk1hbmFnZW1lbnQuR3JwYy5XaXJlUHJvdG9jb2waDF9Nb2RlbC5w",
            "cm90byJWChVBdXRoZW50aWNhdGlvblJlcXVlc3QSEwoLQWNjb3VudE5hbWUY",
            "ASABKAkSEAoIUGFzc3dvcmQYAiABKAkSFgoOaG9zdElkZW50aWZpZXIYAyAB",
            "KAkibAoWQXV0aGVudGljYXRpb25SZXNwb25zZRJSCgdBY2NvdW50GAEgASgL",
            "MkEuRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0Lk1hbmFnZW1lbnQuR3Jw",
            "Yy5XaXJlUHJvdG9jb2wuQWNjb3VudDKJAwoZQXV0aGVudGljYXRpb25HcnBj",
            "U2VydmljZRKzAQoMQXV0aGVudGljYXRlEk8uRXRBbGlpLlViaWdpYS5BcGku",
            "VHJhbnNwb3J0Lk1hbmFnZW1lbnQuR3JwYy5XaXJlUHJvdG9jb2wuQXV0aGVu",
            "dGljYXRpb25SZXF1ZXN0GlAuRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0",
            "Lk1hbmFnZW1lbnQuR3JwYy5XaXJlUHJvdG9jb2wuQXV0aGVudGljYXRpb25S",
            "ZXNwb25zZSIAErUBCg5BdXRoZW50aWNhdGVBcxJPLkV0QWxpaS5VYmlnaWEu",
            "QXBpLlRyYW5zcG9ydC5NYW5hZ2VtZW50LkdycGMuV2lyZVByb3RvY29sLkF1",
            "dGhlbnRpY2F0aW9uUmVxdWVzdBpQLkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5z",
            "cG9ydC5NYW5hZ2VtZW50LkdycGMuV2lyZVByb3RvY29sLkF1dGhlbnRpY2F0",
            "aW9uUmVzcG9uc2UiAEKIAQo4RXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0",
            "Lk1hbmFnZW1lbnQuR3JwYy5XaXJlUHJvdG9jb2xCBlViaWdpYVABogIGVWJp",
            "Z2lhqgI4RXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0Lk1hbmFnZW1lbnQu",
            "R3JwYy5XaXJlUHJvdG9jb2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.ModelReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest), global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationRequest.Parser, new[]{ "AccountName", "Password", "HostIdentifier" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationResponse), global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationResponse.Parser, new[]{ "Account" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class AuthenticationRequest : pb::IMessage<AuthenticationRequest> {
    private static readonly pb::MessageParser<AuthenticationRequest> _parser = new pb::MessageParser<AuthenticationRequest>(() => new AuthenticationRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AuthenticationRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationRequest(AuthenticationRequest other) : this() {
      accountName_ = other.accountName_;
      password_ = other.password_;
      hostIdentifier_ = other.hostIdentifier_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationRequest Clone() {
      return new AuthenticationRequest(this);
    }

    /// <summary>Field number for the "AccountName" field.</summary>
    public const int AccountNameFieldNumber = 1;
    private string accountName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string AccountName {
      get { return accountName_; }
      set {
        accountName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Password" field.</summary>
    public const int PasswordFieldNumber = 2;
    private string password_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Password {
      get { return password_; }
      set {
        password_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "hostIdentifier" field.</summary>
    public const int HostIdentifierFieldNumber = 3;
    private string hostIdentifier_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string HostIdentifier {
      get { return hostIdentifier_; }
      set {
        hostIdentifier_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AuthenticationRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AuthenticationRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (AccountName != other.AccountName) return false;
      if (Password != other.Password) return false;
      if (HostIdentifier != other.HostIdentifier) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (AccountName.Length != 0) hash ^= AccountName.GetHashCode();
      if (Password.Length != 0) hash ^= Password.GetHashCode();
      if (HostIdentifier.Length != 0) hash ^= HostIdentifier.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (AccountName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(AccountName);
      }
      if (Password.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Password);
      }
      if (HostIdentifier.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(HostIdentifier);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (AccountName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AccountName);
      }
      if (Password.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Password);
      }
      if (HostIdentifier.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(HostIdentifier);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AuthenticationRequest other) {
      if (other == null) {
        return;
      }
      if (other.AccountName.Length != 0) {
        AccountName = other.AccountName;
      }
      if (other.Password.Length != 0) {
        Password = other.Password;
      }
      if (other.HostIdentifier.Length != 0) {
        HostIdentifier = other.HostIdentifier;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            AccountName = input.ReadString();
            break;
          }
          case 18: {
            Password = input.ReadString();
            break;
          }
          case 26: {
            HostIdentifier = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class AuthenticationResponse : pb::IMessage<AuthenticationResponse> {
    private static readonly pb::MessageParser<AuthenticationResponse> _parser = new pb::MessageParser<AuthenticationResponse>(() => new AuthenticationResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AuthenticationResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.AuthenticationReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationResponse(AuthenticationResponse other) : this() {
      account_ = other.account_ != null ? other.account_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AuthenticationResponse Clone() {
      return new AuthenticationResponse(this);
    }

    /// <summary>Field number for the "Account" field.</summary>
    public const int AccountFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.Account account_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.Account Account {
      get { return account_; }
      set {
        account_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AuthenticationResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AuthenticationResponse other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Account, other.Account)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (account_ != null) hash ^= Account.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (account_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Account);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (account_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Account);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AuthenticationResponse other) {
      if (other == null) {
        return;
      }
      if (other.account_ != null) {
        if (account_ == null) {
          Account = new global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.Account();
        }
        Account.MergeFrom(other.Account);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            if (account_ == null) {
              Account = new global::EtAlii.Ubigia.Api.Transport.Management.Grpc.WireProtocol.Account();
            }
            input.ReadMessage(Account);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
