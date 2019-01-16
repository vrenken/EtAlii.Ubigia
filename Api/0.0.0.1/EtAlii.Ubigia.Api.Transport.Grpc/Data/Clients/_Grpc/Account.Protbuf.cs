// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Account.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol {

  /// <summary>Holder for reflection information generated from Account.proto</summary>
  public static partial class AccountReflection {

    #region Descriptor
    /// <summary>File descriptor for Account.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AccountReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Cg1BY2NvdW50LnByb3RvEi1FdEFsaWkuVWJpZ2lhLkFwaS5UcmFuc3BvcnQu",
            "R3JwYy5XaXJlUHJvdG9jb2waDF9Nb2RlbC5wcm90byJRCg5BY2NvdW50UmVx",
            "dWVzdBI/CgJJZBgBIAEoCzIzLkV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9y",
            "dC5HcnBjLldpcmVQcm90b2NvbC5HdWlkIloKD0FjY291bnRSZXNwb25zZRJH",
            "CgdBY2NvdW50GAEgASgLMjYuRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0",
            "LkdycGMuV2lyZVByb3RvY29sLkFjY291bnQynQEKEkFjY291bnRHcnBjU2Vy",
            "dmljZRKGAQoDR2V0Ej0uRXRBbGlpLlViaWdpYS5BcGkuVHJhbnNwb3J0Lkdy",
            "cGMuV2lyZVByb3RvY29sLkFjY291bnRSZXF1ZXN0Gj4uRXRBbGlpLlViaWdp",
            "YS5BcGkuVHJhbnNwb3J0LkdycGMuV2lyZVByb3RvY29sLkFjY291bnRSZXNw",
            "b25zZSIAQnIKLUV0QWxpaS5VYmlnaWEuQXBpLlRyYW5zcG9ydC5HcnBjLldp",
            "cmVQcm90b2NvbEIGVWJpZ2lhUAGiAgZVYmlnaWGqAi1FdEFsaWkuVWJpZ2lh",
            "LkFwaS5UcmFuc3BvcnQuR3JwYy5XaXJlUHJvdG9jb2xiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.ModelReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountRequest), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountRequest.Parser, new[]{ "Id" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountResponse), global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountResponse.Parser, new[]{ "Account" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class AccountRequest : pb::IMessage<AccountRequest> {
    private static readonly pb::MessageParser<AccountRequest> _parser = new pb::MessageParser<AccountRequest>(() => new AccountRequest());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AccountRequest> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountRequest() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountRequest(AccountRequest other) : this() {
      id_ = other.id_ != null ? other.id_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountRequest Clone() {
      return new AccountRequest(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AccountRequest);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AccountRequest other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Id, other.Id)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (id_ != null) hash ^= Id.GetHashCode();
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
      if (id_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Id);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (id_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Id);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(AccountRequest other) {
      if (other == null) {
        return;
      }
      if (other.id_ != null) {
        if (id_ == null) {
          id_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
        }
        Id.MergeFrom(other.Id);
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
            if (id_ == null) {
              id_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Guid();
            }
            input.ReadMessage(id_);
            break;
          }
        }
      }
    }

  }

  public sealed partial class AccountResponse : pb::IMessage<AccountResponse> {
    private static readonly pb::MessageParser<AccountResponse> _parser = new pb::MessageParser<AccountResponse>(() => new AccountResponse());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<AccountResponse> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.AccountReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountResponse() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountResponse(AccountResponse other) : this() {
      account_ = other.account_ != null ? other.account_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public AccountResponse Clone() {
      return new AccountResponse(this);
    }

    /// <summary>Field number for the "Account" field.</summary>
    public const int AccountFieldNumber = 1;
    private global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Account account_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Account Account {
      get { return account_; }
      set {
        account_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as AccountResponse);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(AccountResponse other) {
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
    public void MergeFrom(AccountResponse other) {
      if (other == null) {
        return;
      }
      if (other.account_ != null) {
        if (account_ == null) {
          account_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Account();
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
              account_ = new global::EtAlii.Ubigia.Api.Transport.Grpc.WireProtocol.Account();
            }
            input.ReadMessage(account_);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
